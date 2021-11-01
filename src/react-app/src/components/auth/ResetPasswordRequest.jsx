import React, { useRef, useState } from "react";
import { useHistory } from "react-router-dom";
import { FiMail } from "react-icons/fi";
import { isEmailValid } from "../helper/HelperFunstions";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "../../assets/css/authentication.css";
import AuthService from "../../service/Auth";
import Loader from "../helper/Loader";

const ResetPasswordRequest = () => {
  const email = useRef();
  const [loading, setLoading] = useState(false);

  const history = useHistory();

  const handleSubmit = async (event) => {
    console.log(window.location.origin);
    event.preventDefault();

    if (!email.current.value) {
      email.current.focus();
      return false;
    }

    if (!isEmailValid(email.current.value) || !email.current.value) {
      toast.error("Invalid email");
      return false;
    }

    try {
      setLoading(true);
      const response = await AuthService.resetPasswordRequest(
        email.current.value,
        window.location.origin + "/ResetPassword"
      );

      if (response.status === 200) {
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
                  <h3>Reset Password</h3>
                  <div className="d-flex justify-content-end social_icon"></div>
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
    </>
  );
};

export default ResetPasswordRequest;
