import axios from "axios";

import {
  PageBody,
  CreateTagsContainer,
  PageTitle,
  CreateTagsForm,
} from "./createTags.styles";

// Create tag page
const CreateTag = () => {
  // Handles create tag form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      TagName: e.target.TagName.value,
    };

    // Send request
    axios
      .post("http://localhost:5063/api/tag/createtag", body, {
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

  return (
    <PageBody>
      <CreateTagsContainer>
        <PageTitle>Create Tag</PageTitle>
        <CreateTagsForm onSubmit={handleSubmit}>
          <label for="tagname">Tag Name:</label>
          <input type="text" id="tagname" name="TagName" />
          <input type="submit" value="Submit" />
        </CreateTagsForm>
      </CreateTagsContainer>
    </PageBody>
  );
};

export default CreateTag;
