using Oris_First_Semestrovka.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_semestrovka_test.Attributes
{
    public class GetAttribute : HttpMethodAttribute
    {
        public GetAttribute(string actionName) : base(actionName)
        {
        }
    }
}
