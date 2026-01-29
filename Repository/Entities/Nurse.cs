namespace Repository.Entities
{
    public class Nurse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection< Appointment> Appointment=new List<Appointment>();



    }
}
