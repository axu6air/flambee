import axios from "axios";

class ImageService {
  getBase64FromFile = (file, callback) => {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      callback(reader.result);
    };
    reader.onerror = function (error) {
      callback("error");
    };
  };

  getAvatar = async () => {
    return await axios.get(`/GetAvatar`);
  };

  uploadAvatar = async (avatar, userId) => {
    if (avatar && avatar.avatarImage && userId) {
      const avatarForm = this.prepareAvatarForm(avatar, userId);

      await axios.post("/UploadAvatar", avatarForm, {
        headers: { "Content-Type": "multipart/form-data" },
      });
    }
  };

  prepareAvatarForm = (avatar, userId) => {
    let avatarForm = new FormData();

    avatarForm.append("avatarImage", avatar.avatarImage);
    avatarForm.append("avatarBase64", avatar.avatarBase64);
    avatarForm.append("previewBase64", avatar.previewBase64);
    avatarForm.append("title", avatar.title);
    avatarForm.append("userId", userId);

    return avatarForm;
  };

  getBase64Image = (base64Avatar) => {
    const arr = base64Avatar.split(",");
    return arr.length > 0 ? arr[1] : null;
  };
}

export default new ImageService();
