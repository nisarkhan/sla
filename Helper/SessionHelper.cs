using SLAUnavailability.Web.Controllers;
using SLAUnavailability.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLAUnavailability.Web.Helper
{ 

    public static class SessionHelper  
    {
        public static void StoreSessionHomeIndexViewModel(HomeIndexViewModel list)
        {
            HttpContext.Current.Session.Add("HomeIndexViewModel", list);
        }

        public static HomeIndexViewModel GetSessionHomeIndexViewModel()
        {
            return ((HomeIndexViewModel)HttpContext.Current.Session["HomeIndexViewModel"]);
        }

        public static void StoreSessionCompanyName(IEnumerable<SelectListItem> list)
        {
            HttpContext.Current.Session.Add("CompanyName", list);
        }

        public static IEnumerable<SelectListItem> GetSessionCompanyName()
        {
            return ((IEnumerable<SelectListItem>)HttpContext.Current.Session["CompanyName"]);
        }

        public static void StoreSessionReviewStatus(IEnumerable<SelectListItem> list)
        {
            HttpContext.Current.Session.Add("ReviewStatus", list);
        }

        public static IEnumerable<SelectListItem> GetSessionReviewStatus()
        {
            return ((IEnumerable<SelectListItem>)HttpContext.Current.Session["ReviewStatus"]);
        }

        public static void StoreSessionReviewStatusReason(IEnumerable<SelectListItem> list)
        {
            HttpContext.Current.Session.Add("ReviewStatusReason", list);
        }

        public static IEnumerable<SelectListItem> GetSessionReviewStatusReason()
        {
            return ((IEnumerable<SelectListItem>)HttpContext.Current.Session["ReviewStatusReason"]);
        }


    }
}