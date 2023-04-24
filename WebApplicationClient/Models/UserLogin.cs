using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplicationClient.Models
{
    public class UserLogin
    {
        public int id { get; set; }
        public string? UserName { get; set; }

        public string? passcode { get; set; }
        public int isActive { get; set; }
    }
}
