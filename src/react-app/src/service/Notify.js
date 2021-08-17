import { toast } from "react-toastify";

class Notify {
  error = (errors) => {
    console.log("Notify errors", errors);
    console.log();
    if (Array.isArray(errors)) {
      errors.forEach((err, index) => {
        toast.error(err);
        console.log(err, index);
      });
    } else toast.error(errors);
  };

  success = (message) => {
    toast.success(message);
  };

  handleNotification = (response) => {
    const self = this;
    const statusCode = response.status;
    console.log(response);

    try {
      if (statusCode === 200 && response.data.message) {
        self.success(response.data.message);
      } else {
        if (response.data && response.data.errors) {
          self.error(response.data.errors);
        }
      }
    } catch (err) {
      self.error("Error occured");
    }
  };
}

export default new Notify();
