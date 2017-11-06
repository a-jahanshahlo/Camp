using System;

namespace Camps.WebUI.ViewModels.Passengers
{
    public class PassengerIndexViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nid { get; set; }
        public bool Gender { get; set; }
        public string Mobile { get; set; }
        public int? UserId { get; set; }
    }
}