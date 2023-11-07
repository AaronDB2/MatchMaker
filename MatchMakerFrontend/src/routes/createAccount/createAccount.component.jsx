import axios from "axios";

import {
  PageBody,
  CreateAccountContainer,
  PageTitle,
  CreateAccountForm,
} from "./createAccount.styles";

// Create Account page
const CreateAccount = () => {
  // Handles create Account form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      UserName: e.target.UserName.value,
      Email: e.target.Email.value,
      Password: e.target.Password.value,
      ConfirmPassword: e.target.ConfirmPassword.value,
      Admin: e.target.Admin.value,
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
            <input type="checkbox" id="admin" name="Admin" value="admin" />
            <label for="admin">Admin</label>
          </div>
          <input type="submit" value="Submit" />
        </CreateAccountForm>
      </CreateAccountContainer>
    </PageBody>
  );
};

export default CreateAccount;
