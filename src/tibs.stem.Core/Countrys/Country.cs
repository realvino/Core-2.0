using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Countrys 
{
    [Table("Country")]
    public class Country : FullAuditedEntity
    {
        public const int MaxcodeLength = 3;
        public const int MaxnameLength = 50;
        public const int ISDCodeLength = 5;
        [Required]
        [MaxLength(MaxnameLength)]
        public virtual string CountryName { get; set; }

        [Required]
        [MaxLength(MaxcodeLength)]
        public virtual string CountryCode { get; set; }

        [MaxLength(ISDCodeLength)]
        public virtual string ISDCode { get; set; } 
    }
}
