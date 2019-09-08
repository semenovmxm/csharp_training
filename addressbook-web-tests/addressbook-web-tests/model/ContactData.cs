using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string contactDetails;

        public ContactData() { }

        public ContactData(string lastname)
        {
            this.Lastname = lastname;
        }

        public ContactData(string firstName, string lastname)
        {
            this.Firstname = firstName;
            this.Lastname = lastname;
        }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        public string Middlename { get; set; }        

        public string Nickname { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }

        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmails(Email) + CleanUpEmails(Email2) + CleanUpEmails(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string Homepage { get; set; }
        public string Bday { get; set; }
        public string Bmonth { get; set; }
        public string Byear { get; set; }
        public string Aday { get; set; }
        public string Amonth { get; set; }
        public string Ayear { get; set; }
        public string New_group_id { get; set; }
        public string New_group { get; set; }
        public string Address2 { get; set; }
        public string Phone2 { get; set; }
        public string Notes { get; set; }

        

        public string ContactDetails
        {
            get
            {
                if (contactDetails != null)
                {
                    return contactDetails;
                }
                else
                {
                    return ContactDetailsTextConcatenate();
                }

            }
            set
            {
                contactDetails = value;
            }
        }

        private string ContactDetailsTextConcatenate()
        {
            return  System.String.Format(
                @"{0} {2}

H: {7}M: {8}W: {9}F: {10}
{11}{12}{13}",

            Firstname, //0
            Middlename,//1
            Lastname,//2
            CleanUpContactDetails(Nickname),//3
            CleanUpContactDetails(Title),//4
            CleanUpContactDetails(Company),//5
            CleanUpContactDetails(Address),//6
            CleanUpContactDetails(HomePhone),//7
            CleanUpContactDetails(MobilePhone),//8
            CleanUpContactDetails(WorkPhone),//9
            CleanUpContactDetails(Fax),//10
            CleanUpContactDetails(Email),//11
            CleanUpContactDetails(Email2),//12
            CleanUpContactDetails(Email3),//13
            CleanUpContactDetails(Homepage),//14
            Bday,//15
            Bmonth,//16
            Byear,//17
            Aday,//18
            Amonth,//19
            CleanUpContactDetails(Ayear),//20
            CleanUpContactDetails(Address2),//21
            CleanUpContactDetails(Phone2),//22
            CleanUpContactDetails(Notes)//23
                ).Trim();
            //Homepage:       {14}
            //Birthday {15}. {16} {17}
            //Anniversary {18}. {19} {20}
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()-]", "") + "\r\n";
        }

        private string CleanUpEmails(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return Regex.Replace(email, "[ ]", "") + "\r\n";
        }
        private string CleanUpContactDetails(string contactDetails)
        {
            if (contactDetails == null || contactDetails == "")
            {
                return "";
            }
            return contactDetails + "\r\n";
        }
        
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            return Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "lastname=" + Lastname + "\nfirstName= " + Firstname;
        }
        
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Lastname.CompareTo(other.Lastname);
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts select c).ToList();
            }
        }
    }



}
