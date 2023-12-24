import styled from "styled-components";
import { Link } from "react-router-dom";

export const MobileNavContainer = styled.div`
  width: 100vw;
  height: 100vh;
  background: white;
  text-align: center;

  a:hover,
  a:active {
    text-decoration: underline;
  }
`;

export const NavLinks = styled.div`
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
`;

export const NavLink = styled(Link)`
  padding: 0.5rem;
  cursor: pointer;
`;
