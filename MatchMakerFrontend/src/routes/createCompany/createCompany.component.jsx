import axios from "axios";

import {
  PageBody,
  CreateCompanyContainer,
  PageTitle,
  CreateCompanyForm,
} from "./createCompany.styles";

// Create company page
const CreateCompany = () => {
  // Handles create company form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      CompanyName: e.target.CompanyName.value,
      CompanyDescription: e.target.CompanyDescription.value,
      TagName: e.target.TagName.value,
    };

    // Send request
    axios
      .post("http://localhost:5063/api/company/createcompany", body, {
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

    e.target.CompanyName.value = "";
    e.target.CompanyDescription.value = "";
    e.target.TagName.value = "";
  };

  return (
    <PageBody>
      <CreateCompanyContainer>
        <PageTitle>Create Company</PageTitle>
        <CreateCompanyForm onSubmit={handleSubmit}>
          <label for="companyname">Company Name:</label>
          <input type="text" id="companyname" name="CompanyName" />
          <label for="companydescription">Description:</label>
          <textarea
            id="companydescription"
            name="CompanyDescription"
            rows="10"
            cols="50"
          />
          <label for="tag-name">Tag:</label>
          <input type="text" id="tag-name" name="TagName" />
          <input type="submit" value="Submit" />
        </CreateCompanyForm>
      </CreateCompanyContainer>
    </PageBody>
  );
};

export default CreateCompany;
