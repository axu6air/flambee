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

  // getBase64FromFile = (file, callback) => {
  //   // var xhr = new XMLHttpRequest();
  //   // xhr.onload = function () {
  //   //   var reader = new FileReader();
  //   //   reader.onloadend = function () {
  //   //     callback(reader.result);
  //   //   };
  //   //   reader.readAsDataURL(xhr.response);
  //   // };
  //   // xhr.open("GET", file);
  //   // xhr.responseType = "blob";
  //   // xhr.send();

  //   var canvas = document.createElement("canvas");
  //   canvas.width = file.width;
  //   canvas.height = file.height;
  //   var ctx = canvas.getContext("2d");
  //   ctx.drawImage(file, 0, 0);
  //   var dataURL = canvas.toDataURL("image/png");
  //   return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
  // };

  getBase64Image = (base64Avatar) => {
    const arr = base64Avatar.split(",");
    return arr.length > 0 ? arr[1] : null;
  };
}

export default new ImageService();
