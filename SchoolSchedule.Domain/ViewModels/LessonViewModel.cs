using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class LessonViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название предмета")]
    [MaxLength(50, ErrorMessage = "Превышено максимальное кол-во символов: 50")]
    [MinLength(2, ErrorMessage = "Минимальное кол-во символов: 2")]
    public string LessonName { get; set; }

    public string ClassRoom { get; set; }
}