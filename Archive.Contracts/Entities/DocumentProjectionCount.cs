using System;
using System.Collections.Generic;
using System.Text;

namespace Archive.Contracts.Entities
{
    public class DocumentProjectionCount : DocumentProjection
    {
        public int SearchCount { get; set; }
    }
}
