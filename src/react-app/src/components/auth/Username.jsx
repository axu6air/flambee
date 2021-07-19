import React, { useState, useEffect } from "react";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCheckCircle, faTimes } from "@fortawesome/free-solid-svg-icons";
import loaderGIF from "../../assets/img/loader-sm.gif";
import { useRef } from "react";
import Test from "../TestComponent";

const Username = ({ handleUsernameChange, hanndleUsernameValidity }) => {
  const url = "/CheckUsernameAvailability";
  const [data, setData] = useState(false);
  const [username, setUsername] = useState("");
  const [loader, setLoader] = useState(false);
  const usernameRef = useRef("");

  const validate = () => {
    const validityStyle = {
      maxWidth: "16px",
      maxHeight: "16px",
    };

    if (loader) {
      return <img src={loaderGIF} style={validityStyle} alt="loading..." />;
    } else if (data) {
      return <FontAwesomeIcon icon={faCheckCircle} />;
    } else {
      return <FontAwesomeIcon icon={faTimes} />;
    }
  };

  useEffect(() => {
    let source = axios.CancelToken.source();
    setData(false);

    const loadData = async () => {
      setLoader(true);
      try {
        const response = await axios.get(url, {
          params: { username: username },
          cancelToken: source.token,
        });
        console.log("response: ", response.data);
        console.log("AxiosCancel: got response");
        setData(response.data);
      } catch (error) {
        if (axios.isCancel(error)) {
          console.log("AxiosCancel: caught cancel");
        } else {
          throw error;
        }
      } finally {
        setLoader(false);
      }
    };

    if (username.length > 4) loadData();

    return () => {
      console.log("AxiosCancel: unmounting");
      source.cancel();
    };
  }, [url, username]);

  const handleChange = () => {
    setUsername(usernameRef.current.value);
    handleUsernameChange(usernameRef.current.value);
    hanndleUsernameValidity(data);
  };

  return (
    <>
      <input
        ref={usernameRef}
        type="text"
        name="username"
        className="form-control"
        placeholder="username (minimum 4 character)"
        autoComplete="nope"
        onChange={handleChange}
      />
      <span className="validation-icon">{validate()}</span>
      <Test name={username}></Test>
    </>
  );
};

export default Username;
