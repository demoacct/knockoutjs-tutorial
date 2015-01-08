$(document).ready(function () {

    function TopicDaoViewModel() {

        var self = this;

        self.Title = ko.observable();
        self.Description = ko.observable();
        self.DateCreated = ko.observable();
        self.Link = ko.observable();
        
        self.Topics = ko.observableArray([]);
        self.InsertList = ko.observableArray([]);

        var koModel = ko.toJS(self);

        self.Create = function () {
            $.ajax({
                url: "/searchcollection/create",
                type: "post",
                data: { model: koModel }
            }).done(function (result) {

            });
        };

        $.getJSON("/searchcollection/topics", function (result) {

            var koTopics = ko.toJS(result);

            $.each(koTopics, function (i, v) {

                var Id = v.Id;
                var IdStr = v.IdStr;
                var Title = v.Title;
                var Description = v.Description;
                var DateCreated = v.DateCreated;
                var DateCreatedFormatted = v.DateCreatedFormatted;
                var Enable = v.Enable;
                var Link = v.Link;
                var User = v.User;

                self.Topics.push(new TopicViewModel(Id, IdStr, Title, Description, DateCreated, DateCreatedFormatted, Enable, Link, User));

            });

        });

        self.AddRow = function () {

            self.InsertList.push(new TopicViewModel("", "", "", "", "", "", true, ""));

        };

        self.RemoveRow = function (row) {

            self.InsertList.remove(row);

        };

        self.Delete = function (row) {

            var koTopic = ko.toJS(row);
            var IdStr = koTopic.IdStr;

            $.ajax({
                url: "/searchcollection/delete",
                type: "post",
                data: { id: IdStr, __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() }
            }).done(function (result) {

                self.Topics.remove(row);

            });
        };

        self.Save = function () {

            var koTopics = ko.toJS(self.InsertList());

            $.ajax({
                url: "/searchcollection/create",
                type: "post",
                data: { model: koTopics, __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() }
            }).done(function (result) {
                self.InsertList.removeAll();
                self.Topics.removeAll();

                $.getJSON("/searchcollection/topics", function (result) {

                    var koTopics = ko.toJS(result);

                    $.each(koTopics, function (i, v) {

                        var Id = v.Id;
                        var IdStr = v.IdStr;
                        var Title = v.Title;
                        var Description = v.Description;
                        var DateCreated = v.DateCreated;
                        var DateCreatedFormatted = v.DateCreatedFormatted;
                        var Enable = v.Enable;
                        var Link = v.Link;

                        self.Topics.push(new TopicViewModel(Id, IdStr, Title, Description, DateCreated, DateCreatedFormatted, Enable, Link));

                    });

                });

            });

        };
    }

    ko.applyBindings(new TopicDaoViewModel());

});