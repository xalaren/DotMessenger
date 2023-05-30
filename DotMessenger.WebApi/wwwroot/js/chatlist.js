let chats = [];
let tokenKey = 'accessToken';
let accountKey = 'accountKey';
let chatsKey = 'Chats';
let currentChatKey = 'currentChat';

let authControllerUrl = 'api/AuthController/';
let chatsControllerUrl = 'api/ChatsController/';

init()

function init() {
    getLoginData();
}

function getLoginData() {
    currentAccount = JSON.parse(sessionStorage.getItem(accountKey));

    if (currentAccount === undefined || currentAccount === null) {
        showModalResult('Ошибка 401', 'Вы не авторизованы', true);
    }

    loadChats();
}
function loadChats() {
    token = sessionStorage.getItem(tokenKey);
    let userId = currentAccount.id;

    $.ajax({
        url: chatsControllerUrl + `getChatsFromUser?userId=${userId}`,
        method: 'GET',
        success: function (response) {
            if(response.error) {
                showModalResult(`Ошибка ${response.errorCode}`, response.errorMessage, false);
                return;
            }
            let result = response.value;

            if(result.length <= 0) {
                return;
            }
            result.filter(e => e !== null && e !== undefined).forEach(element => {
                chats.push(new Chat(element.id, element.title, element.createdAt));
            });

            sessionStorage.removeItem(chatsKey);
            sessionStorage.setItem(chatsKey, JSON.stringify(chats));

            handleChatsResult();
        },
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        error: function(xhr, status, error) {
            showModalResult('Ошибка', 'Введены неверные данные');
        }
    });
}

function handleChatsResult() {
    console.log(chats);
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
    chatElement.id=`chat-${chat.id}`;
    chatElement.addEventListener('click', goToChat);
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

function openModalForCreateChat() {
    let modal = document.getElementById('chat-create-modal');
    modal.style.display = 'block';
}

function createChat() {
    hideModal();
    let title = document.getElementById('chat-create-text').value;
    let chat = new Chat(0, title, new Date());
    let token = sessionStorage.getItem(tokenKey);

    console.log(chat);

    $.ajax({
        url: chatsControllerUrl + `create?accountId=${currentAccount.id}&title=${title}`,
        method: 'POST',
        success: function (response) {
            if (response.error) {
                showModalResult('Создание чата', response.errorMessage);
            }
            location.reload();
        },
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        data: JSON.stringify(chat),
        error: function(xhr, status, error) {
            showModalResult('Ошибка', 'Не удалось создать чат', false);
        }
    });
}

function goToChat(event) {
    let element = event.target.closest('.content__items__item');
    let index = parseInt(element.id.match(/\d+/)) - 1;

    console.log(chats[index]);
    if(chats[index] !== null && chats[index] !== undefined) {
        sessionStorage.removeItem(currentChatKey);
        sessionStorage.setItem(currentChatKey, JSON.stringify(chats[index]));

        redirectTo('chat');
    }
}