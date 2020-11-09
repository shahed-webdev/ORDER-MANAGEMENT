
(function ($) {
    $.dateRange = function (params) {
        params = $.extend({ from: null, to: null }, params);

        // Get the elements
        const fromInput = $(params.from).pickadate();
        var fromPicker = fromInput.pickadate("picker");

        const toInput = $(params.to).pickadate();
        var toPicker = toInput.pickadate("picker")

        // Check if there’s a “from” or “to” date to start with and if so, set their appropriate properties.
        if (fromPicker.get("value")) {
            toPicker.set("min", fromPicker.get("select"))
        }
        if (toPicker.get("value")) {
            fromPicker.set("max", toPicker.get("select"))
        }

        // Apply event listeners in case of setting new “from” / “to” limits to have them update on the other end. If ‘clear’ button is pressed, reset the value.
        fromPicker.on("set", function(event) {
            if (event.select) {
                toPicker.set("min", fromPicker.get("select"))
            } else if ("clear" in event) {
                toPicker.set("min", false)
            }
        });

        toPicker.on("set", function(event) {
            if (event.select) {
                fromPicker.set("max", toPicker.get("select"))
            } else if ("clear" in event) {
                fromPicker.set("max", false);
            }
        });
    }
})(jQuery);