//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace Final_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class User
    {
        //public int ID { get; set; }


        [DisplayName("User Name ")]
        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Please Enter Password")]
        public string Login_Password { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}
