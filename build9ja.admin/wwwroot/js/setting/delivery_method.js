
jQuery(document).ready(function () {
    jQuery('#shippingTable').dataTable({
        "pagingType": "simple_numbers",
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": `${url}Setting/GetDeliveryMethod`,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "state", "name":"State", "autoWidth": false },
            { "data": "localGovt", "name":"City", "autoWidth": false },
            { "data": "price", "name":"Price", "autoWidth": false },
            { "data": "status", "name":"Status", "autoWidth": false },
            { "data": '' }
        ],
        "columnDefs": [{
            "targets": 0,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['state'];
            }
        },
        {
            "targets": 1,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              return full['localGovt'];
            }
        },
        {
            "targets": 2,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              return full['price'];
            }
        },
        {
            "targets": 3,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              return full['status'];
            }
        },
        {
            // Actions
            "targets": -1,
            "title": 'Actions',
            "orderable": false,
            "className": 'no-sort text-right',
            render: function (data, type, full, meta) {
              return (
                `<div class="card-toolbar text-right">
                    <button class="btn p-0 shadow-none" type="button" id="dropdowneditButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="svg-icon">
                            <svg width="20px" height="20px" viewBox="0 0 16 16" class="bi bi-three-dots text-body" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" d="M3 9.5a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3z"></path>
                            </svg>
                        </span>
                    </button>
                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdowneditButton"  style="position: absolute; transform: translate3d(1001px, 111px, 0px); top: 0px; left: 0px; will-change: transform;">
                        <a href="javascript:void(0)" data-id="${full["shippingId"]}" class="dropdown-item click-edit"  data-toggle="tooltip" title="" data-placement="right"
                        data-original-title="Check out more demos">Edit</a>
                    </div>
                </div>`
              );
            }
          }
        ]
    });

    jQuery("#new-delivery-form").submit(function(e){
        e.preventDefault();
        var formData = new FormData();
        formData.append('state', jQuery("#state option:selected").val());
        formData.append('localGovt', jQuery("#lga option:selected").val());
        formData.append('price', jQuery("#basePrice").val());
        formData.append('status', jQuery("#add_status option:selected").val());
           axios.post(`${url}Setting/AddDeliveryMethod`, formData)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#new-delivery-form").find("input[type=text], input[type=email], textarea").val("");
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

    jQuery("#shippingTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var vId = jQuery(this).attr('data-id');
        axios.get(`${url}Setting/GetDeliveryMethodById?id=${vId}`)
            .then(response => {
                var d = response.data;
                 jQuery(".current-state").val(d.state);
                 jQuery("#edit_state").val(d.state);
                 toggleLGAEdit(d.state);
                 jQuery("#edit_lga").val(d.localGovt)
                 jQuery("#editBasePrice").val(d.price);
                 jQuery('#edit_status').val(d.status)
                 jQuery('#deliveryIdString').val(d.shippingId)
                 jQuery('.editpopup').addClass('offcanvas-on');
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
    

    jQuery("#update-delivery-form").submit(function(e){
        e.preventDefault();
        var formData = new FormData();
        formData.append('state', jQuery("#edit_state option:selected").val());
        formData.append('localGovt', jQuery("#edit_lga option:selected").val());
        formData.append('price', jQuery("#editBasePrice").val());
        formData.append('status', jQuery("#edit_status option:selected").val());
        formData.append('shippingId', jQuery("#deliveryIdString").val());

           axios.put(`${url}Setting/UpdateDeliveryMethod`, formData)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#update-delivery-form").find("input[type=text], input[type=email], textarea").val("");
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
})