using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class ClassViewModel
{
    [Required]
    [MaxLength(4)]
    public string ClassName { get; set; }
}