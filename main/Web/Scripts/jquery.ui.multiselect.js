jQuery.fn.multiselect = function () {

    var multiselects = $(this),
        checkboxes = multiselects.find("input:checkbox"),
        items = checkboxes.parent();

    // Highlight pre-selected checkboxes
    items.find(":checked").parent().addClass("multiselect-on");

    // Highlight checkboxes that the user selects
    checkboxes.click(function () {

        var clickedCheckedbox = $(this),
                isChecked = clickedCheckedbox.is(":checked");

        clickedCheckedbox.parent().toggleClass("multiselect-on", isChecked);
    });

    //when submitting the form for which the multiselect belongs it adds hidden input field for the the selected checkboxes
    multiselects.each(function () {
        var multiselect = $(this),
            form = multiselect.closest("form");

        form.submit(function () {

            var hiddenInputName = multiselect.attr("data-hidden-input-name"),
                selectedCheckedBoxes = multiselect.find("label.multiselect-on input:checkbox");

            selectedCheckedBoxes.each(function (index, checkbox) {
                var newHiddenInput =
                $("<input/>")
                    .attr("type", "hidden")
                    .attr("name", hiddenInputName + "[" + index + "]")
                    .val($(checkbox).val());

                form.append(newHiddenInput);

            });
        });
    });
};
