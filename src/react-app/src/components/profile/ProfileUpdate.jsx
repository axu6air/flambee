import React, { Component } from "react";
import { Link } from "react-router-dom";
import { isEmailValid } from "../helper/HelperFunstions";
import { FiMail, FiPhone } from "react-icons/fi";
import { WiMoonAltFirstQuarter, WiMoonAltThirdQuarter } from "react-icons/wi";
import Loader from "../helper/Loader";
import AvatarUpload from "../avatar/AvatarUpload";
import { AuthContext } from "../auth/AuthContext";
import UserService from "../../service/UserService";
import "../../assets/css/authentication.css";

class ProfileUpdate extends Component {
  static contextType = AuthContext;

  state = {
    user: {
      firstName: "A",
      lastName: "B",
      email: "",
      phoneNumber: "",
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

  getUserData = () => {
    const { currentUser } = this.context;
    this.setState({ loading: true });

    UserService.getUserProfileData(currentUser.applicationUserId)
      .then((response) => {
        const userData = response.data;

        this.setState({ user: userData });

        // this.setState((prevState) => ({
        //   user: {
        //     ...prevState.user,
        //     firstName: userData.firstName,
        //     lastName: userData.lastName,
        //     email: userData.email,
        //     phoneNumber: userData.phoneNumber,
        //   },
        // }));
      })
      .then(() => this.setState({ loading: false }));
  };

  componentDidMount() {
    this.getUserData();
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
        console.log(this.state.user);
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
                          value={this.state.firstName}
                          onChange={this.handleInputChange}
                        />
                      </div>
                      <div>{this.state.firstName}</div>
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
                          value={this.state.lastName}
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
                          value={this.state.email}
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
                          value={this.state.phoneNumber}
                        />
                      </div>
                      {this.state.error.phoneNumber && (
                        <div className="error-message">
                          {this.state.error.phoneNumber}
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
