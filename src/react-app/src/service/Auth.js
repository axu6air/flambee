import axios from "axios";
import { toast } from "react-toastify";

class AuthService {
  login = async (username, password) => {
    const model = {
      username,
      password,
    };
    return await axios.post("/Login", model);
  };

  signup = async (user) => {
    return await axios.post("/Register", user);
  };

  logout = () => {
    localStorage.bearerToken = "";
    localStorage.currentUser = null;
    axios.post("/Logout").then((response) => {
      if (response.status === 200) {
        window.location.reload();
        return true;
      }
    });

    return false;
  };

  resetPasswordRequest = async (email, clientResetPasswordUrl) => {
    const requestModel = {
      email,
      clientRecoveryPasswordUrl: clientResetPasswordUrl,
    };

    return await axios.post("/RecoveryPasswordRequest", requestModel);
  };

  resetPassword = async (email, password, confirmPassword, token) => {
    const model = {
      password,
      confirmPassword,
      token,
      email,
    };

    return await axios.post("/RecoveryPassword", model);
  };

  processErrorMessages = (errors) => {
    if (typeof errors.response.errors === "object")
      errors = Object.values(errors);
    errors.forEach((error) => toast.error(error));
  };
}

export default new AuthService();
