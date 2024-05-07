
jQuery(document).ready(function () {
    jQuery('#commissionTable').dataTable({
        "pagingType": "simple_numbers",

        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }]
    });


    jQuery("#new-commission-form").submit(function(e){
        e.preventDefault();
        var model = {
            commissionType : jQuery("#commissionType").val(),
            commissionPercentage : jQuery("#commissionPercentage").val(),
            status: jQuery('#status option:selected').val()
        }
        
       
           axios.post(`${url}Setting/AddCommission`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                   window.location.reload();
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

    jQuery("#commissionTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var vId = jQuery(this).attr('data-id');
        axios.get(`${url}Setting/GetCommissionById?id=${vId}`)
            .then(response => {
                var d = response.data;
                 jQuery("#edit_commissionType").val(d.commissionType);
                 jQuery("#edit_commissionPercentage").val(d.commissionPercentage);
                 jQuery("#edit_status").val(d.status);
                 jQuery("#edit_parentId").val(d.parentId);
                 
                 jQuery("#comId").val(d.id);
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
    

    jQuery("#edit-commission-form").submit(function(e){
        e.preventDefault();
        e.preventDefault();
        var model = {
            commissionType : jQuery("#edit_commissionType").val(),
            commissionPercentage : jQuery("#edit_commissionPercentage").val(),
            status: jQuery('#status option:selected').val(),
            id: jQuery("#comId").val(),
        }
           axios.put(`${url}Setting/UpdateCommission`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#edit-category-form").find("input[type=text], input[type=email], textarea").val("");
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