import "../../css/index.css";

function LoginPage() {
    return (
      
        <div className="TemplateBox">
            <div className="InputsText">
                <form>
                    <input
                        type="text"
                        id="login"
                        className="fadeIn second"
                        name="login"
                        placeholder="Login"
                    />
                    <input
                        type="text"
                        id="password"
                        className="fadeIn third"
                        name="login"
                        placeholder="Password"
                    />
                    <input type="submit" className="fadeIn fourth" value="Log In" />
                </form>
        </div>
      </div>
    );
  }
  
  export default LoginPage;
  