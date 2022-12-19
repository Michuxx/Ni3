import "../../css/index.css";
import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function LoginPage() {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  const LoginFunction = (e) => {
    e.preventDefault();
    setError(null);

    let freshError = null;
    if (login.trim().length === 0) freshError = "nie moze być puste";
    if (freshError) {
      setError(freshError);
      return;
    }
    setLoading(true);
    axios
      .post("https://localhost:7168/api/Ni3/accounts/Check", {
        login: login,
        password: password,
      })
      .then(async (res) => {
        await localStorage.setItem("user", JSON.stringify(res.data));
        navigate("/MainPage");
      })
      .catch((err) => {
        setError("Login lub haslo jest nieprawidlowe");
        setLoading(false);
      });
    setLoading(false);
  };

  return (
    <div className="TemplateBox">
      <div className="InputsText">
        <form onSubmit={LoginFunction}>
          <input
            type="text"
            id="login"
            className="fadeIn second"
            name="login"
            placeholder="Login"
            onChange={(e) => setLogin(e.target.value)}
          />
          <input
            type="text"
            id="password"
            className="fadeIn third"
            name="password"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
          />
          <input type="submit" className="fadeIn fourth" value="Log In" />
        </form>
        {error && <p>Login lub haslo jest nieprawidlowe</p>}
      </div>
    </div>
  );
}

export default LoginPage;
