$(document).ready(function() {
    $("#txtPolicyNumber").blur(function(e) {
        e.preventDefault();
        if ($("#txtPolicyNumber").val() != "") {
            RetrieveAssuredCode();
            //   RetrieveProductCode();
            //RetrieveTBIL_POLY_STATUS()
        }
    })
    $("#txtAssuredCode").blur(function(e) {
        // RetrieveInsuredDetails();
    })
    $("#cmdSearch").click(function(e) {
        e.preventDefault();
        Retrieve_Policy_Nos()
    })
    $('#cboSearch').change(function(e) {
        e.preventDefault();
        GetPolicyNoFromDpList();
    });

    $('#chkConfirmWaiver').change(function(e) {
        e.preventDefault();

        if ($(this).is(":checked")) {
            // it is checked
            GetCoverCodes();
        }
        else {
            $('#drpWaiverCodes').hide();
            $('#lblWaiverCode').hide();
            $('#lblWaiverEffDate').hide();
            $('#txtWaiverEffectiveDate').hide();
            $('#lblWaiverEffFormat').hide();
        }
    });

    $('#drpWaiverCodes').change(function(e) {
        e.preventDefault();
        if ($('#drpWaiverCodes').val() != "Select") {
            VerifyAdditionalPolicyCover();
        }
    });


    $('#txtPolicyStartDate').blur(function(e) {
        e.preventDefault();
        if ($('#txtPolicyStartDate').val() != "") {
            var res = CheckDate($('#txtPolicyStartDate').val());
            if (res == true) {
                $('#ans').text("");
                return true
            }
            else {
                $('#ans').text("Not a valid Policy start date format");
                $('#txtPolicyStartDate').focus();
                return false;
            }
        }
    });


    $('#txtPolicyEndDate').blur(function(e) {
        e.preventDefault();
        if ($('#txtPolicyEndDate').val() != "") {
            var res = CheckPolicyEndDate($('#txtPolicyEndDate').val());
            if (res == "Valid") {
                $('#ans').text("");
                return true
            }
            else if (res == "Invalid") {
                $('#ans').text("Not a valid Policy end date format");
                $('#txtPolicyEndDate').focus();
                return false;
            }
            else if (res == "Invalid1") {
                alert("Future Date")
                $('#ans').text("Policy end date must be a future date");
                $('#txtPolicyEndDate').focus();
                return false;
            }
        }
    });

    $('#txtWaiverEffectiveDate').blur(function(e) {
        e.preventDefault();
        if ($('#txtWaiverEffectiveDate').val() != "") {
            var res = CheckDate($('#txtWaiverEffectiveDate').val());
            alert(res);
            if (res == true) {
                $('#ans').text("");
                return true
            }
            else {
                $('#ans').text("Not a valid Waiver effective date format");
                $('#txtWaiverEffectiveDate').focus();
                return false;
            }
        }
    });

    //            $('#btnSave').click(function(e) {
    //                e.preventDefault();
    //              ValidateOnClient();
    //            });
});



function RetrieveAssuredCode() {
    InitializeClientControls()
    var policyNo = $("#txtPolicyNumber").val();
    //alert("This is the class code: " + document.getElementById('txtClassCod').value + " and Item code :" + document.getElementById('txtTransNum').value);
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/GetPolicyPerInfo",
        data: JSON.stringify({ _policyNo: policyNo }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_RetrieveAssuredCode,
        failure: OnFailure_RetrieveAssuredCode,
        error: OnError_RetrieveAssuredCode
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_RetrieveAssuredCode(response) {
    //debugger;

    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_AdminCodeInfoValues(admobjects);

}
// retrieve the values for branch
function retrieve_AdminCodeInfoValues(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        $('#txtAssuredCode').val($(this).find("TBIL_POLY_ASSRD_CD").text())
        $('#txtPolyStatus').val($(this).find("TBIL_POLY_STATUS").text())
        var status = $('#txtPolyStatus').val();
        if (status == "W") {
            $("#chkConfirmWaiver").prop("checked", true);
            $('#drpWaiverCodes').show();
            $('#lblWaiverCode').show();
            $('#lblWaiverEffDate').show();
            $('#txtWaiverEffectiveDate').show();
            $('#lblWaiverEffFormat').show();
            $('#txtWaiverEffectiveDate').val(formatDate($(this).find("WAIVER_DT").text()));

            GetCoverCodes()
            var waiverCode = $(this).find("WAIVERCODE").text();
            Get_Effected_Waiver_Dsc(waiverCode)
           // $('#drpWaiverCodes').select($(this).find("WAIVERCODE").text());
        }
        else {
            $('#drpWaiverCodes').hide();
            $('#lblWaiverCode').hide();
            $('#lblWaiverEffDate').hide();
            $('#txtWaiverEffectiveDate').hide();
            $('#lblWaiverEffFormat').hide();
        }
    });
    RetrieveInsuredDetails();
    RetrieveProductCode();
}
function OnError_RetrieveAssuredCode(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Error! Policy Number does not exist');
    $("#txtPolicyNumber.").focus();
}

function OnFailure_RetrieveAssuredCode(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}

function RetrieveInsuredDetails() {
    var assuredCode = $("#txtAssuredCode").val();
    //alert("This is the class code: " + document.getElementById('txtClassCod').value + " and Item code :" + document.getElementById('txtTransNum').value);
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/GetInsuredDetails",
        data: JSON.stringify({ _assuredCode: assuredCode }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_RetrieveInsuredDetails,
        failure: OnFailure_RetrieveInsuredDetails,
        error: OnError_RetrieveInsuredDetails
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_RetrieveInsuredDetails(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_RetrieveInsuredDetails(admobjects);

}
// retrieve the values for branch
function retrieve_RetrieveInsuredDetails(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        $('#txtAssuredName').
                val($(this).find("TBIL_INSRD_SURNAME").text() + '  ' + $(this).find("TBIL_INSRD_FIRSTNAME").text())
        $('#HidAssuredName').
                val($(this).find("TBIL_INSRD_SURNAME").text() + '  ' + $(this).find("TBIL_INSRD_FIRSTNAME").text())
    });

}
function OnError_RetrieveInsuredDetails(response) {
    //debugger;
    var errorText = response.responseText;
    //alert('Error! Policy Number does not exist');
    // $("#txtAssuredCode").focus();
    $("#txtPolicyNumber").focus();
    //$("#txtPolicyNumber").focus();

}

function OnFailure_RetrieveInsuredDetails(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}

function RetrieveProductCode() {
    var policyNo = $("#txtPolicyNumber").val();
    //alert("This is the class code: " + document.getElementById('txtClassCod').value + " and Item code :" + document.getElementById('txtTransNum').value);
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/GetProductCode",
        data: JSON.stringify({ _policyNo: policyNo }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_RetrieveProductCode,
        failure: OnFailure_RetrieveProductCode,
        error: OnError_RetrieveProductCode
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_RetrieveProductCode(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_RetrieveProductCode(admobjects);

}
// retrieve the values for branch
function retrieve_RetrieveProductCode(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        $('#txtPolicyProCode').val($(this).find("TBIL_POL_PRM_PRDCT_CD").text())

        $('#txtPolicyStartDate').val(formatDate($(this).find("TBIL_POL_PRM_FROM").text()));
        $('#txtPolicyEndDate').val(formatDate($(this).find("TBIL_POL_PRM_TO").text()));
        RetrieveProductDetails();
    });

}
function OnError_RetrieveProductCode(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Error! Product code not found in Policy Premium Information Table');
}

function OnFailure_RetrieveProductCode(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}
function RetrieveProductDetails() {
    var policyProductCode = $("#txtPolicyProCode").val();
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/GetProductDetails",
        data: JSON.stringify({ _policyProductCode: policyProductCode }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_RetrieveProductDetails,
        failure: OnFailure_RetrieveProductDetails,
        error: OnError_RetrieveProductDetails
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_RetrieveProductDetails(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_RetrieveProductDetails(admobjects);

}
// retrieve the values for branch
function retrieve_RetrieveProductDetails(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        $('#txtProdDesc').val($(this).find("TBIL_PRDCT_DTL_DESC").text())
        $('#HidProdDesc').val($(this).find("TBIL_PRDCT_DTL_DESC").text())
        //  alert($(this).find("TBIL_INSRD_SURNAME").text())
    });

}
function OnError_RetrieveProductDetails(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Error! Prodouct code not found');
    //$("#txtPolicyProCode").focus();

}

function OnFailure_RetrieveProductDetails(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}


function GetCoverCodes() {
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/GetCoverCodes",
        //data: JSON.stringify({ param_value: param_value }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_GetCoverCodes,
        failure: OnFailure_GetCoverCodes,
        error: OnError_GetCoverCodes
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_GetCoverCodes(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_GetCoverCodes(admobjects);

}
// retrieve the values for branch
function retrieve_GetCoverCodes(admobjects) {
    //debugger;
    $('#drpWaiverCodes').empty();
    $('#drpWaiverCodes')
            .append('<option value="Select">' + "Select" + '</option>')
    $.each(admobjects, function() {
        var admobject = $(this);
        //document.getElementById("drpWaiverCodes").value = $(this).find("TBIL_COV_CD").text();
       $('#drpWaiverCodes')
            .append('<option value=' + $(this).find("TBIL_COV_CD").text() + '>' +
             $(this).find("TBIL_COV_DESC").text() + ' ; ' + $(this).find("TBIL_COV_CD").text() + '</option>')
    });
    $('#drpWaiverCodes').show();
    $('#lblWaiverCode').show();
}
function OnError_GetCoverCodes(response) {
    //debugger;
    var errorText = response.responseText;
}

function OnFailure_GetCoverCodes(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}

//function CheckDate(my, id) {
//    var d = new Date();
//    var userdate = new Date(my)
//    // var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(18|20)\d{2}$/; //mm/dd/yyyy
//     var date_regex=/^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/
//     
//     if (id == "txtPolicyStartDate") {
//         if (!(date_regex.test(my)))
//          {
//             $('#ans').text("Not a valid Policy start date format");
//             $('#txtPolicyStartDate').focus();
//            return;
//          }
//         else {
//             $('#ans').text("");
//             return true
//          }
//      }


//      if (id == "txtPolicyEndDate") {
//          if (!(date_regex.test(my))) 
//          {
//             $('#ans').text("Not a valid Policy end date format");
//             $('#txtPolicyEndDate').focus();
//             return;
//         }
//         else {
//               if (userdate <= d) {
//                 //document.getElementById("ans").innerHTML = "Date must be greater than today";
//                 $('#ans').text("Policy End Date must be greater than today");
//                 $('#txtPolicyEndDate').focus();
//                return;
//                }
//               else {
//                 $('#ans').text("");
//                 return true
//                }
//             }
//      }

//      if (id == "txtWaiverEffectiveDate") {
//          if (!(date_regex.test(my))) {
//              $('#ans').text("Not a valid Waiver Effective Date format");
//              $('#txtWaiverEffectiveDate').focus();
//             return;
//          }
//          else {
//              $('#ans').text("");
//              return true
//          }
//      }

//  }

function CheckDate(my) {
    var returnMsg;
      var d = new Date();
      var userdate = new Date(my)
      // var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(18|20)\d{2}$/; //mm/dd/yyyy
      var date_regex = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/
      if (!(date_regex.test(my))) {
          returnMsg = false;
          }
          else {
              returnMsg = true;
          }
          return returnMsg;  
  }

  function CheckPolicyEndDate(my) {
      var returnMsg;
      var d = new Date();
      var userdate = new Date(my)
      // var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(18|20)\d{2}$/; //mm/dd/yyyy
      var date_regex = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/
      if (!(date_regex.test(my))) {
          returnMsg = "Invalid";
      }
      else {
          if (userdate <= d) {
              returnMsg = "Invalid1";
          }
          else {
              returnMsg = "Valid";
          }
      }
      return returnMsg;
  }

function ValidateOnClient() {
    if ($("#txtPolicyNumber").val() == "") {
        $("#ans").text("Please enter a policy number");
        return false;
    }
    else if ($("#txtAssuredCode").val() == "") {
        $("#ans").text("Please enter a assurance code");
        return false;
    }
    else if ($("#txtPolicyProCode").val() == "") {
        $("#ans").text("Please enter policy product code");
        return false;
    }
    else if ($("#txtPolicyStartDate").val() == "") {
        $("#ans").text("Please enter policy start date");
        return false;
    }

    else if (CheckDate($('#txtPolicyStartDate').val())==false) {
                $('#ans').text("Not a valid Policy start date format");
                $('#txtPolicyStartDate').focus();
                return false;
    }
    else if ($("#txtPolicyEndDate").val() == "") {
        $("#ans").text("Please enter policy end date");
        return false;
    }
    else if (CheckPolicyEndDate($('#txtPolicyEndDate').val()) == "Invalid") {
        $('#ans').text("Not a valid policy end date format");
        $('#txtPolicyEndDate').focus();
        return false;
    }
    else if (CheckPolicyEndDate($('#txtPolicyEndDate').val()) == "Invalid1") {
    $('#ans').text("Policy end date must be a future date");
        $('#txtPolicyEndDate').focus();
        return false;
    }
    else if (!$('#chkConfirmWaiver').is(":checked")) {
        // $("#ans").text("");
        $("#ans").text("Please confirm waiver");
        return false
    }
    else if ($("#drpWaiverCodes").val() == "Select") {
        $("#ans").text("Please select a waiver code");
        return false;
    }
    else if ($("#txtWaiverEffectiveDate").val() == "") {
        $("#ans").text("Please enter waiver effective date");
        return false;
    }
    else if (CheckDate($('#txtWaiverEffectiveDate').val()) == false) {
        $('#ans').text("Not a valid waiver effective date format");
        $('#txtPolicyStartDate').focus();
        return false;
    }
    else if ($("#txtPolyStatus").val() == "") {
        $("#ans").text("Waiver cannot be process because status is null");
        return false;
    }
    else if ($("#txtPolyStatus").val() == "W") {
        $("#ans").text("Waiver has already been processed");
        return false;
    }
//    else if (CheckDate($("#txtPolicyStartDate").val(), 'txtPolicyStartDate')) {
//    return;
//    }
    else {
        return true;
        $('#drpWaiverCodes').hide();
        $('#lblWaiverCode').hide();
        $('#lblWaiverEffDate').hide();
        $('#txtWaiverEffectiveDate').hide();
        $('#lblWaiverEffFormat').hide();
    }
}

function Retrieve_Policy_Nos() {
    //var _search = $("#txtsearch").val();
    var _search = $("#txtSearch").val();
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/GetPolicyNos",
        data: JSON.stringify({ _search: _search }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_Retrieve_Policy_Nos,
        failure: OnFailure_Retrieve_Policy_Nos,
        error: OnError_Retrieve_Policy_Nos
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_Retrieve_Policy_Nos(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_Retrieve_Policy_Nos(admobjects);
}
// retrieve the values for branch
function retrieve_Retrieve_Policy_Nos(admobjects) {
    //debugger;
    $('#cboSearch').empty();
    $('#cboSearch')
            .append('<option value=' + "" + '>' + "" + ";" + "" + '</option>')

    $.each(admobjects, function() {
        var admobject = $(this);

        $('#cboSearch')
            .append('<option value=' + $(this).find("TBIL_POLY_POLICY_NO").text() + '>' +
             $(this).find("TBIL_INSRD_SURNAME").text() + ";" + $(this).find("TBIL_POLY_POLICY_NO").text() + '</option>')
        //  alert($(this).find("TBIL_INSRD_SURNAME").text())
    });

}
function OnError_Retrieve_Policy_Nos(response) {
    //debugger;
    var errorText = response.responseText;
}

function OnFailure_Retrieve_Policy_Nos(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}



function GetPolicyNoFromDpList() {
    //             $('cboSearch').change(function() {
    var details = $('#cboSearch').val();
    $('#txtPolicyNumber').val(details)
    RetrieveAssuredCode();
    //RetrieveTBIL_POLY_STATUS()
}

function VerifyAdditionalPolicyCover() {
    var _WaiverCodes = $("#drpWaiverCodes").val();
    var _PolicyNumber = $("#txtPolicyNumber").val();
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/VerifyAdditionalCover",
        data: JSON.stringify({ _WaiverCodes: _WaiverCodes, _PolicyNumber: _PolicyNumber }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_VerifyAdditionalPolicyCover,
        failure: OnFailure_VerifyAdditionalPolicyCover,
        error: OnError_VerifyAdditionalPolicyCover
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_VerifyAdditionalPolicyCover(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_VerifyAdditionalPolicyCover(admobjects);
}
// retrieve the values for branch
function retrieve_VerifyAdditionalPolicyCover(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        var MSG = $(this).find("MSG").text()
        if (MSG == "0") {
            alert("Waiver code not found in Additional policy cover table, Waiver not applicable");
            $('#lblWaiverEffDate').hide();
            $('#txtWaiverEffectiveDate').hide();
            $('#lblWaiverEffFormat').hide();
        }
        else {
            $('#lblWaiverEffDate').show()
            $('#txtWaiverEffectiveDate').show()
            $('#lblWaiverEffFormat').show()
        }
    });

}
function OnError_VerifyAdditionalPolicyCover(response) {
    //debugger;
    var errorText = response.responseText;

}

function OnFailure_VerifyAdditionalPolicyCover(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}

function InitializeClientControls() {
    $('#lblMsg').text("");
    $('#ans').text("");
    $('#txtAssuredCode').val("");
    $('#txtAssuredName').val("");
    $('#drpWaiverCodes').empty();
    $('#txtPolicyProCode').val("");
    $('#txtProdDesc').val("");
    $('#txtPolicyStartDate').val("");
    $('#txtPolicyEndDate').val("");
    $('#txtWaiverEffectiveDate').val("");
    $('#txtPolyStatus').val("");
    $("#chkConfirmWaiver").prop("checked", false);
    $('#drpWaiverCodes').hide();
    $('#lblWaiverCode').hide();
    $('#lblWaiverEffDate').hide();
    $('#txtWaiverEffectiveDate').hide();
    $('#lblWaiverEffFormat').hide();
}


function formatDate(dateValue) {
    var dateValue_array = dateValue.split('T');
    var final_dateValue_array = dateValue_array[0].split('-');
    var formattedDate = final_dateValue_array[2] + '/' +
                 final_dateValue_array[1] + '/' + final_dateValue_array[0];
    return formattedDate
}


function Get_Effected_Waiver_Dsc(waiverCode) {
    $.ajax({
        type: "POST",
        url: "Pol_Per_Det_Retrieval.aspx/GetEffectedWaiverDsc",
        data: JSON.stringify({ waiverCode: waiverCode }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_Effected_Waiver_Dsc,
        failure: OnFailure_Effected_Waiver_Dsc,
        error: OnError_Effected_Waiver_Dsc
    });
    // this avoids page refresh on button click
   return false;
}
function OnSuccess_Effected_Waiver_Dsc(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_Effected_Waiver_Dsc(admobjects);

}
// retrieve the values for branch
function retrieve_Effected_Waiver_Dsc(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        $('#HidWaiverDesc').val($(this).find("TBIL_COV_DESC").text())
        $('#HidWaiverCode').val($(this).find("TBIL_COV_CD").text())
        WaiverDes = $('#HidWaiverDesc').val().trim()
        WaiverCod = $('#HidWaiverCode').val().trim()
        $('#drpWaiverCodes').val(WaiverCod.trim());
    });
}
function OnError_Effected_Waiver_Dsc(response) {
    //debugger;
    var errorText = response.responseText;
}

function OnFailure_Effected_Waiver_Dsc(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}
