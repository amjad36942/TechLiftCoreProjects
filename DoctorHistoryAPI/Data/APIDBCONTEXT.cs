
using DoctorHistoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorHistoryAPI.Data
{
    public class APIDBCONTEXT : DbContext
    {
        public APIDBCONTEXT(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DocHistory> docHistory { get; set; }
    }
}
