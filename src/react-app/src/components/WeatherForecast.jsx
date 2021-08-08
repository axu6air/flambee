import React from "react";
import axios from "axios";
import Moment from "react-moment";
import { useAuthContext } from "./auth/AuthContext";

const WeatherForecast = () => {
  const [weather, setWeather] = React.useState([]);
  const { currentUser } = useAuthContext();
  console.log(currentUser);
  React.useEffect(() => {
    const getWeather = async () => {
      await axios
        .get("/WeatherForecast", {
          headers: { Authorization: `Bearer ${currentUser.token}` },
        })
        .then((response) => {
          setWeather(response.data);
        })
        .catch((error) => {
          console.log(error);
          setWeather([]);
        });
    };
    if (currentUser && currentUser.token) getWeather();
  }, [currentUser]);

  return (
    weather.length > 0 && (
      <section>
        <div className="container weather-forecast-section">
          <h4>Weather forecast</h4>
          <ul style={{ listStyle: "none" }}>
            {weather.map((item, idx) => {
              console.log(item);
              let date = new Date(item.date);
              console.log("date: ", date);
              return (
                <li key={idx}>
                  <p className="weather-forecast-item d-flex justify-content-center align-items-center">
                    {/* {date.toString()} */}
                    <Moment format="MMMM do">{date}</Moment>
                    {" - " + item.summary + " " + item.temperatureC + "°C"}
                  </p>
                </li>
              );
            })}
          </ul>
        </div>
      </section>
    )
  );
};

export default WeatherForecast;
