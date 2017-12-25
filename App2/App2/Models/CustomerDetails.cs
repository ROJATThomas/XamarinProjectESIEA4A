using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace App2.Models
{
    public class CustomerDetails : RealmObject
    {
        [PrimaryKey]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerAge { get; set; }
    }
}
