import React, {useState} from 'react';
import {LoginForm} from './components/LoginFormComponent'
import {RegisterForm} from "./components/RegisterFormComponent";

function App() {
  const[displayRegister, setDisplayRegister] = useState(false)
  const[displayLogin, setDisplayLogin] = useState(true)
  const toggleForm = () => {
    setDisplayRegister(!displayRegister);
    setDisplayLogin(!displayLogin);
  }

  return(
      <div>
        {displayLogin && <LoginForm />}
        {displayRegister && <RegisterForm/>}
      </div>
  )
}

export default App;
