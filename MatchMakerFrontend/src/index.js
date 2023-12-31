import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import React from "react";

import App from "./App";

// Gets div from index.html where id=root
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <BrowserRouter>
    <App />
  </BrowserRouter>
);
