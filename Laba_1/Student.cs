

namespace Lab2_OOP
{
    public class EducationForm
    {
        private string _name;

        public EducationForm(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }

    public class Achievement
    {
        private string _title;

        public Achievement(string title)
        {
            _title = title;
        }

        public string GetTitle()
        {
            return _title;
        }
    }

    public abstract class Student
    {
        public const int MAX_COURSE = 6;
        private static int _studentCount = 0;

        protected string _id;
        protected string _fullName;
        protected int _course;
        protected Marks _marks;
        protected ScholarshipRule _scholarshipRule;

        protected EducationForm _educationForm;
        protected List<Achievement> _achievements;

        protected Student(string id, string fullName, int course, int[] initialGrades, ScholarshipRule rule, EducationForm educationForm, List<Achievement> achievements = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Номер зачетной книжки не может быть пустым!");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("ФИО студента не может быть пустым!");
            }

            if (course < 1 || course > MAX_COURSE)
            {
                throw new ArgumentException($"Курс должен быть в диапазоне от 1 до {MAX_COURSE}!");
            }

            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule), "Правило стипендии не может быть null!");
            }

            if (educationForm == null)
            {
                throw new ArgumentNullException(nameof(educationForm), "Форма обучения не может быть null!");
            }

            _id = id;
            _fullName = fullName;
            _course = course;
            _marks = new Marks(initialGrades);
            _scholarshipRule = rule;
            _educationForm = educationForm;

            if (achievements != null)
            {
                _achievements = achievements;
            }
            else
            {
                _achievements = new List<Achievement>();
            }

            _studentCount++;
        }

        public string GetId()
        {
            return _id;
        }

        public string GetFullName()
        {
            return _fullName;
        }

        public int GetCourse()
        {
            return _course;
        }

        public EducationForm GetEducationForm()
        {
            return _educationForm;
        }

        public List<Achievement> GetAchievements()
        {
            return _achievements;
        }

        public void AddAchievement(Achievement achievement)
        {
            if (achievement != null)
            {
                _achievements.Add(achievement);
            }
        }

        public virtual double GetRating()
        {
            double bonus = _achievements.Count * 0.05;
            return GetAverageGrade() + bonus;
        }

        public abstract bool CanBeAdmitted();
        public abstract bool ShouldBeExpelled();

        public void Promote()
        {
            if (_course >= MAX_COURSE)
            {
                throw new InvalidOperationException("Студент уже на последнем курсе!");
            }
            _course++;
        }

        public void AddMark(int mark)
        {
            _marks.AddMark(mark);
        }

        public double GetAverageGrade()
        {
            return _marks.GetAverage();
        }

        public double GetScholarship()
        {
            return _scholarshipRule.CalculateScholarship(GetAverageGrade());
        }

        public void SetScholarshipRule(ScholarshipRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }
            _scholarshipRule = rule;
        }

        public override string ToString()
        {
            string achievementsStr = "нет";
            if (_achievements.Count > 0)
            {
                List<string> titles = new List<string>();
                foreach (var a in _achievements)
                {
                    titles.Add(a.GetTitle());
                }
                achievementsStr = string.Join(", ", titles);
            }

            return $"[Зачетка: {_id}] {_fullName}, Курс: {_course}, Форма: {_educationForm.GetName()}, Достижения: [{achievementsStr}], Ср.балл: {GetAverageGrade():F2}, Рейтинг: {GetRating():F2}, Стипендия: {GetScholarship()} руб.";
        }
    }

    public class BudgetStudent : Student
    {
        public BudgetStudent(string id, string fullName, int course, int[] grades, ScholarshipRule rule, List<Achievement> achievements = null)
            : base(id, fullName, course, grades, rule, new EducationForm("Бюджет"), achievements)
        {
        }

        public override bool CanBeAdmitted()
        {
            return GetAverageGrade() >= 3.0;
        }

        public override bool ShouldBeExpelled()
        {
            return GetAverageGrade() < 2.5;
        }
    }

    public class ContractStudent : Student
    {
        public ContractStudent(string id, string fullName, int course, int[] grades, ScholarshipRule rule, List<Achievement> achievements = null)
            : base(id, fullName, course, grades, rule, new EducationForm("Контракт"), achievements)
        {
        }

        public override bool CanBeAdmitted()
        {
            return GetAverageGrade() >= 2.5;
        }

        public override bool ShouldBeExpelled()
        {
            return GetAverageGrade() < 2.0;
        }
    }

    public class ExchangeStudent : Student
    {
        public ExchangeStudent(string id, string fullName, int course, int[] grades, ScholarshipRule rule, List<Achievement> achievements = null)
            : base(id, fullName, course, grades, rule, new EducationForm("Обмен"), achievements)
        {
        }

        public override bool CanBeAdmitted()
        {
            return GetAverageGrade() >= 3.5;
        }

        public override bool ShouldBeExpelled()
        {
            return GetAverageGrade() < 3.0;
        }
    }

    public class TargetStudent : Student, IWorkable
    {
        private string _enterpriseName;
        private int _workingYearsRequired;

        public TargetStudent(string id, string fullName, int course, int[] grades, ScholarshipRule rule, string enterpriseName, int workingYearsRequired, List<Achievement> achievements = null)
            : base(id, fullName, course, grades, rule, new EducationForm("Целевое"), achievements)
        {
            if (string.IsNullOrWhiteSpace(enterpriseName))
            {
                throw new ArgumentException("Название предприятия не может быть пустым!");
            }

            if (workingYearsRequired <= 0)
            {
                throw new ArgumentException("Срок отработки должен быть больше 0!");
            }

            _enterpriseName = enterpriseName;
            _workingYearsRequired = workingYearsRequired;
        }

        public string GetEnterpriseName()
        {
            return _enterpriseName;
        }

        public int GetWorkingYearsRequired()
        {
            return _workingYearsRequired;
        }

        public override bool CanBeAdmitted()
        {
            return GetAverageGrade() >= 3.0;
        }

        public override bool ShouldBeExpelled()
        {
            return GetAverageGrade() < 2.8;
        }

        public override string ToString()
        {
            return base.ToString() + $", Целевик: {_enterpriseName} (отработка: {_workingYearsRequired} лет)";
        }
    }
}