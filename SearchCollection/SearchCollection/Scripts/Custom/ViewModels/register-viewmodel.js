$(document).ready(function () {

    function RegisterViewModel() {

        var self = this;

        self.FirstName = ko.observable();
        self.LastName = ko.observable();
        self.Email = ko.observable();
        self.Username = ko.observable();
        self.Password = ko.observable();
        self.Status = ko.observable();
        self.DateCreated = ko.observable();
        self.ConfirmPassword = ko.observable();
        self.LastLogin = ko.observable();
        self.Role = ko.observable();
        
        var confirmed = self.Password() == self.ConfirmPassword();

        self.Register = function () {

            var regModel = ko.toJS(self);

            $.ajax({
                data: { model: JSON.stringify(regModel), __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() },
                url: "/searchcollection/register",
                type: "post"
            }).done(function (result) {

                if (result) {
                    alert("Congratulations! You are successfully registered!")
                    location.href = "/";
                }

            });

        };

    }

    ko.applyBindings(new RegisterViewModel());

});

