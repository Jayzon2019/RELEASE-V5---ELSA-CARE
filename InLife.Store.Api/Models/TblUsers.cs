using System;
using System.Collections.Generic;

namespace InLife.Store.Api.Models
{
    public partial class TblUsers
    {
        public int UserId { get; set; }
        public DateTime? Dob { get; set; }
        public int GenderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserRoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserImg { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public string RestCode { get; set; }
        public DateTime RestDate { get; set; }
        public string ActivationCode { get; set; }
        public DateTime ActivationDate { get; set; }
    }
}
