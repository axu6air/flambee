import axios from "axios";
import React from "react";
import { useEffect } from "react";
import { Redirect } from "react-router-dom";

const Logout = () => {
  localStorage.bearerToken = "";
  localStorage.currentUser = null;

  useEffect(() => {
    const logoutRequest = () => {
      axios.post("/Logout").then((response) => {
        if (response.status === 200) {
          return <Redirect to="/Login" />;
        }
      });
    };

    logoutRequest();
  });

  return <Redirect to="/Login" />;
};

export default Logout;
