import React from "react";
import WeatherForecast from "./WeatherForecast";
import "../assets/css/home.css";
import { useAuthContext } from "./auth/AuthContext";

const Dashboard = () => {
  const { currentUser } = useAuthContext();
  console.log(currentUser);

  return (
    <section className="jumbotron text-center">
      <div className="home-main">
        <div className="container">
          <div className="vert-horz-center">
            <div className="d-flex justify-content-center align-items-center">
              <h1>Hi, {currentUser && currentUser.firstName}</h1>
            </div>
            {/* <div className="d-flex justify-content-center align-items-center">
              <p className="free-text">
                "Code is like humor. When you have to explain it, it’s bad."
              </p>
            </div> */}
            {currentUser && currentUser.token && <WeatherForecast />}
          </div>
        </div>
      </div>
    </section>
  );
};

export default Dashboard;
