using System;

namespace FluentHtml.Tests
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string StateProvidence { get; set; }
        public string PostalCode { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessFax { get; set; }
        public string MobilePhone { get; set; }
        public string OtherPhone { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public DateTime SysCreateDate { get; set; }
        public string SysCreateUserName { get; set; }
        public DateTime SysUpdateDate { get; set; }
        public string SysUpdateUserName { get; set; }
    }
}
