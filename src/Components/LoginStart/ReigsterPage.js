import axios from "axios";
import { useState } from "react";
import "../../css/index.css";

function RegisterPage() {
  const [newLogin, setNewLogin] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [RepeatPassword, setRepeatPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const Registration = (e) => {
    e.preventDefault();
    setError(null);
    let freshError = null;

    if (newLogin.trim().length === 0) freshError = "nie moze być puste";
    if (freshError) {
      setError(freshError);
      return;
    }
    setLoading(true);
    axios
      .post("https://localhost:7168/api/Ni3/accounts/New", {
        login: newLogin,
        password: newPassword,
      })
      .then((res) => console.log("utworzono uzytkownika"))
      .catch((err) => {
        setError("Login lub haslo jest nieprawidlowe");
        setLoading(false);
      });
    setLoading(false);
  };

  return (
    <div className="TemplateBox">
      <div className="InputsText">
        <form onSubmit={Registration}>
          <input
            type="text"
            id="newLogin"
            name="newLogin"
            placeholder="New Login"
            onChange={(e) => setNewLogin(e.target.value)}
          />
          <input
            type="text"
            id="newPassword"
            name="newPassword"
            placeholder="Password"
            onChange={(e) => setNewPassword(e.target.value)}
          />
          <input
            type="text"
            id="RepeatPassword"
            name="RepeatPassword"
            placeholder="Repeat password"
            onChange={(e) => setRepeatPassword(e.target.value)}
          />
          <input type="submit" className="fadeIn fourth" value="SIGN UP" />
        </form>
      </div>
    </div>
  );
}

export default RegisterPage;
