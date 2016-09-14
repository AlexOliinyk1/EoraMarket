(function () {
})();

var vm = new CharacrterViewModel();

function CharacrterViewModel() {
    var self = this;

    self.StartCreateCharakter = function () {
        $.get("/Character/Create", "", function (result) {
            $("#createFormContainer").html(result);
        });
        //$.ajax({url: "",type: "GET"}).success(function (result) {});
    }
}