using System;
using System.Collections.Generic;
using System.Text;

namespace Archive.Contracts.Entities
{
    public class WorkerDateProjection
    {
        public string WorkerName { get; set; }
        
        public DateTime RequestDate { get; set; }

        public int DepartmentNumber { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentPhone { get; set; }
    }
}
