using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class GiftViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Title")]
        public string Title { get; set; } = "";

        [Required]
        [Display(Name="Description")]
        public string Description { get; set; } = "";

        [Required]
        [Display(Name="URL")]
        public string Url { get; set; } = "";

        [Required]
        [Display(Name="Priority")]
        public int Priority { get; set; }

        [Display(Name="User")]
        public int UserID { get; set; }
    }
}