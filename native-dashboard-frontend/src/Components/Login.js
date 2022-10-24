import axios from 'axios';
import React, { useEffect, useState } from 'react';
import "./Login.css";
import {urlUsers} from '../endpoints';

export default function Login() {

    const initialValues = {userEmail:"", password:""};
    const [formValues, setFormValues] = useState(initialValues);
    useEffect(() =>{
        axios.get("https://localhost:7001/api/Users")
        .then((response) => {
            console.log(response.data);
        })
    }, [])

    const handleChange = (e) => {
        const {name, value} = e.target;
        setFormValues({...formValues, [name]:value});
        //console.log(formValues);
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        const {name, value} = e.target;
        setFormValues({...formValues, [name]:value});
        console.log(formValues.userEmail);
        console.log(formValues.password);
        axios.post('https://localhost:7001/Auth/login', formValues)
          .then((response) => {
            console.log(response);
          })
          .catch((error) => {
            console.log(error);
            console.log('some errorrrrr....');
          });
    };
    
    return (
        <div>
            <h1>User Login</h1>
            <form method="post" onSubmit = {handleSubmit}>
                <div>
                    <label>User Email</label>
                    <input type = "text" name='userEmail' placeholder='email' value={formValues.useremail}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Password</label>
                    <input type="password" name='password' placeholder='password' value={formValues.password}
                        onChange={handleChange}
                    />
                </div>
                <button type = "submit">Login</button>
            </form>
        </div>
    )
}
