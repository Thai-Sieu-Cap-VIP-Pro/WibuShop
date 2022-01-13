using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneManagement.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int AccountID { get; set; }
        public int CartTotal { get; set; }
        public string CartDate { get; set; }
    }
}
