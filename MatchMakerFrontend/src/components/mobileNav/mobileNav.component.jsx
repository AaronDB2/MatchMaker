import { useState } from "react";

import { MobileNavContainer, NavLinks, NavLink } from "./mobileNav.styles";

// Mobile navigation  component
const MobileNav = ({ handler }) => {
  // Check for jwt token
  const [data, setData] = useState(localStorage.getItem("token") || "");

  return (
    <MobileNavContainer>
      <NavLinks>
        <NavLink to="/" onClick={handler}>
          LOGO/HOME
        </NavLink>
        <NavLink to="/search-challenges" onClick={handler}>
          SEARCH CHALLENGES
        </NavLink>
        {data ? (
          <NavLink to="/profile" onClick={handler}>
            PROFILE
          </NavLink>
        ) : (
          <NavLink to="/login" onClick={handler}>
            LOGIN
          </NavLink>
        )}
      </NavLinks>
    </MobileNavContainer>
  );
};

export default MobileNav;
