import styled from "styled-components";
import { Link } from "react-router-dom";

export const PageBody = styled.div`
  grid-area: main;
`;

export const ProfileContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
`;

export const PageTitle = styled.h1`
  text-align: center;
`;

export const EditDataForm = styled.form``;

export const NavLink = styled(Link)`
  padding: 0.5rem;
  cursor: pointer;
  border: solid black 1px;
  margin: 1rem;

  &:hover,
  &:active {
    background: grey;
  }
`;

export const EditDataContainer = styled.div`
  display: flex;

  div {
    margin: 1rem;

    h3 {
      margin: 0;
    }
  }

  /* Media Query for mobile */
  @media (max-width: 64rem) {
    display: flex;
    flex-direction: column;
  }
`;

export const ButtonContainer = styled.div`
  text-align: center;

  /* Media Query for mobile */
  @media (max-width: 64rem) {
    div {
      display: flex;
      flex-direction: column;
    }
  }
`;
