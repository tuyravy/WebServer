using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Monthly_Reporting.Properties;
using System.Configuration;

namespace Monthly_Reporting.Models
{
    public class Reports
    {
        public string ReportCode{ get; set; }
        public string BrCode { get; set; }
    }
}