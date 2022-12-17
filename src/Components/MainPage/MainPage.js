import "../../css/MainPage.css";
import React from "react";
import Logo from "../../images/university.png";
import Idea from "../../images/idea.png";

function MainPage() {
  return (
    <div className="MainContainer">
      <div className="NavBar">
        <img src={Logo} alt="Logo" height={"95%"} />
        <p>Ni3 - I nie ma trójki</p>
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
