//Global error handler for dataSource
kendo.data.DataSource.prototype.options.error = function (e) {
  alert("Global Datasource Error Handler. Errors: " + e.errors);
};

var CONST_DLGNAME = "dlg_";
var CONST_DLGCONTAINERNAME = "dlgContainer_";
var CONST_DLGCLOSEBTNNAME = "btnClose_";
var CONST_DLGADDCLOSEBTN = false;
/**
 * Use to determine if Kendo UI control has an event registered or not.
 * @param {Kendo UI control} widget 
 * @param {string} eventName
 * @return 
 */
function numberOfHandlers(widget, eventName) {
  if (widget._events.hasOwnProperty(eventName)) {
    return widget._events[eventName].length;
  }
  return 0;
}

function getDialogName(guid) {
  return CONST_DLGNAME + guid;
}

function getDialogContainerName(guid) {
  return dlgContainerName = CONST_DLGCONTAINERNAME + guid;
}

function getDialogContainerNameFromDialog(dialogName) {
  var guid = dialogName.substring(CONST_DLGNAME.length);
  return CONST_DLGCONTAINERNAME + guid;
}

function getCloseButtonNameFromDialog(dialogName) {
  var guid = dialogName.substring(CONST_DLGNAME.length);
  return CONST_DLGCLOSEBTNNAME + guid;
}

/*
  Kendo Grid Methods
*/
function initGridMenus(e) {
  $(".gridTemplateCellMenu").each(function () {
    eval($(this).children("script").last().html());
  });
}

function validateEntityDialogAttributes(controlId, containerDiv, dialogGuid, addressActionUrl) {
  if (!controlId) {
    return "The HTML control 'id' attribute is mandatory.";
  }

  if (!dialogGuid) {
    return "The data attribute 'data-dialogguid' is mandatory.";
  }

  if (!addressActionUrl) {
    return "The data attribute 'data-actionurl' is mandatory.";
  }

  if (containerDiv) {
    if (!$('#' + containerDiv).length) {
      return "The control with id: " + containerDiv + " does not exist in the DOM.";
    }

    //Check if controlId is a child of containerDiv
    if (!$('#' + containerDiv).has('#' + controlId).length) {
      return "The control '" + controlId + "' must be a child of: " + containerDiv;
    }
  }
  return "";
}

function openKendoEntityDialog(e) {
  var controlId = this.element.prop("id");
  var mydata = this.element.data();
  var containerDiv = mydata.containerdiv;
  var dialogGuid = mydata.dialogguid;
  var dialogToFit = mydata.dialogtofit;
  var dialogTitle = mydata.dialogtitle;
  var addressActionUrl = mydata.actionurl;
  var addressActionUrlParams = mydata.actionurlparams;//important - contains any action parameters
  var modelRoutepath = mydata.modelroutepath;
  var closejscallback = mydata.closejscallback;

  if ($('#dialogId').length > 0) {
    $('#dialogId').val(dialogGuid);
  }

  var err = validateEntityDialogAttributes(controlId, containerDiv, dialogGuid, addressActionUrl);
  if (err.length > 0) {
    var message = "ERROR: openEntityDialog(e) \n" + err;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }

  kendoDialogCreate(dialogGuid, closejscallback, dialogToFit);
  kendoDialogOpen(dialogGuid, dialogTitle, addressActionUrl, addressActionUrlParams);
}

function openEntityDialog(e) {
  var controlId = $(e).attr('id');
  var mydata = $(e).data();
  var containerDiv = mydata.containerdiv;
  var dialogGuid = mydata.dialogguid;
  var dialogToFit = mydata.dialogtofit;
  var dialogTitle = mydata.dialogtitle;
  var addressActionUrl = mydata.actionurl;
  var addressActionUrlParams = mydata.actionurlparams;//important - contains any action parameters
  var modelRoutepath = mydata.modelroutepath;
  var closejscallback = mydata.closejscallback;

  var err = validateEntityDialogAttributes(controlId, containerDiv, dialogGuid, addressActionUrl);
  if (err.length > 0) {
    var message = "ERROR: openEntityDialog(e) \n" + err;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
    return;
  }

  kendoDialogCreate(dialogGuid, closejscallback, dialogToFit);
  kendoDialogOpen(dialogGuid, dialogTitle, addressActionUrl, addressActionUrlParams);
}

/*
  Kendo Dialog Methods
*/

/**
 * Create\initialise a Kendo UI Dialog.
 * @param {Guid} guid - unique Guid used to name dialog and container div
 * @param {String} closejscallback - if you want to run some logic after the dialog closes set this to the function name. 
 * @param {Bool} dialogToFit (by default the dialog will maximize to fill the screen) set to true to fit
 * @return 
 */
function kendoDialogCreate(guid, closejscallback, dialogToFit) {
  var dialogName = getDialogName(guid);
  var dlgContainerName = getDialogContainerName(guid);
  //Create dlg containerName if dosnt exist (never delete it)
  if ($('#' + dlgContainerName).length < 1) {
    $(document.body).append("<div id=" + dlgContainerName + " class='dlgContainer'></div>");
  }

  //Destroy dialog if it exists
  if ($("#" + dialogName).data("kendoWindow")) {
    $("#" + dialogName).data("kendoWindow").destroy();
  }
  //Recreate the dialog
  $("#" + dlgContainerName).append("<div id=" + dialogName + " class='dlgDialog'></div>");

  var dialog =
    $("#" + dialogName).kendoWindow({
      // width: "100%",
      title: "",
      modal: true,
      actions: ["Close", "Minimize", "Maximize"],
      visible: false,
      close: kendoDialogDeactivate,
      refresh: kendoDialogRefresh,
      error: kendoDialogError
    });

  if (dialogToFit !== true && dialogToFit !== 1)
  {
    $("#" + dialogName).data("kendoWindow").maximize();
    dialog.width = "100%";
  }

  if (closejscallback) {
    dialog.data("closejscallback", closejscallback);
  }
}

/**
 * Open a Kendo UI Dialog.
 * @param {Guid} guid - unique Guid used to name dialog and container div
 * @param {String} dialogTitle (title of dialog)
 * @param {String} url (url to load into dialog)
 * @param {String} urlData (url parameters)
 * @param {String} openDialog (false by default - if true opens dialog with no wait (if this is true then do not set refresh event)
 * @return 
 */
function kendoDialogOpen(guid, dialogTitle, url, urlData, openDialog) {
  var dialogName = getDialogName(guid);
  var dlgContainerName = getDialogContainerName(guid);
  try {
    openDialog = typeof openDialog !== 'undefined' ? openDialog : false;
    var dialog = $("#" + dialogName).data("kendoWindow");
    if (dialog == null) {
      logError('UtilsKendo.kendoDialogOpen(): The dialog ' + dialogName + ' does not exist. Unable to open it.');
      return;
    }
    dialog.title(dialogTitle);

    if (numberOfHandlers(dialog, "refresh") > 0) {
      openDialog = false;
    }
    else {
      openDialog = true;
    }

    if (openDialog != true) {
    	  showProgress();
    }

    dialog.refresh({
      url: url,
      data: urlData
    });

  }
  catch (err) {
    if (arguments != null && arguments.callee != null && arguments.callee.trace)
      logError(err, arguments.callee.trace());
    if (dlgContainerName != null && $('#' + dlgContainerName).length > 0) { hideProgress(); }
  }
}

function kendoDialogError(e) {
  var dialogName = e.sender.element[0].id;
  var dialog = $("#" + dialogName).data("kendoWindow");
  try
  {
    var errMsg = "Ajax error occured with a dialog. Status: " + e.status;
    if (e.xhr)
    {
      errMsg = errMsg + '\r\n' + e.xhr.responseText;
    }

    var dlgContainerName = getDialogContainerNameFromDialog(dialogName);
    hideProgress();
    logError(errMsg);
  }
  finally {
    dialog.destroy();
  }
}

function kendoDialogRefresh(e) {
  try {
    var dialogName = e.sender.element[0].id;
    var dlgContainerName = getDialogContainerNameFromDialog(dialogName);
    
    if (!$("#" + dialogName).length > 0) {
      logError('UtilsKendo.kendoDialogOnRefresh(): The dialog ' + dialogName + ' does not exist. Unable to refresh it.');
      return;
    }

    var dialog = $("#" + dialogName).data("kendoWindow");
    if (dialog == null) {
      logError('UtilsKendo.kendoDialogOnRefresh(): The dialog ' + dialogName + ' does not exist. Unable to refresh it.');
      return;
    }
    
  	//===========   Use this to open Dialog at sepcific place  ===============
    $("#" + dialogName).closest(".k-window").css({
  	  position: 'fixed',
  	  margin: 'auto',
  	  top: '1%'
  	});
  	dialog.open();
  	//=========================================================================
    //dialog.maximize().open();

    //Add the Close button after refresh
  	kendoDialogAddCloseButton(dialogName);

    //Hide content loading div
    if (dlgContainerName != null && $('#' + dlgContainerName).length > 0) { hideProgress(); }
  }
  catch (err) {
    if (arguments != null && arguments.callee != null && arguments.callee.trace)
      logError(err, arguments.callee.trace());
    if (dlgContainerName != null && $('#' + dlgContainerName).length > 0) { hideProgress(); }
  }
}


/**
 * Create\initialise a Kendo UI Dialog.
 * @param {string} dialogName name of the dialog
 * @param {Bool} forceAdd - force the addition of the Close button
 * @return 
 */
function kendoDialogAddCloseButton(dialogName, forceAdd)
{
  var closeBtnName = getCloseButtonNameFromDialog(dialogName);
  if (CONST_DLGADDCLOSEBTN == true || forceAdd==true) {
    $("#" + dialogName).append("<br/><button id='" + closeBtnName + "' class='k-button closeDialogBtn'>Close</button><br/>");
    $("#" + closeBtnName).click(function () {
      dialog.close();
    });
  }
}

function kendoDialogDeactivate(e) {
  var dialogName = e.sender.element[0].id;
  var dialog = $("#" + dialogName).data("kendoWindow");
  var mydata = $("#" + dialogName).data();
  var callBack = mydata.closejscallback;
  if (callBack && callBack.length > 0) {
    var fn = window[callBack];
    fn();
  }

  if (dialog == null) {
    logError('UtilsKendo.kendoDialogDeactivate(): The dialog ' + dialogName + ' does not exist. Unable to close it.');
    return;
  }
  dialog.destroy();
  clearError();

  var dlgContainerName = getDialogContainerNameFromDialog(dialogName);
  if (dlgContainerName != null && dlgContainerName.length > 0) {
  	hideProgress(dlgContainerName);
  }
}

/**
 * After the close Event sets Dialog content to "".
 * In scenarios with nested dialogs gets around problems with Kendo controls not rendering correctly.
 */
function kendoDialogEventCloseRemoveContent(e) {
  var dialogName = e.sender.element[0].id;
  var dialog = $("#" + dialogName).data("kendoWindow");
  if (dialog == null) {
    logError('UtilsKendo.kendoDialogEventCloseRemoveContent(): The dialog ' + dialogName + ' does not exist. Unable to close it.');
    return;
  }
  //Set content to nothing - in scenarios with nested dialogs gets around problems with Kendo controls not rendering correctly
  dialog.content("");
  clearError();
}

function uploadControlReset(controlId) {
  if (controlId) {
    //if an controlId is passed as a param, only reset the element's child upload controls (in case many upload wcontrolIdgets exist)
    $("#" + controlId + " .k-upload-files").remove();
    $("#" + controlId + " .k-upload-status").remove();
    $("#" + controlId + " .k-upload.k-header").addClass("k-upload-empty");
    $("#" + controlId + " .k-upload-button").removeClass("k-state-focused");
  } else {
    //reset all the upload things!
    $(".k-upload-files").remove();
    $(".k-upload-status").remove();
    $(".k-upload.k-header").addClass("k-upload-empty");
    $(".k-upload-button").removeClass("k-state-focused");
  }
}


