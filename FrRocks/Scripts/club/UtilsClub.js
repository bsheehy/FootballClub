var errorTimeout = 0;
emptyG = "00000000-0000-0000-0000-000000000000";
kendo.culture("en-GB");

//Does jQuery element exists e.g. if ( $('#toolbar').exists() ){
jQuery.fn.exists = function () { return jQuery(this).length > 0; }

//Search\filter by data attribute e.g. seach for a node where data-myName == 'Brendan' 
//http://stackoverflow.com/questions/4191386/jquery-how-to-find-an-element-based-on-a-data-attribute-value
$.fn.filterByData = function (prop, val) {
  return this.filter(
      function () { return $(this).data(prop) == val; }
  );
}


function disableAllControls(targetId) {
  if (targetId)
  {
    //Diasable all inputs within target dom element
    $("#" + targetId + " input").prop("disabled", true);
  }
  else {
    //Diasable all inputs
    $("input").prop("disabled", true);
  }
  
}


/* 
Ajax Forms - required for client validation on a <form> that was loaded via AJAX.
johnculviner.com/the-unobtrusive-libraries-client-validation-on-ajax-in-asp-net-mvc-3/
*/
$(function () {
  $(document).ajaxComplete(function (event, request, settings) {
    //re-parse the DOM after Ajax to enable client validation for any new form fields that have it enabled
    if ($.validator) {
      $.validator.unobtrusive.parse(document);
    }
    generateTooltips();
  });

  ////http://jvance.com/blog/2012/09/21/FixFormsAuthentication302.xhtml
  $(document).ajaxError(function (e, xhr) {
    if (xhr.status === 401) {
      location.reload();
    }
    else if (xhr.status == 403) {
      alert("You do not have enough permissions to request this resource.");
    }
  });
});

//Adds the SubMenu into the header menu (note method  )
function insertSubMenuIntoHeaderMenu(divName) {
  if (!divName) {
    divName = "headerSubMenu";
  }
  if ($("#headerSubMenuContainer").length > 0) {
    var subMenuTd = $("#headerSubMenuContainer");
    subMenuTd.prepend($("#" + divName));
  }
}

function generateTooltips(width, position) {
  if (!width) {
    width = 250;
  }
  if (!position) {
    position = "top";
  }
  var tooltip = $(".mouseChangeHelp, .dfw-IsMergeField").kendoTooltip({
    width: width,
    position: position
  }).data("kendoTooltip");
}

function setTitle(title) {
  if (title) {
    $('#titleTxt').text(title);
  }
  else {
    var t = $('#title').val();
    //if (!t) {
    //  t = "Dashboard";
    //}
    $('#titleTxt').text(t);
  }
}

//used instead of js alerts
// message - message you want to display on window, 
// callback - method you want to call after a response has been made
// parameters - any data that needs carried across to the response method, 
// buttons - buttons you want to appear on the window must be passed in as an array (e.g. buttons = ["OK"])
function showModalAlert(message, callback, parameters, buttons) {
  var template = '#confirmationTemplate';
  var dfd = new jQuery.Deferred();
  var result = false;
  var fn = null;
  if (callback != "" || callback != null) {
    fn = window[callback];
  }
  var buttonResponse = null;
  var win = $("<div id='popupWindow'></div>")
  .appendTo("body")
  .kendoWindow({
    width: "400px",
    modal: true,
    title: "",
    visible: false,
    close: function (e) {
      this.destroy();
      dfd.resolve(result);
      if (fn != null) {
        dfd.done(fn(buttonResponse, parameters));
      }
    }
  }).data('kendoWindow');

  win.content($(template).html())
  win.center().open();

  $('.popupMessage').html(message);

  if (buttons != null) {
    if (buttons.length > 0) {
      var button = "";
      for (var i = 0; i < buttons.length ; ++i) {
        button = buttons[i];
        $('.dialog_buttons').append(' <input id="' + button + '" type="button" class=" k-button" value="' + button + '" style="width: 70px" />');
        $('#' + button).val(button);
      }
    }
  }

  $("#popupWindow .k-button").click(function () {
    buttonResponse = this.id
    $('#popupWindow').data('kendoWindow').close();
  });

  return dfd.promise();
};

function isString(x) {
  return Object.prototype.toString.call(x) === "[object String]"
}

/* 
  Page content changed
*/
function IsPageDirty() {
  if (window.isDirty) {
    var ignoreChanges = confirm("You have unsaved changes. Continue anyway?");
    return (ignoreChanges);
  }
  return true;
}

function enableSaveBtn(e, kendoBtn) {
  if (!e) e = false;
  if (!kendoBtn) kendoBtn = 'frmSubmit';

  if ($("#" + kendoBtn).length) {
    $("#" + kendoBtn).data("kendoButton").enable(true);
  }
}

function clickHiddenSubmit() {
  $("#hiddenSubmit").click();
}

function showProgress() {
  kendo.ui.progress($('#progressDiv'), true);
  $('.k-loading-mask').css('z-index', '50000');
  $('.k-loading-mask').css('position', 'fixed');
}

function hideProgress() {
  kendo.ui.progress($('#progressDiv'), false);
}

/*
Entity Searches: e.g. Search Premises, Search Person, IPlan etc..
*/
function searchGetParams(searchCriteriaDiv) {
  var data = {};
  if (!searchCriteriaDiv) {
    searchCriteriaDiv = "#searchCriteria";
  }

  //Check prependRoute starts with #
  if (searchCriteriaDiv[0] != '#') {
    searchCriteriaDiv = '#' + searchCriteriaDiv;
  }

  $(searchCriteriaDiv)
    .find('input, select')
    .not(':input[type=button], :input[type=submit]')
    .each(function () {
      var value;
      var name = $(this).attr("id");
      //Check if control is multiselect
      if ($("#" + name).data("role") != undefined && $("#" + name).data("role") == "multiselect") {
        var multiselect = $("#" + name).data("kendoMultiSelect");
        value = multiselect.value().toString();
      }
      else {
        value = this.value;
      }
      data[name] = value;
    });

  return data;
}

//Sets the Grid results count label
function searchOnChange(e) {
  hideProgress();
  if (e && e.sender) {
    var dataSource = e.sender;
    var c = dataSource.total();
    if ($("#searchLblListCount")) {
      $("#searchLblListCount").text(c);
    }

    if(c==0)
    {
      var buttons = ["OK"];
      showModalAlert("No Search Results", "", "", buttons);
    }
  }
}

function searchOnChangeNoWarning(e) {
  hideProgress();
  if (e && e.sender) {
    var dataSource = e.sender;
    var c = dataSource.total();
    if ($("#searchLblListCount")) {
      $("#searchLblListCount").text(c);
    }
  }
}


function searchClearSearch(containerDiv, excludeControlsArray) {
  if (!containerDiv) {
    containerDiv = "#searchCriteria";
  }

  //Check prependRoute starts with #
  if (containerDiv[0] != '#') {
    containerDiv = '#' + containerDiv;
  }

  // Clear Search Parameter
  $(containerDiv)
    .find('input, select')
    .not(':input[type=button], :input[type=submit]')
    .each(function () {
      //if control is not in exclude arrary then clear its value
      if (excludeControlsArray) {
        for (i = 0; i < excludeControlsArray.length; i++) {
          var excludeName = excludeControlsArray[i];
          if (stringsEqual(this.id, excludeName) === true) {
            //break to next iteration of each
            return true;
          }
        }
      }
      this.value = "";
      if (this.dataset.role === 'dropdownlist')
        $(this).data("kendoDropDownList").select(0);
      if (this.dataset.role === 'multiselect') {
        var multi = $(this).data("kendoMultiSelect");
        multi.value("");
        multi.input.blur();
      }
    });
}

function searchBeginSearch(searchGridCtrlName, progressDiv) {
  showProgress();

  if (!searchGridCtrlName) {
    searchGridCtrlName = 'searchGridResults';
  }

  //When doing a gird search by pressing 'Go' we need to reset page back to 0
  //NOTE: N need to call 'read' as setting the grid's page will already cause it to requery data.
  $("#" + searchGridCtrlName).data("kendoGrid").dataSource.page(0);
}

/*
 * ADDRESS
 */
function openAddressDialog(e) {
  var controlId = $(e).attr('id');
  var mydata = $(e).data();
  var containerDiv = mydata.containerdiv;
  var dialogGuid = mydata.dialogguid;
  var dialogTitle = mydata.dialogtitle;
  var addressActionUrl = mydata.actionurl;
  var modelRoutepath = mydata.modelroutepath;

  var parentDialog = getDialogName(dialogGuid);
  $('#dialogId').val(parentDialog);

  var err = validateDialogAttributes(controlId, containerDiv, dialogGuid, addressActionUrl);
  if (err.length) {
    var message = "ERROR: openAddressDialog(e) \n" + err;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }

  if (!modelRoutepath) {
    $(e).data('modelroutepath', 'Address');
  }

  kendoDialogCreate(dialogGuid);

  if (addressActionUrl.indexOf("AddressSource") != -1) {
    if (!dialogTitle) {
      dialogTitle = "Select Source Address";
    }
    //Never need to send Oid to SourceAddress because we NEVER edit these addresses only CREATE them.
    kendoDialogOpen(dialogGuid, dialogTitle, addressActionUrl, { controlId: controlId, addressSelect: true, addressClone: true });
  }
  else {
    if (!dialogTitle) {
      dialogTitle = "Address";
    }
    //Get the AddressOid from the container control.
    var oid = $("#" + containerDiv).find("#" + modelRoutepath).val();
    if (oid) {
      kendoDialogOpen(dialogGuid, dialogTitle, addressActionUrl, { Oid: oid, controlId: controlId, addressSelect: true, addressClone: true });
    }
  }
}

function setAjaxAddAddress(dataItem) {
  var addressControlId = $("#AddressControlId").val();
  setAjaxAddressOnCallingPage(dataItem, addressControlId);
}

function setAjaxEditAddress(dataItem) {
  var addressControlId = $("#AddressControlId").val();
  setAjaxAddressOnCallingPage(dataItem, addressControlId);
}

function setAjaxAddressOnCallingPage(dataItem, addressControlId) {
  var mydata = $("#" + addressControlId).data();
  var prependModel = mydata.modelroutepath;
  var addressContainerDiv = mydata.containerdiv;
  var parentDialog = getDialogName(mydata.dialogguid);

  setAddressSingleLine(prependModel, dataItem, parentDialog, addressContainerDiv, addressControlId);
}

function searchGridAddressSelect(e) {
  var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
  var controlId = $("#AddressControlId").val();
  var mydata = $("#" + controlId).data();
  var prependModel = mydata.modelroutepath;
  var containerDiv = mydata.containerdiv;
  var parentDialog = getDialogName(mydata.dialogguid);

  setAddressSingleLine(prependModel, dataItem, parentDialog, containerDiv, controlId);
}

//Unlike 'setAddressSourceSingleLine' this set the Oid.
function setAddressSingleLine(prependRoute, dataItem, dialogName, addressContainerDiv, addressControlId) {
  //clear existing values
  clearChildCtrls(addressContainerDiv);

  if (!prependRoute) {
    prependRoute = "#Address";
  }

  //Check prependRoute starts with #
  if (prependRoute[0] != '#') {
    prependRoute = '#' + prependRoute;
  }

  if (addressContainerDiv) {
    //Check personContainerDiv starts with #
    if (addressContainerDiv[0] != '#') {
      addressContainerDiv = '#' + addressContainerDiv;
    }
    //Vheck container div for controls
    prependRoute = addressContainerDiv + ' ' + prependRoute;
  }
  $(prependRoute).val(dataItem.Oid).change();

  if (dataItem.AddressSourceType != null) {
    $(prependRoute + "_AddressSourceType_Name").val(dataItem.AddressSourceType.Name);
  }
  else {
    $(prependRoute + "_AddressSourceType_Name").val("");
  }
  $(prependRoute + "_SingleLine").val(dataItem.SingleLine);
  $(prependRoute + "_AllDetails").val(dataItem.AllDetails);

  //close the parent dialog if necessary
  if (dialogName != null && $("#" + dialogName).length > 0) {
    $("#" + dialogName).data("kendoWindow").close();
  }

  //After setting all properties - need to call javascript callBack if it was defined in the calling control
  if (addressControlId) {
    var callBack = $("#" + addressControlId).data('jscallback');
    if (callBack && callBack.length > 0) {
      eval(callBack);
    }
  }
}

function searchGridAddressClone(e) {
  var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
  selectAddressClone(dataItem); //This has to be defined in the calling page
}

/*
 * PERSON
 */

function openPersonDialog(e) {
  var controlId = $(e).attr('id');
  var mydata = $(e).data();
  var containerDiv = mydata.containerdiv;
  var dialogGuid = mydata.dialogguid;
  var dialogTitle = mydata.dialogtitle;
  var addressActionUrl = mydata.actionurl;
  var modelRoutepath = mydata.modelroutepath;

  var err = validateDialogAttributes(controlId, containerDiv, dialogGuid, addressActionUrl);
  if (err.length > 0) {
    var message = "ERROR: openPersonDialog(e) \n" + err;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }

  if (!modelRoutepath) {
    modelRoutepath = "Person";
  }

  kendoDialogCreate(dialogGuid);

  if (!dialogTitle) {
    dialogTitle = "Person";
  }

  //Get the PersonOid from the container control. NOTE: Person Oid is NOT required.
  var oid = $("#" + containerDiv).find("#" + modelRoutepath).val();
  kendoDialogOpen(dialogGuid, dialogTitle, addressActionUrl, { Oid: oid, personSelect: true, controlId: controlId });

}

function setAjaxAddPerson(dataItem) {
  var controlId = $("#PersonControlId").val();
  setAjaxPersonOnCallingPage(dataItem, controlId);
}

function setAjaxPersonOnCallingPage(dataItem, personControlId) {
  var mydata = $("#" + personControlId).data();
  var prependModel = mydata.modelroutepath;
  var addressContainerDiv = mydata.containerdiv;
  var setPerson = mydata.setperson;
  var parentDialog = getDialogName(mydata.dialogguid);

  setPersonSingleLine(prependModel, dataItem, parentDialog, addressContainerDiv, personControlId, setPerson);
}

//This function is used to clear the Person details for the following Person Views:_ViewSingleLine.cshtml , _ViewSingleLineAll.cshtml
function clearPersonSingleLine(prependRoute) {
  if (prependRoute == null) {
    prependRoute = "Person";
  }
  $("#" + prependRoute).val("").change();
  $("#" + prependRoute + "_SingleLine").val("");
  $("#" + prependRoute + "_AllDetails").val("");
  $("#" + prependRoute + "_Address_SingleLine").val("");
  $("#" + prependRoute + "_Address_AllDetails").val("");
}

//Use 'personContainerDiv' if there are more than 1 Person entry on same form so we know which one to update
//or there are multiple dialogs. setPerson default == True
function setPersonSingleLine(prependRoute, dataItem, dialogName, personContainerDiv, personControlId, setPerson) {
  //clear existing values
  clearChildCtrls(personContainerDiv);
  var isSetPerson = true;

  if (setPerson && (setPerson.toLowerCase() == 'false')) {
    isSetPerson = false;
  }

  if (isSetPerson == true) {
    if (!prependRoute) {
      prependRoute = "#Person";
    }

    //Check prependRoute starts with #
    if (prependRoute[0] != '#') {
      prependRoute = '#' + prependRoute;
    }

    if (personContainerDiv) {
      //Check personContainerDiv starts with #
      if (personContainerDiv[0] != '#') {
        personContainerDiv = '#' + personContainerDiv;
      }
      //Vheck container div for controls
      prependRoute = personContainerDiv + ' ' + prependRoute;
    }

    $(prependRoute).val(dataItem.Oid).change();
    $(prependRoute + "_SingleLine").val(dataItem.SingleLine).attr('readonly', true);
    $(prependRoute + "_AllDetails").val(dataItem.AllDetails).attr('readonly', true);

    if (typeof (dataItem.Address) == 'object' && dataItem.Address != null) {
      $(prependRoute + "_Address").val(dataItem.Address.AddressOid).attr('readonly', true);
      $(prependRoute + "_Address_SingleLine").val(dataItem.Address.SingleLine).attr('readonly', true);
    }
    else {
      if (dataItem.AddressSingleLine != null) {
        $(prependRoute + "_Address").val(dataItem.AddressOid).attr('readonly', true);
        $(prependRoute + "_Address_SingleLine").val(dataItem.AddressSingleLine).attr('readonly', true);
      }
      else {
        $(prependRoute + "_Address").val("").attr('readonly', true);
        $(prependRoute + "_Address_SingleLine").val("").attr('readonly', true);
      }
    }

  }

  //close the parent dialog if necessary
  if (dialogName != null && $("#" + dialogName).length > 0) {
    $("#" + dialogName).data("kendoWindow").close();
  }

  //After setting all properties - need to call javascript callBack if it was defined in the calling control
  if (personControlId) {
    var callBack = $("#" + personControlId).data('jscallback');
    if (callBack && callBack.length > 0) {
      var fn = window[callBack];
      fn(dataItem);
    }
  }
}

//Copies Person detals from one read-only section to another
function copyPersonBtnClick(e) {
  var controlId = $(e).attr('id');
  var mydata = $(e).data();
  var toContainerDiv = mydata.containerdiv;
  var toModelRoutepath = mydata.modelroutepath;
  var fromContainerDiv = mydata.containerdivcopyfrom;
  var fromModelRoutepath = mydata.modelroutepathcopyfrom;

  copyPerson(toContainerDiv, toModelRoutepath, fromContainerDiv, fromModelRoutepath);
}

function copyPersonDropDownChange(e) {
  //var controlId = this.element.attr("id");
  var mydata = this.element.data();
  var toContainerDiv = mydata.containerdiv;
  var toModelRoutepath = mydata.modelroutepath;

  var dataItem = this.dataItem(e.item);
  if (dataItem.Value == "0")
    return;
  var fromControlId = dataItem.Value;
  var fromData = $('#' + fromControlId).data();
  var fromContainerDiv = fromData.containerdiv;
  var fromModelRoutepath = fromData.modelroutepath;

  copyPerson(toContainerDiv, toModelRoutepath, fromContainerDiv, fromModelRoutepath);
}

function copyPerson(toContainerDiv, toModelRoutepath, fromContainerDiv, fromModelRoutepath) {
  if (!toContainerDiv) {
    var message = "The 'data-containerdiv' is required to copy Person from one enity to another";
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }
  if (!toModelRoutepath) {
    var message = "The 'data-modelroutepath' is required to copy Person from one enity to another";
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }
  if (!fromContainerDiv) {
    var message = "The 'data-containerdivcopyfrom' is required to copy Person from one enity to another";
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }
  if (!fromModelRoutepath) {
    var message = "The 'data-modelroutepathcopyfrom' is required to copy Person from one enity to another";
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }

  //Dont copy if these are the same
  if (toContainerDiv === fromContainerDiv)
    return;

  //Check that both search params starts with #
  if (toModelRoutepath[0] != '#') {
    toModelRoutepath = '#' + toModelRoutepath;
  }
  if (fromModelRoutepath[0] != '#') {
    fromModelRoutepath = '#' + fromModelRoutepath;
  }
  if (toContainerDiv[0] != '#') {
    toContainerDiv = '#' + toContainerDiv;
  }
  if (fromContainerDiv[0] != '#') {
    fromContainerDiv = '#' + fromContainerDiv;
  }
  toModelRoutepath = toContainerDiv + ' ' + toModelRoutepath;
  fromModelRoutepath = fromContainerDiv + ' ' + fromModelRoutepath;

  ///Copy data
  $(toModelRoutepath).val($(fromModelRoutepath).val()).change();
  $(toModelRoutepath + "_SingleLine").val($(fromModelRoutepath + "_SingleLine").val()).attr('readonly', true);
  $(toModelRoutepath + "_AllDetails").val($(fromModelRoutepath + "_AllDetails").val()).attr('readonly', true);
  $(toModelRoutepath + "_Address").val($(fromModelRoutepath + "_Address").val()).attr('readonly', true);
  $(toModelRoutepath + "_Address_SingleLine").val($(fromModelRoutepath + "_Address_SingleLine").val()).attr('readonly', true);
}

function personContactTypeChange(e) {
  var dataItem = this.dataItem(e.item);
  if (dataItem.Value && dataItem.Value.toLowerCase() === "o") {
    $('#personOrgName').show();
  }
  else {
    $('#personOrgName').hide();
  }
}

/*
 * =============================================
 * Team Methods
 * =============================================
 */

function refreshTeamMemberGrid() {
  $('#gridTeamMemberGrid').data('kendoGrid').dataSource.read();
}

function selectTeamMember(e) {
  var row = $(e.currentTarget).closest("tr");
  var gridId = this.element.attr("id");
  var dataItem = this.dataItem(row);
  var personOid = dataItem.Oid;
  var teamOid = $('#TeamOid').val();
  var url = $('#SaveTeamMemberUrl').val();
  try {
    $.ajax({
      type: "POST",
      url: url,
      data: {
        'personOid': personOid,
        'teamOid': teamOid
      },
      success: function (result) {
        if (result.success === true) {
          var message = "Team Member added.";
          var buttons = ["OK"];
          showModalAlert(message, "", "", buttons);
          //Remove the row
          var grid = $('#' + gridId).data("kendoGrid");
          grid.removeRow(row);
        }
        else {
          handleModelStateException(result);
        }
      },
      error: function (request) {
        hideProgress();
        handleModelStateException(request);
      },
      beforeSend: function () {
        showProgress();
      },
      complete: function () {
        hideProgress();
      }
    });
  }
  catch (e) {
    var message = e.description;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
  }
}

function refreshTeamAdminsGrid() {
  $('#gridTeamAdminsGrid').data('kendoGrid').dataSource.read();
}

function selectTeamAdminUser(e) {
  var row = $(e.currentTarget).closest("tr");
  var gridId = this.element.attr("id");
  var dataItem = this.dataItem(row);
  var userOid = dataItem.Oid;
  var teamOid = $('#TeamOid').val();
  var url = $('#SaveTeamAdminUserUrl').val();
  try {
    $.ajax({
      type: "POST",
      url: url,
      data: {
        'userOid': userOid,
        'teamOid': teamOid
      },
      success: function (result) {
        if (result.success === true) {
          var message = "Team Admin added.";
          var buttons = ["OK"];
          showModalAlert(message, "", "", buttons);
          //Remove the row
          var grid = $('#' + gridId).data("kendoGrid");
          grid.removeRow(row);
        }
        else {
          handleModelStateException(result);
        }
      },
      error: function (request) {
        hideProgress();
        handleModelStateException(request);
      },
      beforeSend: function () {
        showProgress();
      },
      complete: function () {
        hideProgress();
      }
    });
  }
  catch (e) {
    var message = e.description;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
  }
}

function copyTeamMembers(e) {
  var copyToTeamOid = $("#Oid").val();
  var dropdownlist = $("#CopyTeamMembers").data("kendoDropDownList");
  var copyFromTeamOid = dropdownlist.value();
  var url = $("#CopyTeamMembersUrl").val();
  if (isStringValueEmpty(copyFromTeamOid) === true)
  {
    var buttons = ["OK"];
    showModalAlert("Please select a Team to copy Team Members from.", "", "", buttons);
    return;
  }

  try {
    $.ajax({
      type: "POST",
      url: url,
      data: {
        'copyToTeamOid': copyToTeamOid,
        'copyFromTeamOid': copyFromTeamOid
      },
      success: function (result) {
        if (result.success === true) {
          var message = "Team Members Copied.";
          var buttons = ["OK"];
          showModalAlert(message, "", "", buttons);
          refreshTeamMemberGrid();
        }
        else {
          handleAjaxError(result);
        }
      },
      error: function (request) {
        hideProgress();
        handleAjaxError(request);
      },
      beforeSend: function () {
        showProgress();
      },
      complete: function () {
        hideProgress();
      }
    });
  }
  catch (e) {
    hideProgress();
    var message = e.description;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
  }

}

/*
 * =============================================
 * Committee Methods
 * =============================================
 */
function refreshCommitteeMemberGrid() {
  $('#gridCommitteeMemberGrid').data('kendoGrid').dataSource.read();
}

function selectCommitteeMember(e) {
  var row = $(e.currentTarget).closest("tr");
  var gridId = this.element.attr("id");
  var dataItem = this.dataItem(row);
  var personOid = dataItem.Oid;
  var committeeOid = $('#CommitteeOid').val();
  var url = $('#SaveCommitteeMemberUrl').val();
  try {
    $.ajax({
      type: "POST",
      url: url,
      data: {
        'personOid': personOid,
        'committeeOid': committeeOid
      },
      success: function (result) {
        if (result.success === true) {
          var message = "Committee Member added.";
          var buttons = ["OK"];
          showModalAlert(message, "", "", buttons);
          //Remove the row
          var grid = $('#' + gridId).data("kendoGrid");
          grid.removeRow(row);
        }
        else {
          handleModelStateException(result);
        }
      },
      error: function (request) {
        hideProgress();
        handleModelStateException(request);
      },
      beforeSend: function () {
        showProgress();
      },
      complete: function () {
        hideProgress();
      }
    });
  }
  catch (e) {
    var message = e.description;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
  }
}

function refreshCommitteeAdminsGrid() {
  $('#gridCommitteeAdminsGrid').data('kendoGrid').dataSource.read();
}

function selectCommitteeAdminUser(e) {
  var row = $(e.currentTarget).closest("tr");
  var gridId = this.element.attr("id");
  var dataItem = this.dataItem(row);
  var userOid = dataItem.Oid;
  var committeeOid = $('#CommitteeOid').val();
  var url = $('#SaveCommitteeAdminUserUrl').val();
  try {
    $.ajax({
      type: "POST",
      url: url,
      data: {
        'userOid': userOid,
        'committeeOid': committeeOid
      },
      success: function (result) {
        if (result.success === true) {
          var message = "Committee Admin added.";
          var buttons = ["OK"];
          showModalAlert(message, "", "", buttons);
          //Remove the row
          var grid = $('#' + gridId).data("kendoGrid");
          grid.removeRow(row);
        }
        else {
          handleModelStateException(result);
        }
      },
      error: function (request) {
        hideProgress();
        handleModelStateException(request);
      },
      beforeSend: function () {
        showProgress();
      },
      complete: function () {
        hideProgress();
      }
    });
  }
  catch (e) {
    var message = e.description;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
  }
}

function refreshCommitteeMinutesGrid() {
  $('#gridCommitteeMinutesGrid').data('kendoGrid').dataSource.read();
}

function savedModelCommitteeMinute()
{
  var message = "Committee Minutes Saved";
  var buttons = ["OK"];
  showModalAlert(message, "", "", buttons);
}

function editCommitteeMinutesViewDialog(e) {
  e.preventDefault();
  //var view = e.data.commandName;
  var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
  var Oid = dataItem.Oid;
  var controlId = e.currentTarget.id;
  var mydata = $("#" + controlId).data();
  var containerDiv = mydata.containerdiv;
  var dialogGuid = mydata.dialogguid;
  var dialogToFit = mydata.dialogtofit;
  var dialogTitle = mydata.dialogtitle;
  var addressActionUrl = mydata.actionurl;
  var addressActionUrlParams = {
    committeeMinuteOid: Oid,
    controlId: controlId
  };//important - contains any action parameters
  var modelRoutepath = mydata.modelroutepath;
  var closejscallback = mydata.closejscallback;

  if ($('#dialogId').length > 0) {
    $('#dialogId').val(dialogGuid);
  }

  kendoDialogCreate(dialogGuid, closejscallback, dialogToFit);
  kendoDialogOpen(dialogGuid, dialogTitle, addressActionUrl, addressActionUrlParams);
}
  
function selectPersonMembership(e) {
  var row = $(e.currentTarget).closest("tr");
  var gridId = this.element.attr("id");
  var dataItem = this.dataItem(row);
  var personOid = dataItem.Oid;
  var membershipTypeOid = $('#MembershipTypeOid').val();
  var url = $('#SaveMembershipMemberUrl').val();

  try {
    $.ajax({
      type: "POST",
      url: url,
      data: {
        'personOid': personOid,
        'membershipTypeOid': membershipTypeOid
      },
      success: function (result) {
        if (result.success === true) {
          var message = "Membership Member added.";
          var buttons = ["OK"];
          showModalAlert(message, "", "", buttons);
          //Remove the row
          var grid = $('#' + gridId).data("kendoGrid");
          grid.removeRow(row);
        }
        else {
          handleModelStateException(result);
        }
      },
      error: function (request) {
        hideProgress();
        handleModelStateException(request);
      },
      beforeSend: function () {
        showProgress();
      },
      complete: function () {
        hideProgress();
      }
    });
  }
  catch (e) {
    var message = e.description;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
  }
}

function refreshPersonMembershipGrid() {
  $('#personMembershipGridResults').data('kendoGrid').dataSource.read();
}


/*
 * GENERAL FUNCTIONS
 * 
 */
function isStringValueEmpty(sValue) {
  if (!sValue) {
    return true;
  }
  sValue = sValue.trim();
  //checking if a string is empty, null or undefined
  if (!sValue || 0 === sValue.length) {
    return true;
  }

  return false;
}