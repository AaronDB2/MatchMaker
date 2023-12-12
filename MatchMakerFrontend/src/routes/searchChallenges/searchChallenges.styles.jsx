import styled from "styled-components";

export const PageBody = styled.div`
  grid-area: main;
`;

export const SearchChallengesContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
`;

export const PageTitle = styled.h1`
  text-align: center;
`;

export const SearchChallengesForm = styled.form``;

export const SearchChallengeTable = styled.table`
  border: 1px solid black;

  th,
  td {
    border: 1px solid black;
    font-size: 1rem;
    padding: 1rem;
  }

  /* Media Query for mobile */
  @media (max-width: 64rem) {
    th,
    td {
      display: none;
    }

    th:first-of-type,
    td:first-of-type {
      display: block;
    }
  }
`;
