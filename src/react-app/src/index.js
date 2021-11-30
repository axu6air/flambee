import React from "react";
import ReactDOM from "react-dom";
import Notify from "./service/Notify";
import App from "./components/App";
import axios from "axios";
import "./index.css";
import "bootstrap/dist/css/bootstrap.css";

// let axiosIns = axios.create({
//   headers: {},
// });

axios.defaults.baseURL = "http://localhost:50447";
//axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;

axios.interceptors.request.use((request) => {
  const token = localStorage.getItem("bearerToken");
  request.headers.common.Authorization = `Bearer ${token}`;

  return request;
});

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
