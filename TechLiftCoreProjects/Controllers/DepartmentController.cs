using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLiftCoreProjects.Data;
using TechLiftCoreProjects.Models;
using TechLiftCoreProjects.Models.VMs;

namespace TechLiftCoreProjects.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDBcontext _context;
        public DepartmentController(ApplicationDBcontext context) {
            _context = context;
        }
        public IActionResult Index()
        {
         IEnumerable<DoctorVM> doctor  =  _context.doctorView.FromSqlRaw("selectdatafromtables");
            
          //  IEnumerable<Department> dpt = _context.Department.FromSqlRaw("select * from department");
            // return View(dpt);

            return PartialView("_Search", doctor);
            return PartialView("_Search", doctor);


        }
    }
}
