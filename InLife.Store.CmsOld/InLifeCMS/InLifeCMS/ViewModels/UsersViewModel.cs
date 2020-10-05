using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
{
    public partial class UsersViewModel
    {
        [Key]
        public int intUserId { get; set; }

        public DateTime? dteDob { get; set; }

        public int intGenderId { get; set; }
        [Required(ErrorMessage = "First Name is required!!!")]
        public string strFirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required!!!")]
        public string strLastName { get; set; }
        [Required(ErrorMessage = "User Role is required!!!")]
        public int intUserRoleId { get; set; }
        [Required(ErrorMessage = "Email is required!!!")]
        public string strEmail { get; set; }
        [Required(ErrorMessage = "Password is required!!!")]
        public string strPassword { get; set; }
        [Required(ErrorMessage = "Password is required!!!")]
        public string password { get; set; }
        [Required(ErrorMessage = "confirm Password is required!!!")]
        [Compare("password")]
        public string conPassword { get; set; }
        public string strUserImg { get; set; }
        public string strPhone { get; set; }
        public DateTime dteCreatedDate { get; set; }
        public int intCreatedBy { get; set; }
        public DateTime? dteUpdatedDate { get; set; }
        public int? intUpdatedBy { get; set; }
        public bool blnIsActive { get; set; }
        public bool blnIsArchived { get; set; }
        public string strRestCode { get; set; }
        public DateTime steRestDate { get; set; }
        public string strActivationCode { get; set; }
        public DateTime dteActivationDate { get; set; }

        public string strGender { get; set; }
        public string strCreatedByUser { get; set; }
        public string strUpdatedByUser { get; set; }
        public string strUserRole { get; set; }

    }
}
