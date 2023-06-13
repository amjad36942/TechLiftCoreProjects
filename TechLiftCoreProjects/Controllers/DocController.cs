using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechLiftCoreProjects.Data;
using TechLiftCoreProjects.Models;
using TechLiftCoreProjects.Models.VMs;
using TechLiftCoreProjects.Repositories;

namespace TechLiftCoreProjects.Controllers
{

   
    public class DocController : Controller
    {
        private readonly ApplicationDBcontext _context;
        private readonly IDoctorRep _rep;
        static string sortorder = "asc";

        public DocController(ApplicationDBcontext context, IDoctorRep rep)
        {
            _context = context;
            _rep = rep;
        }



        [Route("Clinic/alldoctors")]
        public IActionResult show(int deptid, string day,IEnumerable<DoctorInfo> doc)

        {


            if (deptid != 0 || day != null)

            {
                var result = _rep.showbydepartorday(deptid, day);
                ///join inner join 
                ///select * from tbl1 inner join tbl2 on tbl1.id= tbl2.id
                ///LInq
                ///
                //var result = (from c in _context.DoctorInfo

                //              join k in _context.Department on c.DeptId equals k.DeptId

                //              where c.DeptId == deptid || c.DoctorDays == day

                //              select new DoctorVM
                //              {
                //                  DepartmentName = k.DeptName,
                //                  DoctorDays = c.DoctorDays,
                //                  DoctorFullName = c.PersonFullName,

                //              }).ToList();

                //return View(_context.DoctorInfo.Where(a=>a.DeptId==deptid).ToList());
                return PartialView("_SearchDoc", result);

            }
            //return View(_context.DoctorInfo.ToList());
            if(doc.Count() == 0)
            {
                return View(_rep.ShowAll());
            }
            return View(doc);
           
        }
        [AllowAnonymous]
        [Route("clinic/NewCase")]
        public IActionResult AddDoc()
        {
            ViewBag.dept = _context.Department.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddDoc(DoctorInfo inf)
        {
            _rep.CreateDoc(inf);
            //_context.DoctorInfo.Add(inf);
            //_context.SaveChanges();
            ViewBag.dept = _context.Department.ToList();
            return RedirectToAction("show");
        }

        public IActionResult DocCases()
        {
            ViewBag.doctors = _context.DoctorInfo.ToList();
            return View();
        }


        public IActionResult DocDetails(Guid id)
        {
            return View(_rep.GetDoctorById(id));

        }

        public IActionResult AddHistory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddHistory(DocHistory h)
        {
            _context.docHistory.Add(h);
            _context.SaveChanges();
            return View();

        }

        public async Task<IActionResult> GetHistoryFromApi()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7140/api/History");
        var msg =  await   client.GetAsync("https://localhost:7140/api/History");
            ///json format data convert serialization 
            ///
            /// from json to anyformat deserilization
            /// 

            var data = await client.GetStringAsync("https://localhost:7140/api/History");

      List<DocHistory> lst    =  JsonConvert.DeserializeObject<List<DocHistory>>(data);
            return View(lst);
        }


        public IActionResult SortData()
        {
            if(sortorder == "asc")
            {
                sortorder = "desc";
                IEnumerable<DoctorInfo> docLst = _context.DoctorInfo.OrderByDescending(a => a.PersonFirstName).ToList();
                return View("show",docLst);
            }
          else  if (sortorder == "desc")
            {
                sortorder = "asc";
                IEnumerable<DoctorInfo> docLst = _context.DoctorInfo.OrderBy(a => a.PersonFirstName).ToList();
                return View("show", docLst);
            }
            return RedirectToAction("show");
        }
    }
}
