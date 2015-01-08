$(document).ready(function () {
    function LoginViewModel() {

        var self = this;

        self.Username = ko.observable();
        self.Password = ko.observable();

        self.Login = function () {

            var regModel = ko.toJS(self);

            $.ajax({
                data: { model: JSON.stringify(regModel), __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() },
                url: "/searchcollection/login",
                type: "post"
            }).done(function (result) {

                if (!result) {
                    alert("You're not a member, please register!");
                } else {
                    location.href = "/";
                }
            });

        };

    }

    ko.applyBindings(new LoginViewModel());
});