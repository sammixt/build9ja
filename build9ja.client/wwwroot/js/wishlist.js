var addToWishList = function (productId) {
    var wishList = {
        productIdString: productId
    }
    jQuery.ajax({
        type: 'POST',
        url: `${urls}Wishlist/CreateWishList`,
        // headers: {
        //     Authorization: 'Bearer ' + authItem.token
        // },
        data: JSON.stringify(wishList),
        contentType: "application/json;charset=utf-8",
        traditional: true,
    }).done(function (response) {
        notificationWishlist(response.message);
        getCartCount();

    })
        .fail(function (data) {
            notificationWishlist(data.responseJSON.message);
        });
}

var removeFromWishList = function (productId) {
    var wishList = {
        productIdString: productId
    }
    jQuery.ajax({
        type: 'DELETE',
        url: `${urls}Wishlist/DeleteWishList`,
        // headers: {
        //     Authorization: 'Bearer ' + authItem.token
        // },
        data: JSON.stringify(wishList),
        contentType: "application/json;charset=utf-8",
        traditional: true,
    }).done(function (response) {
        notificationWishlist(response.message);
        window.location.reload;

    })
        .fail(function (data) {
            notificationWishlist(data.responseJSON.message);
        });
}

var getWishlistCount = function(){
    axios.get(`${urls}Wishlist/WishListCount`)
       .then(response => {
        jQuery(".wishlist-count").text(response.data.count);
       })
       .catch(function (error) {
        processErrors(error)
       });
}

jQuery(document).ready(function(){
    getWishlistCount();
})