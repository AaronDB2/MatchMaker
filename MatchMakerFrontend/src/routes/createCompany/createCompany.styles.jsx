import styled from "styled-components";

export const PageBody = styled.div`
  grid-area: main;
`;

export const CreateCompanyContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
`;

export const PageTitle = styled.h1`
  text-align: center;
`;

export const CreateCompanyForm = styled.form`
  /* Media Query for mobile */
  @media (max-width: 64rem) {
    max-width: 300px;
  }
`;
