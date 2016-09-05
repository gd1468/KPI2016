using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManagement.DomainModel.Complex_types
{
    [ComplexType]
    public class UserInformation
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("FullName")]
        public string FullName
        {
            get
            {
                return FirstName + LastName;
            }
            private set { }
        }

        public Address Address { get; set; }
    }
}
