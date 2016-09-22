function shopViewModel() {
    var self = this;

    self.TotalResults = ko.observable();
    self.Pager = ko.pager(self.TotalResults);

    self.Pager().CurrentPage.subscribe(function () {
        loadGoods();
    });

    self.goods = ko.observableArray([]);

    self.uiFilter = {
        productName: ko.observable('').extend({ throttle: 30 }),
        minPrice: ko.observable(),
        maxPrice: ko.observable(),
        activeClass: ko.observable()
    };

    self.selectItemCallback = function (data) {
        console.log(data);
    }

    self.detail = ko.observable({});
    self.previewProduct = function (product) {
        $.get("/api/Goods/Detail/" + product.id).success(function (result) {
            self.detail(result);
        }).fail(function (result) {
            console.log(result);
        });
    };

    self.applyFilters = function () {
        if (self.Pager().CurrentPage() != 1) {
            self.Pager().CurrentPage(1);
        } else {
            loadGoods();
        }
    };

    function loadGoods() {
        $.get("/api/Goods", {
            filter: {
                productName: self.uiFilter.productName,
                minPrice: self.uiFilter.minPrice,
                maxPrice: self.uiFilter.maxPrice,
                page: self.Pager().CurrentPage(),
                perPage: self.Pager().PageSize(),
                activeClass: {},
            }
        }).success(function (result) {
            console.log(result);
            self.goods(result.products);
            if (result.products.length > 0) {
                self.previewProduct(result.products[0]);
            }
            self.TotalResults(result.totalProducts);
        }).fail(function (result) {
            console.log(result);
        });
    }

    function getUserCharacter() {
        var q = $.get("/Charcter/GetActiveCharacter").success(function (result) {
            console.log(result);
        }).fail(function (result) {
            console.log(result);
        });
        return q;
    }

    self.init = function () {
        getUserCharacter().always(function () {
            loadGoods();
        });


    };

    self.uiFilter.productName.subscribe(function (newValue, oldValue) {

    });
}

$(document).ready(function () {
    var vm = new shopViewModel();
    vm.init();
    ko.applyBindings(vm);
});


