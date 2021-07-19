import React from "react";
import { DropdownButton, Dropdown } from "react-bootstrap";
import "../../assets/css/header.css";
import { VscSettingsGear } from "react-icons/vsc";
import { useAuthContext } from "../auth/AuthContext";

const Header = (params) => {
  const { currentUser } = useAuthContext();

  return (
    <header>
      <nav className="navbar navbar-dark bg-dark clearfix" role="navigation">
        <div className="navbar-header">
          <a className="navbar-brand brand-logo" href="/">
            Refactored Enigma
          </a>

          <div className="header-items">
            <DropdownButton
              id="dropdown-item-button"
              title={<VscSettingsGear />}
              variant="secondary"
              size="sm"
            >
              {!(currentUser && currentUser.username) && (
                <Dropdown.Item href="/Login">Login</Dropdown.Item>
              )}
              {!(currentUser && currentUser.username) && (
                <Dropdown.Item href="/Signup">Signup</Dropdown.Item>
              )}
              {currentUser && currentUser.username && (
                <Dropdown.Item href="/Logout">Logout</Dropdown.Item>
              )}
            </DropdownButton>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Header;
