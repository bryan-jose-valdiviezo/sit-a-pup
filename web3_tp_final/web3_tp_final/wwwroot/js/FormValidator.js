"use strict";

class FormValidator {
    constructor(formId) {
        this.formId = formId;
    }

    ValidateForm() {
        var test = this;
        var validation = true;
        $(this.formId + " :input").each(function () {
            if (!test.ValidateInput($(this), $($(this).data('validation'))))
                validation = false;
        });

        return validation;
    }

    ValidateInput(input, ValidationBox) {
        input.removeClass('is-valid is-invalid');
        ValidationBox.removeClass();

        var data = input.data();

        if (data["present"] && !this.PresentValidation(input))
            return false;

        if (data["compare"] && !this.CompareValidation(input))
            return false;

        return true;
    }

    ActivateValidationBoxes(input, isValid, ValidationBox) {
        if (isValid) {
            input.addClass('is-valid')
            ValidationBox.addClass('valid-feedback');
        }
        else {
            input.addClass('is-invalid')
            ValidationBox.addClass('invalid-feedback');
        }

        return isValid;
    }

    PresentValidation(input) {
        var ValidationBox = $(input.data('validation'));
        var value = input.val();
        var validation;

        if (value.length == 0) {
            validation = this.ActivateValidationBoxes(input, false, ValidationBox)
            ValidationBox.html("Must be present");
        } else {
            validation = this.ActivateValidationBoxes(input, true, ValidationBox)
            ValidationBox.html("Good !");
        }

        return validation;
    }

    CompareValidation(input) {
        var ValidationBox = $(input.data('validation'));
        var value = input.val()
        var compareToValue = $(input.data('compare-to')).val();
        var position = input.data('position');
        var validation = true;

        if (compareToValue.length == 0) {
            validation = this.ActivateValidationBoxes(input, false, ValidationBox);
            ValidationBox.html("Comparator must be present");
        }

        else if (position == "top") {
            if (value <= compareToValue) {
                validation = this.ActivateValidationBoxes(input, false, ValidationBox);
                ValidationBox.html("Must be greater");
            }
        }

        else {
            if (value >= compareToValue) {
                validation = this.ActivateValidationBoxes(input, false, ValidationBox);
                ValidationBox.html("Must be lower")
            }
        }

        if (!validation)
            return validation;


        ValidationBox.html("Good !");
        return this.ActivateValidationBoxes(input, true, ValidationBox);
    }
}