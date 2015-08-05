using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SLAUnavailability.Core.Models;
using SLAUnavailability.Web.Models;

namespace SLAUnavailability.Web.Helper
{
    public class HelperClass  
    {
        public HomeIndexViewModel HomeViewModel { get; set; }

        public HomeIndexViewModel LoadCIUnavailabilities(DataCenterDB _db)
        {
            var vModel = new HomeIndexViewModel();
            vModel = new HomeIndexViewModel { CIUnavailability = _db.CIUnavailabilities.Where(x => x.IS_FINALIZED != "Y").ToList() };
            return vModel;
        }

        public HomeIndexViewModel LoadCIUnavailabilitiesWithCache(DataCenterDB _db)
        { 
            var vModel = new HomeIndexViewModel();
            if (SessionHelper.GetSessionHomeIndexViewModel() == null)
            {
                vModel = new HomeIndexViewModel { CIUnavailability = _db.CIUnavailabilities.Where(x => x.IS_FINALIZED != "Y").ToList() };
                SessionHelper.StoreSessionHomeIndexViewModel(vModel);
            }
            else
            {
                vModel = SessionHelper.GetSessionHomeIndexViewModel();
            }
            return vModel;
        }
         
        public IEnumerable<SelectListItem> LoadCompanyNameDataWithCache(DataCenterDB _db)
        {
            IEnumerable<SelectListItem> selectList;
            if (SessionHelper.GetSessionCompanyName() == null)
            {
                HomeIndexViewModel vModel = new HomeIndexViewModel { CIUnavailability = _db.CIUnavailabilities.ToList() };
                var stands = vModel.CIUnavailability.Select(x => x.COMPANY).Where(e => e != null).Distinct().ToList();
                 selectList = 
                    from s in stands
                        select new SelectListItem
                        {
                            Value = s,
                            Text = s   
                        };
                vModel.ListOfCompanies = selectList;
                SessionHelper.StoreSessionCompanyName(selectList);
            }
            else
            {
                selectList = SessionHelper.GetSessionCompanyName();
            }
            return selectList;
        }

        public IEnumerable<SelectListItem> LoadReviewStatusDataWithCache(DataCenterDB _db)
        {
            IEnumerable<SelectListItem> reviewStatusReason;
            if (SessionHelper.GetSessionReviewStatus() == null)
            {
                reviewStatusReason = _db.ReviewStatus.Select(b => new SelectListItem
                {
                    Value = b.ID.ToString(),
                    Text = b.STATUS_NAME
                }).ToList();
                SessionHelper.StoreSessionReviewStatus(reviewStatusReason);
            }
            else
            {
                reviewStatusReason = SessionHelper.GetSessionReviewStatus();
            }
            return reviewStatusReason;
        }

        public IEnumerable<SelectListItem> LoadReviewStatusReasonDataWithCache(DataCenterDB _db)
        {
            IEnumerable<SelectListItem> reviewStatusReason;
            if (SessionHelper.GetSessionReviewStatusReason() == null)
            {
                reviewStatusReason = _db.ReviewStatusReasons.Where(x => x.STATUS_ID == ReviewStatusType.Adjusted).Select(b => new SelectListItem
                {
                    Value = b.ID.ToString(),
                    Text = b.REVIEW_STATUS_REASON
                }).ToList();
                //reviewStatusReason = _db.ReviewStatusReasons.Select(b => new SelectListItem
                //{
                //    Value = b.ID.ToString(),
                //    Text = b.REVIEW_STATUS_REASON
                //}).ToList();
                SessionHelper.StoreSessionReviewStatusReason(reviewStatusReason);
            }
            else
            {
                reviewStatusReason = SessionHelper.GetSessionReviewStatusReason();
            }
            return reviewStatusReason;
        }

        public static DateTime? CalculatedStartEndDate(DateTime? adjDate, DateTime? actDate)
        {
            DateTime? date;
            if (adjDate != null)
            {
                date = adjDate.Value;
            }
            else if (actDate !=null)
            {
                date = actDate.Value;
            }
            else
            {
                date = null;
            }
            return date;
        }

        public static string CalculatedDuration(int? adjDuration, string actDuration)
        {
            string duration;
            if (adjDuration != null)
            {
                duration = adjDuration.Value.ToString();
            }
            else if (actDuration != null)
            {
                duration = actDuration;
            }
            else
            {
                duration = "";
            }
            return duration;
        }
    }
}