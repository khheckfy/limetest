App = {
    init: function () {
        App.initiControls();
    },

    initiControls: function () {
        $('#tbFrom,#tbTo').datepicker();
    }
}

$(function () {
    App.init();
});