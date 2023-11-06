import { jwtDecode } from "jwt-decode";
import axios from "axios";

import {
  PageTitle,
  PageBody,
  EditDataForm,
  ProfileContainer,
  NavLink,
} from "./profile.styles";

// Profile page component
const Profile = () => {
  // Decode the Jwt token
  const decodedToken = jwtDecode(localStorage["token"]);

  // Handles change password form submit event
  const handleSubmitUpdatePassword = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      CurrentPassword: e.target.CurrentPassword.value,
      Password: e.target.Password.value,
      ConfirmPassword: e.target.ConfirmPassword.value,
      UserName:
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ],
    };

    // Send request
    axios
      .post("http://localhost:5063/api/account/updateUserPassword", body, {
        headers: {
          Authorization: "Bearer " + localStorage["token"],
        },
      })
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

    e.target.CurrentPassword.value = "";
    e.target.Password.value = "";
    e.target.ConfirmPassword.value = "";
  };

  // Handles change company form submit event
  const handleSubmitUpdateCompany = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      CompanyName: e.target.CompanyName.value,
      UserName:
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ],
    };

    // Send request
    axios
      .post("http://localhost:5063/api/account/updateUserCompany", body, {
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
  };

  return (
    <PageBody>
      <ProfileContainer>
        <PageTitle>
          Hello{" "}
          {
            decodedToken[
              "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            ]
          }
        </PageTitle>
        <EditDataForm onSubmit={handleSubmitUpdatePassword}>
          <label for="currentPassword">Current Password:</label>
          <input type="password" id="currentPassword" name="CurrentPassword" />
          <label for="password">New Password:</label>
          <input type="password" id="password" name="Password" />
          <label for="confirmPassword">Confirm Password:</label>
          <input type="password" id="confirmPassword" name="ConfirmPassword" />
          <input type="submit" value="Submit" />
        </EditDataForm>
        <EditDataForm onSubmit={handleSubmitUpdateCompany}>
          <label for="companyName">Company:</label>
          <input type="text" id="company" name="CompanyName" />
          <input type="submit" value="Submit" />
        </EditDataForm>
        <NavLink to="/create-company">CREATE COMPANY</NavLink>
        <NavLink to="/create-tag">CREATE TAG</NavLink>
        <NavLink to="/create-challenge">CREATE CHALLENGE</NavLink>
      </ProfileContainer>
    </PageBody>
  );
};

export default Profile;
