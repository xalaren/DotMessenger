let currentChat;
let currentChatKey = 'currentChat';
var users = []
let accountsKey = 'accountsKey'
let tokenKey = 'accessToken';

const chatsControllerUrl = 'api/ChatsController';
const accountsControllerUrl = 'api/AccountsController';

init();

function init() {
    loadChat();
}

function loadChat() {
    currentChat = JSON.parse(sessionStorage.getItem(currentChatKey));

    if(currentChat === null || currentChat === undefined) {
        showModalResult('Ошибка', 'Чат не загружен', true);
    }

    let chatHead = document.getElementById('chat-head');
    chatHead.innerHTML = currentChat.title;

    loadUsers();
}

function showModalResult(title, text, redirect) {
    let modalElement = document.createElement('div');
    modalElement.className = 'modal';
    modalElement.innerHTML = `
    <div class="modal__container">
        <div class="modal__header">
            <h1 class="modal__title">${title}</h1>
            <button class="modal__close__button" onclick="${redirect ? 'closeWithRedirect()' : 'closeModal()'}">×</button>
        </div>
        <div class="modal__content">
            <div>${text}</div>
        </div>
    </div>`;
    document.body.appendChild(modalElement);
}

function loadUsers() {
    if(currentChat === null || currentChat === undefined) {
        return;
    }

    let token = sessionStorage.getItem(tokenKey);
    $.ajax({
        success: function (response) {
            if(response.error) {
                showModalResult('Ошибка', response.errorMessage);
            }

            console.log(response.value);
        },
        url: chatsControllerUrl + `/getUsers?chatId=${currentChat.id}`,
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        error: function(xhr, status, error) {
            showModalResult('Ошибка', 'Не удалось загрузить пользователей');
        }
    });

}

function defineUsers(accountId) {
    let token = sessionStorage.getItem(tokenKey);
    $.ajax({
        success: function (response) {
            if(response.error) {
                showModalResult('Ошибка', response.errorMessage);
            }
            let account = response.value;
            users.push(new SharedAccount(account.id, account.nickname, account.name, account.email, account.phone));
        },
        url: accountsControllerUrl + `/get-by-id?accountId=${accountId}`,
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token,
        },
        error: function(xhr, status, error) {
            showModalResult('Ошибка', 'Не удалось загрузить пользователя');
        }
    });
}

function handleUsersResult() {
    let membersTitle = document.getElementById('members-title');
    membersTitle.innerHTML = `Участников ${users.length}`;
}

function closeWithRedirect() {
    closeModal();
    redirectTo('chatlist');
}



