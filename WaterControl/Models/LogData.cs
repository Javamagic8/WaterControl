
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterControl.Data
{
    public class LogData
    {
        public int Id { get; set; }
        public DateTime LastDate { get; set; }
        public DateTime Today { get; set; } = DateTime.Now;

    }
}
