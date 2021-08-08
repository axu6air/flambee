import React, { useContext, useState, useEffect } from "react";
import { parseJwt } from "../helper/HelperFunstions";
import AuthVerify from "./AuthVerify";
import AuthService from "../../service/Auth";

export const AuthContext = React.createContext();

export const useAuthContext = () => {
  return useContext(AuthContext);
};

export const AuthProvider = ({ children }) => {
  const [currentUser, setCurrentUser] = useState(
    JSON.parse(localStorage.getItem("currentUser"))
  );

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem("currentUser"));
    const token = localStorage.getItem("bearerToken");
    setCurrentUser(user);

    if (user) {
      const decodedJwt = parseJwt(token);
      console.log("-----Auth CONTEXT Verify------");
      console.log(decodedJwt.exp * 1000);
      console.log(decodedJwt.exp * 1000 < Date.now());
      if (decodedJwt.exp * 1000 < Date.now()) {
        AuthService.logout();
      }
    }
  }, []);

  return (
    <AuthContext.Provider value={{ currentUser, setCurrentUser }}>
      <AuthVerify>{children}</AuthVerify>
    </AuthContext.Provider>
  );
};
