import styled from "styled-components";
import { Link } from "react-router-dom";

export const PageBody = styled.div`
  grid-area: main;
  background-color: white;
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

export const ChallengeId = styled.h3`
  text-align: center;
`;

export const ChallengeDescription = styled.p`
  text-align: center;
`;

export const Devider = styled.hr`
  border-top: solid black 1px;
  width: 100vw;
`;

export const QuestionForm = styled.form``;

export const QuestionDisplayContainer = styled.div``;

export const QuestionTitle = styled.h3``;

export const QuestionDescription = styled.p``;

export const QuestionUserName = styled.h4``;

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

export const ChallengeInfoContainer = styled.div`
  border: solid black 1px;
  padding: 1rem;
  display: flex;
  flex-direction: column;

  span {
    margin: 1rem;
  }
`;

export const ButtonContainer = styled.div`
  display: flex;

  span {
    padding: 0.5rem;
    cursor: pointer;
    border: solid black 1px;
    margin: 1rem;

    &:hover,
    &:active {
      background: grey;
    }
  }
`;
