
jQuery(document).ready(function () {
    jQuery('#productUnitTable').dataTable({
        "pagingType": "simple_numbers",

        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }]
    });

    jQuery("#new-staff-form").submit(function(e){
        e.preventDefault();
        var model = {
            firstName : jQuery("#firstname").val(),
            lastName : jQuery("#lastname").val(),
            sex : jQuery("#gender option:selected").val(),
            dateOfBirthString: jQuery("#dateOfBirth").val(),
            status:jQuery('#status option:selected').val(),
            phoneNumber : jQuery("#phoneNumber").val(),
            altPhoneNumber : jQuery("#altPhoneNumber").val(),
            emailAddress : jQuery("#emailAddress").val(),
            address : jQuery("#address").val(),
            city : jQuery("#city").val(),
            state : jQuery("#state").val(),
            country : jQuery("#country").val()
           }
           axios.post(`${url}People/AddUser`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#new-staff-form").find("input[type=text], input[type=email]").val("");
                } else {
                    
                    toastr['error'](`👋 ${response.data.message}`, 'Error!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                }
           })
           .catch(function (error) {
            processErrors(error)
           });
    });

    jQuery("#productUnitTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var staffId = jQuery(this).attr('data-id');
        axios.get(`${url}People/GetStaff?staffId=${staffId}`)
            .then(response => {
                var d = response.data;
                jQuery("#edit_firstname").val(d.firstName);
                jQuery("#edit_lastname").val(d.lastName);
                jQuery("#edit_gender").val(d.sex);
                jQuery("#edit_dateOfBirth").val(d.getDateOfBirth);
                jQuery('#edit_status').val(d.status);
               jQuery("#edit_phoneNumber").val(d.phoneNumber);
               jQuery("#edit_altPhoneNumber").val(d.altPhoneNumber);
               jQuery("#edit_emailAddress").val(d.emailAddress),
               jQuery("#edit_address").val(d.address),
               jQuery("#edit_city").val(d.city),
               jQuery("#edit_state").val(d.state),
               jQuery("#edit_country").val(d.country)
               jQuery('#staffId').val(d.staffId)
            })
            .catch(function (error) {
                if (error.response) {
                    toastr['error'](error.response.data.message, 'Error!', {
                        closeButton: true,
                        tapToDismiss: false,
                        rtl: false
                    });
                }
            });
    });

    jQuery("#edit-staff-form").submit(function(e){
        e.preventDefault();
        var model = {
            firstName : jQuery("#edit_firstname").val(),
            lastName : jQuery("#edit_lastname").val(),
            sex : jQuery("#edit_gender option:selected").val(),
            dateOfBirthString: jQuery("#edit_dateOfBirth").val(),
            status:jQuery('#edit_status option:selected').val(),
            phoneNumber : jQuery("#edit_phoneNumber").val(),
            altPhoneNumber : jQuery("#edit_altPhoneNumber").val(),
            emailAddress : jQuery("#edit_emailAddress").val(),
            address : jQuery("#edit_address").val(),
            city : jQuery("#edit_city").val(),
            state : jQuery("#edit_state").val(),
            country : jQuery("#edit_country").val(),
            staffId: jQuery("#staffId").val()
           }
           axios.put(`${url}People/UpdateStaff`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#edit-staff-form").find("input[type=text], input[type=email]").val("");
                } else {
                    
                    toastr['error'](`👋 ${response.data.message}`, 'Error!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                }
           })
           .catch(function (error) {
            processErrors(error)
           });
    });

    var populatePermission = function(){
        var perm = jQuery('#permission');
        axios.get(`${url}Permission/GetPermissions`)
                    .then(response => {
                        var d = response.data;
                        jQuery.each(d, function(k,v){
                            perm.append(jQuery("<option />").val(v.permissionName).text(v.permissionName))
                            console.log(v.permissionName)
                        })
                    })
                    .catch(function (error) {
                        if (error.response) {
                            toastr['error'](error.response.data.message, 'Error!', {
                                closeButton: true,
                                tapToDismiss: false,
                                rtl: false
                            });
                        }
                    });
    };

    populatePermission();

    jQuery("#productUnitTable tbody").on('click','.create-login',function(e){
        e.preventDefault();
        var staffId = jQuery(this).attr('data-id');
        jQuery("#loginStaffId").val(staffId);
        // axios.get(`${url}People/GetStaff?staffId=${staffId}`)
        //     .then(response => {
        //         var d = response.data;
        //         jQuery("#edit_firstname").val(d.firstName);
        //         jQuery("#edit_lastname").val(d.lastName);
        //         jQuery("#edit_gender").val(d.sex);
        //         jQuery("#edit_dateOfBirth").val(d.getDateOfBirth);
        //         jQuery('#edit_status').val(d.status);
        //        jQuery("#edit_phoneNumber").val(d.phoneNumber);
        //        jQuery("#edit_altPhoneNumber").val(d.altPhoneNumber);
        //        jQuery("#edit_emailAddress").val(d.emailAddress),
        //        jQuery("#edit_address").val(d.address),
        //        jQuery("#edit_city").val(d.city),
        //        jQuery("#edit_state").val(d.state),
        //        jQuery("#edit_country").val(d.country)
        //        jQuery('#staffId').val(d.staffId)
        //     })
        //     .catch(function (error) {
        //         if (error.response) {
        //             toastr['error'](error.response.data.message, 'Error!', {
        //                 closeButton: true,
        //                 tapToDismiss: false,
        //                 rtl: false
        //             });
        //         }
        //     });
    });

    jQuery("#create-login-form").submit(function(e){
        e.preventDefault();
        var model = {
            memberId : jQuery("#loginStaffId").val(),
            userName : jQuery("#userName").val(),
            password : jQuery("#password").val(),
            permission : jQuery("#permission option:selected").val()
           }
           axios.post(`${url}People/CreateAdminLogin`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#create-login-form").find("input[type=text], input[type=password]").val("");
                } else {
                    
                    toastr['error'](`👋 ${response.data.message}`, 'Error!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                }
           })
           .catch(function (error) {
            processErrors(error)
           });
    });
})