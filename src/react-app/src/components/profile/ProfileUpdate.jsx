import React, { Component } from "react";
import { Link } from "react-router-dom";
import { isEmailValid } from "../helper/HelperFunstions";
import { FaKey } from "react-icons/fa";
import { FiMail, FiPhone } from "react-icons/fi";
import { WiMoonAltFirstQuarter, WiMoonAltThirdQuarter } from "react-icons/wi";
import Loader from "../helper/Loader";
import AvatarUpload from "../avatar/AvatarUpload";
import { AuthContext } from "../auth/AuthContext";
import "../../assets/css/authentication.css";

class ProfileUpdate extends Component {
  static contextType = AuthContext;

  state = {
    user: {
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      confirmPassword: "",
    },
    isFormValid: false,
    loading: false,
    error: {
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      confirmPassword: "",
    },
    userId: null,
  };

  componentDidMount() {
    console.log("Profile Update componentDidMount");
  }

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
    } else if (!isEmailValid(state.user.email)) {
      errorCount++;
      error.email = "invalid email";
    }

    if (!state.user.phoneNumber) {
      errorCount++;
      error.phoneNumber = "phone number is required";
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

  render() {
    const { currentUser } = this.context;

    return (
      <>
        {this.state.loading && <Loader />}
        <section>
          <div className="home-main">
            <div className="card-container">
              <div className="container d-flex justify-content-center align-items-center">
                <div className="card card-form">
                  <div>
                    <AvatarUpload
                      triggerRequired={false}
                      userId={currentUser.applicationUserId}
                    />
                  </div>
                </div>
                <div className="card card-form">
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
                          autoComplete="firstName"
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
                          autoComplete="lastName"
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
                            <FiMail />
                          </span>
                        </div>
                        <input
                          type="email"
                          name="email"
                          className="form-control"
                          placeholder="email"
                          autoComplete="emailAddress"
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
                          autoComplete="phoneNumber"
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
                          autoComplete="passwordAuto"
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
                          autoComplete="confirmPassword"
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
      </>
    );
  }
}

export default ProfileUpdate;
