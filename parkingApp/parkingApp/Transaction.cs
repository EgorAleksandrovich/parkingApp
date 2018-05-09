using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public class Transaction
    {
        public int CarId { get; set; }
        public int WriteOffs { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
