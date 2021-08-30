import axios from "axios";

class UserService {
  getUserProfileData = async (userId) => {
    return await axios.get(`/User/${userId}`);
  };
}

export default new UserService();
