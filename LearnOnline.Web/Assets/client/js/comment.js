function showComment(objectId, objectType, objContainer, canComment) {
    $(objContainer).html($("#comment").html());
    if (!canComment) {
        $(objContainer + " textarea").attr("disabled", "disabled");
        $(objContainer + " button").attr("disabled", "disabled");
    }
    $(objContainer + " input[name='objectId']").val(objectId);
    $(objContainer + " input[name='objectType']").val(objectType);
    getComment(objContainer, 0);
    $(objContainer + " button").click(function () {
        var content = { objectId: objectId, objectType: objectType, content: encodeURIComponent($(objContainer + " textarea").val()) };
        $.ajax({
            url: "/Comment/PostComment",
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(content),
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                if (data != 'null' && data != '') {
                    $(objContainer + " textarea").val('');
                    var oldComment = $(objContainer + " .comment-list ul").html();
                    var comment = [data];
                    $(objContainer + " .comment-list ul").setTemplateElement("commentListItem");
                    $(objContainer + " .comment-list ul").processTemplate(comment);
                    $(objContainer + " .comment-list ul").append(oldComment);
                }
            }
        });
    });

    //$("#comment").show();
}
function getComment(objContainer, currentComment) {
    var objectId = $(objContainer + " input[name='objectId']").val();
    var objectType = $(objContainer + " input[name='objectType']").val();
    $.ajax({
        url: "/Comment/GetComment?objectId=" + objectId + "&objectType=" + objectType + "&currentComment=" + currentComment,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data != 'null' && data != '') {
                var oldComment = $(objContainer + " .comment-list ul").html();
                $(objContainer + " .comment-list ul").setTemplateElement("commentListItem");
                $(objContainer + " .comment-list ul").processTemplate(data.Comments);
                $(objContainer + " .comment-list ul").prepend(oldComment);
                if (data.GotMore) {
                    $(objContainer + " .view-more").html('<a href="javascript:getComment(\'' + objContainer + '\',' + data.CurrentCommentCount + ')">Xem thêm</a>');
                } else {
                    $(objContainer + " .view-more").html('');
                }
            }
        }
    });
}
function showReply(objectId, objectType, objContainer, canComment) {
    $(objContainer).html($("#reply").html());
    if (!canComment) {
        $(objContainer + " textarea").attr("disabled", "disabled");
        $(objContainer + " button").attr("disabled", "disabled");
    }
    $(objContainer).css("display", "");
    $(objContainer).append("<input type='hidden' name='objectId'/><input type='hidden' name='objectType'/>");
    $(objContainer + " input[name='objectId']").val(objectId);
    $(objContainer + " input[name='objectType']").val(objectType);
    getReply(objContainer, 0);
    $(objContainer + " button").click(function () {
        var content = { objectId: objectId, objectType: objectType, content: encodeURIComponent($(objContainer + " textarea").val()) };
        $.ajax({
            url: "/Comment/PostComment",
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(content),
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                if (data != 'null' && data != '') {
                    $(objContainer + " textarea").val('');
                    var oldComment = $(objContainer + " .reply-list ul").html();
                    var comment = [data];
                    $(objContainer + " .reply-list ul").setTemplateElement("replyListItem");
                    $(objContainer + " .reply-list ul").processTemplate(comment);
                    $(objContainer + " .reply-list ul").prepend(oldComment);
                }
            }
        });
    });
    //$("#comment").show();
}
function getReply(objContainer, currentComment) {
    var objectId = $(objContainer + " input[name='objectId']").val();
    var objectType = $(objContainer + " input[name='objectType']").val();
    $.ajax({
        url: "/Comment/GetComment?objectId=" + objectId + "&objectType=" + objectType + "&currentComment=" + currentComment,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data != 'null' && data != '') {
                var oldComment = $(objContainer + " .reply-list ul").html();
                $(objContainer + " .reply-list ul").setTemplateElement("replyListItem");
                $(objContainer + " .reply-list ul").processTemplate(data.Comments);
                $(objContainer + " .reply-list ul").append(oldComment);
                if (data.GotMore) {
                    $(objContainer + " .view-more").html('<a href="javascript:getReply(\'' + objContainer + '\',' + data.CurrentCommentCount + ')">Xem thêm</a>');
                } else {
                    $(objContainer + " .view-more").html('');
                }
            }
        }
    });
}

function deleteCommentConfirm(func) {
    $.fancybox.open("#commentDeleteConfirm", {
        beforeShow: function() {
            $("#commentDeleteConfirmBtn").attr("onclick", func);
        }
    });
    
}
function deleteComment(id) {
    $.ajax({
        url: "/Comment/DeleteComment?id=" + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data != 'null' && data != '' && data) {
                $("#comment_" + id).remove();
            }
            $.fancybox.close();
        }
    });
}
function deleteReply(id) {
    $.ajax({
        url: "/Comment/DeleteComment?id=" + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data != 'null' && data != '' && data) {
                $("#reply_" + id).remove();
            }
        }

    });
}


