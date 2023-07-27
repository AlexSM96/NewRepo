namespace SchoolSchedule.Domain.ViewModels;

public class ScheduleEditViewModel
{
    public int WeekDay { get; set; }

    public string WeekDayName { get; set; }

    public int ClassId { get; set; }

    public string ClassName { get; set; }

    public int LessonNumber {get; set;}

    public int TeacherId { get; set;}

    public string TeacherName { get; set; }

    public string LessonName { get; set; }
}