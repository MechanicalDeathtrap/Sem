using Oris_First_Semestrovka.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_semestrovka_test.Attributes
{
    public class PostAttribute : HttpMethodAttribute
    {
        public PostAttribute(string actionName) : base(actionName)
        {
        }
    }
}
