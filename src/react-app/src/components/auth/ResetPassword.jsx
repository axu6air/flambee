import React, { useState, useRef, useEffect } from "react";
import AuthService from "../../service/Auth";
import { useLocation, useHistory } from "react-router-dom";
import queryString from "query-string";
import { FaKey } from "react-icons/fa";
import { toast } from "react-toastify";

import "react-toastify/dist/ReactToastify.css";
import "../../assets/css/authentication.css";
import Loader from "../helper/Loader";

const ResetPassword = ({ match, location }) => {
  const [token, setToken] = useState();
  const [email, setEmail] = useState();
  const [loading, setLoading] = useState(false);

  const history = useHistory();

  const password = useRef();
  const confirmPassword = useRef();

  const { search } = useLocation();

  useEffect(() => {
    const queryValue = queryString.parse(search);

    let error = false;
    if (queryValue && queryValue.token) setToken(queryValue.token);
    else error = true;
    if (queryValue.email) setEmail(queryValue.email);
    else error = true;

    console.log(token);

    if (error) toast.error("Invalid request");
  }, [search, token]);

  const prepareModel = () => {
    const responseModel = {
      password: password.current.value,
      confirmPassword: confirmPassword.current.value,
      token: token.split(" ").join("+"),
      email,
    };

    return responseModel;
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (!password.current.value) password.current.focus();
    if (!confirmPassword.current.value) confirmPassword.current.focus();

    const model = prepareModel();
    console.log(model);

    try {
      setLoading(true);
      const response = await AuthService.resetPassword(
        email,
        password.current.value,
        confirmPassword.current.value,
        token.split(" ").join("+")
      );
      console.log(response);
      if (response.data.status === 200) {
        history.push("/Login");
      }
    } finally {
      setLoading(false);
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
                  <h3>Set new password</h3>
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
                          <FaKey />
                        </span>
                      </div>
                      <input
                        ref={password}
                        type="password"
                        className="form-control"
                        placeholder="new password"
                        autoComplete="new-password"
                      />
                    </div>
                    <div className="input-group form-group">
                      <div className="input-group-prepend">
                        <span className="input-group-text">
                          <FaKey />
                        </span>
                      </div>
                      <input
                        ref={confirmPassword}
                        type="password"
                        className="form-control"
                        placeholder="confirm password"
                        autoComplete="confirm-password"
                      />
                    </div>
                    <div className="form-group" align="right">
                      <input
                        type="submit"
                        value="Reset Password"
                        className="container d-flex justify-content-center btn login-btn"
                        disabled={loading}
                      />
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
};

export default ResetPassword;
