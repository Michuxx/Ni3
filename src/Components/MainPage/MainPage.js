import "../../css/MainPage.css";
import React, { useEffect } from "react";
import Logo from "../../images/university.png";
import Idea from "../../images/idea.png";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

function MainPage() {
  const [user, setUser] = useState(JSON.parse(localStorage.getItem("user")));
  const [courses, setCourses] = useState([]);
  const navigate = useNavigate();
  useEffect(() => {
    axios.get("https://localhost:7168/api/Ni3/courses/All").then((Response) => {
      const courseList = Response.data.map((course) => ({
        courseName: course.courseName,
        description: course.description,
      }));
      setCourses(courseList);
    });
  }, []);

  const Wyloguj = () => {
    localStorage.removeItem("user");
    navigate("/");
  };

  const Zaloguj = () => {
    navigate("/");
  };

  useEffect(() => {
    if (user == null) {
      navigate("/");
    }
  }, [user]);

  const GetAllCourses = () => {};

  return (
    <div className="MainContainer">
      <div className="NavBar">
        <img src={Logo} alt="Logo" height={"95%"} />
        <p>Ni3 - I nie ma trójki</p>
        <div id="login_string">
          {user && user.login}
          <div>
            {user ? (
              <button onClick={Wyloguj} id="guzik_wyloguj">
                WYLOGUJ SIE
              </button>
            ) : (
              <button onClick={Zaloguj}>ZALOGUJ SIE</button>
            )}
          </div>
        </div>
      </div>
      <main>
        <div className="Content">
          <div className="wrapper2">
            <div className="CourseLabel">
              <img src={Idea} alt="icon" height={"96px"} />
              <h1>Lista Kursów</h1>
            </div>
          </div>
          <div className="Courses">
            <ol>
              {courses.map((course) => (
                <li key={course.courseName}>
                  <span>{course.courseName}</span>
                  <span>{course.description}</span>
                </li>
              ))}
            </ol>
          </div>
        </div>
      </main>
      <footer>© 2022/2023 Ni3 | wszelkie prawa zastrzeżone :3</footer>
    </div>
  );
}

export default MainPage;
