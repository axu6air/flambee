import React from "react";
import { Route, Redirect } from "react-router-dom";
import { useAuthContext } from "./AuthContext";

const PrivateRoute = ({ component: Component, ...rest }) => {
  const { currentUser } = useAuthContext();

  return (
    <Route
      {...rest}
      render={(props) => {
        return currentUser ? (
          <Component {...props} />
        ) : (
          <Redirect to="/login" />
        );
      }}
    ></Route>
  );
};

export default PrivateRoute;
