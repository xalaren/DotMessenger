init();


function init() {
    displayLoginForm();

    fillBirthDaySelect();
    fillBirthYearSelect();
}

function displayLoginForm() {
    let registerform = document.getElementById('registerform');
    let loginform = document.getElementById('loginform');

    registerform.style.display='none';
    loginform.style.display='flex';
}

function displayRegisterForm() {
    let registerform = document.getElementById('registerform');
    let loginform = document.getElementById('loginform');

    registerform.style.display='flex';
    loginform.style.display='none';
}

function fillBirthDaySelect() {
    let birthDaySelect = document.getElementById('birth-day-select');
    let innerString = '';

    for(let i = 1; i <= 31; i++) {
        innerString += `<option value="${i}">${i}</option>`
    }

    birthDaySelect.innerHTML = innerString;
}

function fillBirthYearSelect() {
    let birthYearSelect = document.getElementById('birth-year-select');
    let innerString = '';

    for(let i = 1970; i <= new Date().getFullYear() - 3; i++) {
        innerString += `<option value="${i}">${i}</option>`
    }

    birthYearSelect.innerHTML = innerString;
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

