using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Model
{
    public class Reservation 
    {

       public int  Id { get; set; }
        public int userId { get; set; }
        public int RoomId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
