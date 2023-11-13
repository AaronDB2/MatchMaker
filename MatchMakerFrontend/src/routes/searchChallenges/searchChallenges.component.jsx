import { useState, useEffect } from "react";
import axios from "axios";

import {
  PageBody,
  SearchChallengesContainer,
  PageTitle,
  SearchChallengesForm,
  SearchChallengeTable,
} from "./searchChallenges.styles";
import { NavLink } from "../navigation/navigation.styles";

// Search challenges page component
const SearchChallenges = () => {
  // Define local state
  const [challenges, setChallenges] = useState([]);

  // Effect for fetching all the challenges
  useEffect(() => {
    // Send request to get all challeges and set challenges state with the response
    axios
      .get("http://localhost:5063/api/challenge/getFilterdChallenges")
      .then(function (response) {
        setChallenges(Object.values(response.data));
      })
      .catch(function (error) {
        console.log(error);
      });
  }, []);

  // Handles search challenge form submit event
  const handleSubmit = (e) => {
    e.preventDefault();

    // Send request
    axios
      .get(
        `http://localhost:5063/api/challenge/getFilterdChallenges?searchBy=${e.target.searchBy.value}&searchString=${e.target.searchString.value}`
      )
      .then(function (response) {
        setChallenges(Object.values(response.data));
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  return (
    <PageBody>
      <SearchChallengesContainer>
        <PageTitle>Search Challenges</PageTitle>
        <SearchChallengesForm onSubmit={handleSubmit}>
          <label for="challenge-attribute">Search By: </label>
          <select id="challenge-attribute" name="searchBy">
            <option value="ChallengeTitle">Challenge Title</option>
            <option value="ChallengeId">Challenge Id</option>
          </select>
          <input
            type="text"
            id="search-input"
            name="searchString"
            placeholder="Fill in search value"
          />
          <input type="submit" value="Search" />
        </SearchChallengesForm>
        <SearchChallengeTable>
          <tbody>
            <tr>
              <th>Challenge Title</th>
              <th>Challenge Id</th>
              <th>Challenge Description</th>
            </tr>
            {challenges.map((challenge, index) => (
              <tr key={index}>
                <th>
                  <NavLink
                    to={{
                      pathname: `/challenge/${challenge["challengeId"]}`,
                    }}
                  >
                    {challenge["challengeTitle"]}
                  </NavLink>
                </th>
                <th>{challenge["challengeId"]}</th>
                <th>{challenge["challengeDescription"]}</th>
              </tr>
            ))}
          </tbody>
        </SearchChallengeTable>
      </SearchChallengesContainer>
    </PageBody>
  );
};

export default SearchChallenges;
