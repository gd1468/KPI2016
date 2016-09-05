using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManagement.DomainModel.Complex_types
{
    public class Address
    {
        [Column("Street")]
        public string Street { get; set; }

        [Column("AddressNumber")]
        public string AddressNumber { get; set; }
    }
}
