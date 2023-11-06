import axios from "axios";
import { jwtDecode } from "jwt-decode";

import {
  PageBody,
  CreateChallengeContainer,
  PageTitle,
  CreateChallengeForm,
} from "./createChallenge.styles";

// Gets current date
function getDate() {
  const today = new Date();
  return today;
}

// Create challenge page
const CreateChallenge = () => {
  // Decode the Jwt token
  const decodedToken = jwtDecode(localStorage["token"]);

  // Handles create challenge form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      ChallengeTitle: e.target.ChallengeTitle.value,
      ChallengeDescription: e.target.ChallengeDescription.value,
      ChallengeFile: e.target.ChallengeFile.value,
      ChallengeViewStatus: e.target.ChallengeViewStatus.value,
      ChallengeProgressionStatus: e.target.ChallengeProgressionStatus.value,
      EndDate: e.target.EndDate.value,
      UserName:
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ],
      DateSubmitted: getDate(),
    };

    // Send request
    axios
      .post("http://localhost:5063/api/Challenge/createChallenge", body, {
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

    e.target.ChallengeTitle.value = "";
    e.target.ChallengeDescription.value = "";
    e.target.ChallengeFile.value = "";
    e.target.ChallengeViewStatus.value = "";
    e.target.ChallengeProgressionStatus.value = "";
    e.target.EndDate.value = "";
  };

  return (
    <PageBody>
      <CreateChallengeContainer>
        <PageTitle>Create Challenge</PageTitle>
        <CreateChallengeForm onSubmit={handleSubmit}>
          <label for="challengeTitle">Challenge Title:</label>
          <input type="text" id="challengetitle" name="ChallengeTitle" />
          <label for="challengedescription">Description:</label>
          <textarea
            id="challengedescription"
            name="ChallengeDescription"
            rows="10"
            cols="50"
          />
          <label for="challengeFile">Challenge File:</label>
          <input type="text" id="challengefile" name="ChallengeFile" />
          <label for="challengeViewStatus">View Status:</label>
          <input
            type="text"
            id="challengeviewstatus"
            name="ChallengeViewStatus"
          />
          <label for="challengeProgressionStatus">Progression Status:</label>
          <input
            type="text"
            id="challengeprogressionstatus"
            name="ChallengeProgressionStatus"
          />
          <label for="endDate">End Date:</label>
          <input type="date" id="enddate" name="EndDate" />
          <input type="submit" value="Submit" />
        </CreateChallengeForm>
      </CreateChallengeContainer>
    </PageBody>
  );
};

export default CreateChallenge;
