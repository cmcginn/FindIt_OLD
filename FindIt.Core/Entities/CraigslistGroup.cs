﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Entities
{
    public class CraigslistGroup:BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string UrlSuffix { get; set; }
        public virtual bool Active { get; set; }
    }
}