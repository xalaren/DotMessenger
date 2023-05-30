let chats = [];
let tokenKey = 'accessToken';
let accountKey = 'Account';
let currentAccount;

let authControllerUrl = 'api/AuthController/';
let chatsControllerUrl ='api/ChatsController/';

init()

function init() {
    getLoginData();
}

function getLoginData() {
    let token = sessionStorage.getItem(tokenKey);

    if(token === undefined || token === null) {
        showModalResult('Ошибка 401', 'Вы не авторизованы');
    }

    let result;
    fetch(authControllerUrl + 'getAccount',{
        method: 'GET',
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token,
        }
    })
        .then(response => {
            if(response.ok) {
                return response.json();
            }
        })
        .then(data => {
            if(data.error) {
                showModalResult(`Ошибка ${data.errorCode}`, errorMessage, true);
                return;
            }

            result = data.value;
            sessionStorage.removeItem(accountKey);
            sessionStorage.setItem(accountKey, JSON.stringify(result));
            loadAccount();
        })
        .catch(error => {
            showModalResult('Ошибка', 'Не удалось получить данные логина', true);
        });
}

function loadAccount() {
    currentAccount = JSON.parse(sessionStorage.getItem(accountKey));

    if(currentAccount === null || currentAccount === undefined) {
        showModalResult('Ошибка', 'Аккаунт не найден', false);
    }

    loadChats();
}

function loadChats() {
    // console.log(currentAccount);
    let chat = new Chat(1, 'Chat AAA', new Date().toDateString());
    displayChat(chat);
}

function displayChat(chat) {
    let chatContainer = document.getElementById('chats-container');
    let chatElement = document.createElement('li');
    chatElement.className='content__items__item';
    chatElement.innerHTML = `
        <p class="item-name">${chat.title}</p>
        <p class="item-text">${chat.createdAt}</p>
    `;

    chatContainer.appendChild(chatElement);
}



function showModalResult(title, text, close) {
    let modalElement = document.createElement('div');
    modalElement.className = 'modal';
    modalElement.innerHTML = `
    <div class="modal__container">
        <div class="modal__header">
            <h1 class="modal__title">${title}</h1>
            <button class="modal__close__button" onclick="${close ? 'closeWithRedirect()' : 'closeModal()'}">×</button>
        </div>
        <div class="modal__content">
            <div>${text}</div>
        </div>
        </div>`;
    document.body.appendChild(modalElement);
}

function closeWithRedirect() {
    closeModal();
    redirectTo('index');
}

function goToChat() {

}