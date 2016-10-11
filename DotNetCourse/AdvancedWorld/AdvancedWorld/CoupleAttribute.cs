using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    internal sealed class CoupleAttribute : System.Attribute
    {
        public string Pair { get; set; }
        public double Probability { get; set; }
        public string ChildType { get; set; }
    }
}
