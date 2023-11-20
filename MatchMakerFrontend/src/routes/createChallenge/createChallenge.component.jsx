import { useState } from "react";
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
  return `${today.getDate()}-${
    today.getMonth() + 1
  }-${today.getFullYear()} ${today.getHours()}:${today.getMinutes()}:${today.getSeconds()}`;
}

// Create challenge page
const CreateChallenge = () => {
  // Set local state
  const [file, setFile] = useState();

  // Decode the Jwt token
  const decodedToken = jwtDecode(localStorage["token"]);

  // Handles create challenge form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    const formData = new FormData();
    formData.append("UploadChallengeFile", file);
    formData.append("ChallengeTitle", e.target.ChallengeTitle.value);
    formData.append(
      "ChallengeDescription",
      e.target.ChallengeDescription.value
    );

    formData.append("ChallengeViewStatus", e.target.ChallengeViewStatus.value);
    formData.append(
      "ChallengeProgressionStatus",
      e.target.ChallengeProgressionStatus.value
    );
    formData.append("EndDate", e.target.EndDate.value);
    formData.append("DateSubmitted", getDate());
    formData.append(
      "UserName",
      decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    );

    // Send request
    axios
      .post("http://localhost:5063/api/Challenge/createChallenge", formData, {
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

  // Set the file state
  const saveFile = (e) => {
    setFile(e.target.files[0]);
  };

  return (
    <PageBody>
      <CreateChallengeContainer>
        <PageTitle>Create Challenge</PageTitle>
        <CreateChallengeForm
          encType="multipart/form-data"
          onSubmit={handleSubmit}
        >
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
          <input
            type="file"
            id="challengefile"
            name="ChallengeFile"
            onChange={saveFile}
          />
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
