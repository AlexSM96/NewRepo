using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class ClassViewModel
{
    [Required(ErrorMessage = "Введите название класса")]
    [MaxLength(4, ErrorMessage = "Превышено макcимальное кол-во символов(включая пробел): 4")]
    [MinLength(2, ErrorMessage = "Минимальное кол-во символов: 2")]
    public string ClassName { get; set; }
}