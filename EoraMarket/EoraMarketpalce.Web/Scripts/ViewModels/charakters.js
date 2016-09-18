function characterViewModel() {
    var self = this;

    self.init = function () {
    };
}

var vm;
$(document).ready(function () {
    vm = new characterViewModel();
    vm.init();
    ko.applyBindings(vm);
});
