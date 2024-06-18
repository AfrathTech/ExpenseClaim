using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpenseApi.Models
{
    public class InputRequest
    {
        [Required]
        [DefaultValue("")]
        public  string Text { get; set; }
    }
}
