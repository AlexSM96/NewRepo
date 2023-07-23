using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class ScheduleViewModel
{
    [Display(Name = "День недели")]
    public string DayWeek { get; set; }

    [Display(Name = "Класс")]
    public string ClassName { get; set; }

    [Display(Name = "Номер урока")]
    public int LessonNumber { get; set; }

    [Display(Name = "Время начала")]
    public string LessonTimeStart { get; set; }

    [Display(Name = "Время окончания")]
    public string LessonTimeEnd { get; set; }

    [Display(Name = "Предмет")]
    public string LessonName { get; set; }

    [Display(Name = "Учитель")]
    public string TeacherName { get; set; }

    [Display(Name = "Аудитория")]
    public string ClassRoom { get; set; }
}