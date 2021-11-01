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
    if (response) {
      const self = this;
      const statusCode = response.status;

      try {
        if (statusCode === 200 && response.message) {
          self.success(response.message);
        } else {
          if (response && response.errors) {
            self.error(response.errors);
          }
        }
      } catch (err) {
        self.error("Error occured");
      }
    }
  };
}

export default new Notify();
