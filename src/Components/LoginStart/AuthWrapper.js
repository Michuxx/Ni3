import "../../css/index.css";
import {useState} from 'react';
import LoginPage from "./LoginPage";
import RegisterPage from "./ReigsterPage";

function AuthWrapper() {
    const [toggle, setToggle] = useState(true);

    return (
      <div className="AuthWrapper">
        <div className="TemplateBox">
        <h2
              className={toggle ? "active" : "inactive underlineHover"}
              onClick={() => setToggle(true)}
            >
              {" "}
              Sign In
            </h2>
            <h2
              className={!toggle ? "active" : "inactive underlineHover"}
              onClick={() => setToggle(false)}
            >
              Sign Up{" "}
            </h2>

            {toggle ? <LoginPage /> : <RegisterPage />}
            
        </div>
      </div>
    );
  }
  
  export default AuthWrapper;