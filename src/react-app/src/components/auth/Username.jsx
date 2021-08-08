import React, { useState, useRef } from "react";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle, faTimes } from "@fortawesome/free-solid-svg-icons";
import loaderGIF from "../../assets/img/loader-sm.gif";

const Username = ({ handleUsernameChange, handleUsernameValidity }) => {
  let cancelSource;
  const [result, setResult] = useState(false);
  const [loader, setLoader] = useState(false);
  const username = useRef("");

  const generateLogo = () => {
    const validityStyle = {
      maxWidth: "16px",
      maxHeight: "16px",
    }; //useMemo

    if (loader) {
      return <img src={loaderGIF} style={validityStyle} alt="loading..." />;
    } else if (result) {
      return <FontAwesomeIcon icon={faCheckCircle} />;
    } else {
      return <FontAwesomeIcon icon={faTimes} />;
    }
  };

  const handleChange = () => {
    handleUsernameChange(username.current.value);
    validateUsername();
  };

  const validateUsername = async () => {
    const searchTerm = username.current.value;
    setResult(false);

    if (searchTerm.length < 4) return;

    if (cancelSource) cancelSource.cancel();

    const cancelToken = axios.CancelToken;
    cancelSource = cancelToken.source();
    try {
      setLoader(true);
      setResult(false);
      const response = await axios.get("/CheckUsernameAvailability", {
        params: { username: searchTerm },
        cancelToken: cancelSource.token,
      });
      setResult(response.data.available);
      handleUsernameValidity(response.data);
    } catch (thrown) {
      if (axios.isCancel(thrown)) {
        console.log("Request canceled", thrown.message);
      } else {
        console.log("cancel error");
      }
    } finally {
      setLoader(false);
    }
  };

  return (
    <>
      <input
        ref={username}
        type="text"
        name="username"
        className="form-control"
        placeholder="username (minimum 4 character)"
        autoComplete="usernameAuto"
        onChange={handleChange}
      />
      <span className="validation-icon">{generateLogo()}</span>
    </>
  );
};

export default Username;
