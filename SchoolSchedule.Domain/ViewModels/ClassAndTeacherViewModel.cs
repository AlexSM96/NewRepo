using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Domain.ViewModels;

public class ClassAndTeacherViewModel
{
    public string ClassName { get; set; }

    public string LessonName { get; set; }

    public string TeacherName { get; set; }
}