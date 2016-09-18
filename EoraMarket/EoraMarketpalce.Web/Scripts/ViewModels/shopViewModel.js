function shopViewModel() {
    var self = this;

    self.goods = ko.observableArray([]);

    self.filter = {
        races: [],
        classes: []
    };

    function loadGoods() {
        $.get("/api/Goods/GetGoods").success(function (result) {
            console.log(result);

            self.goods(result);
        }).fail(function (result) {
            console.log(result);
        });
    }

    function loadRaces() {
        $.get("/api/Race/Get").success(function (result) {
            console.log(result);
            self.filter.races = result;
        }).fail(function (result) {
            console.log(result);
        });
    }

    function loadClasses() {
        $.get("/api/Class/Get").success(function (result) {
            console.log(result);
            self.filter.classes = result;
        }).fail(function (result) {
            console.log(result);
        });
    }

    self.init = function () {
        loadGoods();
        //loadRaces();
        //loadClasses();
    };
}

$(document).ready(function () {
    var vm = new shopViewModel();
    vm.init();
    ko.applyBindings(vm);
});
