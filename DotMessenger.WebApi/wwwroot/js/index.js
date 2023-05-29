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
    redirectTo('chatlist');
}
function register() {
    //Register code here
    displayLoginForm();
}

function redirectTo(pageName) {
    document.location.href = `./${pageName}.html`;
}

