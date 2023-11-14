import axios from "axios";
import { useParams } from "react-router-dom";

import {
  PageBody,
  EditChallengeContainer,
  PageTitle,
  EditChallengeForm,
} from "./editChallenge.styles";

// Edit challenge page
const EditChallenge = () => {
  const { challengeId } = useParams();

  // Handles edit challenge form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      ChallengeId: challengeId,
      ChallengeTitle: e.target.ChallengeTitle.value,
      ChallengeDescription: e.target.ChallengeDescription.value,
      ChallengeFile: e.target.ChallengeFile.value,
      ChallengeViewStatus: e.target.ChallengeViewStatus.value,
      ChallengeProgressionStatus: e.target.ChallengeProgressionStatus.value,
      EndDate: e.target.EndDate.value,
    };

    // Send request
    axios
      .post("http://localhost:5063/api/challenge/editchallenge", body, {
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
      <EditChallengeContainer>
        <PageTitle>Edit Challenge: {challengeId}</PageTitle>
        <EditChallengeForm onSubmit={handleSubmit}>
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
        </EditChallengeForm>
      </EditChallengeContainer>
    </PageBody>
  );
};

export default EditChallenge;
