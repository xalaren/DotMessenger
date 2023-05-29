import React, {useState} from "react";
import {Account} from '../models';

export function RegisterForm() {
    return(
        <div className="mainform" id="registerform">
        <div className="mainform__leftblock">
            <h1 className="leftblock__title">Регистрация</h1>
            <div className="leftblock__textboxes">
                <input type="text" id="nickname-input-register" placeholder="Никнейм" required/>
                <input type="password" id="password-input-register" placeholder="Пароль" required/>
                <input type="text" id="email-input-register" placeholder="Email" required/>
                <input type="text" id="name-input-register" placeholder="Имя" required/>
                <input type="text" id="lastname-input-register" placeholder="Фамилия" required/>
                <input type="text" id="phone-input-register" placeholder="+7 (999) 999-99-99"/>
            </div>
            <div className="leftblock__buttons">
                <button>Регистрация</button>
            </div>
        </div>
        <div className="mainform__rightblock">
            <h2 className="welcome__title">Добро пожаловать</h2>
            <h1 className="name__title">.Dot Messenger</h1>
            <br/>
        </div>
    </div>
    )
}