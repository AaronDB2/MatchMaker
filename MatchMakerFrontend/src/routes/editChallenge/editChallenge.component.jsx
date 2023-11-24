import { useState } from "react";
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
  // Set local state
  const [file, setFile] = useState();
  const [fileResult, setResultFile] = useState();
  const { challengeId } = useParams();

  // Handles edit challenge form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    const formData = new FormData();
    formData.append("ChallengeId", challengeId);
    formData.append("UploadEndResultFile", fileResult);
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

    // Send request
    axios
      .post("http://localhost:5063/api/challenge/editchallenge", formData, {
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
    e.target.ResultFile.value = "";
  };

  // Handles submit event of the tag form
  const handleSubmitTag = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      TagName: e.target.TagName.value,
      ChallengeId: challengeId,
    };

    // Send request
    axios
      .post("http://localhost:5063/api/challenge/challengetag", body, {
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

    e.target.TagName.value = "";
  };

  // Set the file state
  const saveChallengeFile = (e) => {
    setFile(e.target.files[0]);
  };

  // Set the file state
  const saveFileEndResult = (e) => {
    setResultFile(e.target.files[0]);
  };

  return (
    <PageBody>
      <EditChallengeContainer>
        <PageTitle>Edit Challenge: {challengeId}</PageTitle>
        <EditChallengeForm
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
            onChange={saveChallengeFile}
          />
          <label for="resultFile">End Result File:</label>
          <input
            type="file"
            id="resultfile"
            name="ResultFile"
            onChange={saveFileEndResult}
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
        </EditChallengeForm>
        <h3>Add tags</h3>
        <EditChallengeForm onSubmit={handleSubmitTag}>
          <label for="tag-name">Tag:</label>
          <input type="text" id="tag-name" name="TagName" />
          <input type="submit" value="Submit" />
        </EditChallengeForm>
      </EditChallengeContainer>
    </PageBody>
  );
};

export default EditChallenge;
