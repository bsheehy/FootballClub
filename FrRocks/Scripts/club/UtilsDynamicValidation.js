/*
 * Use this to Validate a know form by name in javascript.
 * To Use:
  var valid = $('#contentForm')
          .updateValidation()
          .validate();
        if (!valid.form()) {
          return;
        }
 */
(function ($) {

  $.fn.updateValidation = function () {
    var $this = $(this);
    var form = $this.closest("form")
        .removeData("validator")
        .removeData("unobtrusiveValidation");

    $.validator.unobtrusive.parse(form);

    return $this;
  };
})(jQuery);






/*
 * Unobtrusive validation in partial view
 * To Use:
    $.validator.unobtrusive.addValidation("#myform")
 */
//(function ($) { 
//  $.validator.unobtrusive.addValidation = function (selector) { 
//    //get the relevant form 
//    var form = $(selector); 
//    // delete validator in case someone called form.validate()
//    $(form).removeData("validator"); 
//    $.validator.unobtrusive.parse(form); 
//  })(jQuery);
