
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterControl.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public int GateId { get; set; }
        public string GateName { get; set; }
        public string TypeEvent { get; set; }
        public int CountThread { get; set; }
        public int AfterEvent { get; set; }
        public int BeforeEvent { get; set; }
        public string DispatcherName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
