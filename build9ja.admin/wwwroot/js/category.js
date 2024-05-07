
jQuery(document).ready(function () {
    jQuery('#categoryTable').dataTable({
        "pagingType": "simple_numbers",
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": `${url}Category/GetCategories`,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "name":"Category", "autoWidth": false },
            { "data": "description", "name":"Description", "autoWidth": false },
            { "data": "parentCategory", "name":"Parent Category", "autoWidth": false },
            { "data": "image", "name":"Image", "autoWidth": false },
            { "data": "status", "name":"Status", "autoWidth": false },
            { "data": '' }
        ],
        "columnDefs": [{
            "targets": 0,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['name'];
            }
        },
        {
            "targets": 1,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['description'];
            }
        },
        {
            "targets": 2,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              
              return full['parentCategory'];
            }
        },
        {
            "targets": 3,
            "responsivePriority": 1,
            "render": function (data, type, full, meta) {
              return(
                  `
                  <div class="h-45px w-45px d-flex align-items-center">
                    <img class="img-fluid" src="${full['image']}" alt="product">
                </div>
                  `
              )
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

    jQuery("#new-category-form").submit(function(e){
        e.preventDefault();
        var _files = jQuery(`#img`)[0].files[0];
        var isChecked = jQuery('#isTopCategory').is(":checked") ? true : false;
        var formData = new FormData();
        formData.append('name', jQuery("#name").val());
        formData.append('description', jQuery("#description").val());
        formData.append('status', jQuery('#status option:selected').val());
        formData.append('parentId', jQuery('#parentId option:selected').val());
        formData.append('isTopCategory', isChecked);
        formData.append('uploadImage', _files);
       
           axios.post(`${url}Category/AddCategory`, formData)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#new-category-form").find("input[type=text], input[type=email], textarea").val("");
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

    jQuery("#categoryTable tbody").on('click','.click-edit',function(e){
        e.preventDefault();
        var vId = jQuery(this).attr('data-id');
        axios.get(`${url}Category/GetById?id=${vId}`)
            .then(response => {
                var d = response.data;
                 jQuery("#edit_name").val(d.name);
                 jQuery("#edit_description").val(d.description);
                 jQuery("#edit_status").val(d.status);
                 jQuery("#edit_parentId").val(d.parentId);
                 jQuery("#edit_isTopCategory").prop("checked", d.isTopCategory );
                 
                 jQuery("#catId").val(d.id);
                 jQuery("#cat_image").attr('src',d.image)

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
    

    jQuery("#edit-category-form").submit(function(e){
        e.preventDefault();
        var _files = jQuery(`#edit_img`)[0].files[0];
        var formData = new FormData();
        var isChecked = jQuery('#edit_isTopCategory').is(":checked") ? true : false;
        formData.append('name', jQuery("#edit_name").val());
        formData.append('description', jQuery("#edit_description").val());
        formData.append('status', jQuery('#edit_status option:selected').val());
        formData.append('id', jQuery('#catId').val());
        formData.append('parentId', jQuery('#edit_parentId option:selected').val());
        formData.append('isTopCategory', isChecked);
        formData.append('uploadImage', _files);
           axios.put(`${url}Category/UpdateCategory`, formData)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#edit-category-form").find("input[type=text], input[type=email], textarea").val("");
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

    var populatePermission = function(){
        var perm = jQuery('#parentId');
        var edit = jQuery("#edit_parentId");
        axios.get(`${url}Category/GetAllCategory`)
                    .then(response => {
                        var d = response.data;
                        jQuery.each(d, function(k,v){
                            perm.append(jQuery("<option />").val(v.id).text(v.name));
                            edit.append(jQuery("<option />").val(v.id).text(v.name));
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

})