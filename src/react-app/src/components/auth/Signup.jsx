import React, { Component } from "react";
import { Redirect, Link } from "react-router-dom";
import Username from "./Username";
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

class Signup extends Component {
  state = {
    user: {
      firstName: "",
      lastName: "",
      username: "",
      email: "",
      phoneNumber: "",
      password: "",
      confirmPassword: "",
      avatarUploadModel: {
        avatar: null,
        avatarBase64: null,
        previewBase64: null,
        title: "",
      },
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
      avatarImage: null,
      avatarBase64: null,
      previewBase64: null,
      title: "",
    },
    avatarForm: null,
    responseSucceeded: false,
    regexFields: {
      username: "",
    },
  };

  componentDidMount() {
    axios.get("/GetFormRules").then((response) => {
      if (response && response.data) {
        const usernameRegex = response.data.username;
        this.setState((prevState) => ({
          regexFields: {
            ...prevState.regexFields,
            username: usernameRegex,
          },
        }));
      }
    });
  }

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

  handleUsernameValidity = (isValid) => {
    console.log("isUsernameValid", isValid);
    this.setState({ isUsernameValid: isValid });
  };

  sendSignupRequest = async () => {
    this.setState({ loading: true });
    const state = this.state;

    await AuthService.signup(state.user)
      .then((response) => {
        if (response.status === 200 && response.data.userId) {
          if (state.avatar.avatarBase64) {
            const avatarForm = this.prepareAvatarForm(
              state.avatar,
              response.data.userId
            );

            this.sendAvatarUploadRequest(avatarForm);
          }

          this.setState({ responseSucceeded: true });
        } else {
          this.setState({ responseSucceeded: false });
        }
      })
      .catch((error) => {
        this.setState({ responseSucceeded: false });
      })
      .then(() => this.setState({ loading: false }));
  };

  sendAvatarUploadRequest = async (avatarForm) => {
    try {
      await axios.post("/UploadAvatar", avatarForm, {
        headers: { "Content-Type": "multipart/form-data" },
      });
    } catch (error) {
      toast.error("Could not upload avatar");
    }
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
    } else if (!isEmailValid(state.user.email)) {
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

  handleAvatar = (avatar) => {
    if (avatar) {
      this.setState({
        avatar: {
          ...this.state.avatar,
          avatarImage: avatar.avatar,
          avatarBase64: avatar.avatarBase64,
          previewBase64: avatar.previewBase64,
          title: avatar.title,
        },
      });
    }
  };

  prepareAvatarForm = (avatar, userId) => {
    let avatarForm = new FormData();

    avatarForm.append("avatarImage", avatar.avatarImage);
    avatarForm.append("avatarBase64", avatar.avatarBase64);
    avatarForm.append("previewBase64", avatar.previewBase64);
    avatarForm.append("title", avatar.title);
    avatarForm.append("userId", userId);

    return avatarForm;
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
      <>
        {this.state.loading && <Loader />}
        <section>
          <div className="home-main">
            <div className="card-container">
              <div className="container d-flex justify-content-center align-items-center">
                <div className="card card-form">
                  <div className="card-header">
                    <h3>Signup</h3>
                  </div>
                  <div className="card-body">
                    <form
                      onSubmit={(event) => {
                        this.handleSubmit(event);
                      }}
                    >
                      <div>
                        <AvatarUpload
                          avatar={this.state.avatar}
                          onAvatarSelect={this.handleAvatar}
                        />
                      </div>
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

export default Signup;
