jQuery(document).ready(function () {
    let description;
    let key_features;
    let box_content;
    let additional_note;
    jQuery('.addproduct-js').slick('refresh');
    jQuery('.js-Select').select2();
    jQuery('.addproduct-js').slick('refresh');
    jQuery(document).ready(function() {
        jQuery('.js-example-basic-single').select2();
        
    });
    jQuery(document).ready(function() {
        jQuery('.js-example-basic-multiple').select2();
    });
    jQuery('.js-size-multiple').select2();
    ClassicEditor
        .create( document.querySelector( '#description' ))
        .then( newEditor => {
            description = newEditor;
        } )
        .catch( error => {
                console.error( error );
        } );
    ClassicEditor
        .create(document.querySelector( '#key_features' ) )
        .then( newEditor => {
            key_features = newEditor;
        } )
        .catch( error => {
                console.error( error );
        } );
    ClassicEditor
        .create(document.querySelector( '#box_content' ) )
        .then( newEditor => {
            box_content = newEditor;
        } )
        .catch( error => {
                console.error( error );
        } );
    ClassicEditor
        .create(document.querySelector( '#additional_note' ) )
        .then( newEditor => {
            additional_note = newEditor;
        } )
        .catch( error => {
                console.error( error );
        } );
    jQuery('#myTable').DataTable();
    

      var populateCategory = function(){
        var perm = jQuery('#categoryId');
        axios.get(`${url}Category/GetAllCategory`)
                    .then(response => {
                        var d = response.data;
                        jQuery.each(d, function(k,v){
                            perm.append(jQuery("<option />").val(v.id).text(v.name));
                        })
                        jQuery('#categoryId').multipleSelect({
                            filter: true,
                            filterAcceptOnEnter: true
                          });
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
                        jQuery('#vendorId').multipleSelect({
                            filter: true,
                            filterAcceptOnEnter: true
                          });
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

    var populateBrand = function(){
        var perm = jQuery('#brandId');
        axios.get(`${url}Brand/GetAllBrand`)
                    .then(response => {
                        var d = response.data;
                        jQuery.each(d, function(k,v){
                            perm.append(jQuery("<option />").val(v.id).text(v.brandName));
                        })
                        jQuery('#brandId').multipleSelect({
                            filter: true,
                            filterAcceptOnEnter: true
                          });
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

    populateBrand();

    jQuery("#submitbasic").on('click',function(e){
        e.preventDefault();
        var productId = getUrlParameter("pid");
       var $vendorId = jQuery("#vendorId option:selected").val();
       var $youtubeLink = jQuery("#youtubeLink").val();
       var $basePrice = jQuery("#basePrice").val();
       var $keyFeatures = key_features.getData();
       var $boxContent = box_content.getData();
       var $categoryId = jQuery("#categoryId option:selected").val();
       var $brandId = jQuery("#brandId option:selected").val();
       var $productName = jQuery("#productName").val();
       var $isActive = jQuery('#isActive').is(":checked") ? true : false;;
       var $description = description.getData();
       var $additionalNote = additional_note.getData();

       var model = {
           vendorId : $vendorId,
           youtubeLink : $youtubeLink,
           basePrice: $basePrice,
           keyFeatures : $keyFeatures,
           boxContent : $boxContent,
           categoryId : $categoryId,
           brandId : $brandId,
           productName : $productName,
           isActive : $isActive,
           additionalNote : $additionalNote,
           productDescription: $description,
           productIdString : productId
       };

       axios.put(`${url}Product/UpdateProduct`, model)
       .then(response => {
            if (response.data.statusCode === 200) {
                toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                    closeButton: true,
                    tapToDismiss: true,
                    rtl: false
                });
                //jQuery("#edit-category-form").find("input[type=text], input[type=email], textarea").val("");
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
       jQuery(this).attr('data-toggle','pill');
       window.location.href = "#ad-info";
        
    });

    jQuery("#submitspec").on('click',function(e){
        e.preventDefault();
        var productId = getUrlParameter("pid");
       var model = {
        mainMaterial: jQuery("#mainMaterial").val(),
        colorFamily: jQuery("#colorFamily").val(),
        dimension: jQuery("#dimension").val(),
        weight: jQuery("#weight").val(),
        model: jQuery("#model").val(),
         productType: jQuery("#type").val(),
         productIdString: productId
       };

       axios.patch(`${url}Product/PatchProductSpecification`, model)
       .then(response => {
            if (response.data.statusCode === 200) {
                toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                    closeButton: true,
                    tapToDismiss: true,
                    rtl: false
                });
                //jQuery("#edit-category-form").find("input[type=text], input[type=email], textarea").val("");
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
       jQuery(this).attr('data-toggle','pill');
       window.location.href = "#ad-info";
        
    });

    jQuery("#submitvariation").on('click',function(e){
        e.preventDefault();
        var productId = getUrlParameter("pid");
       var model = { 
        Variation: jQuery("#variation").val(),
        SKU: jQuery("#sku").val(),
        IMEI: jQuery("#imei").val(),
        Quantity: jQuery("#quantity").val(),
        SellingPrice: jQuery("#selling_price").val(),
        DiscountPrice: jQuery("#discount_price").val(),
        Id : jQuery("#variationId").val(),
        productIdString: productId
       };

       axios.patch(`${url}Product/PatchProductVariation`, model)
       .then(response => {
            if (response.data.statusCode === 200) {
                toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                    closeButton: true,
                    tapToDismiss: true,
                    rtl: false
                });
                //jQuery("#edit-category-form").find("input[type=text], input[type=email], textarea").val("");
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
       jQuery(this).attr('data-toggle','pill');
       window.location.href = "#ad-info";
        
    });

    jQuery("#submitImage").on('click',function(e){
        e.preventDefault();
        var productId = getUrlParameter("pid");
        var imageType = jQuery('#imageType').val();
        var _files = jQuery(`#imageUpload`)[0].files[0];
        var formData = new FormData();
        formData.append('ProductIdString', productId);
        formData.append('ImageType', imageType);
        formData.append('uploadImage', _files);

        axios.patch(`${url}Product/PatchProductImage`, formData)
           .then(response => {
                if (response.data.statusCode === 200) {
                    toastr['success'](`👋 ${response.data.message}`, 'Success!', {
                        closeButton: true,
                        tapToDismiss: true,
                        rtl: false
                    });
                    jQuery("#myModalLabel1").modal('hide');
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
});

function getData(id,variation,sku,imei,qty,sp,dp){
    jQuery("#variation").val(variation);
    jQuery("#sku").val(sku);
    jQuery("#imei").val(imei)
    jQuery("#quantity").val(qty);
    jQuery("#selling_price").val(sp);
    jQuery("#discount_price").val(dp);
    jQuery("#variationId").val(id);
}