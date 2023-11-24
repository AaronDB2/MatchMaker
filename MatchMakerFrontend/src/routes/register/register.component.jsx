import axios from "axios";

import {
  PageBody,
  RegisterContainer,
  RegisterForm,
  PageTitle,
} from "./register.styles";

// Register page component
const Register = () => {
  // Handles Register form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      UserName: e.target.Username.value,
      Email: e.target.Email.value,
      Password: e.target.Password.value,
      ConfirmPassword: e.target.ConfirmPassword.value,
    };

    // Send request
    axios
      .post("http://localhost:5063/api/account/register", body)
      .then(function (response) {
        console.log(response);

        // Set local storage
        localStorage["token"] = response.data.token;

        // Creates storageChangeEvent
        const storageChangeEvent = new StorageEvent("storage", {
          key: "token",
          newValue: response.data.token,
        });

        // Triggers storageChangeEvent so that it triggers navigation rerender
        window.dispatchEvent(storageChangeEvent);
      })
      .catch(function (error) {
        console.log(error);
      });

    e.target.Username.value = "";
    e.target.Email.value = "";
    e.target.Password.value = "";
    e.target.ConfirmPassword.value = "";
  };

  return (
    <PageBody>
      <RegisterContainer>
        <PageTitle>Register</PageTitle>
        <RegisterForm onSubmit={handleSubmit}>
          <label for="username">Username:</label>
          <input type="text" id="username" name="Username" />
          <label for="email">Email:</label>
          <input type="email" id="email" name="Email" />
          <label for="password">Password:</label>
          <input type="password" id="password" name="Password" />
          <label for="confirm-password">Confirm Password:</label>
          <input type="password" id="confirm-password" name="ConfirmPassword" />
          <input type="submit" value="Submit" />
        </RegisterForm>
      </RegisterContainer>
    </PageBody>
  );
};

export default Register;
