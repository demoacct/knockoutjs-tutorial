function SRUserViewModel(Id, Username, Password, DateCreated, Role, LastLogin, Status) {

    var self = this;

    self.Id = ko.observable(Id);
    self.Username = ko.observable(Username);
    self.Password = ko.observable(Password);
    self.DateCreated = ko.observable(DateCreated);
    self.Role = ko.observable(Role);
    self.LastLogin = ko.observable(LastLogin);
    self.Status = ko.observable(Status);

}