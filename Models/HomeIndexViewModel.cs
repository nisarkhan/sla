using System.Collections.Generic;
using SLAUnavailability.Core.Models;
using System.Web.Mvc;

namespace SLAUnavailability.Web.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<SelectListItem> ListOfCompanies { get; set; }
        public IEnumerable<SelectListItem> ListOfReviewStatusReason { get; set; }

        public string SELECTED_COMPANY { get; set; }

        public List<CIUnavailability> CIUnavailability { get; set; }

        public HomeIndexViewModel()
        {
            CIUnavailability = new List<CIUnavailability>();
        }
    }
}