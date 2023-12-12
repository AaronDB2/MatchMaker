import { useState } from "react";

import { HamburgerIcon } from "./hamburgerMenu.styles";

// Hamburger menu component
const HamburgerMenu = ({ handler }) => {
  return (
    <HamburgerIcon>
      <i className="fa fa-bars" onClick={handler}></i>
    </HamburgerIcon>
  );
};

export default HamburgerMenu;
