using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Attributes
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class| AttributeTargets.Property, AllowMultiple = false)]
    internal class DescriptionAttribute:Attribute
    {
        private string _description;
        public DescriptionAttribute(string description)
        {
            _description = description;
        }
    }
}
