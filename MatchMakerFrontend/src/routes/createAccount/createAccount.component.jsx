import axios from "axios";
import { useState } from "react";

import {
  PageBody,
  CreateAccountContainer,
  PageTitle,
  CreateAccountForm,
} from "./createAccount.styles";

// Create Account page
const CreateAccount = () => {
  // Define local state
  const [adminCheck, setAdminCheck] = useState(false);
  const [companyManagerCheck, setCompanyManagerCheck] = useState(false);

  // Handles create Account form submit event
  const handleSubmit = (e) => {
    e.preventDefault();

    var admin = "";
    var companyManager = "";

    if (adminCheck) {
      admin = "Admin";
    }

    if (companyManagerCheck) {
      companyManager = "CompanyManager";
    }

    // Prepare request body
    var body = {
      UserName: e.target.UserName.value,
      Email: e.target.Email.value,
      Password: e.target.Password.value,
      ConfirmPassword: e.target.ConfirmPassword.value,
      Admin: admin,
      CompanyManager: companyManager,
    };

    // Send request
    axios
      .post("http://localhost:5063/api/Account/createUserAccount", body, {
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

    e.target.UserName.value = "";
    e.target.Email.value = "";
    e.target.Password.value = "";
    e.target.ConfirmPassword.value = "";
    e.target.Admin.value = "";
    e.target.CompanyManager.value = "";
  };

  // change handler for checkboxes
  const checkHandler = (e) => {
    if (e.target.id === "admin") {
      setAdminCheck(!adminCheck);
    }

    if (e.target.id === "companyManager") {
      setCompanyManagerCheck(!companyManagerCheck);
    }
  };

  return (
    <PageBody>
      <CreateAccountContainer>
        <PageTitle>Create Account</PageTitle>
        <CreateAccountForm onSubmit={handleSubmit}>
          <label for="username">Username:</label>
          <input type="text" id="username" name="UserName" />
          <label for="email">Email:</label>
          <input type="Email" id="email" name="Email" />
          <label for="password">Password:</label>
          <input type="password" id="password" name="Password" />
          <label for="confirmPassword">Confirm Password:</label>
          <input type="password" id="confirmPassword" name="ConfirmPassword" />
          <h3>Roles </h3>
          <div>
            <input
              type="checkbox"
              id="admin"
              name="Admin"
              value="Admin"
              checked={adminCheck}
              onChange={checkHandler}
            />
            <label for="admin">Admin</label>
            <input
              type="checkbox"
              id="companyManager"
              name="CompanyManager"
              value="CompanyManager"
              checked={companyManagerCheck}
              onChange={checkHandler}
            />
            <label for="companyManager">Company Manager</label>
          </div>
          <input type="submit" value="Create Account" />
        </CreateAccountForm>
      </CreateAccountContainer>
    </PageBody>
  );
};

export default CreateAccount;
