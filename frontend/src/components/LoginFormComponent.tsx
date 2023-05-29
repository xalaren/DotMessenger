import React from "react";

export function LoginForm() {
    return (
        <div className="mainform">
            <div className="mainform__leftblock" id="loginform">
                <h1 className="leftblock__title">Вход</h1>
                <div className="leftblock__textboxes">
                    <input type="text" id="nickname-input-login" placeholder="Никнейм" required/>
                        <input type="password" id="password-input-login" placeholder="Пароль" required/>
                </div>
                <div className="leftblock__buttons">
                    <button id="login-button">Войти</button>
                </div>
            </div>
            <div className="mainform__rightblock">
                <h2 className="welcome__title">Добро пожаловать</h2>
                <h1 className="name__title">.Dot Messenger</h1>
                <button id="new-user-button">Я - новый пользователь</button>
            </div>
        </div>
    );
}