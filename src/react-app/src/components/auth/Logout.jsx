import React from "react";
import { useEffect } from "react";
import { Redirect } from "react-router-dom";
import AuthService from "../../service/Auth";

const Logout = () => {
  useEffect(() => {
    AuthService.logout();
    window.location.reload();
  });

  return <Redirect to="/Login" />;
};

export default Logout;
