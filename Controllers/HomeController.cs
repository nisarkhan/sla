using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SLAUnavailability.Core.Models;
using SLAUnavailability.Web.Helper;
using SLAUnavailability.Web.Models;
using System;
using System.Data.Entity;

namespace SLAUnavailability.Web.Controllers
{
    public class HomeController :DbContextController
    {
        HelperClass helper = new HelperClass();

        public ActionResult Index()
        {             
            TempData["message"] = string.Empty;
            var vModel = helper.LoadCIUnavailabilitiesWithCache(_db);
            vModel.ListOfCompanies = SelectedByCompanyName();
            vModel.SELECTED_COMPANY = vModel.ListOfCompanies.First().ToString();
            LoadDropdowns();
            
            return View(vModel);
        }

        [HttpPost]
        public ActionResult ApproveAll(string selected_company_name)
        {
            if (selected_company_name == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            List<CIUnavailability> records = _db.CIUnavailabilities.Where(x => x.COMPANY == selected_company_name).ToList();

            if (records != null)
            {
                foreach (CIUnavailability item in records)
                {
                    item.IS_FINALIZED = "Y";
                    item.FINALIZED_BY = User.Identity.Name;
                    item.FINALIZED_DATE = DateTime.Now;
                    _db.Entry(item).State = EntityState.Modified;
                } 
                try
                {
                    int success = _db.SaveChanges();
                    if (success != 0)
                    {
                        TempData["type"] = "success";
                        TempData["title"] = "Approved All Record's";
                        TempData["message"] = string.Format("The record( '{0}' ) is successfully approved.", selected_company_name);
                    }
                }
                catch (Exception e)
                {
                    string error = e.Message;
                    throw;
                }
            }

            var vModel = new HomeIndexViewModel();
            vModel.CIUnavailability =   _db.CIUnavailabilities.Where(x => x.IS_FINALIZED != "Y").ToList(); 
                
            vModel.ListOfCompanies = SelectedByCompanyName();
            LoadDropdowns();

            //filtered
            List<CIUnavailability> vModelFiltered = vModel.CIUnavailability
                .Where(c => (string.IsNullOrEmpty(selected_company_name)
                        || c.COMPANY == selected_company_name)).ToList();


            vModel.CIUnavailability = vModelFiltered;
            vModel.SELECTED_COMPANY = selected_company_name;
            return View("Index", vModel);
        }

        [HttpPost]
        public ActionResult Index(string selected_company_name)
        {
            var vModel = new HomeIndexViewModel();
            vModel.CIUnavailability = _db.CIUnavailabilities.Where(x => x.IS_FINALIZED != "Y").ToList(); 

            vModel.ListOfCompanies = SelectedByCompanyName();
            LoadDropdowns();

            //filtered
            var vModelFiltered = vModel.CIUnavailability
                .Where(c => (string.IsNullOrEmpty(selected_company_name)
                        || c.COMPANY == selected_company_name)).ToList();


            vModel.CIUnavailability = vModelFiltered;
            vModel.SELECTED_COMPANY = selected_company_name; 
            return View(vModel);
        }
         

        [HttpPost]
        public ActionResult IncludeRecord(string id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var record = _db.CIUnavailabilities.FirstOrDefault(x => x.CI_UNAVAILABILITY_ID == id);

            if (record != null)
            {
                record.REVIEW_STATUS_ID = ReviewStatusType.Imported;
                record.REVIEW_STATUSREASON_ID = null;

                record.ADJUSTED_START_DATE = null;
                record.ADJUSTED_END_DATE = null;
                record.ADJUSTED_DURATION = null;
                record.ADJUSTED_ASSIGNMENT_STATUS = null;
                record.COMMENTS = null;
                record.UPDATED_BY = User.Identity.Name;
                record.UPDATED_DATE = DateTime.Now;

                try
                {
                    int success = _db.SaveChanges();
                    if (success == 1)
                    {
                        TempData["type"] = "success";
                        TempData["title"] = "Include Record!";
                        TempData["message"] = string.Format("The record( '{0}' ) is successfully Included.", record.NAME);
                    }
                }
                catch (Exception e)
                {
                    string error = e.Message;
                    throw;
                }
            }
            LoadDropdowns();
            //SelectedByCompanyName();
            var model = LoadCIUnavailabilitiesWithNoCache();
            model.ListOfCompanies = SelectedByCompanyName();
            //load the latest data from DB....
            return View("Index", model);

        }

        [HttpPost]
        public ActionResult ExcludeRecord(string id, string reasonReviewStatusId, string comments)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            CIUnavailability record = _db.CIUnavailabilities.FirstOrDefault(x => x.CI_UNAVAILABILITY_ID == id);

            if (record != null)
            {
                int? review_status_reason_id = Convert.ToInt32(reasonReviewStatusId);
                //modal popup - no changes required
                record.REVIEW_STATUS_ID = ReviewStatusType.Excluded;
                record.REVIEW_STATUSREASON_ID = review_status_reason_id;
                record.COMMENTS = comments;
                record.UPDATED_BY = User.Identity.Name;
                record.UPDATED_DATE = DateTime.Now;

                try
                {
                    int success = _db.SaveChanges();
                    if (success == 1)
                    {
                        TempData["type"] = "success";
                        TempData["title"] = "Exclude Record!";
                        TempData["message"] = string.Format("The record( '{0}' ) is successfully Excluded.", record.NAME);
                    }
                }
                catch (Exception e)
                {
                    string error = e.Message;
                    throw;
                }
            }
            LoadDropdowns();
            //SelectedByCompanyName();
            var model = LoadCIUnavailabilitiesWithNoCache();
            model.ListOfCompanies = SelectedByCompanyName();
            //load the latest data from db....
            return View("Index", model); 
        }
       
        public ActionResult Edit(string id, string model_id)
        {
            if (id == null) {return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}

            LoadDropdowns();   
            var client = _db.CIUnavailabilities.FirstOrDefault(x => x.CI_UNAVAILABILITY_ID == id); 
            // Changing the values of the record 
            if (client == null)
            {
                return HttpNotFound();
            } 
            return View(client); 
        } 

        [HttpPost]
        public ActionResult Edit(CIUnavailability record, string id)
        {
            var data = new CIUnavailability(); 
            data = _db.CIUnavailabilities.FirstOrDefault(x => x.CI_UNAVAILABILITY_ID == id);

            data.REVIEW_STATUSREASON_ID = record.REVIEW_STATUSREASON_ID;
            data.ADJUSTED_START_DATE = record.ADJUSTED_START_DATE;
            data.ADJUSTED_END_DATE = record.ADJUSTED_END_DATE;
            data.ADJUSTED_DURATION = record.ADJUSTED_DURATION;
            data.ADJUSTED_ASSIGNMENT_STATUS = record.ADJUSTED_ASSIGNMENT_STATUS;
            data.COMMENTS = record.COMMENTS;
            data.UPDATED_BY = User.Identity.Name;
            data.UPDATED_DATE = DateTime.Now;

            // If any changes have been made to this record, mark this as an Adjusted record
            if (data.ADJUSTED_START_DATE != null ||
                data.ADJUSTED_END_DATE != null ||
                data.ADJUSTED_DURATION != null ||
                !string.IsNullOrEmpty(data.COMMENTS) ||
                !string.IsNullOrEmpty(data.ADJUSTED_ASSIGNMENT_STATUS))
            {
                data.REVIEW_STATUS_ID = ReviewStatusType.Adjusted;
            }
            else
            {
                // No changes so remains as Imported.  Cannot reach this code if record has been perviously excluded, so it's safe to reset to Imported if all adjusted fields are null/empty.
                data.REVIEW_STATUS_ID = ReviewStatusType.Imported;
                data.REVIEW_STATUSREASON_ID = null;
            }

            try
            {
                int success = _db.SaveChanges();
                if(success == 1)
                {
                    TempData["type"] = "success";
                    TempData["title"] = "Update successfully!";
                    TempData["message"] = "The current record is saved successfully.";
                }
            }
            catch (Exception e)
            {
                string error = e.Message; 
                throw;
            }

            LoadDropdowns();
            return View("Edit", data);
        }

        private void LoadDropdowns()
        {
            var reviewStatusReason = helper.LoadReviewStatusReasonDataWithCache(_db);
            ViewBag.DropDownReviewStatusReason = reviewStatusReason; 
        }

        public IEnumerable<SelectListItem> SelectedByCompanyName()
        {
            var model =   helper.LoadCompanyNameDataWithCache(_db);  
            return model.ToList(); 
        }

        public HomeIndexViewModel LoadCIUnavailabilitiesWithNoCache()
        {
            return new HomeIndexViewModel { CIUnavailability = _db.CIUnavailabilities.ToList() }; 
        }
    }
}