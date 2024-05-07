jQuery(document).ready(function() {
//     jQuery('.english-select').multipleSelect({
//   filter: true,
//   filterAcceptOnEnter: true
// });

// jQuery('.arabic-select').multipleSelect({
//     filter: true,
//     filterAcceptOnEnter: true
//   });

  jQuery('#productUnitTable').dataTable( {
    "pagingType": "simple_numbers",
    
    "columnDefs": [ {
      "targets"  : 'no-sort',
      "orderable": false,
    }]
    });

//     var start = moment().subtract(29, 'days');
// var end = moment();

// function cb(start, end) {
// jQuery('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
// }

// jQuery('#reportrange').daterangepicker({
// startDate: start,
// endDate: end,
// ranges: {
//    'Today': [moment(), moment()],
//    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
//    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
//    'This Month': [moment().startOf('month'), moment().endOf('month')],
//    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
// }
// }, cb);

// cb(start, end);

});
