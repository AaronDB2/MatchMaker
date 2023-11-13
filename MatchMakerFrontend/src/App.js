import React from "react";
import { Fragment } from "react";
import { Routes, Route } from "react-router-dom";

import { GlobalStyle } from "./global.styles";

import Navigation from "./routes/navigation/navigation.component";
import Home from "./routes/home/home.component";
import Login from "./routes/login/login.component";
import SearchChallenges from "./routes/searchChallenges/searchChallenges.component";
import Profile from "./routes/profile/profile.component";
import CreateCompany from "./routes/createCompany/createCompany.component";
import CreateTag from "./routes/createTags/createTags.component";
import CreateChallenge from "./routes/createChallenge/createChallenge.component";
import CreateAccount from "./routes/createAccount/createAccount.component";
import Challenge from "./routes/challenge/challenge.component";

// App component. Hold routing functionality
const App = () => {
  return (
    <Fragment>
      <GlobalStyle />
      <Routes>
        <Route path="/" element={<Navigation />}>
          <Route index element={<Home />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/login" element={<Login />} />
          <Route path="/search-challenges" element={<SearchChallenges />} />
          <Route path="/create-company" element={<CreateCompany />} />
          <Route path="/create-tag" element={<CreateTag />} />
          <Route path="/create-challenge" element={<CreateChallenge />} />
          <Route path="/create-account" element={<CreateAccount />} />
          <Route path="/challenge/:challendeId" element={<Challenge />} />
        </Route>
      </Routes>
    </Fragment>
  );
};

export default App;
