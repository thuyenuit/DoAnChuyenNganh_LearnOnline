String.prototype.replaceAll = function (target, replacement) {
    return this.split(target).join(replacement);
};
var myNicEditor;
var isEditingAnswer;
var isEditingQuestion;
var oldQuestionTitle;
var isLogged = false;
function PostAnswer(questionId) {
    var answerContent = nicEditors.findEditor('postAnswer_' + questionId).getContent();
    var content = { questionId: questionId, content: encodeURIComponent(answerContent) };
    if (StripHtml(answerContent) != '') {
        $.ajax({
            url: POST_ANSWER_URL,
            type: "POST",
            data: content,
            traditional: true,
            success: function (data) {
                //nicEditors.findEditor('postAnswer_' + questionId).setContent('');
                //$("#answerList").prepend(data);
                //var l = $("#answerList").children("li:first");
                //l.hide().slideDown(700);
                //var totalAnswer = parseInt($("#totalAnswer").text());
                //$("#totalAnswer").text(totalAnswer + 1);
                //try {
                //    FB.XFBML.parse();
                //} catch (ex) { };
                //MathJax.Hub.Queue(["Typeset", MathJax.Hub, l.id]);
                window.location.href = window.location.href;
            }
        });
    } else {
        alert(LANGUAGE_ENTER_ANSWER_NOTICE);
    }
}
function beginEditAnswer(answerId) {
    if (!isEditingAnswer) {
        $("#edit_answer_panel_" + answerId).css("display", "");
        var url = "/answer/getanswer?id=" + answerId;
        $.getJSON(url, function (data) {
            if (data) {
                myNicEditor = new nicEditor().panelInstance("edit_answer_panel_" + answerId);
                nicEditors.findEditor("edit_answer_panel_" + answerId).setContent(data.Content);
            }
        });
        $("#edit_answer_" + answerId).css("display", "");
        $("#cancel_edit_answer_" + answerId).css("display", "");
        $("#answer_content_" + answerId).css("display", "none");
        $("#vote_container_" + answerId).css("display", "none");
        $("#answer_action_container_" + answerId).css("display", "none");
        $(".nicEdit-main").focus();
        isEditingAnswer = true;
    }
}
function cancelEditAnswer(answerId) {
    if (isEditingAnswer) {
        nicEditors.findEditor("edit_answer_panel_" + answerId).setContent('');
        myNicEditor.removeInstance("edit_answer_panel_" + answerId);
        $("#edit_answer_panel_" + answerId).css("display", "none");
        $("#edit_answer_" + answerId).css("display", "none");
        $("#cancel_edit_answer_" + answerId).css("display", "none");
        $("#answer_content_" + answerId).css("display", "");
        $("#vote_container_" + answerId).css("display", "block");
        $("#answer_action_container_" + answerId).css("display", "block");
        isEditingAnswer = false;
    }
}
function editAnswer(answerId) {
    var answerContent = nicEditors.findEditor("edit_answer_panel_" + answerId).getContent();
    var content = { answerId: answerId, content: encodeURIComponent(answerContent) };
    $.ajax({
        url: ROOT_URL + "Answer/EditAnswer",
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(content),
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data != 'null') {
                $("#answer_content_" + answerId).html(answerContent);
                nicEditors.findEditor("edit_answer_panel_" + answerId).setContent('');
                myNicEditor.removeInstance("edit_answer_panel_" + answerId);
                $("#edit_answer_panel_" + answerId).css("display", "none");
                $("#edit_answer_" + answerId).css("display", "none");
                $("#cancel_edit_answer_" + answerId).css("display", "none");
                $("#answer_content_" + answerId).css("display", "");
                $("#vote_container_" + answerId).css("display", "block");
                $("#answer_action_container_" + answerId).css("display", "block");
                isEditingAnswer = false;
            }
        }
    });
}
function PostAnswerOpenQuestion(questionId) {
    var answerContent = encodeURIComponent(nicEditors.findEditor('postAnswer_' + questionId).getContent());
    var content = { questionId: questionId, content: encodeURIComponent(nicEditors.findEditor('postAnswer_' + questionId).getContent()) };
    if (StripHtml(answerContent) != '') {
        $.ajax({
            url: POST_ANSWER_URL,
            type: 'POST',
            dataType: 'html',
            data: JSON.stringify(content),
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                if (data != 'null') {
                    window.location.href = QUESTION_URL + "-" + questionId + ".html";
                }
            }
        });
    } else {
        alert(LANGUAGE_ENTER_ANSWER_NOTICE);
    }
}
function voteAnswer(answerId, point, voteElementId, canVote) {
    if (canVote == true) {
        var url = VOTE_ANSWER_URL + "?answerId=" + answerId + "&point=" + point;
        $.getJSON(url, function (data) {
            if (data != 'null') {
                $("." + voteElementId + "_voteBtn").text("(" + data.VoteCount + ")");
                $("." + voteElementId).text(data.VoteCount);
                $("#listVotedUser_" + answerId).text(data.UserVoted);
            }
        });
    } else {
        $.fancybox.open("#userGuestCannotAccessPopup");
    }
}
function beginEditQuestion(id) {
    if (!isEditingQuestion) {
        $("#questionTitle").css("display", "none");
        $("#questionContent_edit").css("display", "");
        $("#btnSaveQuestion").css("display", "");
        $("#btnCancelEditQuestion").css("display", "");
        $("#questionTopicEdit").css("display", "");
        var url = "/question/getquestion?id=" + id;
        $.getJSON(url, function (data) {
            if (data) {
                myNicEditor = new nicEditor().panelInstance("questionContent_edit");
                nicEditors.findEditor("questionContent_edit").setContent(data.Title);
            }
        });
        isEditingQuestion = true;
    }
}
function cancelEditQuestion() {
    window.location.href = window.location.href;
}
function editQuestion(questionId) {
    var content = { questionId: questionId, questionContent: encodeURIComponent(nicEditors.findEditor('questionContent_edit').getContent()) };
    $.ajax({
        url: ROOT_URL + "Question/EditQuestion",
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(content),
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data != 'null') {
                window.location.href = window.location.href;
            }
        }
    });
}
function deleteAnswerConfirm(answerId) {
    $("#dialog-confirm").dialog({
        resizable: false,
        height: 180,
        modal: true,
        buttons: {
            "Delete this answer": function () {
                $(this).dialog("close");
                deleteAnswer(answerId);
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}
function deleteAnswer(answerId) {
    var url = ROOT_URL + "Question/DeleteAnswer" + "?answerId=" + answerId;
    $.getJSON(url, function (data) {
        if (data != 'null') {
            $("#answer_" + answerId).remove();
            var totalAnswer = parseInt($("#totalAnswer").text());
            $("#totalAnswer").text(totalAnswer - 1);
        }
    });
}
function logOut() {
    gapi.auth.signOut();
    var url = ROOT_URL + "User/LogOut";
    $.getJSON(url, function (data) {
    });
}
function loginValidate() {
    if (!checkValidEmailAddress($("#loginEmail").val())) {
        $("#loginError").text("Incorrect email!");
        return;
    }
    if ($("#loginPassword").val() == '') {
        $("#loginError").text("Password cannot be empty!");
        return;
    }
    loginEmail();
}
function loginEmail() {
    var url = Base64.encode(window.location.href);
    var content = { email: $.trim($("#loginEmail").val()), password: $.trim($("#loginPassword").val()), url: url };
    $.ajax({
        url: ROOT_URL + "User/EmailLogin",
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(content),
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data.Result) {
                if (window.location.href.toLowerCase().indexOf("user/changepassword") > -1) {
                    window.location.href = "/";
                } else {
                    window.location.href = window.location.href;
                }
            } else {
                $("#loginError").text(data.Message);
            }
        }
    });
}
function checkUserNameAvailable(username) {
    var url = ROOT_URL + "User/CheckUsernameAvailable?username=" + $.trim(username);
    $.getJSON(url, function (data) {
        return data;
    });
    
}
function checkEmailAvailable(email) {
    var url = ROOT_URL + "User/CheckEmailAvailable?email=" + $.trim(email);
    $.getJSON(url, function (data) {
        return data;
    });

}
function signUpValidate() {
    if (!checkValidEmailAddress($("#signUpEmail").val())) {
        $("#signUpEmail").focus();
        $("#signUpError").text("Incorrect email!");
        return;
    }
    if ($.trim($("#signUpFullName").val()) == '') {
        $("#signUpFullName").focus();
        $("#signUpError").text("Full name cannot be empty!");
        return;
    }
    if ($.trim($("#signUpPassword").val()) == '') {
        $("#signUpPassword").focus();
        $("#signUpError").text("Password not match!");
        return;
    }
    if ($("#signUpPassword").val() != $("#signUpRetypePassword").val()) {
        $("#signUpPassword").focus();
        $("#signUpError").text("Password not match!");
        return;
    }
    signUpWithEmail();
}
function signUpWithEmail() {
    var content = { email: $.trim($("#signUpEmail").val()), password: $.trim($("#signUpPassword").val()), fullName: $.trim($("#signUpFullName").val()) };
    $.ajax({
        url: ROOT_URL + "User/SignUp",
        type: 'POST',
        dataType: 'json',
        data: JSON.stringify(content),
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data.Result) {
                window.location.href = SIGN_UP_SUCCESS_URL;
            } else {
                $("#signUpError").text(data.Message);
            }
        }
    });
}
function loginFacebook() {
    FB.login(function (response) {
        if (response.authResponse) {
            FB.getLoginStatus(function (response2) {
                if (response2.authResponse) {
                    var url = Base64.encode(window.location.href);
                    url = LOGIN_FACEBOOK_URL + "?userId=" + FB.getAuthResponse()['userID'] + "&token=" +
                        FB.getAuthResponse()['accessToken'] + "&url=" + url;
                    //alert(url);
                    window.location = url;
                    //alert(response2.authResponse.accessToken);
                } else {
                    //alert('do something...maybe show a login prompt');
                }
            });
        } else {
            //alert('User cancelled login or did not fully authorize.');
        }
    }, { scope: 'email,publish_actions' });
}
function postToWall(strMessage, strName, strLink, strPicture, strDescription) {
    FB.login(function (response) {
        if (response.authResponse) {
            // Post message to your wall
            var opts = {
                message: strMessage,
                name: strName,
                link: strLink,
                description: strDescription,
                picture: strPicture
            };
            FB.api('/me/feed', 'post', opts, function (response) {
                if (!response || response.error) {
                    //alert('Posting error occured' + response + response.error);
                }
                else {
                    //alert('Success - Post ID: ' + response.id);
                }
            });
        }
        else {
            //alert('Not logged in');
        }
    }, { scope: 'publish_actions' });
    return false;
}

function ShareQuestionToFacebook(questionId) {
    $.fancybox.open('#ShareQuestionPopup');
    $("#shareQuestionPopupQuestionUrl").val(QUESTION_URL + "-" + questionId + ".html");
    $("#shareQuestionPopupQuestionTitle").val($("#question_title_" + questionId).text());
    $("#shareQuestionPopupFirstAnswer").val($("#first_answer_" + questionId).text());
}
function ShareAnswerToFacebook(questionId, answerId) {
    $.fancybox.open('#ShareQuestionPopup');
    $("#shareQuestionPopupQuestionUrl").val(QUESTION_URL + "-" + questionId + ".html");
    $("#shareQuestionPopupQuestionTitle").val($("#question_title_" + questionId).text());
    $("#shareQuestionPopupFirstAnswer").val($("#answer_content_" + answerId).text());
}

function ShareToFacebook() {
    var strMessage = $("#shareMassage").val();
    var strLink = $("#shareQuestionPopupQuestionUrl").val();
    var strName = $("#shareQuestionPopupQuestionTitle").val();
    var strDescription = $("#shareQuestionPopupFirstAnswer").val();
    postToWall(strMessage, strName, strLink, "", strDescription);
    $.fancybox.close('#ShareQuestionPopup');
}
function StripHtml(input) {
    return $("<div/>").html(input).text();
}
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString()) + ";path=/";;
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function deleteCookie(c_name) {
    document.cookie = c_name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/';
}
//==========================================================================================
//Ask question
function AskQuestionNotSign(questionTitle) {
    $.fancybox.open("#addQuestionStep1", { modal: true });
    nicEditors.findEditor("askQuestionContentPopup").setContent($('#askQuestionContent').val());
}

function GetSimilarQuestion(questionId) {
    $("#ulSimilarQuestionList").html('');
    $("#similarQuestionPopupCurrentQuestionId").val(questionId);
    $.fancybox.open("#similarQuestionPopup");
    var url = ROOT_URL + "Question/SimilarQuestionJsonResult?id=" + questionId;
    $.getJSON(url, function (data) {
        if (data != 'null') {

            for (var i = 0; i < data.length; i++) {
                var item = "<li><a href='javascript:ShowDetailSimilarQuestion(" + data[i].Id + ")'>Bài toán " + (i + 1) + ": " + data[i].Title + "</a></li>";
                $("#ulSimilarQuestionList").append(item);
            }
            MathJax.Hub.Queue(["Typeset", MathJax.Hub, "ulSimilarQuestionList"]);
        }
    });
}
function ShowDetailSimilarQuestion(questionId) {
    $("#detailQuestionPopupTitle").html('');
    $("#detailQuestionPopupAnswer").html('');
    $.fancybox.close("#similarQuestionPopup");
    $.fancybox.open("#detailQuestionPopup");
    var url = ROOT_URL + "Question/DetailSimilarQuestionJsonResult?id=" + questionId;
    $.getJSON(url, function (data) {
        if (data != 'null') {
            $("#detailQuestionPopupTitle").html("<b>Câu hỏi: </b>" + data.Title);
            $("#detailQuestionPopupAnswer").html("<b>Trả lời: </b>" + data.AnswerList[0].Content);
            $("#detailQuestionPopupAnswerHide").html(data.AnswerList[0].Content);

            MathJax.Hub.Queue(["Typeset", MathJax.Hub, "detailQuestionPopupTitle"]);
            MathJax.Hub.Queue(["Typeset", MathJax.Hub, "detailQuestionPopupAnswer"]);
        }
    });
}

function BackToGetSimilarQuestion() {
    $.fancybox.open("#similarQuestionPopup");
    $.fancybox.close("#detailQuestionPopup");
}
function UseThisAnswer() {
    nicEditors.findEditor('postAnswer_' + $("#similarQuestionPopupCurrentQuestionId").val()).setContent($("#detailQuestionPopupAnswerHide").html());
    //nicEditors.findEditor('postAnswer_204').setContent("fsdfsdfsd");
    $.fancybox.close("#detailQuestionPopup");
}
function ShowSearchTopic() {
    $("#lblShowMoreTopic").css("display", "none");
    $("#ShowSearchTopicContainer").css("display", "block");

    $("#topicKeyword").keypress(function (e) {
        if (e.which == 13) {
            if ($("#topicKeyword").val() != '') {
                var noInsert = false;
                $('#ulTopicList input:checkbox').each(function () {
                    if ($(this).val() == $("#topicKeyword").val()) {
                        noInsert = true;
                        return false;
                    }
                });
                if (!noInsert) {
                    var item = "<li><input type=\"checkbox\" checked=\"checked\" name=\"topicListSelect\" id=\"topic-" + $("#topicKeyword").val() + "\" value=\"" + $("#topicKeyword").val() + "\" /><label class=\"navigation\" for=\"topic-" + $("#topicKeyword").val() + "\">" + $("#topicKeyword").val() + "</label></li>";
                    $("#ulTopicList").append(item);
                    $("#topicKeyword").val("");
                }
            }
        }
    });

    var autoCompleteOptions, autoComplete;
    jQuery(function () {
        autoCompleteOptions = {
            serviceUrl: ROOT_URL + 'Topic/AutoComplete', onSelect: function (value, data) {
                if ($("#topicKeyword").val() != '') {
                    var noInsert = false;
                    $('#ulTopicList input:checkbox').each(function () {
                        if ($(this).val() == $("#topicKeyword").val()) {
                            noInsert = true;
                            return false;
                        }
                    });
                    if (!noInsert) {
                        var item = "<li><input type=\"checkbox\" checked=\"checked\" name=\"topicListSelect\" id=\"topic-" + value.value + "\" value=\"" + value.value + "\" /><label class=\"navigation\" for=\"topic-" + value.value + "\">" + value.value + "</label></li>";
                        $("#ulTopicList").append(item);
                        $("#topicKeyword").val("");
                    }
                }
            }
        };
        autoComplete = $('#topicKeyword').autocomplete(autoCompleteOptions);
    });
}
function SaveQuestion() {
    var topicList = "";
    var content = { questionContent: encodeURIComponent(nicEditors.findEditor('askQuestionContent').getContent()), topicList: topicList };
    if (StripHtml(nicEditors.findEditor('askQuestionContent').getContent()) != '') {
        $.ajax({
            url: POST_QUESTION_URL,
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(content),
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                if (data != 'null') {
                    //alert(QUESTION_URL + data.StrId + ".html");
                    window.location.href = POST_QUESTION_SUCCESS_URL;
                }
            }
        });
    } else {
        alert(LANGUAGE_ENTER_QUESTION_NOTICE);
    }
}
function SaveQuestionPopup() {
    var topicList = "";
    var content = { questionContent: encodeURIComponent(nicEditors.findEditor('askQuestionContentPopup').getContent()), topicList: topicList };
    if (StripHtml(nicEditors.findEditor('askQuestionContentPopup').getContent()) != '') {
        $.ajax({
            url: POST_QUESTION_URL,
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(content),
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                if (data != 'null') {
                    //alert(QUESTION_URL + data.StrId + ".html");
                    window.location.href = POST_QUESTION_SUCCESS_URL;
                }
            }
        });
    } else {
        alert(LANGUAGE_ENTER_QUESTION_NOTICE);
    }
}
function validateSearchForm() {
    if ($("#txtSearchKeyword").val() == '') {
        $("#txtSearchKeyword").focus();
        return false;
    }

    return true;
}
function validateSearchForm2() {
    if ($("#txtSearchKeyword2").val() == '') {
        $("#txtSearchKeyword2").focus();
        return false;
    }
    return true;
}
//======================================================================================
function BuildEditorForAnswerOpenQuestion(answerContent, questionId) {
    BuildNicEditor(answerContent, '668px');
    $("#answer_action_" + questionId).css("display", "block");
    $(".answerControlToHide_" + questionId).css("display", "none");
}
function GetQuestionToAnswer(questionId) {
    var url = "/Question/GetQuestionToAnswer?id=" + questionId;
    $.getJSON(url, function (data) {
        if (data != null && data.Data.result == 'true') {
            $.fancybox.open("#getQuestionToAnswerPopup");
            $("#btnGetQuestionToAnswer_" + questionId).val("Đang trả lời...");
            $("#btnGetQuestionToAnswer_" + questionId).attr('disabled', 'disabled');
            $("#postAnswer_" + questionId).removeAttr("disabled");
        }
    });
}

//======= Google API ===========
function googleSigninCallback(authResult) {
    if (authResult['status']['signed_in']) {
        var url = Base64.encode(window.location.href);
        url = LOGIN_GOOGLE_URL + "?token=" + authResult['access_token'] + "&url=" + url;
        window.location = url;
    } else {
        console.log('Sign-in state: ' + authResult['error']);
    }
}
// Library
function checkValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);

    return pattern.test(emailAddress);
};
function checkValidUserName(userName) {
    var pattern = new RegExp(/^[a-z0-9_]{5,30}$/);

    return pattern.test(userName);
};


// -========= Report ===========
function openReportForm(objectType, objectId) {
    $.fancybox.open("#report", {
        beforeLoad: function () {
            $.ajax({
                url: ROOT_URL + "Report/GetForm",
                type: "POST",
                traditional: true,
                data: { objectType: objectType, objectId: objectId },
                success: function (data) {
                    if (data) {
                        $("#report").html(data);
                    }
                }
            });
        },
    });
}
function sendReport() {
    var reasonDescription = encodeURIComponent($("#report textarea[name='reasonDescription']").val());
    var affectedUserId = $("#report input[name='AffectedUserId']").val();
    var objectType = $("#report input[name='ObjectType']").val();
    var objectId = $("#report input[name='ObjectId']").val();
    var reason = $("#report select").val();
    $.ajax({
        url: ROOT_URL + "Report/SendReport",
        type: "POST",
        traditional: true,
        data: { objectType: objectType, objectId: objectId, reasonDescription: reasonDescription, affectedUserId: affectedUserId, reason: reason },
        success: function (data) {
            if (data) {
                $.fancybox.close();
            }
        }
    });
}
// Login popup
function loginPopup() {
    $.fancybox.open("#loginPopup");
}
function loginWithEmail() {
    var username = $("#loginPopup input[name='login_username']").val();
    var password = $("#loginPopup input[name='login_password']").val();
    if (username.length < 5 || username.length > 30) {
        $("input[name='login_username']").addClass("error");
        $("#error-login-block").text("Tên đăng nhập không hợp lệ!");
        return false;
    }
    if (password.length < 6 || password.length > 20) {
        $("input[name='login_password']").addClass("error");
        $("#error-login-block").text("Mật khẩu không hợp lệ!");
        return false;
    }
    
    $.ajax({
        url: ROOT_URL + "User/LoginWithEmail",
        type: "POST",
        traditional: true,
        data: { username: username, password: password },
        success: function (data) {
            if (data) {
                window.location.href = window.location.href;
            } else {
                $("#error-login-block").text("Tên đăng nhập hoặc mật khẩu không chính xác!");
                return false;
            }
        }
    });
}
function previewExam() {
    $.fancybox.open("#userExam", {
        beforeShow: function () {
            $(".examQuestion").each(function () {
                var questionId = $(this).find("input").val();
                if (nicEditors.findEditor('math_' + questionId)) {
                    var content = nicEditors.findEditor('math_' + questionId).getContent();
                    if (content) {
                        $(this).find(".userAnswer").html(content);
                        MathJax.Hub.Queue(["Typeset", MathJax.Hub, "math_preview_" + questionId]);

                    }
                }

            });
            $.fancybox.update("#userExam");
        },
    });
}

function guestViewDemo(action, boxTitle) {
    $.fancybox.open("#guestGradePopup", {
        beforeShow: function () {
            $("#guestGradePopupTitle").text(boxTitle);
            $("#guestGradePopupForm").attr("action", GUEST_DEMO_URL.replace("[action]", action));
        },
    });
}
function htmlEncode(value) {
    return $('<div/>').text(value).html();
}
function htmlDecode(value) {
    return $('<div/>').html(value).text();
}

function  StoreUserQuestion(textBoxId) {
    var question = '';
    try {
        question = nicEditors.findEditor(textBoxId).getContent();
    } catch (e) {
        question = $('#' + textBoxId).val();
    } 
    
    setCookie('StoreUserQuestion', question, 1);
}
function GetStoredUserQuestion(textBoxId) {
    var content = getCookie('StoreUserQuestion');
    if (content != '' && content != 'undefined' && content != null) {
        try {
            nicEditors.findEditor(textBoxId).setContent(content);
        } catch (e) {
            $('#' + textBoxId).val(content);
        } 
        
    }
    deleteCookie('StoreUserQuestion');
}
function GuestAskQuestion() {
    StoreUserQuestion('askQuestionContent');
    $.fancybox.open('#loginPopupAuto');
}
function FreeUserAskQuestion() {
    StoreUserQuestion('askQuestionContent');
    $.fancybox.open('#userFreePopup');
}

function showExamGuideAuto() {
    var examId = getCookie('showExamGuideAutoExamId');
    if ($.isNumeric(examId)) {
        deleteCookie('showExamGuideAutoExamId');
        var url = "/Exam/ExamGuide/?examId=" + examId;
        $.fancybox.open('#examGuide', {
            beforeLoad: function () {
                $.ajax({
                    url: url, success: function (result) {
                        $("#examGuide").html(result);
                        $.fancybox.update('#examGuide');
                    }
                });
            }
        });
    }
}
$(document).ready(function () {
    showExamGuideAuto();
});


