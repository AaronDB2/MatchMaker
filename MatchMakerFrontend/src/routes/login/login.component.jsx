import axios from "axios";
import { useNavigate } from "react-router-dom";

import {
  PageBody,
  LoginContainer,
  LoginForm,
  PageTitle,
  NavLink,
} from "./login.styles";

// Login page component
const Login = () => {
  const navigate = useNavigate();

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

        // Set local storage
        localStorage["token"] = response.data.token;
        localStorage["refreshToken"] = response.data.refreshToken;

        // Creates storageChangeEvent
        const storageChangeEvent = new StorageEvent("storage", {
          key: "token",
          newValue: response.data.token,
        });

        // Triggers storageChangeEvent so that it triggers navigation rerender
        window.dispatchEvent(storageChangeEvent);

        if (response.status === 200) {
          navigate("/");
        }
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
          <input type="email" id="email" name="Email" required />
          <label for="password">Password:</label>
          <input type="password" id="password" name="Password" required />
          <input type="submit" value="Login" />
        </LoginForm>
        <NavLink to="/register">Register A New Account</NavLink>
      </LoginContainer>
    </PageBody>
  );
};

export default Login;
