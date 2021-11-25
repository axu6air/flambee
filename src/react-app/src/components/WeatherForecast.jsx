import React from "react";
import axios from "axios";
import Moment from "react-moment";
import { useAuthContext } from "./auth/AuthContext";

const WeatherForecast = () => {
  const [weather, setWeather] = React.useState([]);
  //const [openWeatherData, setOpenWeatherData] = React.useState({});
  const [location, setLocation] = React.useState("");
  const { currentUser } = useAuthContext();
  React.useEffect(() => {
    const getWeather = async () => {
      await axios
        .get("/WeatherForecast", {
          headers: { Authorization: `Bearer ${currentUser.token}` },
        })
        .then((response) => {
          setWeather(response);
        })
        .catch((error) => {
          setWeather([]);
        });
    };
    if (currentUser && currentUser.token) getWeather();
  }, [currentUser]);

  const getWeather = (lat, lon) => {
    fetch(
      `https://api.openweathermap.org/data/2.5/forecast?lat=${lat}&lon=${lon}&APPID=4686a3eb96c9719c7ca18d76f1bf24ae`
    )
      .then((response) => response.json())
      .then((data) => processOpenWeatherData(data));
  };

  const lowestTemperature = 999,
    maximumTemperature = 0;
  const processOpenWeatherData = (data) => {
    if (data && data.list.length > 0) {
      // const weatherList = data.list;
      // weatherList.forEach((weather) => {
      //   let date = new Date(weather.dt * 1000).toLocaleString();
      //   console.log(weather);
      //   console.log(date);
      // });
    }
  };

  const getLocation = () => {
    // event.preventDefault();
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(showPosition);
    } else {
      setLocation("Geolocation is not supported by this browser.");
    }
  };

  const showPosition = (position) => {
    setLocation(
      "Latitude: " +
        position.coords.latitude +
        " Longitude: " +
        position.coords.longitude
    );
    getWeather(position.coords.latitude, position.coords.longitude);
  };

  return (
    weather.length > 0 && (
      <section>
        <div className="container weather-forecast-section">
          <br />
          <p className="weather-forecast-item d-flex justify-content-center align-items-center">
            {location}
          </p>
          <br />
          <h4>Weather forecast</h4>
          <ul style={{ listStyle: "none" }}>
            {weather.map((item, idx) => {
              let date = new Date(item.date);
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
            <button onClick={getLocation()} hidden>
              Click
            </button>
          </ul>
        </div>
      </section>
    )
  );
};

export default WeatherForecast;
