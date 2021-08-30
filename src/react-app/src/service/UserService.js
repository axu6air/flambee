import axios from "axios";

class UserService {
  getUserDetails = async () => {
    await axios.get("/User").then((response) => {
      console.log(response);
    });
  };
}

export default new UserService();
