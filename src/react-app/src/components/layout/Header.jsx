import React from "react";
import { DropdownButton } from "react-bootstrap";
import { VscSettingsGear } from "react-icons/vsc";
import { useAuthContext } from "../auth/AuthContext";
import "../../assets/css/header.css";
import { Link } from "react-router-dom";

const Header = (params) => {
  const { currentUser } = useAuthContext();

  return (
    <header>
      <div className="container-fluid full-header">
        <div className="row">
          <div className="brand-header d-flex justify-content-center">
            <a
              className="brand-logo"
              href={currentUser && currentUser.username ? "/Dashboard" : "/"}
            >
              Refactored Enigma
            </a>
          </div>
          <div className="header-items d-flex justify-content-end">
            <DropdownButton
              id="dropdown-item-button"
              title={<VscSettingsGear />}
              variant="secondary"
              size="sm"
            >
              {!(currentUser && currentUser.username) && (
                <div className="dropdown-item">
                  <Link className="fill" to="/Login">
                    Login
                  </Link>
                </div>
              )}
              {!(currentUser && currentUser.username) && (
                <div className="dropdown-item container d-flex justify-content-center">
                  <Link className="fill" to="/Signup">
                    Signup
                  </Link>
                </div>
              )}
              {currentUser && currentUser.username && (
                <div className="dropdown-item">
                  <Link className="fill" to="/ProfileUpdate">
                    Update Profile
                  </Link>
                </div>
              )}
              {currentUser && currentUser.username && (
                <div className="dropdown-item">
                  <Link className="fill" to="/Logout">
                    Logout
                  </Link>
                </div>
              )}
            </DropdownButton>
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
