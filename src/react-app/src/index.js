import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./components/App";
import "bootstrap/dist/css/bootstrap.css";
import axios from "axios";

axios.defaults.baseURL = "http://localhost:50449";
const token = localStorage.getItem("bearerToken");
axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
axios.interceptors.response.use(
  (response) => response,
  (error) => {
    console.log(error);
    if (error.status === 401) {
      localStorage.removeItem("bearerToken");
      localStorage.removeItem("currentUser");
    }
    if (!error.status) {
      console.log("Network error: " + error);
    }
  }
);

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);
