using System.ComponentModel.DataAnnotations;

namespace DoctorHistoryAPI.Models
{
    public class DocHistory
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
