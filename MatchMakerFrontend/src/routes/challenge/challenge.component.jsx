import { Fragment } from "react";
import { useState, useEffect } from "react";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useParams } from "react-router-dom";

import {
  PageBody,
  ChallengeContainer,
  PageTitle,
  ChallengeDescription,
  QuestionForm,
  QuestionDisplayContainer,
  QuestionTitle,
  QuestionDescription,
  QuestionUserName,
} from "./challenge.styles";

import { NavLink } from "../navigation/navigation.styles";

// Challenge Page
const Challenge = () => {
  // Define local state
  const [challenge, setChallenge] = useState();
  const [token, setToken] = useState(localStorage.getItem("token") || "");
  const [challengeQuestions, setChallengeQuestions] = useState([]);
  // FIX TYPO
  const { challengeId } = useParams();

  // Decode the Jwt token
  const decodedToken = jwtDecode(localStorage["token"]);

  // Effect for fetching challenge by id
  useEffect(() => {
    // Send request to get challege by id and set challenge state with the response
    axios
      .get(`http://localhost:5063/api/challenge/${challengeId}`)
      .then(function (response) {
        setChallenge(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }, []);

  // Effect for fetching challenge questions
  useEffect(() => {
    axios
      .get(
        `http://localhost:5063/api/question?searchBy=questionId&searchString=${challengeId}`
      )
      .then(function (response) {
        setChallengeQuestions(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }, []);

  // Submit handler for questions form
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      QuestionTitle: e.target.QuestionTitle.value,
      QuestionDescription: e.target.QuestionDescription.value,
      UserName:
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ],
      ChallengeId: challenge["challengeId"],
    };

    // Send request
    axios
      .post("http://localhost:5063/api/question", body, {
        headers: {
          Authorization: "Bearer " + localStorage["token"],
        },
      })
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });

    e.target.QuestionTitle.value = "";
    e.target.QuestionDescription.value = "";
  };

  return (
    <PageBody>
      <ChallengeContainer>
        {challenge ? (
          <Fragment>
            <PageTitle>{challenge["challengeTitle"]}</PageTitle>
            <ChallengeDescription>
              {challenge["challengeDescription"]}
            </ChallengeDescription>
            <NavLink to={`/challenge/${challengeId}/edit`}>
              Edit Challenge
            </NavLink>
          </Fragment>
        ) : null}
        {token ? (
          <Fragment>
            <QuestionForm onSubmit={handleSubmit}>
              <label for="questionTitle">Question Title</label>
              <input type="text" id="question-title" name="QuestionTitle" />
              <label for="questionDescription">Question Description</label>
              <input
                type="text"
                id="question-description"
                name="QuestionDescription"
              />
              <input type="submit" value="Submit" />
            </QuestionForm>
            <QuestionDisplayContainer>
              {challengeQuestions.map((question, index) => (
                <Fragment key={index}>
                  <QuestionTitle>{question["questionTitle"]}</QuestionTitle>
                  <QuestionUserName>{question["userId"]}</QuestionUserName>
                  <QuestionDescription>
                    {question["questionDescription"]}
                  </QuestionDescription>
                </Fragment>
              ))}
            </QuestionDisplayContainer>
          </Fragment>
        ) : null}
      </ChallengeContainer>
    </PageBody>
  );
};

export default Challenge;
