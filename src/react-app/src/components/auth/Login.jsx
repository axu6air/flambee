import React, { useEffect, useRef, useState } from "react";
import AuthService from "../../service/Auth";
import { Link, useHistory } from "react-router-dom";
import { useAuthContext } from "./AuthContext";
import { FaUserAlt, FaKey } from "react-icons/fa";
import Loader from "../helper/Loader";
import "react-toastify/dist/ReactToastify.css";
import "../../assets/css/authentication.css";

const Login = () => {
  const { setCurrentUser } = useAuthContext();
  const username = useRef();
  const password = useRef();
  const history = useHistory();

  const [loading, setLoading] = useState(false);
  const isMountedVal = useRef(1);

  useEffect(() => {
    isMountedVal.current = 1;
    return () => {
      isMountedVal.current = 0;
    };
  });

  const login = async () => {
    setLoading(true);

    await AuthService.login(username.current.value, password.current.value)
      .then((response) => {
        if (response.status === 200) {
          console.log(response);

          const token = response.token;
          localStorage.bearerToken = token;

          const userInfoModel = response.userInfoModel;
          console.log(userInfoModel);
          const user = {
            firstName: userInfoModel.firstName,
            lastName: userInfoModel.lastName,
            phoneNumber: userInfoModel.phoneNumber,
            applicationUserId: response.id,
            username: response.username,
            email: userInfoModel.userModel?.email,
            token: response.token,
          };

          console.log("user", user);

          setCurrentUser(user);
          localStorage.currentUser = JSON.stringify(user);
          history.push("/Dashboard");
        }
      })
      .catch((error) => {
        console.error(error);
        setCurrentUser(null);
      })
      .then(() => {
        setLoading(false);
      });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (!username.current.value) {
      username.current.focus();
    } else if (!password.current.value) {
      password.current.focus();
    } else {
      login();
    }
  };

  return (
    <>
      {loading && <Loader />}
      <section>
        <div className="home-main">
          <div className="card-container">
            <div className="container d-flex justify-content-center align-items-center">
              <div className="card card-form">
                <div className="card-header">
                  <h3>Login</h3>
                  <div className="d-flex justify-content-end social_icon">
                    <span>
                      <i className="fab fa-facebook-square"></i>
                    </span>
                    <span>
                      <i className="fab fa-google-plus-square"></i>
                    </span>
                    <span>
                      <i className="fab fa-twitter-square"></i>
                    </span>
                  </div>
                </div>
                <div className="card-body">
                  <form onSubmit={handleSubmit}>
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FaUserAlt />
                        </span>
                      </div>
                      <input
                        ref={username}
                        type="text"
                        className="form-control"
                        placeholder="username"
                        autoComplete="new-username"
                      />
                    </div>
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FaKey />
                        </span>
                      </div>
                      <input
                        ref={password}
                        type="password"
                        className="form-control"
                        placeholder="password"
                        autoComplete="new-password"
                      />
                    </div>
                    <div className="form-group" align="right">
                      <input
                        type="submit"
                        value="Login"
                        className="container d-flex justify-content-center btn login-btn"
                        disabled={loading}
                      />
                    </div>
                  </form>
                </div>
                <div className="card-footer">
                  <div className="d-flex justify-content-center links">
                    Don't have an account?<Link to="/Signup">Signup</Link>
                  </div>
                  <div className="d-flex justify-content-center">
                    <a href="/ResetPasswordRequest">Forgot your password?</a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};

export default Login;
