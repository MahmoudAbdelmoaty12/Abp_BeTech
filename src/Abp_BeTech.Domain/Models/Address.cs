using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Values;
using Volo.Abp.Identity;

namespace Abp_BeTech.Models
{
    public class Address : ValueObject

    {

        public string City { get; set; }
        public string Street { get; set; }
        [ForeignKey("IdentityUser")]
        public Guid? IdentityUserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        public Address() { }
        public Address(string city, string street)
        {
            City = city;
            Street = street;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return City;
            yield return Street;
        }
    }
}
