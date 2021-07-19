import axios from "axios";
import React, { useRef } from "react";
import { FiMail } from "react-icons/fi";
import { isEmailValid } from "../helper/HelperFunstions";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "../../assets/css/authentication.css";

const ResetPasswordRequest = () => {
  const email = useRef();

  const handleSubmit = async (event) => {
    console.log(window.location.origin);
    event.preventDefault();

    if (!email.current.value) {
      email.current.focus();
      return false;
    }

    if (!isEmailValid(email.current.value)) return false;
    const requestModel = {
      email: email.current.value,
      clientRecoveryPasswordUrl: window.location.origin + "/ResetPassword",
    };

    email.current.value = "";

    await axios
      .post("/RecoveryPasswordRequest", requestModel)
      .then((response) => {
        if (response.data.status === 200) {
          toast.success(response.data.message);
        }
      })
      .catch((error) => {
        if (error.response.status === 400)
          toast.error(error.response.data.message);
        else toast.error("Error occured");
      });
  };

  return (
    <section>
      <div className="home-main">
        <div className="card-container">
          <div className="container d-flex justify-content-center align-items-center">
            <div className="card card-form">
              <div className="card-header">
                <h3>Login</h3>
                <div className="d-flex justify-content-end social_icon">
                  {/* <span>
                    <i className="fab fa-facebook-square"></i>
                  </span>
                  <span>
                    <i className="fab fa-google-plus-square"></i>
                  </span>
                  <span>
                    <i className="fab fa-twitter-square"></i>
                  </span> */}
                </div>
              </div>
              <div className="card-body">
                <form onSubmit={handleSubmit}>
                  <div className="input-group form-group">
                    <div className="input-group-prepend">
                      <span className="input-group-text">
                        <FiMail />
                      </span>
                    </div>
                    <input
                      ref={email}
                      type="text"
                      className="form-control"
                      placeholder="email"
                      autoComplete="email-address"
                    />
                  </div>
                  <div className="form-group" align="right">
                    <input
                      type="submit"
                      value="Reset Password"
                      className="container d-flex justify-content-center btn login-btn"
                    />
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ResetPasswordRequest;
