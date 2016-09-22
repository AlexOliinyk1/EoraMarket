function createProductViewModel() {
    var self = this;

    self.inProgress = ko.observable(false);

    self.model = {
        product: {
            name: ko.observable(""),
            price: ko.observable(""),
            image: ko.observable(""),
            typeId: ko.observable(""),
            classes: ko.observableArray([]),
            stats: ko.observableArray([])
        }
    };

    self.addStat = function () {
        var lastIndex = self.model.product.stats().length - 1;
        var id = lastIndex < 0 ? 0 : self.model.product.stats()[lastIndex].id + 1;
        self.model.product.stats.push({
            index: id,
            statName: ko.observable(""),
            statValue: ko.observable("")
        });
    };

    self.removeStat = function (toRemove) {
        var arr = jQuery.grep(self.model.product.stats(), function (item) {
            return item.index !== toRemove.index;
        });
        self.model.product.stats(arr);
    };

    self.startCreate = function () {
        self.inProgress(true);
        
        var data = {
            name: self.model.product.name(),
            price: self.model.product.price(),
            typeId: 1,
            image: self.model.product.image(),

            classes: getSelectedClasses(),
            stats: self.model.product.stats()
        }

        $.post("/api/Goods/SaveProduct", data).success(function (result) {
            location.href = "/Shop";
        }).fail(function (result) {
            console.log(result);
        }).always(function () {
            self.inProgress(false);
        });
    };

    self.changeImage = function (file, sender) {
        var reader = new FileReader();

        reader.onload = function (e) {
            self.model.product.image(e.target.result);
        }

        reader.readAsDataURL(file);
    };

    self.init = function () {
        $.get("/api/Class/Get").success(function (result) {
            console.log(result);
            $.each(result, function (index, value) {
                self.model.product.classes.push({
                    checked: ko.observable(false),
                    data: value
                });
            });
        }).fail(function (result) {
            console.log(result);
        });

        self.model.product.name;
    };

    function getSelectedClasses() {
        var classesModels = self.model.product.classes();
        var classes = [];

        for (var i = 0; i < classesModels.length; i++) {
            if (classesModels[i].checked()) {
                classes.push(classesModels[i].data);
            }
        }

        return classes;
    }
}

$(document).ready(function () {
    var vm = new createProductViewModel();
    vm.init();
    ko.applyBindings(vm);
});
