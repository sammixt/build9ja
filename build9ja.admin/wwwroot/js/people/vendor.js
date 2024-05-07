
jQuery(document).ready(function () {
    jQuery('#vendorTable').dataTable({
        "pagingType": "simple_numbers",
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": `${url}People/GetVendors`,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "company", "name":"Company", "autoWidth": true },
            { "data": "firstName", "name":"First Name", "autoWidth": true },
            { "data": "lastName", "name":"Last Name", "autoWidth": true },
            { "data": "email", "name":"Email", "autoWidth": true },
            { "data": "cacNumber", "name":"CAC Number", "autoWidth": true },
            { "data": "taxNumber", "name":"Tax Number", "autoWidth": true },
            { "data": "status", "name":"Status", "autoWidth": true },
            { "data": '' }
        ],
        "columnDefs": [{
            "targets": 0,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['company'];
            }
        },
        {
            "targets": 1,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['firstName'];
            }
        },
        {
            "targets": 2,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['lastName'];
            }
        },
        {
            "targets": 3,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['email'];
            }
        },
        {
            "targets": 4,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['cacNumber'];
            }
        },
        {
            "targets": 5,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['taxNumber'];
            }
        },
        {
            "targets": 6,
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
                        <a href="javascript:void(0)" data-id="${full["sellerId"]}" class="dropdown-item click-edit"  data-toggle="tooltip" title="" data-placement="right"
                        data-original-title="Check out more demos">Edit</a>
                        <a href="javascript:void(0)" data-id="${full["sellerId"]}" class="dropdown-item create-login"  data-toggle="tooltip" title="" data-placement="right"
                        data-original-title="Check out more demos">Create Login</a>
                        <a href="javascript:void(0)" data-id="${full["sellerId"]}" class="dropdown-item bank-detail"  data-toggle="tooltip" title="" data-placement="right"
                        data-original-title="Check out more demos">Bank Detail</a>
                    </div>
                </div>`
              );
            }
          }
        ]
    });

    jQuery("#new-vendor-form").submit(function(e){
        e.preventDefault();
        var model = {
            company : jQuery("#company").val(),
            taxNumber : jQuery("#taxNumber").val(),
            cacNumber : jQuery("#cacNumber").val(),
            firstName : jQuery("#firstName").val(),
            lastName : jQuery("#lastName").val(),
            status:jQuery('#status option:selected').val(),
            phoneNumber : jQuery("#phoneNumber").val(),
            description : jQuery("#description").val(),
            email : jQuery("#email").val(),
            address : jQuery("#address").val(),
            city : jQuery("#city").val(),
            state : jQuery("#state").val(),
            country : jQuery("#country").val()
           }
           axios.post(`${url}People/AddVendor`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#new-vendor-form").find("input[type=text], input[type=email], textarea").val("");
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

    jQuery("#vendorTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var vId = jQuery(this).attr('data-id');
        axios.get(`${url}People/GetVendorById?id=${vId}`)
            .then(response => {
                var d = response.data;
                 jQuery("#edit_company").val(d.company);
                 jQuery("#edit_taxNumber").val(d.taxNumber);
                 jQuery("#edit_cacNumber").val(d.cacNumber);
                 jQuery("#edit_firstName").val(d.firstName);
                 jQuery("#edit_lastName").val(d.lastName);
                 jQuery('#edit_status').val(d.status);
                 jQuery("#edit_phoneNumber").val(d.phoneNumber);
                 jQuery("#edit_description").val(d.description);
                 jQuery("#edit_email").val(d.email);
                 jQuery("#edit_address").val(d.address);
                 jQuery("#edit_city").val(d.city);
                 jQuery("#edit_state").val(d.state);
                 jQuery("#edit_country").val(d.country);
                 jQuery("#sellerId").val(d.sellerId);

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

    jQuery("#vendorTable tbody").on('click','.bank-detail',function(e){
        e.preventDefault();
        var vId = jQuery(this).attr('data-id');
        jQuery("#bankVendorId").val(vId);
        jQuery('.bankdetailpopup').addClass('offcanvas-on');
        axios.get(`${url}People/GetVendorBankInfoById?vid=${vId}`)
            .then(response => {
                var d = response.data;
                 jQuery("#bankName").val(d.bankName);
                 jQuery("#accountNumber").val(d.accountNumber);
                 jQuery("#accountName").val(d.accountName);                
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

    jQuery("#bank-detail-form").submit(function(e){
        e.preventDefault();
        var model = {
            bankName : jQuery("#bankName").val(),
            accountNumber : jQuery("#accountNumber").val(),
            accountName: jQuery("#accountName").val(),
            sellerId : jQuery("#bankVendorId").val()
        }

        axios.patch(`${url}People/AddBankVendorBankInfo`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#bank-detail-form").find("input[type=text], input[type=email], textarea").val("");
                    jQuery('.bankdetailpopup').removeClass('offcanvas-on');
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
    })
    

    jQuery("#edit-vendor-form").submit(function(e){
        e.preventDefault();
        var model = {
            company : jQuery("#edit_company").val(),
            taxNumber : jQuery("#edit_taxNumber").val(),
            cacNumber : jQuery("#edit_cacNumber").val(),
            firstName : jQuery("#edit_firstName").val(),
            lastName : jQuery("#edit_lastName").val(),
            status:jQuery('#edit_status option:selected').val(),
            phoneNumber : jQuery("#edit_phoneNumber").val(),
            description : jQuery("#edit_description").val(),
            email : jQuery("#edit_email").val(),
            address : jQuery("#edit_address").val(),
            city : jQuery("#edit_city").val(),
            state : jQuery("#edit_state").val(),
            country : jQuery("#edit_country").val(),
            sellerId: jQuery("#sellerId").val()
           }
           axios.put(`${url}People/UpdateVendor`, model)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#edit-vendor-form").find("input[type=text], input[type=email], textarea").val("");
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

    jQuery("#vendorTable tbody").on('click','.create-login',function(e){
        e.preventDefault();
        var staffId = jQuery(this).attr('data-id');
        jQuery("#vendorId").val(staffId);
        jQuery('.loginpopup').addClass('offcanvas-on');
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
            memberId : jQuery("#vendorId").val(),
            userName : jQuery("#userName").val(),
            password : jQuery("#password").val(),
            permission : jQuery("#permission option:selected").val()
           }
           axios.post(`${url}People/CreateVendorLogin`, model)
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