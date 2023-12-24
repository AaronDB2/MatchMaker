import styled from "styled-components";

export const HamburgerIcon = styled.div`
  cursor: pointer;
  display: none;
  margin: 0 2rem;
  font-size: 4rem;

  /* Media Query for mobile */
  @media (max-width: 64rem) {
    display: block;
  }
`;
