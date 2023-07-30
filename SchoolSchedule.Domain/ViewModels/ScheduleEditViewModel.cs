using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class ScheduleEditViewModel
{
    public int WeekDay { get; set; }

    [Required(ErrorMessage = "Введите день недели")]
    [MaxLength(2, ErrorMessage = "Превышено максимальное кол-во символов")]
    public string WeekDayName { get; set; }

    public int ClassId { get; set; }

    [Required(ErrorMessage = "Введите название класса")]
    [MaxLength(4, ErrorMessage = "Превышено максимальное кол-во символов")]
    public string ClassName { get; set; }

    [Required(ErrorMessage = "Введите номер урока")]
    [MaxLength(1, ErrorMessage = "Превышено максимальное кол-во символов")]
    public int LessonNumber {get; set;}

    public int TeacherId { get; set;}

    [Required(ErrorMessage = "Введите ФИО преподавателя")]
    [MaxLength(50, ErrorMessage = "Превышено максимальное кол-во символов")]
    public string TeacherName { get; set; }

    [Required(ErrorMessage = "Введите название урока")]
    [MaxLength(50, ErrorMessage = "Превышено максимальное кол-во символов")]
    public string LessonName { get; set; }
}