// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const url = 'https://localhost:7093/';

function processErrors(error){
    if (error.response) {
        var data = error.response.data;
        var errorMsg = "";
        if (data.errors) {
            errorMsg = data.errors.toString();
        }
        if (data.message) {
            errorMsg = data.message;
        }
        toastr['error'](errorMsg, 'Error!', {
            closeButton: true,
            tapToDismiss: false,
            rtl: false
        });
    }
}

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            var excess = sParameterName.length === 3 ? "=":""
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]+excess);
        }
    }
    return false;
};
