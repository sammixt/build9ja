
jQuery(document).ready(function () {
    jQuery('#permissionTable').dataTable({
        "pagingType": "simple_numbers",

        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }]
    });

    jQuery("#new-permission-form").submit(function(e){
        e.preventDefault();
        var model = {
            permissionName : jQuery("#permissionName").val(),
            status:jQuery('#status option:selected').val()
           }
           axios.post(`${url}Permission/Addpermission`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#new-permission-form").find("input[type=text], input[type=email]").val("");
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

    jQuery("#permissionTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var permissionId = jQuery(this).attr('data-id');
        axios.get(`${url}Permission/GetPermission?id=${permissionId}`)
            .then(response => {
                var d = response.data;
                jQuery("#edit_permissionName").val(d.permissionName);
                jQuery("#edit_status").val(d.status);
                jQuery('#permissionId').val(d.id)
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

    jQuery("#edit-permission-form").submit(function(e){
        e.preventDefault();
        var model = {
            permissionName : jQuery("#edit_permissionName").val(),
            status:jQuery('#edit_status option:selected').val(),
            id: jQuery("#permissionId").val()
           }
           axios.put(`${url}Permission/EditPermission`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#edit-permission-form").find("input[type=text], input[type=email]").val("");
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