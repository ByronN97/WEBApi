using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Model
{
    public class Room 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
    }
}
