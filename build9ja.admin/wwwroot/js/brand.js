
jQuery(document).ready(function () {
    jQuery('#brandTable').dataTable({
        "pagingType": "simple_numbers",
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": `${url}Brand/GetBrands`,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "brandName", "name":"Brand", "autoWidth": false },
            { "data": "image", "name":"Image", "autoWidth": false },
            { "data": '' }
        ],
        "columnDefs": [{
            "targets": 0,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['brandName'];
            }
        },
        {
            "targets": 1,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              return(
                  `
                  <div class="h-45px w-45px d-flex align-items-center">
                    <img class="img-fluid" src="${full['brandLogo']}" alt="product">
                </div>
                  `
              )
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
                        <a href="javascript:void(0)" data-id="${full["id"]}" class="dropdown-item click-edit"  data-toggle="tooltip" title="" data-placement="right"
                        data-original-title="Check out more demos">Edit</a>
                    </div>
                </div>`
              );
            }
          }
        ]
    });

    jQuery("#new-brand-form").submit(function(e){
        e.preventDefault();
        var _files = jQuery(`#img`)[0].files[0];
        var formData = new FormData();
        formData.append('brandName', jQuery("#brandName").val());
        var isChecked = jQuery('#isTopbrand').is(":checked") ? true : false;
        formData.append('isTopBrand', isChecked);
        formData.append('uploadImage', _files);
           axios.post(`${url}Brand/AddBrand`, formData)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#new-brand-form").find("input[type=text], input[type=email], textarea").val("");
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

    jQuery("#brandTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var vId = jQuery(this).attr('data-id');
        axios.get(`${url}Brand/GetById?id=${vId}`)
            .then(response => {
                var d = response.data;
                 jQuery("#edit_brandName").val(d.brandName);
                 jQuery("#edit_isTopbrand").prop("checked", d.isTopBrand );
                 jQuery("#brandId").val(d.id);
                 jQuery("#brand_image").attr('src',d.brandLogo)

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
    

    jQuery("#edit-brand-form").submit(function(e){
        e.preventDefault();
        var _files = jQuery(`#edit_img`)[0].files[0];
        var formData = new FormData();
        formData.append('brandName', jQuery("#edit_brandName").val());
        var isChecked = jQuery('#edit_isTopbrand').is(":checked") ? true : false;
        formData.append('isTopBrand', isChecked);
        formData.append('id', jQuery("#brandId").val());
        formData.append('uploadImage', _files);

           axios.put(`${url}Brand/UpdateBrand`, formData)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#edit-brand-form").find("input[type=text], input[type=email], textarea").val("");
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