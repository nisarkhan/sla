using System.Web.Mvc;
using SLAUnavailability.Core.Models;

namespace SLAUnavailability.Web.Controllers
{
    public abstract class DbContextController : Controller
    {
        protected DataCenterDB _db;

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            _db = new DataCenterDB(User.Identity.Name);
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
