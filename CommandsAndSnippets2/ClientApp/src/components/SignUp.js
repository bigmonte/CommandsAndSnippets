import React, {useState} from 'react';
import './SignUp.css';

const SignUp = () => {
  const [formState, setFormState] = useState({
    firstName: '',
    lastName: '',
    email: '',
    username: '',
    password: '',
    confirmPassword: ''
  });

  const handleInputChange = event => {
    const target = event.target;
    const name = target.name;
    setFormState({
      ...formState,
      [name]: target.value
    });
  };

  const handleSubmit = async event => {
    event.preventDefault();
    const email = formState.email;
    const password = formState.password;
    console.log(`INPUT SUBMIT -  email: ${email}; password: ${password}`);
    await signUpRequest();
  };

  const signUpRequest = async () => {
    await fetch('/auth/signup', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(formState),
    }).then(r => r.json())
      .then(result => {
        if (result === "Success") {
          setFormState({
            firstName: '',
            lastName: '',
            email: '',
            username: '',
            password: '',
            confirmPassword: ''
          });
        }
      })
  };

  return (
    <>
      <div className="sign-up-section padding">
        <h5>Create an account</h5>
      </div>
      <form onSubmit={handleSubmit}>
        <div className="container padding">
          <div className="grid">
            <div className="s6">
              <div className="field label border label medium-width">
                <input type="text" name="firstName" value={formState.firstName}
                       onChange={handleInputChange}/>
                <label className="active">First Name</label>
              </div>
            </div>
            <div className="s6">
              <div className="field label border medium-width">
                <input name="lastName" value={formState.lastName}
                       onChange={handleInputChange}/>
                <label className="">Last Name</label>
              </div>
            </div>
            <div className="s6">
              <div className="field label border">
                <input type="text" name="username" value={formState.username}
                       onChange={handleInputChange}/>
                <label className="active">Username</label>
              </div>
            </div>

            <div className="s6">
              <div className="field label border">
                <input type="email" name="email" value={formState.email}
                       onChange={handleInputChange}/>
                <label className="active">Your Email</label>
              </div>
            </div>

            <div className="s6">
              <div className="field label border medium-width">
                <input type="password" name="password" value={formState.password}
                       onChange={handleInputChange}/>
                <label className="active">Password</label>
              </div>
            </div>
            <div className="s6">
              <div className="field label border medium-width">
                <input type="password" name="confirmPassword" value={formState.confirmPassword}
                       onChange={handleInputChange}/>
                <label className="">Confirm Password</label>
              </div>
            </div>
          </div>
          <div className="row">
            <button type="submit">Sign-Up</button>
            <button>Reset</button>
          </div>
        </div>
      </form>
    </>
  );

}
export default SignUp