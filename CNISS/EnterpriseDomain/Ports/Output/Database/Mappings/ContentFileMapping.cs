﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class ContentFileMapping:ClassMap<ContentFile>
    {
        public ContentFileMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned().Column("ContentFileId");
            Map(x => x.DataFile).Length(int.MaxValue);
        }
    }
}