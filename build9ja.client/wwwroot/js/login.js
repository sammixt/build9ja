jQuery(document).ready(function(){
    
    jQuery("#register").on('click',function(e){
        e.preventDefault();
        var registrationModel = {
            firstName : jQuery("#firstname").val(),
            lastName : jQuery("#lastname").val(),
            email : jQuery("#email").val(),
            phoneNumber : jQuery("#phoneNumber").val(),
            password : jQuery("#password").val(),
            returnUrl : jQuery("#returnUrl").val()
        };

        axios.post(`${urls}Home/ProcessRegister`, registrationModel)
        .then(response => {
                if (response.data.statusCode === 200) {
                   window.location.href = urls + `${response.data.message}`
                } else {
                    alert(response.data.message)
                    // toastr['error'](`👋 ${response.data.message}`, 'Error!', {
                    //     closeButton: true,
                    //     tapToDismiss: true,
                    //     rtl: false
                    // });
                }
        })
        .catch(function (error) {
            processErrors(error)
        });
    });

    jQuery("#login").on('click',function(e){
        e.preventDefault();
        var loginModel = {
            email : jQuery("#loginEmail").val(),
            password : jQuery("#loginPassword").val(),
            returnUrl : jQuery("#loginReturnUrl").val()
        };

        axios.post(`${urls}Home/Logon`, loginModel)
        .then(response => {
                if (response.data.statusCode === 200) {
                    window.location.href = urls + `${response.data.message}`
                } else {
                    alert(response.data.message)
                    // toastr['error'](`👋 ${response.data.message}`, 'Error!', {
                    //     closeButton: true,
                    //     tapToDismiss: true,
                    //     rtl: false
                    // });
                }
        })
        .catch(function (error) {
            processErrors(error)
        });
    })
})