
namespace App.Web.Models
{
    public class Hospital
    {

        public float Latitude { get; set; } //coordX 
        public float Longitude { get; set; } //coordY

        public TimeOnly StartWorkingHours { get; set; }
        public TimeOnly EndWorkingHours { get; set; }
        public string LicenseNumber { get; set; }

        public Guid UserId { get; set; } //fk to user
        public User User { get; set; }

        public ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();

    }
}
