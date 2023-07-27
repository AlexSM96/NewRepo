using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class ClassViewModel
{
    [Required(ErrorMessage = "Введите название класса")]
    [MaxLength(4, ErrorMessage = "Максимальное количество символов: 4")]
    public string ClassName { get; set; }
}