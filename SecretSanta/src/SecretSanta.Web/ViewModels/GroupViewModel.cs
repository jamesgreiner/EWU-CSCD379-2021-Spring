using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class GroupViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Group Name")]
        public string GroupName { get; set; } = "";
    }
}