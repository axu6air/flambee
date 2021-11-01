import React from "react";
import ReactDOM from "react-dom";
import Notify from "./service/Notify";
import App from "./components/App";
import axios from "axios";
import "./index.css";
import "bootstrap/dist/css/bootstrap.css";

axios.defaults.baseURL = "http://localhost:50447";
const token = localStorage.getItem("bearerToken");
axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;

axios.interceptors.response.use(
  (response) => {
    if (!response.handleLocally) {
      Notify.handleNotification(response);
    }

    return Promise.resolve(response.data);
  },
  (error) => {
    const statusCode = error.response ? error.response.status : null;
    if (statusCode === 401) {
      localStorage.removeItem("bearerToken");
      localStorage.removeItem("currentUser");
    }

    Notify.handleNotification(error.response);

    return Promise.reject(error);
  }
);

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);
