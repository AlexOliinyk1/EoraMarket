function CharacrterViewModel() {
    var self = this;

    self.StartCreateCharakter = function () {
        $.get("/Character/Create", "", function (result) {
            $("#createFormContainer").html(result);
        });
        //$.ajax({url: "",type: "GET"}).success(function (result) {});
    }

    self.succefullyCreated = function (result) {
        alert(result);
    }
}

(function () {
    var vm = new CharacrterViewModel();
})();
