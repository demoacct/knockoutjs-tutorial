
    function TopicViewModel(Id, IdStr, Title, Description, DateCreated, DateCreatedFormatted, Enable, Link, User) {

        var self = this;

        self.Id = ko.observable(Id);
        self.Title = ko.observable(Title);
        self.Description = ko.observable(Description);
        self.DateCreated = ko.observable(DateCreated);
        self.IdStr = ko.observable(IdStr);
        self.DateCreatedFormatted = ko.observable(DateCreatedFormatted);
        self.Enable = ko.observable(Enable);
        self.Link = ko.observable(Link);
        self.Url = ko.observable(Link);//ko.observable("/searchcollection/topic/" + self.IdStr());
        self.User = ko.observable(User);
    }
