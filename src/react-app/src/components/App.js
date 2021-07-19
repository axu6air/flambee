import React from "react";
import Home from "./Home";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Header from "./layout/Header";
import Login from "./auth/Login";
import Signup from "./auth/Signup";
import Logout from "./auth/Logout";
import "../App.css";
import { ToastContainer } from "react-toastify";
import ResetPasswordRequest from "./auth/ResetPasswordRequest";
import ResetPassword from "./auth/ResetPassword";
import { AuthProvider } from "./auth/AuthContext";
import PrivateRoute from "./auth/PrivateRoute";
import Dashboard from "./Dashboard";
const App = () => {
  return (
    <>
      <Router>
        <Switch>
          <AuthProvider>
            <Header />
            <ToastContainer
              position="top-right"
              autoClose={3000}
              hideProgressBar={false}
              newestOnTop={false}
              closeOnClick
              rtl={false}
              pauseOnFocusLoss
              draggable
              pauseOnHover
            />
            <Route exact path="/">
              <Home></Home>
            </Route>
            <PrivateRoute exact path="/Dashboard" component={Dashboard} />
            <Route exact path="/Login">
              <Login />
            </Route>
            <Route exact path="/Signup">
              <Signup></Signup>
            </Route>
            <Route exact path="/Logout">
              <Logout />
            </Route>
            <Route exact path="/ResetPasswordRequest">
              <ResetPasswordRequest />
            </Route>
            <Route exact path="/ResetPassword">
              <ResetPassword />
            </Route>
          </AuthProvider>
        </Switch>
      </Router>
    </>
  );
};

export default App;
