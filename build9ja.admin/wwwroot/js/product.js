jQuery(document).ready(function () {
    // jQuery('#productTable').dataTable({
    //     "pagingType": "simple_numbers",

    //     "columnDefs": [{
    //         "targets": 'no-sort',
    //         "orderable": false,
    //     }]
    // });

    jQuery('#productTable').dataTable({
        "pagingType": "simple_numbers",
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": `${url}Product/GetProducts`,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "productName", "name":"Product", "autoWidth": false },
            { "data": "category", "name":"Category", "autoWidth": false },
            { "data": "vendor", "name":"Vendor", "autoWidth": false },
            { "data": "basePice", "name":"Price", "autoWidth": false },
            { "data": "status", "name":"Status", "autoWidth": false },
            { "data": "isFeatured", "name":"Is Featured", "autoWidth": false },
            { "data": '' }
        ],
        "columnDefs": [{
            "targets": 0,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['productName'];
            }
        },
        {
            "targets": 1,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['category'];
            }
        },
        {
            "targets": 2,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['vendor'];
            }
        },
        {
            "targets": 3,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
                return full['basePrice'];
            }
        },
        {
            "targets": 4,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['status'];
            }
        },
        {
            "targets": 5,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
                if(full['isFeatured']== true){
                     return("Featured") 
                }else{
                    return "";
                }
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
                    <a href="javascript:void(0)" data-id="${full["productIdString"]}" data-name="${full['productName']}" data-featured="${full['isFeatured']}" class="dropdown-item click-edit"  data-toggle="tooltip" title="" data-placement="right"
                    data-original-title="Check out more demos">Update Status</a>    
                    <a href="/Product/Detail?pid=${full["productIdString"]}" class="dropdown-item"   data-placement="right"
                        data-original-title="Check out more demos">Edit</a>
                    </div>
                   
                </div>`
              );
            }
          }
        ]
    });

    var populateCategory = function(){
        var perm = jQuery('#categoryId');
        axios.get(`${url}Category/GetAllCategory`)
                    .then(response => {
                        var d = response.data;
                        jQuery.each(d, function(k,v){
                            perm.append(jQuery("<option />").val(v.id).text(v.name));
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

    populateCategory();

    var populateVendor = function(){
        var perm = jQuery('#vendorId');
        axios.get(`${url}People/GetAllVendors`)
                    .then(response => {
                        var d = response.data;
                        jQuery.each(d, function(k,v){
                            perm.append(jQuery("<option />").val(v.id).text(v.company));
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

    populateVendor();

    jQuery("#new-product-form").submit(function(e){
        e.preventDefault();
        var model = {
            productName : jQuery("#productName").val(),
            basePrice : jQuery("#basePrice").val(),
            categoryId : jQuery("#categoryId option:selected").val(),
            vendorId : jQuery("#vendorId option:selected").val()
           }
           axios.post(`${url}Product/AddProduct`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#create-login-form").find("input[type=text], input[type=password]").val("");
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

    jQuery("#productTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var vId = jQuery(this).attr('data-id');
        var pName = jQuery(this).attr('data-name');
        var isFeature = jQuery(this).attr('data-featured');
        var isFeatureBool = (isFeature.toLowerCase() === 'true');
        jQuery("#isFeatured").prop("checked", isFeatureBool );
        jQuery("#productIdString").val(vId)
        jQuery("#productNameEdit").val(pName)
        jQuery('.editpopup').addClass('offcanvas-on');
    });

    jQuery("#update-status-form").submit(function(e){
        e.preventDefault();
        var isChecked = jQuery('#isFeatured').is(":checked") ? true : false;
        var model = {
            productIdString : jQuery("#productIdString").val(),
            status : jQuery("#edit_status option:selected").val(),
            isFeatured : isChecked
        }
        axios.put(`${url}Product/UpdateStatus`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
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