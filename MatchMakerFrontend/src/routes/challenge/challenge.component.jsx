import { Fragment } from "react";
import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";

import {
  PageBody,
  ChallengeContainer,
  PageTitle,
  ChallengeDescription,
} from "./challenge.styles";

// Challenge Page
const Challenge = () => {
  // Define local state
  const [challenge, setChallenge] = useState();
  // FIX TYPO
  const { challendeId } = useParams();
  console.log(challenge);

  // Effect for fetching challenge by id
  useEffect(() => {
    // Send request to get challege by id and set challenge state with the response
    axios
      .get(`http://localhost:5063/api/challenge/${challendeId}`)
      .then(function (response) {
        setChallenge(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }, []);

  return (
    <PageBody>
      <ChallengeContainer>
        {challenge ? (
          <Fragment>
            <PageTitle>{challenge["challengeTitle"]}</PageTitle>
            <ChallengeDescription>
              {challenge["challengeDescription"]}
            </ChallengeDescription>
          </Fragment>
        ) : null}
      </ChallengeContainer>
    </PageBody>
  );
};

export default Challenge;
