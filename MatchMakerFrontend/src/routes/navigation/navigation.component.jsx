import { Fragment } from "react";
import { Outlet } from "react-router-dom";

import {
  NavigationContainer,
  NavFooterContainer,
  LogoContainer,
  NavLinks,
  NavLink,
} from "./navigation.styles";

// Navigation bar component
const Navigation = () => {
  return (
    <Fragment>
      <NavigationContainer>
        <LogoContainer>
          <NavLink to="/">LOGO/HOME</NavLink>
        </LogoContainer>
        <NavLinks>
          <NavLink to="/search-challenges">SEARCH CHALLENGES</NavLink>
          <NavLink to="/login">LOGIN</NavLink>
          <NavLink to="/profile">PROFILE</NavLink>
        </NavLinks>
      </NavigationContainer>
      <Outlet />
      <NavFooterContainer>
        <LogoContainer>
          <NavLink to="/">LOGO/HOME</NavLink>
        </LogoContainer>
        <NavLinks>
          <NavLink to="/search-challenges">SEARCH CHALLENGES</NavLink>
          <NavLink to="/login">LOGIN</NavLink>
          <NavLink to="/profile">PROFILE</NavLink>
        </NavLinks>
      </NavFooterContainer>
    </Fragment>
  );
};

export default Navigation;
