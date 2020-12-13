using System;
using System.Collections.Generic;
using System.Text;

namespace Archive.Contracts.Entities
{
    public class WorkerCountProjection
    {
        public string WorkerName { get; set; }

        public string RequestsCount { get; set; }

        public int DepartmentNumber { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentPhone { get; set; }
    }
}
