import "../../css/MainPage.css";
import React, { useEffect } from "react";
import Logo from "../../images/university.png";
import Idea from "../../images/idea.png";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function MainPage() {
  const [user, setUser] = useState(JSON.parse(localStorage.getItem("user")));
  const navigate = useNavigate();

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
  return (
    <div className="MainContainer">
      <div className="NavBar">
        <img src={Logo} alt="Logo" height={"95%"} />
        <p>Ni3 - I nie ma trójki</p>
        {user && user.login}
        {user ? (
          <button onClick={Wyloguj}>WYLOGUJ SIE</button>
        ) : (
          <button onClick={Zaloguj}>ZALOGUJ SIE</button>
        )}
      </div>
      <main>
        <div className="Content">
          <div className="wrapper2">
            <div className="CourseLabel">
              <img src={Idea} alt="icon" height={"96px"} />
              <h1>Lista Kursów</h1>
            </div>
            <div className="Buttons">
              <button type="button" className="Add">
                +
              </button>
            </div>
          </div>
          <div className="Courses"></div>
        </div>
      </main>
      <footer>© 2022/2023 Ni3 | wszelkie prawa zastrzeżone :3</footer>
    </div>
  );
}

export default MainPage;
