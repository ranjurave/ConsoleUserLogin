import axios from 'axios';
import React, { useEffect } from 'react';
import "./Login.css";
import {urlUsers} from '../endpoints';

export default function Login() {
    useEffect(() =>{
        axios.get(urlUsers)
        .then((response) => {
            console.log(response.data);
        })
    }, [])

    return (
        <div>
            <form method="post">
                <label>User Email</label>
                <input name='useremail'></input>
                <label>Password</label>
                <input type="password" name='password'></input>
                <button type = "submit">Login</button>

            </form>
        </div>
    )
}
