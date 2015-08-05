using SLAUnavailability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLAUnavailability.Web.Helper
{
    public class ExtractFormCollectionData
    {
        public List<CIUnavailability> XtractFormCollectionData(FormCollection formCollection)
        {
            //int i = 0;
            List<CIUnavailability> excelDataList = new List<CIUnavailability>();
            CIUnavailability model = new CIUnavailability();

            foreach (string formData in formCollection)
            {
                //if (formData == "selectedRow_" + i)
                if (formData == "selectedRow_")
                {
                    model.CI_UNAVAILABILITY_ID = formCollection[formData].ToString(); 
                    //i++;
                    excelDataList.Add(model);
                    model = new CIUnavailability();
                }
            }
            return excelDataList;
        }



    }
}