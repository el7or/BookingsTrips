using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingsTrips.Helper
{
    public class Required_AR : RequiredAttribute
    {
        public Required_AR()
        {
            this.ErrorMessage = "{0} مطلوب !";
        }
    }
}