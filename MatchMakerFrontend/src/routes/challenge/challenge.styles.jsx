import styled from "styled-components";

export const PageBody = styled.div`
  grid-area: main;
`;

export const ChallengeContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
`;

export const PageTitle = styled.h1`
  text-align: center;
`;

export const ChallengeDescription = styled.p`
  text-align: center;
`;

export const QuestionForm = styled.form`
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

export const QuestionDisplayContainer = styled.div``;

export const QuestionTitle = styled.h2``;

export const QuestionDescription = styled.p``;

export const QuestionUserName = styled.h3``;
