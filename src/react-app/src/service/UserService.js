import axios from "axios";

class UserService {
  getUserProfileData = async () => {
    return await axios.get(`/User`);
  };
}

export default new UserService();
