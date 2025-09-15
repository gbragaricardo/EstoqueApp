    // BOOTSTRAP

    //var confirmModal = document.getElementById('confirmModal');
    //confirmModal.addEventListener('show.bs.modal', function (event) {
    //  var button = event.relatedTarget;

    //// pega atributos do botão que abriu o modal
    //var id = button.getAttribute('data-id');
    //var handler = button.getAttribute('data-handler');
    //var message = button.getAttribute('data-message');

    //// seta os valores no form
    //var input = confirmModal.querySelector('#confirmModalId');
    //input.value = id;

    //var form = confirmModal.querySelector('#confirmModalForm');
    //form.setAttribute('action', '?handler=' + handler); // envia para o handler Razor

    //var msg = confirmModal.querySelector('#confirmModalMessage');
    //msg.textContent = message ?? 'Tem certeza que deseja continuar?';
    //});

function openModal(button) {
    const modal = document.getElementById('confirmModal');

    // pega atributos do botão
    const id = button.getAttribute('data-id');
    const handler = button.getAttribute('data-handler');
    const message = button.getAttribute('data-message');

    // seta os valores no form
    modal.querySelector('#confirmModalId').value = id;
    modal.querySelector('#confirmModalForm').setAttribute('action', '?handler=' + handler);
    modal.querySelector('#confirmModalMessage').textContent = message ?? 'Tem certeza que deseja continuar?';

    // abre modal
    modal.classList.remove('hidden');
}

function closeModal() {
    document.getElementById('confirmModal').classList.add('hidden');
}