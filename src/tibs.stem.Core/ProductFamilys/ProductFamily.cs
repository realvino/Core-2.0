using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Collections;

namespace tibs.stem.ProductFamilys
{
    [Table("ProductFamily")]
    public class ProductFamily : FullAuditedEntity
    {
        public virtual string ProductFamilyCode { get; set; }

        public virtual string ProductFamilyName { get; set; }

        public virtual string Description { get; set; }
        public virtual bool? Discount { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collections { get; set; }
        public virtual int? CollectionId { get; set; }
        public virtual int Warranty { get; set; }


    }
}
