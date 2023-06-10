using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NinjectPractice.Exceptions
{
    public class ElementNotFound : Exception
    {
        public ElementNotFound(string message) : base(message) {}
    }
}