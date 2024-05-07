jQuery(document).ready(function(){
    jQuery("#submitImage").on('click',function(e){ 
        e.preventDefault();
        let _subTitle = jQuery('#subtitle').val();
        let _title = jQuery('#title').val();
        let _link = jQuery('#link').val();
        let _sliderTypes = jQuery('#imageType option:selected').val();
        var _files = jQuery(`#imageUpload`)[0].files[0];
        var formData = new FormData();
        formData.append('title', _title);
        formData.append('uploadImage', _files);
        formData.append('subTitle', _subTitle);
        formData.append('sliderTypes', _sliderTypes);
        formData.append('link', _link);

        upload(formData);
    });

    function upload(formData) {
        axios.patch(`${url}Setting/AddSlider`, formData)
            .then(response => {
                const addedUser = response.data;
                toastr['success'](`👋 ${addedUser.message}`, 'Success!', {
                    closeButton: true,
                    tapToDismiss: true,
                    rtl: false
                });
                window.setTimeout(window.location.reload(), 2000);
            })
            .catch(function(error) {
                if (error.response) {
                    toastr['error'](error.response.data.message, 'Error!', {
                        closeButton: true,
                        tapToDismiss: false,
                        rtl: false
                    });
                }
            });
    }
})
