using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace CarParts.Model
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int BlockNumber { get; set; }
        public int ZipCode { get; set; }
    }
}