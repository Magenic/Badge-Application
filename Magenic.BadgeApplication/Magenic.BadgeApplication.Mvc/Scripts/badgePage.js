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
        showCommunityBadgeAllTab: ko.observable(true),
        showCommunityBadgeEarnedTab: ko.observable(false),

        showAllCorporateBadges: function () {
            this.showCorporateBadgeAllTab(true);
            this.showCorporateBadgeEarnedTab(false);
        },

        showEarnedCorporateBadges: function () {
            this.showCorporateBadgeAllTab(false);
            this.showCorporateBadgeEarnedTab(true);
        },

        showAllCommunityBadges: function () {
            this.showCommunityBadgeAllTab(true);
            this.showCommunityBadgeEarnedTab(false);
        },

        showEarnedCommunityBadges: function () {
            this.showCommunityBadgeAllTab(false);
            this.showCommunityBadgeEarnedTab(true);
        }
    };

    ko.applyBindings(BadgePageViewModel);
});