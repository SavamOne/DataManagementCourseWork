using System;
using System.Collections.Generic;
using System.Text;

namespace Archive.Contracts.Entities
{
    public class WorkerDepartmentProjection
    {
        public int WorkerId { get; set; }

        public string WorkerName { get; set; }

        public int DepartmentNumber { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentPhone { get; set; }


        public int PgUserId { get; set; }

        public string PgUserName { get; set; }
    }
}
