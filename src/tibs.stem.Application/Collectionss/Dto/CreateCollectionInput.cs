using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Collections;

namespace tibs.stem.Collectionss.Dto
{
    [AutoMapTo(typeof(Collection))]
   public class CreateCollectionInput
    {
        public int Id { get; set; }
        public virtual string CollectionCode { get; set; }
        public virtual string CollectionName { get; set; }
    }
}
