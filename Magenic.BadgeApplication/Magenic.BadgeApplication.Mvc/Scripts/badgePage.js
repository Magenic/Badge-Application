/*globals ko:true*/

ko.bindingHandlers.disableClick = {
    init: function (element, valueAccessor) {
        $(element).click(function (evt) {
            if (valueAccessor())
                evt.preventDefault();
        });
    },

    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        ko.bindingHandlers.css.update(element, function () { return { disabled_anchor: value }; });
    }
};

$(document).ready(function () {
    var BadgePageViewModel = {
        showCorporateBadgeAllTab: ko.observable(true),
        showCorporateBadgeEarnedTab: ko.observable(false),

        showAll: function () {
            this.showCorporateBadgeAllTab(true);
            this.showCorporateBadgeEarnedTab(false);
        },

        showEarned: function () {
            this.showCorporateBadgeAllTab(false);
            this.showCorporateBadgeEarnedTab(true);
        }
    };

    ko.applyBindings(BadgePageViewModel);
});