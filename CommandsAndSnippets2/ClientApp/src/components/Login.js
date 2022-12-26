import {useEffect, useState} from 'react';
import { useNavigate } from 'react-router-dom';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();

  const handleInputChange = (event) => {
    const {name, value} = event.target;
    if (name === "email") {
      setEmail(value);
    } else {
      setPassword(value);
    }
  }

  const loginRequest = async () => {
    await fetch('/auth/signin', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email,
        password
      }),
    }).then(r => {
      if(!r.ok) {
        return false;
      }
      return r.json()
    })
      .then(data => {
        if (data.success) {
          window.ui('.toast.green');
          setEmail('')
          setPassword('');
          setTimeout(() => {
            setIsLoggedIn(true);
            const redirectUrl = localStorage.getItem('redirectUrl');
            navigate(redirectUrl);
          }, 2000)
        } else {
          window.ui('.toast.red8');
        }
      })
  }

  const handleSubmit = (event) => {
    event.preventDefault();
    loginRequest();
  }


  return (
    <>
      <div className="title-section padding">
        <h5>Login</h5>
      </div>
      <div className="container padding">
        <form onSubmit={handleSubmit}>
          <div className="grid">
            <div className="s12">
              <div className="field label border medium-width">
                <input type="email" name="email" value={email}
                       onChange={handleInputChange}/>
                <label className="active">Your Email</label>
              </div>
            </div>
            <div className="s12">
              <div className="field label border medium-width">
                <input type="password" name="password" value={password}
                       onChange={handleInputChange}/>
                <label className="active">Password</label>
              </div>
            </div>
          </div>
          <div className="row">
            <button type="submit">Login</button>
          </div>
        </form>
      </div>
      <div className="toast green white-text">
        <i>done</i><span>Login successfull, you'll be redirected to the home page.</span>
      </div>
      <div className="toast red8 white-text"><i>error</i><span>The username or password you entered is incorrect, please try again.</span></div>
    </>
  );
}

export {Login as default};