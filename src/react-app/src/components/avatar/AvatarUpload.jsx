import React from "react";
import Avatar from "react-avatar-edit";
import avatarPic from "../../assets/img/avatar-sample.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import ImageService from "../../service/ImageService";

import {
  faArrowCircleUp,
  faTimesCircle,
} from "@fortawesome/free-solid-svg-icons";
import "../../assets/css/avatar-upload.css";
import ReactModal from "react-modal";
import { toast } from "react-toastify";

class AvatarUpload extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedAvatar: null,
      selectedAvatarBase64: "",
      preview: null,
      showModal: false,
      title: "",
      loaded: 0,
    };
  }

  componentDidMount() {
    ReactModal.setAppElement("#modal-man");
  }

  handleProps = () => {
    if (this.props.avatar) {
      this.setState({
        selectedAvatar: this.props.avatar.image,
        preview: this.props.avatar.preview,
        title: this.props.avatar.title,
      });
    }
  };

  onClose = () => {
    this.setState({ preview: null });
  };

  onCrop = (preview) => {
    this.setState({ preview });
  };

  onBeforeFileLoad = (event) => {
    const self = this;
    if (event.target.files[0].size > 71680) {
      alert("File is too big!");
      event.target.value = "";
    } else {
      ImageService.getBase64FromFile(event.target.files[0], function (result) {
        self.setState({
          selectedAvatar: event.target.files[0],
          selectedAvatarBase64: result,
        });
      });
    }
  };

  handleModal = (isCancelled) => {
    this.setState((prevState) => ({
      showModal: !prevState.showModal,
    }));
  };

  handleInputChange = (event) => {
    const value = event.target.value;
    const name = event.target.name;

    this.setState((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  onChangeHandler = (event) => {
    this.setState({
      selectedAvatar: event.target.files[0],
      loaded: 0,
    });
    console.log(event.target.files.length);
    if (event.target.files.length > 0) this.handleModal();
  };

  importImage = () => {
    this.inputElement.click();
  };

  handleAvatarSubmit = () => {
    const state = this.state;
    if (state.selectedAvatar) {
      this.setState({ showModal: false });

      this.props.onAvatarSelect({
        avatar: state.selectedAvatar,
        avatarBase64: state.selectedAvatarBase64,
        previewBase64: state.preview,
        title: state.title,
      });
    } else {
      toast.error("Please select your avatar");
    }
    console.log(state);
  };

  render() {
    const customStyles = {
      content: {
        top: "50%",
        left: "50%",
        right: "auto",
        bottom: "auto",
        marginRight: "-50%",
        transform: "translate(-50%, -50%)",
        background: "grey",
      },
    };

    return (
      <>
        <div className="avatar-wrapper">
          <img
            className="profile-pic"
            src={this.state.preview ? this.state.preview : avatarPic}
            alt=""
          />
          <div
            className="upload-button"
            onClick={() => this.handleModal(false)}
          >
            <FontAwesomeIcon icon={faArrowCircleUp} aria-hidden="true" />
          </div>
          {/* <input
            ref={(input) => (this.inputElement = input)}
            className="file-upload"
            type="file"
            accept="image/*"
            onChange={this.onChangeHandler}
          /> */}
        </div>
        <div id="modal-man"></div>

        <ReactModal
          isOpen={this.state.showModal}
          contentLabel="Minimal Modal Example"
          style={customStyles}
        >
          <span
            style={{
              float: "right",
              cursor: "pointer",
            }}
            onClick={() => this.handleModal(true)}
          >
            <FontAwesomeIcon
              icon={faTimesCircle}
              style={{ fontSize: "20px" }}
            />
          </span>
          <div className="card-container">
            <div>
              <div className="card card-form">
                <div className="container d-flex justify-content-center align-items-center">
                  <Avatar
                    src={this.state.selectedAvatarBase64}
                    width={200}
                    height={200}
                    onCrop={this.onCrop}
                    onClose={this.onClose}
                    onBeforeFileLoad={this.onBeforeFileLoad}
                    cropColor="black"
                    label="  pick your avatar"
                    labelStyle={{
                      fontSize: "20px",
                      cursor: "pointer",
                      fontFamily: "Numans",
                      lineHeight: "200px !important",
                      textAlign: "center",
                    }}
                    borderStyle={{
                      padding: "5px",
                      border: "5px solid",
                    }}
                    closeIconColor="yellow"
                  />
                </div>
                <div className="card-body">
                  <div className="input-group form-group">
                    <textarea
                      name="title"
                      className="form-control elevator"
                      onChange={this.handleInputChange}
                    ></textarea>
                  </div>
                  <div className="form-group" align="right">
                    <input
                      type="button"
                      value="Save"
                      className="container d-flex justify-content-center btn login-btn"
                      onClick={this.handleAvatarSubmit}
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </ReactModal>
      </>
    );
  }
}

export default AvatarUpload;
