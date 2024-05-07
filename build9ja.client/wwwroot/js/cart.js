var basket = {};
const basketItems = [];
    const basketItem = {
        id: 0,
        productName: '',
        price: 0,
        quantity: 0,
        pictureUrl: '',
        brand: '',
        category: '',
        productIdString:'',
        variationId: 0,
        variationName: '',
        sku: ''
    };

function addToCart(productIdString){
    var qty = document.getElementById("quantity");
    var quantity =  (qty === null) ? 1 : qty.value;
       
    axios.get(`${urls}api/product?pid=${productIdString}`)
            .then(response => {
                var d = response.data;
                //console.log(d);
                //console.log(d.productVariations.length);
                if(d.productVariations.length === 1){
                    var variables = d.productVariations[0];
                    storeCart(d.productName,variables.sellingPrice,d.productImage.mainImage,d.category,
                        d.brand,d.productIdString,variables.id,variables.variation,variables.sku,quantity);
                }else{
                    var variations = jQuery("#variations");
                    variations.empty();
                    jQuery.each(d.productVariations,function(k,v){
                        variations.append(
                            `<tr class="d-flex">
                                <td class="col-12 col-md-4">
                                    <div class="item-detail">
                                        <h4>${v.variation}
                                        </h4>
                                        <div class="item-attributes">${d.productName}</div>
                                    </div>
                                </td>
                                <td class="col-12 col-md-3 item-price">₦${v.sellingPrice}</td>
                                <td class="col-12 col-md-3 justify-content-center" >
                                <form class="input-group-control"
                                     data-price="${v.sellingPrice}"
                                     data-image="${d.productImage.mainImage}"
                                     data-category="${d.category}"
                                     data-product-name="${d.productName}"
                                     data-brand="${d.brand}"
                                     data-product="${d.productIdString}"
                                     data-variation="${v.id}"
                                     data-variation-name="${v.variation}"
                                     data-sku="${v.sku}">

                                    
                                    <input type="text" class="quantity" name="quantity" readonly class="form-control" maxlength="2" value="0" size="2">
                                    <span class="input-group-btn">
                                    <button type="button" value="quantity3" class="quantity-plus btn btn-outline-secondary" data-type="plus" data-field="">
                                        <i class="fa fa-plus-circle"></i>
                                    </button>
                                    <button type="button" value="quantity3" class="quantity-minus btn btn-outline-secondary" data-type="minus" data-field="">
                                    <i class="fa fa-minus-circle"></i>
                                    </button>
                                    </span> 
                                </form>
                                </td>
                            </tr>`
                        );
                        jQuery("#variationModal").modal('show');
                    })
                }
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
}


function storeCart(productName,price,image,category,brand,productId,varId,variation,sku,_quantity){
    basketItem.productName = productName;
    basketItem.price = price;
    basketItem.quantity = _quantity;
    basketItem.pictureUrl = image;
    basketItem.category = category;
    basketItem.brand = brand;
    basketItem.productIdString = productId;
    basketItem.variationId = varId;
    basketItem.variationName = variation;
    basketItem.sku = sku
    callApi('addtocart', basketItem,notificationCart());
    getCartCount();
}

var callApi = function (endpoint, _data, callback) {

       axios.post(`${urls}api/cart/${endpoint}`, _data)
       .then(response => {
            if (response.data.statusCode === 200) {
                callback();
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
}



var checkCartItem = function(){
    var tempCart = JSON.parse(localStorage.getItem("cart")); 
    if(tempCart.items.length === 0){
        localStorage.removeItem('cart');
    }
}

var getCartCount = function(){
    axios.get(`${urls}api/cart/cartcount`)
       .then(response => {
        jQuery(".cart-count").text(response.data.count);
       })
       .catch(function (error) {
        processErrors(error)
       });
}



jQuery(document).ready(function(){
    getCartCount();
    jQuery('#variations').on('click','.quantity-plus',function(e){
        jQuery(".checkout-continue").prop("disabled",true);
        // Stop acting like a button
        e.preventDefault();
        var inputTag = jQuery(this).parent().parent('form');
        var price = inputTag.attr('data-price');
        var image = inputTag.attr('data-image');
        var category = inputTag.attr('data-category');
        var brand = inputTag.attr('data-brand');
        var product = inputTag.attr('data-product');
        var productName = inputTag.attr('data-product-name');
        var variation = inputTag.attr('data-variation');
        var variationName = inputTag.attr('data-variation-name');
        var sku = inputTag.attr('data-sku');

        storeCart(productName,price,image,category,
            brand,product,variation,variationName,sku,1)// add cart
        // Get the field name
        var inputTag = jQuery(this).parent().parent().find('input');
        var quantity = parseInt(inputTag.val());
            
        inputTag.val(quantity + 1);
        jQuery(".checkout-continue").prop("disabled",false);
        // Increment
        
    });

    jQuery('#variations').on('click','.quantity-minus',function(e){
        // Stop acting like a button
        e.preventDefault();
        var inputTag = jQuery(this).parent().parent('form');
        var variation = inputTag.attr('data-variation');
        basketItem.variationId = variation;
        callApi('reducecart', basketItem,notificationCart());
        getCartCount();
        // Get the field name
        var inputTag = jQuery(this).parent().parent().find('input');
        var quantity = parseInt(inputTag.val());
        if(quantity>0){
            inputTag.val(quantity - 1);
        }
    });

    jQuery('#cart-table').on('click','.quantity-plus',function(e){
       // jQuery(".checkout-continue").prop("disabled",true);
        // Stop acting like a button
        e.preventDefault();
        var inputTag = jQuery(this).parent().parent('form');
        var price = inputTag.attr('data-price');
        var image = inputTag.attr('data-image');
        var category = inputTag.attr('data-category');
        var brand = inputTag.attr('data-brand');
        var product = inputTag.attr('data-product');
        var productName = inputTag.attr('data-product-name');
        var variation = inputTag.attr('data-variation');
        var variationName = inputTag.attr('data-variation-name');
        var sku = inputTag.attr('data-sku');

        storeCart(productName,price,image,category,
            brand,product,variation,variationName,sku,1)// add cart
        // Get the field name
        var inputTag = jQuery(this).parent().parent().find('input');
        var currrentId = inputTag.attr("id");
        var quantity = parseInt(inputTag.val());
        var td = jQuery(this).parent().parent().parent('td');
        var nextTd = td.next("td");
        var newQuantity =  quantity + 1;
        nextTd.text(parseFloat(price) * newQuantity)
        jQuery(`#${currrentId}`).val(newQuantity);
        sum();
        sum_total();
        //inputTag.val(newQuantity);
        

       // jQuery(".checkout-continue").prop("disabled",false);
        // Increment
        
    });

    jQuery('#cart-table').on('click','.quantity-minus',function(e){
        // Stop acting like a button
        e.preventDefault();
        var inputTag = jQuery(this).parent().parent('form');
        var variation = inputTag.attr('data-variation');
        var price = inputTag.attr('data-price');
        basketItem.variationId = variation;
        callApi('reducecart', basketItem,notificationCart());
        getCartCount();
        // Get the field name
        var inputTag = jQuery(this).parent().parent().find('input');
        var currrentId = inputTag.attr("id");
        var quantity = parseInt(inputTag.val());
        var newQuantity = quantity - 1;
        var td = jQuery(this).parent().parent().parent('td');
        var tr = td.parent('tr');
        if(newQuantity>0){
            jQuery(`#${currrentId}`).val(newQuantity);
            //inputTag.val(quantity - 1);
            var nextTd = td.next("td");
            nextTd.text(parseFloat(price) * newQuantity)
        }else{
            tr.remove();
        }
        sum();
        sum_total();
    });

    function sum(){
        let sum = 0;
        jQuery("#cart-table tr").each(function(){
             sum += parseFloat(jQuery(this).find("td:eq(4)").text());
        });

        jQuery("#sub-total").text(sum);
    }

    function sum_total(){
        let sum = 0;
        let cartTotal = 0;
        jQuery("#cart-table tr").each(function(){
            cartTotal += parseFloat(jQuery(this).find("td:eq(4)").text());
       });
       var coupon = parseInt(jQuery('#coupon').text());
       total = coupon + cartTotal;
       jQuery("#total").text(total);
    }

    sum();
    sum_total();
})
