"use strict";

const authControllerUrl = 'api/AuthController';
const accountsControllerUrl = 'api/AccountsController';
let tokenKey = "accessToken";
let accountKey = 'accountKey';

init();
function init() {
    displayLoginForm();
}

function displayLoginForm() {
    let registerform = document.getElementById('registerform');
    let loginform = document.getElementById('loginform');

    registerform.style.display = 'none';
    loginform.style.display = 'flex';
}

function displayRegisterForm() {
    let registerform = document.getElementById('registerform');
    let loginform = document.getElementById('loginform');

    registerform.style.display = 'flex';
    loginform.style.display = 'none';
}
function login() {
    let nickname = document.getElementById('nickname-input-login').value;
    let password = document.getElementById('password-input-login').value;

    $.ajax({
        url: authControllerUrl + `?username=${nickname}&password=${password}`,
        method: 'POST',
        success: function (response) {
                sessionStorage.clear();
                sessionStorage.setItem(tokenKey, response);
                handleLoginResult();
            },
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({
            nickname: nickname,
            password: password,
        }),
        error: function(xhr, status, error) {
            showModalResult('Ошибка', 'Введены неверные данные');
        }
    });
}
function register() {
    let nickname = document.getElementById('nickname-input-register').value;
    let password = document.getElementById('password-input-register').value;
    let name = document.getElementById('name-input-register').value;
    let lastname = document.getElementById('lastname-input-register').value;
    let email = document.getElementById('email-input-register').value;
    let phone = document.getElementById('phone-input-register').value;

    let account = new Account(0, nickname, password, name, lastname, email, phone);

    $.ajax({
        url: accountsControllerUrl + '/register',
        method: 'POST',
        success: function (response) {
            if(response.error) {
                showModalResult('Ошибка', response.errorMessage);
                return;
            }
            showModalResult('Регистрация', response.errorMessage);
            displayLoginForm();
        },
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(account),
        error: function(xhr, status, error) {
            showModalResult('Ошибка', 'Введены неверные данные');
        }
    });
}

function handleLoginResult() {
    let token = sessionStorage.getItem(tokenKey);
    $.ajax({
        url: authControllerUrl + '/getAccount',
        method: 'GET',
        success: function (response) {
            if(response.error) {
                showModalResult(`Ошибка ${response.errorCode}`, response.errorMessage, true);
                return;
            }
            let result = response.value;
            let account = new Account(
                result.id,
                result.nickname,
                result.password,
                result.name,
                result.lastname,
                result.email,
                result.phone);

            sessionStorage.setItem(accountKey, JSON.stringify(account));
            redirectTo('chatlist');
        },
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        error: function(xhr, status, error) {
            showModalResult('Ошибка', 'Не удалось получить данные', true);
        }
    });
}

function showModalResult(title, text) {
    let modalElement = document.createElement('div');
    modalElement.className = 'modal';
    modalElement.innerHTML = `
    <div class="modal__container">
        <div class="modal__header">
            <h1 class="modal__title">${title}</h1>
            <button class="modal__close__button" onclick="closeModal()">×</button>
        </div>
        <div class="modal__content">
            <div>${text}</div>
        </div>
        </div>`;
    document.body.appendChild(modalElement);
}
