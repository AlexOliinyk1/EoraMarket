function shopViewModel() {
    var self = this;

    self.transferInProccess = ko.observable(false);

    self.TotalResults = ko.observable(0);
    self.Pager = ko.pager(self.TotalResults);
    self.Pager().PageSize(5);

    self.uiFilter = {
        productName: ko.observable('').extend({ throttle: 30 }),
        minPrice: ko.observable(),
        maxPrice: ko.observable(),
        activeClass: ko.observable()
    };
    self.applyFilters = function () {
        if (self.Pager().CurrentPage() != 1) {
            self.Pager().CurrentPage(1);
        } else {
            loadGoods();
        }
    };
    self.goods = ko.observableArray([]);
    self.detail = ko.observable(false);
    self.previewProduct = function (product) {
        loadProductDetails(product.id, function (result) {
            self.detail(result);
        })
    };

    self.character = {};
    self.characterCredit = ko.observable(0);
    self.characterInventory = ko.observableArray([]);
    self.toSellDetail = ko.observable(false);
    self.previewToSellProduct = function (product) {
        loadProductDetails(product.ProductId, function (result) {
            self.toSellDetail(result);
        });
    };

    //  BUY

    self.buyProduct = function (product, htmlElement) {
        if (self.character.Name !== undefined) {
            self.transferInProccess(true);
            $.post("/api/Goods/BuyProduct", {
                CharId: self.character.Id,
                ProdId: product.id
            }).success(function (result) {
                getUserCharacter().success(function (result) {
                    //TODO: show message (popup)

                    loadInventory(self.character.Id);
                    self.toSellDetail(false);
                });
            }).fail(function (result) {
                self.transferInProccess(false);
                console.log(result);
            });
        }
    };

    //  SELL

    self.sellProduct = function (product, htmlElement) {
        if (self.character.Name !== undefined) {
            self.transferInProccess(true);
            $.post("/api/Goods/SellProduct", {
                CharId: self.character.Id,
                ProdId: product.id
            }).success(function (result) {
                console.log(result);
                loadInventory(self.character.Id);
                //TODO: show message (popup)
            }).fail(function (result) {
                console.log(result);
            });
        }
    };

    function loadGoods() {
        var userClass = self.character ? { name: self.character.Class } : null;
        $.get("/api/Goods", {
            filter: {
                productName: self.uiFilter.productName,
                minPrice: self.uiFilter.minPrice,
                maxPrice: self.uiFilter.maxPrice,
                page: self.Pager().CurrentPage(),
                perPage: self.Pager().PageSize(),
                activeClass: userClass,
            }
        }).success(function (result) {
            self.goods(result.products);
            if (result.products.length > 0 && self.detail() == undefined) {
                self.previewProduct(result.products[0]);
            }
            self.TotalResults(result.totalProducts);
        }).fail(function (result) {
            console.log(result);
        });
    }
    function loadInventory(charId) {
        $.get("/Character/GetInventory", { characterId: charId }).success(function (result) {
            self.characterInventory(result);
        }).fail(function (result) {
            console.log(result);
        });
    }
    function getUserCharacter() {
        var q = $.get("/Character/GetActiveCharacter").success(function (result) {
            console.log(result);
            self.character = result;
            self.characterCredit(self.character.Credits);
        }).fail(function (result) {
            console.log(result);
        });
        return q;
    }
    function loadProductDetails(productId, successCalback) {
        $.get("/api/Goods/Detail/" + productId).success(successCalback).fail(function (result) {
            console.log(result);
        });
    }
    self.init = function () {
        getUserCharacter().success(function (result) {
            loadInventory(result.Id);
        }).always(function () {
            loadGoods();
        });
    };

    self.uiFilter.productName.subscribe(function (newValue, oldValue) {

    });
    self.Pager().CurrentPage.subscribe(function () {
        loadGoods();
    });
}

$(document).ready(function () {
    var vm = new shopViewModel();
    vm.init();
    ko.applyBindings(vm);
});

