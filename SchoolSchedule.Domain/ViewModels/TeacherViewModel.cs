using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class TeacherViewModel
{
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "Введите ФИО преподавателя")]
    [MaxLength(35, ErrorMessage = "Превышено максимальное кол-во символов: 35")]
    [MinLength(2, ErrorMessage = "Минимальное кол-во символов: 2")]
    public string TeacherName { get; set; }
}