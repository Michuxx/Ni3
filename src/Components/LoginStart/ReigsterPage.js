import "../../css/index.css";

function RegisterPage() {
    return (
      <div className="TemplateBox">
        <div className="InputsText">
            <form>
            <input
                type="text"
                id="login"     
                name="login"
                placeholder="New Login"
            />
            <input
                type="text"
                id="password"              
                name="login"
                placeholder="Password"
            />
            <input
                type="text"
                id="RepeatPassword"
                name="login"
                placeholder="Repeat password"
            />
            <input type="submit" className="fadeIn fourth" value="SIGN UP" />
            </form>
        </div>
      </div>
    );
  }
  
  export default RegisterPage;
  