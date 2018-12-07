$(document).ready(function () {
    $(document).on("click", "th", function () {
        location.href = "event://th/click?columnIndex=" + $(this).index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).on("click", "td", function () {
        location.href = "event://td/click?columnIndex=" + $(this).index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).on("change", "th input:checkbox", function () {
        location.href = "event://th.checkbox/change?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).prop("checked");
    });

    $(document).on("change", "td input:checkbox", function () {
        location.href = "event://td.checkbox/change?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).prop("checked");
    });

    $(document).on("click", "th input:radio", function () {
        location.href = "event://th.radio/click?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).on("click", "td input:radio", function () {
        location.href = "event://td.radio/click?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).on("click", "th input:button, th button", function () {
        location.href = "event://th.button/click?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).on("click", "td input:button, td button", function () {
        location.href = "event://td.button/click?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).on("change", "th select", function () {
        location.href = "event://th.select/change?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).on("change", "td select", function () {
        location.href = "event://td.select/change?columnIndex=" + $(this).closest('td').index() + "&rowIndex=" + $(this).closest('tr').index() + "&data=" + $(this).val();
    });

    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
    });
})

function clear() {
    if (document.selection && document.selection.empty) {
        document.selection.empty();
    }
    else if (window.getSelection) {
        var sel = window.getSelection();
        if (sel && sel.removeAllRanges)
            sel.removeAllRanges();
    }
}