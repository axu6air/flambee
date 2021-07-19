import React, { Component } from "react";
import { Redirect, Link } from "react-router-dom";
import axios from "axios";
import Username from "./Username";
import { FaUserAlt, FaKey } from "react-icons/fa";
import { FiMail, FiPhone } from "react-icons/fi";
import { WiMoonAltFirstQuarter, WiMoonAltThirdQuarter } from "react-icons/wi";
import "../../assets/css/authentication.css";

class Signup extends Component {
  constructor(props) {
    super(props);
    this.usernameRef = React.createRef();
  }

  state = {
    user: {
      firstName: "",
      lastName: "",
      username: "",
      email: "",
      phoneNumber: "",
      password: "",
      confirmPassword: "",
    },
    isFormValid: false,
    loading: false,
    isUsernameValid: false,
    isUsernameLoading: false,
    error: {
      firstName: "",
      lastName: "",
      username: "",
      email: "",
      phoneNumber: "",
      password: "",
      confirmPassword: "",
    },
    responseSucceeded: false,
  };

  validateEmail = (email) => {
    if (!email) return false;

    const emailRegex =
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return emailRegex.test(String(email).toLowerCase());
  };

  handleInputChange = (event) => {
    const target = event.target;
    const value = target.type === "checkbox" ? target.checked : target.value;
    const name = target.name;

    this.setState((prevState) => ({
      user: {
        ...prevState.user,
        [name]: value,
      },
      error: {
        ...prevState.error,
        [name]: "",
      },
    }));
  };

  handleUsernameChange = (username) => {
    console.log("Sign up username", username);

    this.setState((prevState) => ({
      user: {
        ...prevState.user,
        username,
      },
    }));
  };

  hanndleUsernameValidity = (isValid) => {
    console.log("isUsernameValid", isValid);
    this.setState({ isUsernameValid: isValid });
  };

  processErrorMessages = (errors) => {
    this.setState({ errorResponse: true });
  };

  sendSignupRequest = async () => {
    const state = this.state;
    this.setState({ loading: true });

    await axios
      .post("/Auth/Register", state.user)
      .then((response) => {
        console.log(response);
        if (!response.data || !response.data.succeeded)
          this.processErrorMessages(response.data.errorMessages);
        else this.setState({ responseSucceeded: true });
      })
      .catch((error) => {
        console.error(error);
        this.processErrorMessages(error.data.errorMessages);
      })
      .then(() => this.setState({ loading: false }));
  };

  validateForm = () => {
    const self = this;
    const state = self.state;
    let errorCount = 0;
    let error = {};

    if (!state.user.firstName) {
      errorCount++;
      error.firstName = "first name is required";
    }

    if (!state.user.lastName) {
      errorCount++;
      error.lastName = "last name is required";
    }

    if (!state.user.email) {
      errorCount++;
      error.email = "email is required";
    } else if (!self.validateEmail(state.user.email)) {
      errorCount++;
      error.email = "invalid email";
    }

    if (!state.user.username) {
      errorCount++;
      error.username = "username is required";
    }

    if (!state.user.phoneNumber) {
      errorCount++;
      error.phoneNumber = "phone number is required";
    }

    if (!state.isUsernameValid) {
      errorCount++;
      error.username = "username is not valid or already taken";
    }

    if (state.user.password.length < 6) {
      errorCount++;
      error.password = "password requires at least 6 characters";
    }

    if (state.user.password !== state.user.confirmPassword) {
      errorCount++;
      error.confirmPassword = "passwords do not match";
    }

    self.setState({
      isFormValid: errorCount === 0 ? true : false,
      error: error,
    });
    return errorCount === 0 ? true : false;
  };

  handleSubmit = (event) => {
    event.preventDefault();

    const isFormValid = this.validateForm();

    if (isFormValid) {
      this.sendSignupRequest();
    } else {
      console.log(this.state.error);
    }
  };

  render() {
    if (this.state.responseSucceeded) {
      return <Redirect to="/Login" />;
    }

    return (
      <section>
        <div className="home-main">
          <div className="card-container">
            <div className="container d-flex justify-content-center align-items-center">
              <div className="card card-form">
                <div className="card-header">
                  <h3>Sign Up</h3>
                </div>
                <div className="card-body">
                  <form
                    onSubmit={(event) => {
                      this.handleSubmit(event);
                    }}
                  >
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <WiMoonAltFirstQuarter />
                        </span>
                      </div>
                      <input
                        type="text"
                        name="firstName"
                        className="form-control"
                        placeholder="first name"
                        autoComplete="nope"
                        onChange={this.handleInputChange}
                      />
                    </div>
                    {this.state.error.firstName && (
                      <div className="error-message">
                        {this.state.error.firstName}
                      </div>
                    )}
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <WiMoonAltThirdQuarter />
                        </span>
                      </div>
                      <input
                        type="text"
                        name="lastName"
                        className="form-control"
                        placeholder="last name"
                        autoComplete="nope"
                        onChange={this.handleInputChange}
                      />
                    </div>
                    {this.state.error.lastName && (
                      <div className="error-message">
                        {this.state.error.lastName}
                      </div>
                    )}
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FaUserAlt />
                        </span>
                      </div>
                      <div className="text-container">
                        <Username
                          handleUsernameChange={(username) =>
                            this.handleUsernameChange(username)
                          }
                          hanndleUsernameValidity={(valid) =>
                            this.hanndleUsernameValidity(valid)
                          }
                        />
                      </div>
                    </div>
                    {this.state.error.username && (
                      <div className="error-message">
                        {this.state.error.username}
                      </div>
                    )}
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FiMail />
                        </span>
                      </div>
                      <input
                        type="email"
                        name="email"
                        className="form-control"
                        placeholder="email"
                        autoComplete="nope"
                        onChange={this.handleInputChange}
                      />
                    </div>
                    {this.state.error.email && (
                      <div className="error-message">
                        {this.state.error.email}
                      </div>
                    )}
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FiPhone />
                        </span>
                      </div>
                      <input
                        type="text"
                        name="phoneNumber"
                        className="form-control"
                        placeholder="phone number"
                        autoComplete="nope"
                        onChange={this.handleInputChange}
                      />
                    </div>
                    {this.state.error.phoneNumber && (
                      <div className="error-message">
                        {this.state.error.phoneNumber}
                      </div>
                    )}
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FaKey />
                        </span>
                      </div>
                      <input
                        type="password"
                        name="password"
                        className="form-control"
                        placeholder="password"
                        autoComplete="nope"
                        onChange={this.handleInputChange}
                      />
                    </div>
                    {this.state.error.password && (
                      <div className="error-message">
                        {this.state.error.password}
                      </div>
                    )}
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FaKey />
                        </span>
                      </div>
                      <input
                        type="password"
                        name="confirmPassword"
                        className="form-control"
                        placeholder="confirm password"
                        autoComplete="nope"
                        onChange={this.handleInputChange}
                      />
                    </div>
                    {this.state.error.confirmPassword && (
                      <div className="error-message">
                        {this.state.error.confirmPassword}
                      </div>
                    )}
                    <div className="form-group" align="right">
                      <input
                        type="submit"
                        value="Sign up"
                        className="container d-flex justify-content-center btn login-btn"
                      />
                    </div>
                  </form>
                </div>
                <div className="card-footer">
                  <div className="d-flex justify-content-center links">
                    Already have an account?<Link to="/Login">Sign in</Link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    );
  }
}

export default Signup;
