import styled from "styled-components";

export const PageBody = styled.div`
  grid-area: main;
`;

export const RegisterContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
`;

export const PageTitle = styled.h1`
  text-align: center;
`;

export const RegisterForm = styled.form`
  border: solid black 1px;
  padding: 10rem;
  box-shadow: 5px 5px rgba(0, 0, 0, 0.5);
  background-color: white;

  /* Media Query for mobile */
  @media (max-width: 64rem) {
    box-shadow: none;
    border: none;
    padding: 0;
  }
`;
