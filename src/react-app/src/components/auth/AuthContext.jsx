import React, { useContext, useState, useEffect } from "react";

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
    console.log(user);
    console.log(token);
    setCurrentUser(user);
  }, []);

  return (
    <AuthContext.Provider value={{ currentUser, setCurrentUser }}>
      {children}
    </AuthContext.Provider>
  );
};
