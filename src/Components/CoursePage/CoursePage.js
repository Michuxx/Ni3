import "../../css/CommentPage.css";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useParams, useLocation, useNavigate } from "react-router-dom";
import { Navigate } from "react-router-dom";
import { useRef } from "react";

const CoursePage = () => {
  const [course, setCourse] = useState(null);
  const [comments, setComments] = useState([]);
  const { Name } = useParams();
  const { courseName, description } = useLocation().state;
  const [trigger, setTrigger] = useState(false);
  const navigate = useNavigate();

  const textareaRef = useRef(null);

  const SendComment = (e) => {
    e.preventDefault();
    const text = textareaRef.current.value;
    if (text.trim().lenght == 0) {
      return;
    }

    axios
      .post(`https://localhost:7168/api/Ni3/comments/${Name}`, {
        text,
        userLogin: JSON.parse(localStorage.getItem("user")).login,
      })
      .then(() => {
        console.log("dodano komentarz");
        setTrigger(!trigger);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem("user"));
    if (user == null) {
      navigate("/");
    }
  }, []);

  const GoBack = () => {
    navigate("/MainPage");
  };

  useEffect(() => {
    axios
      .get(`https://localhost:7168/api/Ni3/comments/${Name}`)
      .then((res) => {
        setComments(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }, [trigger]);
  return (
    <div className="whole-body">
      <div className="NavBar">
        <button onClick={GoBack} className="guzik_wroc">
          Wróć
        </button>
        Obecny Kurs: {courseName}
      </div>
      <div className="Main-section">
        <div className="CommentSection">
          {comments.map((com, i) => {
            return (
              <div className="comment">
                <div key={"comment" + i}>
                  <span id="login">
                    <p>{com.userLogin}</p>
                  </span>

                  <span id="text">
                    <h3>{com.text}</h3>
                  </span>
                </div>
              </div>
            );
          })}
        </div>
        <div className="add-comment-box">
          <form onSubmit={SendComment}>
            <textarea ref={textareaRef}></textarea>
            <button id="guzik">Wyślij</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default CoursePage;
