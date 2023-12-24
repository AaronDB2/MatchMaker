import { Fragment, useState, useEffect } from "react";
import { Outlet } from "react-router-dom";
import HamburgerMenu from "../../components/hamburgerMenu/hamburgerMenu.component";
import MobileNav from "../../components/mobileNav/mobileNav.component";

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

  // Set local state
  const [showMobileNav, setShowMobileNav] = useState(false);

  // Shows or hides mobile nav
  const handleMobileNav = (e) => {
    setShowMobileNav(!showMobileNav);
  };

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
      {showMobileNav ? (
        <MobileNav handler={handleMobileNav} />
      ) : (
        <Fragment>
          <NavigationContainer>
            <LogoContainer>
              <NavLink to="/">LOGO/HOME</NavLink>
            </LogoContainer>
            <HamburgerMenu handler={handleMobileNav} />
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
            <NavLinks>
              <NavLink to="/">HOME</NavLink>
              <NavLink to="/search-challenges">SEARCH CHALLENGES</NavLink>
              {data ? (
                <NavLink to="/profile">PROFILE</NavLink>
              ) : (
                <NavLink to="/login">LOGIN</NavLink>
              )}
            </NavLinks>
          </NavFooterContainer>
        </Fragment>
      )}
    </Fragment>
  );
};

export default Navigation;
