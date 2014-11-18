namespace UniversitySystem.Web.Controllers
{
    using System.Web.Mvc;
    
    using UniversitySystem.Data;

    public class BaseController : Controller
    {
        protected readonly IUniversitySystemData Data;

        public BaseController()
            : this(new UniversitySystemData())
        {
        }

        public BaseController(IUniversitySystemData data)
        {
            this.Data = data;
        }
    }
}