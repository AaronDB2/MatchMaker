import { Fragment } from "react";
import { Outlet } from "react-router-dom";

import { NavigationContainer, NavLinks, NavLink } from "./navigation.styles";

// Navigation bar component
const Navigation = () => {
  return (
    <Fragment>
      <NavigationContainer>
        <NavLinks>
          <NavLink to="/">LOGO/HOME</NavLink>
          <NavLink to="/search-challenges">Search Challenges</NavLink>
          <NavLink to="/login">Login</NavLink>
          <NavLink to="/profile">Profile</NavLink>
        </NavLinks>
      </NavigationContainer>
      <Outlet />
      <NavigationContainer>
        <NavLinks>
          <NavLink to="/">LOGO/HOME</NavLink>
          <NavLink to="/search-challenges">Search Challenges</NavLink>
          <NavLink to="/login">Login</NavLink>
          <NavLink to="/profile">Profile</NavLink>
        </NavLinks>
      </NavigationContainer>
    </Fragment>
  );
};

export default Navigation;
