using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TechLiftCoreProjects.Areas.Identity.Data;
using TechLiftCoreProjects.Models;
using TechLiftCoreProjects.Models.VMs;

namespace TechLiftCoreProjects.Data
{
    public class ApplicationDBcontext : IdentityDbContext<ProjectsUser>
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
        {

        }
        [NotMapped]
        public DbSet<DoctorVM> doctorView { get; set; }

        public DbSet<DocHistory> docHistory { get; set; }
        //Db Sets Properties me aati he
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<DoctorInfo> DoctorInfo { get; set; }   
        public DbSet<Department> Department { get; set; }
        public DbSet<TechLiftCoreProjects.Models.DoctorCases> DoctorCases { get; set; } = default!;
    }
}
