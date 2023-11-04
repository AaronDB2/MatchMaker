import axios from "axios";

import { PageBody, LoginContainer, LoginForm, PageTitle } from "./login.styles";

// Login page component
const Login = () => {
  // Handles login form submit event
  const handleSubmit = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      Email: e.target.Email.value,
      Password: e.target.Password.value,
    };

    // Send request
    axios
      .post("http://localhost:5063/api/account", body)
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });

    e.target.Email.value = "";
    e.target.Password.value = "";
  };

  return (
    <PageBody>
      <LoginContainer>
        <PageTitle>Login</PageTitle>
        <LoginForm onSubmit={handleSubmit}>
          <label for="email">Email:</label>
          <input type="email" id="email" name="Email" />
          <label for="password">Password:</label>
          <input type="password" id="password" name="Password" />
          <input type="submit" value="Submit" />
        </LoginForm>
      </LoginContainer>
    </PageBody>
  );
};

export default Login;
