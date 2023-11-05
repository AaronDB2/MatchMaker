import { Fragment, useState, useEffect } from "react";
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
  const [data, setData] = useState(localStorage.getItem("token") || "");

  // Function to handle the storage event and update the component
  const handleStorageChange = (e) => {
    if (e.key === "token") {
      setData(e.newValue);
    }
  };

  // Effect that listens if localStorage jwt token changed
  useEffect(() => {
    // Add an event listener for the storage event
    window.addEventListener("storage", handleStorageChange);

    return () => {
      // Clean up the event listener when the component unmounts
      window.removeEventListener("storage", handleStorageChange);
    };
  }, []);

  return (
    <Fragment>
      <NavigationContainer>
        <LogoContainer>
          <NavLink to="/">LOGO/HOME</NavLink>
        </LogoContainer>
        <NavLinks>
          <NavLink to="/search-challenges">SEARCH CHALLENGES</NavLink>
          {data ? (
            <NavLink to="/profile">PROFILE</NavLink>
          ) : (
            <NavLink to="/login">LOGIN</NavLink>
          )}
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
