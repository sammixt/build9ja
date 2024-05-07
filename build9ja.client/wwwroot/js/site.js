// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const urls = 'https://localhost:7172/';

var getUUID = function () {
    var dt = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}

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
        console.log(data);
        // toastr['error'](errorMsg, 'Error!', {
        //     closeButton: true,
        //     tapToDismiss: false,
        //     rtl: false
        // });
    }
}

