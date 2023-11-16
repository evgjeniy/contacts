// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    $('#contactForm').submit(function (e) {
        e.preventDefault();
        
        const contactStringId = $('#contactId').val();
        const isPostRequest = contactStringId === '';
        
        const contactId = isPostRequest ? 0 : parseInt(contactStringId);
        const name = $('#name').val();
        const mobilePhone = $('#mobilePhone').val();
        const jobTitle = $('#jobTitle').val();
        const birthDate = $('#birthDate').val();
        
        if (validateName(name) && validatePhone(mobilePhone) && validateJobTitle(jobTitle) && validateBirthDate(birthDate)) {
            const method = isPostRequest ? 'POST' : 'PUT';
            const url = isPostRequest ? 'create' : `update`;

            fetch(url, {
                method: method,
                headers: {'Content-Type': 'application/json; charset=utf-8'},
                body: JSON.stringify({
                    id: contactId,
                    name: name,
                    mobilePhone: mobilePhone,
                    jobTitle: jobTitle,
                    birthDate: birthDate
                })
            })
                .then(response => response.json())
                .then(data => {
                    closeModalPopup();
                    window.location.reload();
                })
                .catch(error => console.error('Error:', error));
        }
    });
});

function closeModalPopup() { $('#contactModal').modal('hide'); }
function showModalPopup() { $('#contactModal').modal('show'); }

function addContact() {
    $('#contactModalLabel').text('Create new contact');
    $('#contactId').val('');
    $('#name').val('');
    $('#mobilePhone').val('');
    $('#jobTitle').val('');
    $('#birthDate').val('');
    
    showModalPopup();
}

function editContact(id) {
    $('#contactModalLabel').text('Edit contact');

    fetch(`${id}`)
        .then(response => response.json())
        .then(data => {
            $('#contactId').val(data.id);
            $('#name').val(data.name);
            $('#mobilePhone').val(data.mobilePhone);
            $('#jobTitle').val(data.jobTitle);
            $('#birthDate').val(data.birthDate.substring(0, 10));
            $('#contactModal').modal('show');
        })
        .catch(error => console.error('Error:', error));

    showModalPopup();
}

function deleteContact(id) {
    if (confirm("Are you sure you want to delete this contact?")) {
        fetch(`/delete/${id}`, {method: 'DELETE',})
            .then(response => response.json())
            .then(data => $("#contact-row-" + data).remove())
            .catch(error => console.error('Error:', error));
    }
}

function changeValidationStatus(element, isMatch) {
    if (isMatch) element.removeClass("is-invalid").addClass("is-valid");
    else         element.removeClass("is-valid").addClass("is-invalid");
    return isMatch;
}

function validateName(name) {
    const nameRegex = /^[A-ZА-ЯЁ][a-zа-яё]*(?:-[A-ZА-ЯЁ][a-zа-яё]*)*$/;
    return changeValidationStatus($('#name'), nameRegex.test(name));
}

function validatePhone(phone) {
    const belarusPhoneRegex = /^\+375(?:\(\d{2}\)|\d{2})\d{3}(?:-\d{2}-\d{2}|\d{4})$/;
    return changeValidationStatus($('#mobilePhone'), belarusPhoneRegex.test(phone));
}

function validateJobTitle(jobTitle) { return changeValidationStatus($('#jobTitle'), !!jobTitle); }

function validateBirthDate(birthDate) {
    const isMatch = !!birthDate && Date.parse(birthDate) < Date.now();
    return changeValidationStatus($('#birthDate'), isMatch);
}