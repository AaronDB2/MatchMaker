import { jwtDecode } from "jwt-decode";
import axios from "axios";

import {
  PageTitle,
  PageBody,
  EditDataForm,
  ProfileContainer,
  NavLink,
  EditDataContainer,
  ButtonContainer,
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

  // Handles submit event of the tag form
  const handleSubmitTag = (e) => {
    e.preventDefault();
    // Prepare request body
    var body = {
      TagName: e.target.TagName.value,
      UserName:
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ],
    };

    // Send request
    axios
      .post("http://localhost:5063/api/account/usertag", body, {
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
      <ProfileContainer>
        <PageTitle>
          Hello{" "}
          {
            decodedToken[
              "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            ]
          }
        </PageTitle>
        <EditDataContainer>
          <div>
            <h3>Update Password</h3>
            <EditDataForm onSubmit={handleSubmitUpdatePassword}>
              <label for="currentPassword">Current Password:</label>
              <input
                type="password"
                id="currentPassword"
                name="CurrentPassword"
              />
              <label for="password">New Password:</label>
              <input type="password" id="password" name="Password" />
              <label for="confirmPassword">Confirm Password:</label>
              <input
                type="password"
                id="confirmPassword"
                name="ConfirmPassword"
              />
              <input type="submit" value="Submit" />
            </EditDataForm>
          </div>
          <div>
            <h3>Add your company</h3>
            <EditDataForm onSubmit={handleSubmitUpdateCompany}>
              <label for="companyName">Company:</label>
              <input type="text" id="company" name="CompanyName" />
              <input type="submit" value="Submit" />
            </EditDataForm>
          </div>
          <div>
            <h3>Add tags</h3>
            <EditDataForm onSubmit={handleSubmitTag}>
              <label for="tag-name">Tag:</label>
              <input type="text" id="tag-name" name="TagName" />
              <input type="submit" value="Submit" />
            </EditDataForm>
          </div>
        </EditDataContainer>
        <ButtonContainer>
          <h2>Create New Entities</h2>
          <div>
            <NavLink to="/create-company">CREATE COMPANY</NavLink>
            <NavLink to="/create-tag">CREATE TAG</NavLink>
            <NavLink to="/create-challenge">CREATE CHALLENGE</NavLink>
            <NavLink to="/create-account">CREATE ACCOUNT</NavLink>
          </div>
        </ButtonContainer>
      </ProfileContainer>
    </PageBody>
  );
};

export default Profile;
