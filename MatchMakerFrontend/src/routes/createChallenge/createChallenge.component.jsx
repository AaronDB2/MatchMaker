import { useState } from "react";
import axios from "axios";
import { jwtDecode } from "jwt-decode";

import { getDate } from "../../utils/dateTime";

import {
  PageBody,
  CreateChallengeContainer,
  PageTitle,
  CreateChallengeForm,
  RadioValuesContainer,
} from "./createChallenge.styles";

// Gets current date
// function getDate() {
//   const today = new Date();
//   return `${today.getDate()}-${
//     today.getMonth() + 1
//   }-${today.getFullYear()} ${today.getHours()}:${today.getMinutes()}:${today.getSeconds()}`;
// }

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
  const saveFileChallenge = (e) => {
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
            onChange={saveFileChallenge}
          />
          <div>
            <p>View Status: </p>
            <RadioValuesContainer>
              <label for="intern">Intern</label>
              <input
                type="radio"
                id="intern"
                name="ChallengeViewStatus"
                value="Intern"
              />
            </RadioValuesContainer>
            <RadioValuesContainer>
              <label for="public">Public</label>
              <input
                type="radio"
                id="public"
                name="ChallengeViewStatus"
                value="Public"
              />
            </RadioValuesContainer>
          </div>
          <div>
            <p>Progression Status: </p>
            <RadioValuesContainer>
              <label for="open-for-ideas">Open for ideas</label>
              <input
                type="radio"
                id="open-for-ideas"
                name="ChallengeProgressionStatus"
                value="Open for ideas"
              />
            </RadioValuesContainer>
            <RadioValuesContainer>
              <label for="progress">In progress</label>
              <input
                type="radio"
                id="progress"
                name="ChallengeProgressionStatus"
                value="In progress"
              />
            </RadioValuesContainer>
            <RadioValuesContainer>
              <label for="finished">Finished</label>
              <input
                type="radio"
                id="finished"
                name="ChallengeProgressionStatus"
                value="Finished"
              />
            </RadioValuesContainer>
            <RadioValuesContainer>
              <label for="archived">Archived</label>
              <input
                type="radio"
                id="archived"
                name="ChallengeProgressionStatus"
                value="Archived"
              />
            </RadioValuesContainer>
          </div>
          <label for="endDate">End Date:</label>
          <input type="date" id="enddate" name="EndDate" />
          <input type="submit" value="Create Challenge" />
        </CreateChallengeForm>
      </CreateChallengeContainer>
    </PageBody>
  );
};

export default CreateChallenge;
