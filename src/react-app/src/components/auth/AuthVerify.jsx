import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import AuthService from "../../service/Auth";
import { parseJwt } from "../helper/HelperFunstions.js";

class AuthVerify extends Component {
  constructor(props) {
    super(props);

    props.history.listen(() => {
      console.log("-----Auth Verify------");
      const user = JSON.parse(localStorage.getItem("currentUser"));
      const token = localStorage.getItem("bearerToken");

      if (user) {
        const decodedJwt = parseJwt(token);
        console.log(decodedJwt.exp * 1000);
        console.log(decodedJwt.exp * 1000 < Date.now());
        if (decodedJwt.exp * 1000 < Date.now()) {
          AuthService.logout();
        }
      }
    });
  }

  render() {
    return <>{this.props.children}</>;
  }
}

export default withRouter(AuthVerify);
