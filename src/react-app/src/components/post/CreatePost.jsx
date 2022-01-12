import React, { Component } from "react";
import ReactModal from "react-modal";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimesCircle, faPlus } from "@fortawesome/free-solid-svg-icons";
import ImageUploading from "react-images-uploading";
import "../../assets/css/upload-button.css";
import axios from "axios";

class CreatePost extends Component {
  state = {
    showModal: false,
    images: [],
    title: "",
  };

  onChange = (imageList, addUpdateIndex) => {
    console.log(imageList);
    console.log(addUpdateIndex);
    this.setState({ images: imageList });
  };

  handleInputChange = (event) => {
    const value = event.target.value;
    const name = event.target.name;

    this.setState((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  handleSubmit = () => {
    if (this.state.images.length > 0) {
      const model = this.prepareSubmitModel();
      axios
        .post("/Post/Create", model, {
          headers: { "Content-Type": "multipart/form-data" },
        })
        .then((res) => {
          console.log(res);
        });
    }
  };

  prepareSubmitModel = () => {
    let model = new FormData();
    model.append("title", this.state.title);
    model.append("sharedFrom", null);

    model.PostImages = [];

    this.state.images.forEach((image, index) => {
      // if (index === 0) {
      //   model.append("image", image.file);
      //   model.append("postImages", null);
      // }
      // model.PostImages.push({
      //   imageBase64: image.data_url,
      //   image: image.file,
      //   mimeType: image.file.type,
      //   displayName: image.file.name,
      // })
      // model.append(`postImages[${index}]`, image.data_url);
      model.append(`postImages[${index}][image].file`, image.file);
      // model.append(`postImages[${index}][mimeType]`, image.file.type);
      // model.append(`postImages[${index}][displayName]`, image.file.name);
    });

    // console.log(model.getAll());

    return model;
  };

  handleModal = () => {
    ReactModal.setAppElement("#modal-post");
    console.log("CLICKED");
    this.setState((prevState) => ({
      showModal: !prevState.showModal,
    }));
  };

  render() {
    const maxNumber = 4;
    const customStyles = {
      content: {
        top: "50%",
        left: "50%",
        right: "auto",
        bottom: "auto",
        marginRight: "-50%",
        transform: "translate(-50%, -50%)",
        background: "grey",
        height: "auto",
        width: "500px",
        maxHeight: "800px",
        maxWidth: "800px",
        overFlowY: "hidden",
      },
    };

    return (
      <>
        <button onClick={this.handleModal}>
          <FontAwesomeIcon icon={faPlus} style={{ fontSize: "50px" }} />
        </button>

        <div id="modal-post"></div>
        <ReactModal isOpen={this.state.showModal} style={customStyles}>
          <span
            style={{
              paddingTop: "0px",
              float: "right",
              cursor: "pointer",
            }}
            onClick={() => this.handleModal()}
          >
            <FontAwesomeIcon
              icon={faTimesCircle}
              style={{ fontSize: "20px" }}
            />
          </span>
          <ImageUploading
            multiple
            value={this.state.images}
            onChange={this.onChange}
            maxNumber={maxNumber}
            dataURLKey="data_url"
          >
            {({
              imageList,
              onImageUpload,
              onImageRemoveAll,
              onImageUpdate,
              onImageRemove,
              isDragging,
              dragProps,
            }) => (
              <div className="container">
                <div className="form-group">
                  <textarea
                    name="title"
                    className="form-control"
                    onChange={this.handleInputChange}
                    style={{ fontSize: "12px", fontFamily: "Helvetica" }}
                  ></textarea>
                </div>

                <div className="row">
                  {imageList.map((image, index) => (
                    <div className="col-6 image-card" key={index}>
                      <div className="box d-flex justify-content-center align-items-center">
                        <img
                          className="image-card-image"
                          src={image.data_url}
                          alt=""
                          width="100%"
                        />
                        <span
                          className="remove-image"
                          onClick={() => onImageRemove(index)}
                        >
                          <FontAwesomeIcon
                            icon={faTimesCircle}
                            style={{ fontSize: "12px" }}
                          />
                        </span>
                      </div>
                    </div>
                  ))}
                  {this.state.images.length < maxNumber && (
                    <div
                      className="col-6 image-card add-image-card"
                      style={isDragging ? { color: "red" } : null}
                      onClick={onImageUpload}
                      {...dragProps}
                    >
                      <div
                        className="box button-box d-flex justify-content-center align-items-center"
                        style={
                          isDragging ? { border: "1px solid black" } : null
                        }
                      >
                        <FontAwesomeIcon className="" icon={faPlus} />
                      </div>
                      &nbsp;
                    </div>
                  )}
                </div>
              </div>
            )}
          </ImageUploading>
          <div className="form-group" align="right">
            <input
              type="button"
              value="Save"
              className="container d-flex justify-content-center btn login-btn"
              onClick={this.handleSubmit}
            />
          </div>
        </ReactModal>
      </>
    );
  }
}

export default CreatePost;
