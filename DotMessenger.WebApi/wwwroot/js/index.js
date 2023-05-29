"use strict";

const loginUri = 'api/AuthController';
const accountsControllerUri = 'api/AccountsController/';

init();

let Account = function (id, nickname, password, name, lastname, email, phone) {
    this.id = id;
    this.nickname = nickname;
    this.password = password;
    this.name = name;
    this.lastname = lastname;
    this.email = email;
    this.phone = phone;
}

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
    redirectTo('chatlist');
}
function register() {
    let nickname = document.getElementById('nickname-input-register').value;
    let password = document.getElementById('password-input-register').value;
    let name = document.getElementById('name-input-register').value;
    let lastname = document.getElementById('lastname-input-register').value;
    let email = document.getElementById('email-input-register').value;
    let phone = document.getElementById('phone-input-register').value;

    let account = new Account(0, nickname, password, name, lastname, email, phone);

    fetch(accountsControllerUri + 'register', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(account)
    })
        .then(response => response.json())
        .then((data) => {
            showModalResult('Регистрация', data.errorMessage);

            if (!data.error) {
                displayLoginForm();
            }
        });

}

function redirectTo(pageName) {
    document.location.href = `./${pageName}.html`;
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
        </div>
        `;
    document.body.appendChild(modalElement);
}

function closeModal() {
    let modals = document.getElementsByClassName('modal');

    modals[0].remove();
}

