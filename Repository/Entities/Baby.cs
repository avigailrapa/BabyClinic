namespace Repository.Entities
{
    public class Baby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public int Weight { get; set; }
        public string  ImageUrl { get; set; }
        public ICollection<Appointment> Appointment = new List<Appointment>();

      

    }
}
