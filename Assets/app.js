 
       //$(document).ready(function () {
       //    $('#records').DataTable({
       //        dom: 'C<"clear">lfrtip'

       //    });
//});




       $(document).ready(function () {
           var table = $('#records').DataTable({
               dom: 'C<"clear">lfrtip',
               //dom: '<"fcstTableWrapper"t>lp', //show page limit at the bottom
               scrollY: "850px",
               scrollX: true,
               scrollCollapse: true,
               paging: true
           });
           //new $.fn.dataTable.FixedColumns(table);

           $(".modalLink").click(function () {
               var passedId = $(this).data('id');
               $("#id").val(passedId);
               $('#comments').val(""); //clear the textarea everytime modal popsup
                
               $(".modal-body .hiddenid").val(passedId);
           }); 

           $(".modalLink2").click(function () {
               var passedID = $(this).data('id');
               $('#id').val(passedID);
               $(".modal-body .hiddenid2").val(passedID);
           });

           $('#SELECTED_COMPANY').change(function () {
               $('#form_dropdown_select_company').submit()
               return false;
           });

           var selected_company = $('#SELECTED_COMPANY').val();
           if (selected_company == '') {
               $('#btn_approve_all').prop('disabled', true); //disable 
           }
           else
           {
               $('#btn_approve_all').prop('disabled', false); //enable
           }

           $('#btn_approve_all').change(function () {
               $('#form_btn_approve_all').submit()
               return false;
           });
           

           $('#selection').bind('change', function () {
               var selectedValue = $('#selection').val();
               $.post('@Url.Action("SelectCompany", "Home")', { selection: selectedValue }, function (data) { 
                   var form = $(this);
                   form.submit();
               });
           }); 
           
         
           $("#form_dropdown_select_company").submit(function (e) {
               $("#myLoadingElement").show();
           });
           


       });
 