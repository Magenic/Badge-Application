/*globals ko:true*/

ko.bindingHandlers.disableClick = {
    init: function (element, valueAccessor) {
        $(element).click(function (evt) {
            if (valueAccessor()) {
                evt.preventDefault();
            }
        });
    },

    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        ko.bindingHandlers.css.update(element, function () { return { disabled_anchor: value }; });
    }
};

function requestNewBadge() {
    var x = document.getElementById("divRequestNewBadge");
    var y = document.getElementById("requestNewBadge");
    document.getElementById('SubmittedBadgeRequest_Name').value = "";
    document.getElementById('SubmittedBadgeRequest_Description').value = "";
    if (x.style.display === "none") {
        x.style.display = "block";
        y.style.display = "none";
        document.getElementById("submitAcitivty").disabled = true;
    } else {
        x.style.display = "none";
        document.getElementById("submitAcitivty").disabled = false;

    }
}

$(document).ready(function () {
    var BadgePageViewModel = {
        showCorporateBadgeAllTab: ko.observable(false),
        showCorporateBadgeEarnedTab: ko.observable(true),
        showCommunityBadgeAllTab: ko.observable(false),
        showCommunityBadgeEarnedTab: ko.observable(true),

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

    $('.datepicker').datepicker();

    $('.datepicker').each(function () {
        var startingValue = $(this).val();
        $(this).val(startingValue.substring(0, startingValue.indexOf(' ')));
    });

    var x = document.getElementById("divRequestNewBadge");
    if (x.style.display === "none") {
        document.getElementById("submitAcitivty").disabled = false;
    } else {
        document.getElementById("submitAcitivty").disabled = true;

    }
});

