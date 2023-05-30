let chats = [];
let tokenKey = 'accessToken';
let accountKey = 'Account';
let chatsKey = 'Chats';
let currentAccount;

let authControllerUrl = 'api/AuthController/';
let chatsControllerUrl = 'api/ChatsController/';

init()

function init() {
    getLoginData();
    loadAccount();
}

function getLoginData() {
    let token = sessionStorage.getItem(tokenKey);

    if (token === undefined || token === null) {
        showModalResult('Ошибка 401', 'Вы не авторизованы');
    }

    fetch(authControllerUrl + 'getAccount', {
        method: 'GET',
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token,
        }
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
        })
        .then(data => {
            if (data.error) {
                showModalResult(`Ошибка ${data.errorCode}`, errorMessage, true);
                return;
            }

            sessionStorage.removeItem(accountKey);
            sessionStorage.setItem(accountKey, JSON.stringify(data.value));
        })
        .catch(error => {
            showModalResult('Ошибка', 'Не удалось получить данные логина', true);
        });
}

function loadAccount() {
    currentAccount = JSON.parse(sessionStorage.getItem(accountKey));

    loadChats();
}

function loadChats() {
    token = sessionStorage.getItem(tokenKey);

    let userId = currentAccount.id;
    fetch(chatsControllerUrl + `getChatsFromUser?userId=${userId}`, {
        method: 'GET',
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token,
        }
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
        })
        .then(data => {
            if (data.error) {
                showModalResult(`Ошибка ${data.errorCode}`, errorMessage, true);
                return;
            }
            let result = data.value;

            result.forEach(element => {
                chats.push(new Chat(element.id, element.title, element.createdAt));
            });

            sessionStorage.removeItem(chatsKey);
            sessionStorage.setItem(chatsKey, JSON.stringify(chats));

            displayChats();
        })
        .catch(error => {
            console.log(error);
            //showModalResult('Ошибка', 'Не удалось получить данные логина', true);
        });

}

function displayChats() {
    chats.forEach(element => {
        displayChat(element);
    });
}

function displayChat(chat) {
    let chatContainer = document.getElementById('chats-container');
    let chatElement = document.createElement('li');
    chatElement.className = 'content__items__item';
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

function openModalForCreateChat() {
    let modal = document.getElementById('chat-create-modal');
    modal.style.display = 'block';
}

function createChat() {
    let title = document.getElementById('chat-create-text').value;
    let chat = new Chat(0, title, new Date());
    let token = sessionStorage.getItem(tokenKey);

    console.log(chat);

    fetch(chatsControllerUrl + `create?accountId=${currentAccount.id}&title=${title}`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        body: JSON.stringify(chat)
    })
        .then(response => response.json())
        .then(data => {
            if (!data.error) {
                location.reload();
                return;
            }
            showModalResult('Создание чата', data.errorMessage);
        })
        .catch(error => {
            showModalResult('Ошибка', 'Не удалось создать чат', false);
        });
}