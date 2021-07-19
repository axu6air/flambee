import React from "react";
import { useAuthContext } from "./auth/AuthContext";
import "../assets/css/home.css";

const Home = () => {
  const { currentUser } = useAuthContext();
  console.log(currentUser);

  return (
    <section className="jumbotron text-center">
      <div className="home-main">
        <div className="container">
          <div className="vert-horz-center">
            <div className="d-flex justify-content-center align-items-center">
              <h1>Landing page</h1>
            </div>
            <div className="d-flex justify-content-center align-items-center">
              <p className="free-text">
                "Code is like humor. When you have to explain it, it’s bad."
              </p>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Home;
