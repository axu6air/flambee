import React, { Component } from "react";
import { Redirect, Link } from "react-router-dom";
import Username from "../auth/Username";
import { isEmailValid } from "../helper/HelperFunstions";
import { FaUserAlt, FaKey } from "react-icons/fa";
import { FiMail, FiPhone } from "react-icons/fi";
import { WiMoonAltFirstQuarter, WiMoonAltThirdQuarter } from "react-icons/wi";
import "../../assets/css/authentication.css";
import Loader from "../helper/Loader";
import AuthService from "../../service/Auth";
import AvatarUpload from "../avatar/AvatarUpload";
import axios from "axios";
import { toast } from "react-toastify";
import { AuthContext } from "../auth/AuthContext";

class ProfileUpdate extends Component {
  static contextType = AuthContext;

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
    avatar: {
      avatarBase64: null,
      previewBase64: null,
      title: "",
    },
    avatarForm: null,
    responseSucceeded: false,
    regexFields: {
      username: "",
    },
    userId: null,
  };

  componentDidMount() {
    console.log("Profile Update componentDidMount");
  }

  render() {
    const { currentUser } = this.context;

    if (this.state.responseSucceeded) {
      return <Redirect to="/Login" />;
    }

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
                      // avatar={this.state.avatar}
                      // onAvatarSelect={this.handleAvatar}
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
                      {/* <div className="input-group form-group">
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
                            handleUsernameValidity={(valid) =>
                              this.handleUsernameValidity(valid)
                            }
                            usernameRegex={this.state.regexFields.username}
                          />
                        </div>
                      </div> */}
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
