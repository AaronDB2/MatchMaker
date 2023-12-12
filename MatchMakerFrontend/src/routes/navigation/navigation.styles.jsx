import styled from "styled-components";
import { Link } from "react-router-dom";

export const NavigationContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  border-bottom: solid black 2px;
  padding: 0.5rem 0;
  grid-area: header;

  div:first-child {
    margin-right: auto;
  }

  a:hover,
  a:active {
    text-decoration: underline;
  }
`;

export const LogoContainer = styled.div``;

export const NavLinks = styled.div`
  /* Media Query for mobile */
  @media (max-width: 64rem) {
    display: none;
  }
`;

export const NavLink = styled(Link)`
  padding: 0.5rem;
  cursor: pointer;
`;

export const NavFooterContainer = styled.div`
  display: flex;
  justify-content: center;
  border-top: solid black 2px;
  padding: 0.5rem 0;
  grid-area: footer;

  a:hover,
  a:active {
    text-decoration: underline;
  }

  /* Media Query for mobile */
  @media (max-width: 64rem) {
    div {
      display: block;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
    }
  }
`;
