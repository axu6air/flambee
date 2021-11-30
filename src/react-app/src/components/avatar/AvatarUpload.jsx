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
// import { AuthContext } from "../auth/AuthContext";

class AvatarUpload extends React.Component {
  state = {
    avatarId: 0,
    avatarImage: null,
    avatarBase64: "",
    previewBase64: null,
    newAvatar: null,
    showModal: false,
    title: "",
    loaded: 0,
  };

  // static contextType = AuthContext;

  componentDidUpdate() {
    if (
      this.props.triggerRequired &&
      this.props.triggerUpload &&
      this.props.userId
    ) {
      this.uploadAvatar();
    }
  }

  async componentDidMount() {
    ReactModal.setAppElement("#modal-man");

    if (this.props.userId) {
      await ImageService.getAvatar().then((response) => {
        const avatar = response;

        if (avatar && avatar.id) {
          this.setState({
            avatarId: avatar.id,
            avatarBase64: avatar.avatarBase64,
            previewBase64: avatar.previewBase64,
            title: avatar.title,
          });
        }
      });
    }
  }

  onClose = () => {
    this.setState({
      previewBase64: null,
      title: null,
      avatarImage: null,
      avatarBase64: null,
    });
  };

  onCrop = (previewBase64) => {
    this.setState({ previewBase64 });
  };

  onBeforeFileLoad = (event) => {
    const self = this;
    if (event.target.files[0].size > 71680) {
      alert("File is too big!");
      event.target.value = "";
    } else {
      ImageService.getBase64FromFile(event.target.files[0], function (result) {
        self.setState({
          avatarImage: event.target.files[0],
          avatarBase64: result,
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

  uploadAvatar = () => {
    const state = this.state;

    const avatar = {
      avatarImage: state.avatarImage,
      avatarBase64: state.avatarBase64,
      previewBase64: state.previewBase64,
      title: state.title,
    };

    if (this.props.userId && avatar.previewBase64) {
      ImageService.uploadAvatar(avatar, this.props.userId);
    }
  };

  handleAvatarSubmit = () => {
    const state = this.state;
    this.setState({ showModal: false });

    if (!this.props.triggerRequired && state.avatarImage) this.uploadAvatar();
    else {
    }

    if (state.avatarImage) {
      this.setState({ showModal: false });

      // this.props.onAvatarSelect({
      //   avatarImage: state.avatarImage,
      //   avatarBase64: state.avatarBase64,
      //   previewBase64: state.previewBase64,
      //   title: state.title,
      // });
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
            src={
              this.state.previewBase64 ? this.state.previewBase64 : avatarPic
            }
            alt=""
          />
          <div
            className="upload-button"
            onClick={() => this.handleModal(false)}
          >
            <FontAwesomeIcon icon={faArrowCircleUp} aria-hidden="true" />
          </div>
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
                    src={this.state.newAvatar}
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
                      value={this.state.title}
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
