using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Nurse Nurse { get; set; }
        [ForeignKey("Nurse")]
        public int NurseId { get; set; }
        public Baby Baby { get; set; }
        [ForeignKey("Baby")]
        public int BabyId { get; set; }
        public string Description { get; set; }

    }
}
