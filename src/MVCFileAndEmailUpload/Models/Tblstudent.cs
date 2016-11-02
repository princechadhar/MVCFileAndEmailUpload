using System;
using System.Collections.Generic;

namespace MVCFileAndEmailUpload.Models
{
    public partial class Tblstudent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RollNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string File { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
