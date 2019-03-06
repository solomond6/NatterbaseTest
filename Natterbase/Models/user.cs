//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Natterbase.Models
{
    using System;
    using System.Collections.Generic;
    using FluentValidation;
    using System.Linq;
    using FluentValidation.Attributes;


    [Validator(typeof(UserValidator))]
    public partial class user
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }


    public class UserValidator : AbstractValidator<user>
    {
        public UserValidator()
        {
            RuleFor(x => x.email).Must(BeUniqueEmail).WithMessage("Email already exists");
            RuleFor(x => x.username).Must(BeUniqueUsername).WithMessage("Username already exists");
        }

        private bool BeUniqueEmail(string email)
        {
            return new NatterbaseEntities().users.FirstOrDefault(x => x.email == email) == null;
        }

        private bool BeUniqueUsername(string username)
        {
            return new NatterbaseEntities().users.FirstOrDefault(x => x.username == username) == null;
        }
    }
}
