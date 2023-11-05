import styled from "styled-components";

export const PageBody = styled.div`
  grid-area: main;
`;

export const CreateTagsContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
`;

export const PageTitle = styled.h1`
  text-align: center;
`;

export const CreateTagsForm = styled.form`
  display: flex;
  flex-direction: column;
  max-width: 880px;

  label {
    padding-top: 1rem;
  }

  input[type="submit"] {
    margin: 1rem 0;
  }
`;
