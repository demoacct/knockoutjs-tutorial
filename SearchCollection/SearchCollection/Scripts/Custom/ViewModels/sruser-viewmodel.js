function SRUserViewModel(Id, IdStr, FirstName, LastName, Username, Password, DateCreated, Role, LastLogin, Status, StatusStr, DateCreatedFormatted) {

    var self = this;

    self.Id = ko.observable(Id);
    self.IdStr = ko.observable(IdStr);
    self.FirstName = ko.observable(FirstName);
    self.LastName = ko.observable(LastName);
    self.Username = ko.observable(Username);
    self.Password = ko.observable(Password);
    self.DateCreated = ko.observable(DateCreated);
    self.Role = ko.observable(Role);
    self.LastLogin = ko.observable(LastLogin);
    self.Status = ko.observable(Status);
    self.DateCreatedFormatted = ko.observable(DateCreatedFormatted);
    self.StatusStr = ko.observable(StatusStr);

}