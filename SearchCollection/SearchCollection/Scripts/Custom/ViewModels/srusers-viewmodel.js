$(document).ready(function () {

    function UsersViewModel() {

        var self = this;

        self.Id = ko.observable();
        self.IdStr = ko.observable();
        self.FirstName = ko.observable();
        self.LastName = ko.observable();
        self.Email = ko.observable();
        self.Username = ko.observable();
        self.Password = ko.observable();
        self.DateCreated = ko.observable();
        self.Role = ko.observable();
        self.Status = ko.observable();
        self.DateCreatedFormatted = ko.observable();
        self.StatusStr = ko.observable();

        self.Users = ko.observableArray([]);

        $.getJSON("/searchcollection/users", function (result) {

            var koUsers = ko.toJS(result);

            $.each(koUsers, function (i, v) {

                var Id = v.Id;
                var IdStr = v.IdStr;
                var FirstName = v.FirstName;
                var LastName = v.LastName;
                var Email = v.Email;
                var Username = v.Username;
                var Password = v.Password;
                var DateCreated = v.DateCreated;
                var DateCreatedFormatted = v.DateCreatedFormatted;
                var Role = v.Role;
                var LastLogin = v.LastLogin;
                var Status = v.Status;
                var StatusStr = v.StatusStr;

                self.Users.push(new SRUserViewModel(Id,IdStr,FirstName, LastName, Username,Password,DateCreated,Role,LastLogin,Status, StatusStr, DateCreatedFormatted));

            });

        });

        self.Deactivate = function (user) {

            var koUser = ko.toJS(user);


            if (confirm("Are you sure to deactivate " + koUser.FirstName + " " + koUser.LastName) == true) {
                $.ajax({
                    url: "/searchcollection/deactivate",
                    type: "post",
                    data: { id: koUser.IdStr, __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() }
                }).done(function (result) {
                    if (result) {
                        alert("User deactivated!");
                        location.href = "/searchcollection/sruser";
                    }
                });
            }
        };

        self.Activate = function (user) {

            var koUser = ko.toJS(user);


            if (confirm("Are you sure to activate " + koUser.FirstName + " " + koUser.LastName) == true) {
                $.ajax({
                    url: "/searchcollection/activate",
                    type: "post",
                    data: { id: koUser.IdStr, __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() }
                }).done(function (result) {
                    if (result) {
                        alert("User activated!");
                        location.href = "/searchcollection/sruser";
                    }
                });
            }
        };

    }

    ko.applyBindings(new UsersViewModel());

});