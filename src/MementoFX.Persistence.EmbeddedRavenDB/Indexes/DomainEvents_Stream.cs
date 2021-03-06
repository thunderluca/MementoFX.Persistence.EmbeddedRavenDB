﻿using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoFX.Persistence.EmbeddedRavenDB.Indexes
{
    public class DomainEvents_Stream : AbstractIndexCreationTask
    {
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition()
            {
                Name = "DomainEvents/Stream",
                Map = @"from evt in docs
						let mtd = evt[""@metadata""]
                        where mtd[""Memento-DomainEvent""] != null
						select new
						{
							Tag = mtd[""Raven-Entity-Name""],
							_ = evt.Select( k => this.CreateField( k.Key, k.Value, true, true ) ),
							Id = evt.__document_id,
							TimelineId = evt.TimelineId,
							TimeStamp = evt.TimeStamp
						}"
            };
        }
    }
}
