import React from "react";
import "../../assets/css/loader.css";
import "../../assets/css/spinner.css";

const Loader = () => (
  <div className="overlay">
    <div className="overlay__inner">
      <div className="overlay__content">
        <div className="lds-ripple">
          <div></div>
          <div></div>
        </div>
      </div>
    </div>
  </div>
);

export default Loader;
