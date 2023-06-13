using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLiftCoreProjects.Data;
using TechLiftCoreProjects.Models;

namespace TechLiftCoreProjects.Controllers
{
    public class AppController : Controller
    {
        /// <summary>
        /// global bariable
        /// </summary>
        /// <returns></returns>
        /// 
        ///ORM -- object relational mapping
        /////enrity framework core EF Core 
        private readonly ApplicationDBcontext _context;

       

        public  AppController(ApplicationDBcontext context)
        {
            _context = context; 

        }


     


        public IActionResult bookappointment()
        {
            ViewBag.dpt = _context.Department.ToList();
            return View();
        }


        public IActionResult AppHome()
        {
            return View();
        }

        [HttpPost]
                                       /////model
        public IActionResult AppHome(Appointment otc )
        {
            _context.Appointment.Add(otc);
            _context.SaveChanges();


            return RedirectToAction("ShowAppointments");
        }


        public IActionResult ShowAppointments()
        {
            ///select top(1) from tblname
            ///linq
            ///
            _context.Appointment.First();/// first top tow// sql top 1

            /// selct top(5) from tblname
            /// 

            ///linq
            ///
            _context.Appointment.Take(5);
            // agggregate ///sum of all columns

            /// select marks as sum(marks) from table///sql
            /// 

          int result =   _context.Appointment.Select(a => a.AppId).Sum();
            //// average of a comumns 
            ///
            _context.Appointment.Select(a => a.AppId).Average();

            ///orderby asc // des// sorting by nanme
            ///
            /// order by asc
            _context.Appointment.OrderBy(a => a.PatientName).ToList();
            /// order by desc
            /// 
            _context.Appointment.OrderByDescending(a => a.appointmentDay).ToList();
            /// group by 
            /// 

            _context.Appointment.GroupBy(a => a.appointmentDay).ToList();


            return View(_context.Appointment.ToList());
        }


        public IActionResult Edit(int Id)
        {///select * from tblname where id = +id
            //LINQ language integrated query --- sql version
            return View(_context.Appointment.Where(k=>k.AppId == Id).SingleOrDefault());
        }


        [HttpPost]
        public IActionResult Edit(int Id,Appointment otc)
        {
            _context.Appointment.Update(otc);
            _context.SaveChanges();
            return RedirectToAction("ShowAppointments");
        }

        public IActionResult Details(int Id)
        {
            return View(_context.Appointment.Where(a=>a.AppId == Id).SingleOrDefault());
        }


        public IActionResult Delete(int Id)
        {
            /// delete from tblname where Id = id
            _context.Appointment.Remove(_context.Appointment.Where(a => a.AppId == Id).SingleOrDefault());
            _context.SaveChanges();
            return RedirectToAction("ShowAppointments");
        }

    }
}
