import { createGlobalStyle } from "styled-components";

export const GlobalStyle = createGlobalStyle`
  * {
    box-sizing: border-box;
  }

  html {
    height: 100%;
  }
  
  body {
    height: 100%;
    margin: 0;
    font-family: "Open Sans Condensed", sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    background-image: url("/img/webb.png")
  }
  
  a {
    text-decoration: none;
    color: black;
  }

  #root {
    display: grid;
    grid-template-rows: auto 1fr auto;
    grid-template-areas:
    "header"
    "main"
    "footer";
    height: 100%;
  }

  form {
    display: flex;
    flex-direction: column;
    max-width: 880px;
    background-color: white;
    label {
      padding-top: 1rem;
    }

    input[type="submit"] {
      margin: 1rem 0;
    }
  }
`;
