//Set defaults for all of the ajax calls
$.ajaxSetup({
	type: "POST",
	cache: false,
	global: true,//enables global event handler
	error: function (request) {
	  handleAjaxError(request);
	}
});

function kendoGridErrorHandlerRequestEnd(e) {
  hideProgress();
  this.lastRequestType = e.type;
  //if (e.type === "destroy" && e.response.Errors) {
  //  this.cancelChanges();
  //}

  //if ((e.type == "update" || e.type == "create") && e.response.Errors) {
  //  this.preventDefault();
  //}
}

//KENDO UI GRIDS: Use this method rather than 'handleAjaxError' for the Datasource Error handler events of Kendo UI Grids 
//This method will attempt to cancel any Client side changes if error occurs when CRUD in grid.
//http://www.telerik.com/blogs/handling-server-side-validation-errors-in-your-kendo-ui-grid
//http://blog.codebeastie.com/kendo-grid-error-handling/
//http://www.telerik.com/forums/grid-inline-editing-with-server-side-validation-errors-2ff29a4fc11f

// To get GridName and Handle Error use answer by 'Atanas Korchev' here http://stackoverflow.com/questions/20886651/get-a-reference-to-kendo-grid-from-inside-the-error-handler
function kendoGridErrorHandler(gridName) {
  return function (e) {
    hideProgress();
    // handle the event.
    var grid = $(gridName).data("kendoGrid");

    var requestType = this.lastRequestType;
    this.cancelChanges();

    handleAjaxError(e);
  }
}

function handleModelStateException(e) {
  hideProgress();
  if (e.errors) {
    var message = "Errors:\n";
    $.each(e.errors, function (key, value) {
      if ('errors' in value) {
        $.each(value.errors, function () {
          message += this + "\n";
        });
      }
    });
    showError(message);
    return true;
  }
  else if (isString(e.Errors)) {
    var message = e.Errors;
    showError(message);
    return true;
  }
  else {
    handleAjaxError(e);
  }
}

function kendoListViewErrorHandler(e)
{
  hideProgress();
  this.cancelChanges();

  if (e.errors) {
    var message = "Errors:\n";
    $.each(e.errors, function (key, value) {
      if ('errors' in value) {
        $.each(value.errors, function () {
          message += this + "\n";
        });
      }
    });
    showError(message);
    return true;
  }
  else {
    handleAjaxError(e);
  }
}

function handleAjaxError(modelJsonResult) {
  hideProgress();
  var errMsg = "Unknown Error.";
  if (isString(modelJsonResult)) {
    errMsg = modelJsonResult;
  }
  if (modelJsonResult.success === false && modelJsonResult.url) {
    window.location = modelJsonResult.url;
  }

  if (modelJsonResult.errors) {
    var message = "Errors:\n";
    $.each(modelJsonResult.errors, function (key, value) {
      if ('errors' in value) {
        $.each(value.errors, function () {
          message += this + "\n";
        });
      }
    });
    showError(message);
    return true;
  }

  if (modelJsonResult.ModelStateErrors) {
    var message = "Errors:\n";
    $.each(modelJsonResult.ModelStateErrors, function (key, values) {
      if (modelJsonResult.DisplayKeyInError == true) {
        message += key;
      }
      for (var i in values) {
        message += values[i] + "  ";
      }
      message += "\n"
    });
    showError(message);
    return true;
  }

  if (modelJsonResult.ModelStateErrorsAsString) {
    var message = modelJsonResult.ModelStateErrorsAsString;
    showError(message);
    return true;
  }

  if (modelJsonResult.Errors) {
    var message = "Errors:\n";
    $.each(modelJsonResult.Errors, function (key, value) {
      if ('errors' in value) {
        $.each(value.Errors, function () {
          message += this + "\n";
        });
      }
    });
    showError(message);
    return true;
  }

  if (modelJsonResult) {
    //Always check if modelJsonResult is a ajaxcontext first.
    if (modelJsonResult.responseText || modelJsonResult.xhr) {
      return handleAjaxContentError(modelJsonResult);
    }
    else if (modelJsonResult.error && typeof modelJsonResult.error != 'function') {
      errMsg = modelJsonResult.error;
    }
    else if (modelJsonResult.XMLHttpRequest && modelJsonResult.XMLHttpRequest.responseText) {
      errMsg = modelJsonResult.XMLHttpRequest.responseText;
    }
    showError(errMsg);
    if (window.logErrorAfterAjaxHandled == true) {
      logError(errMsg);
    }
  }
  return true;
}

function handleAjaxContentError(ajaxcontext) {
  if (errorTimeout != 0) {
    clearTimeout(errorTimeout);
    errorTimeout = 0;
  }
  var html = undefined;
  if (ajaxcontext == null) {
    return;
  }

  if (ajaxcontext.responseText != null) {
    html = ajaxcontext.responseText;
  }
  else {
    if (ajaxcontext.xhr != null) {
      html = ajaxcontext.xhr.responseText;
    }
  }

  if (html == null && ajaxcontext.XMLHttpRequest) {
    html = ajaxcontext.XMLHttpRequest.responseText;
  }

  if (html == null) {
    return;
  }

  var message = '';
  if (ajaxcontext.status == 400) {
    message = html;
  }
  var regx = new RegExp('<title>(.*?)</title>');
  var match = regx.exec(html);
  if (!match) {
    regx = new RegExp('ErrorMessage');
    match = regx.exec(html);
  }
  if (match) {
    message = match[1];
  }
  if (message == '' || message == null)
    //message = 'An error has occured while processing your request.';
    message = html;
  showError(message);

  if (window.logErrorAfterAjaxHandled == true) {
    logError(html);
  }
  return true;
}

/**
 * Logs a javascript error to Elmah with stack trace if applicable. 
 * Note: joel.net/logging-errors-with-elmah-in-asp.net-mvc-3--part-5--javascript
 * @param {Exception} ex
 * @param {string} stack
 * @return 
 */
function logError(ex, stack) {
  if (ex == null) return;
  if (!window.logErrorUrl) {
    var buttons = ["OK"];
    showModalAlert('Error: Global javascript variable window.logErrorUrl must be defined.', "", "", buttons);
    return;
  }

  var url = ex.fileName != null ? ex.fileName : document.location;
  if (stack == null && ex.stack != null) stack = ex.stack;

  // format output
  var out = ex.message != null ? ex.name + ": " + ex.message : ex;
  out += ": at document path '" + url + "'.";
  if (stack != null) out += "\n  at " + stack.join("\n  at ");

  if (window.logErrorAlertClient == true) {
    var message = "Error xx1xx Occured:\n" + out;
    var buttons = ["OK"];
    showModalAlert(message, "", "", buttons);
  }

  // send error message
  $.ajax({
    type: 'POST',
    url: window.logErrorUrl,
    data: { message: out }
  });
}

function showError(message) {
	$('#errorWindow')
    .data('kendoWindow')
    .open();
	$('.k-window-title')
    .replaceWith("<div class='errorWindow'></div>");
	$('#errorWindow').parent()
    .find('.k-window-titlebar,.k-window-actions')
    .css('background-color', '#B80000');
	$('.k-window-actions')
    .css({ 'color': '#B80000', 'background-color': '' });
	$('#errorMsg')
    .html(message)
    .css('color', '#FFFFFF')
    .show();

	errorTimeout = setTimeout(function () {
		clearError();
		$('#errorWindow').data('kendoWindow').close();
	}, 10000);
}

function clearError() {
	$('#errorMsg')
    .html('')
    .hide();

	if (errorTimeout != 0) {
		clearTimeout(errorTimeout);
		errorTimeout = 0;
	}
	return true;
}

function onErrorOpen() {
	$('#errorWindow')
    .data('kendoWindow')
    .center()
    .wrapper.css({ top: 95 });
	var svBtn = document.getElementById('saveBtn');
	var svSpin = document.getElementById('saveSpinner');
	if (svSpin != null) {
		// hide loading indicator
		kendo.ui.progress($('#saveSpinner'), false);
		$('#saveSpinner').css('display', 'none');
	}
	if (svBtn != null) {
		// Show Save Button
		$('#saveBtn').css('display', '');
	}
}
