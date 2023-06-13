using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLiftCoreProjects.Models
{
   
    public class DoctorCases
    {
        [Key]
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        [DataType(DataType.Date)]
       // [Column(TypeName ="nvarchar(20)")]
        public DateTime casedate { get; set; }  

        public string caseDescription { get; set; } 
    }
}
