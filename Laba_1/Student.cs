namespace Lab2_OOP
{
    public abstract class Student
    {
        public const int MAX_COURSE = 6;
        private static int _studentCount = 0;

        protected string _id;
        protected string _fullName;
        protected int _course;
        protected Marks _marks;
        protected ScholarshipRule _scholarshipRule;

        protected Student(string id, string fullName, int course, int[]? initialGrades, ScholarshipRule rule)
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

            _id = id;
            _fullName = fullName;
            _course = course;
            _marks = new Marks(initialGrades);
            _scholarshipRule = rule;
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

        public abstract double GetRating();

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
            return $"[Зачетка: {_id}] {_fullName}, Курс: {_course}, Ср.балл: {GetAverageGrade():F2}, Рейтинг: {GetRating():F2}, Стипендия: {GetScholarship()} руб.";
        }
    }

    public class BudgetStudent : Student
    {
        public BudgetStudent(string id, string fullName, int course, int[]? grades, ScholarshipRule rule)
            : base(id, fullName, course, grades, rule)
        {
        }

        public override double GetRating()
        {
            return GetAverageGrade() * 1.1;
        }
    }

    public class ContractStudent : Student
    {
        public ContractStudent(string id, string fullName, int course, int[]? grades, ScholarshipRule rule)
            : base(id, fullName, course, grades, rule)
        {
        }

        public override double GetRating()
        {
            return GetAverageGrade();
        }
    }

    public class ExchangeStudent : Student
    {
        public ExchangeStudent(string id, string fullName, int course, int[]? grades, ScholarshipRule rule)
            : base(id, fullName, course, grades, rule)
        {
        }

        public override double GetRating()
        {
            return GetAverageGrade() * 0.95;
        }
    }
}