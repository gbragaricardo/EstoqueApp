
    var confirmModal = document.getElementById('confirmModal');
    confirmModal.addEventListener('show.bs.modal', function (event) {
      var button = event.relatedTarget;

    // pega atributos do botão que abriu o modal
    var id = button.getAttribute('data-id');
    var handler = button.getAttribute('data-handler');
    var message = button.getAttribute('data-message');

    // seta os valores no form
    var input = confirmModal.querySelector('#confirmModalId');
    input.value = id;

    var form = confirmModal.querySelector('#confirmModalForm');
    form.setAttribute('action', '?handler=' + handler); // envia para o handler Razor

    var msg = confirmModal.querySelector('#confirmModalMessage');
    msg.textContent = message ?? 'Tem certeza que deseja continuar?';
    });