"use strict";

function redirectTo(pageName) {
    document.location.href = `./${pageName}.html`;
}

function closeModal() {
    let modals = document.getElementsByClassName('modal');

    modals[0].remove();
}

function logout() {
    redirectTo('index');
    sessionStorage.clear();
}