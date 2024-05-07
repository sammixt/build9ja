jQuery(document).ready(function(){
    jQuery("#continue").click(function(){
       var state= jQuery("#state option:selected").val();
        var city =jQuery("#lga option:selected").val();
        axios.get(`${urls}Shop/GetShipping?state=${state}&city=${city}`)
        .then(response => {
         jQuery("#sub_total").text('₦'+response.data.subTotal);
         jQuery("#shipping").text('₦'+response.data.shippingCost);
         jQuery("#total").text('₦'+response.data.total);
        })
        .catch(function (error) {
         processErrors(error)
        });
    })

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
});