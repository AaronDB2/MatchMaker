import React from "react";
import { Fragment } from "react";
import { Routes, Route } from "react-router-dom";
import { GlobalStyle } from "./global.styles";

import Navigation from "./routes/navigation/navigation.component";
import Home from "./routes/home/home.component";
import Login from "./routes/login/login.component";
import SearchChallenges from "./routes/searchChallenges/searchChallenges.component";
import Profile from "./routes/profile/profile.component";

// App component. Hold routing functionality
const App = () => {
  return (
    <Fragment>
      <GlobalStyle />
      <Routes>
        <Route path="/" element={<Navigation />}>
          <Route index element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/search-challenges" element={<SearchChallenges />} />
          <Route path="/profile" element={<Profile />} />
        </Route>
      </Routes>
    </Fragment>
  );
};

export default App;
