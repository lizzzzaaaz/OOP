//using System;

namespace Lab1_OOP
{
    public class Student
    {
        private string _id;
        private string _fullName;
        private int _course;
        private double _averageGrade;
        private int[] _grades;

        public Student(string id, string fullName, int course, double averageGrade, int[]? initialGrades)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Номер зачетной книжки не может быть пустым!");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО студента не может быть пустым!");

            if (course < 1 || course > 6)
                throw new ArgumentException("Курс должен быть в диапазоне от 1 до 6!");

            if (averageGrade < 2.0 || averageGrade > 5.0)
                throw new ArgumentException("Средний балл должен быть от 2.0 до 5.0!");

            _id = id;
            _fullName = fullName;
            _course = course;
            _averageGrade = averageGrade;

            if (initialGrades != null)
            {
                _grades = (int[])initialGrades.Clone();
            }
            else
            {
                _grades = new int[0];
            }
        }

        public Student(string id, string fullName)
            : this(id, fullName, 1, 4.0, new int[] { 4, 4, 4 })
        {
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

        public double GetAverageGrade()
        {
            return _averageGrade;
        }

        public void PromoteToNextCourse()
        {
            if (_course >= 6)
                throw new InvalidOperationException("Студент уже на последнем курсе!");
            _course++;
        }

        public void AddMark(int mark)
        {
            AddMark(new int[] { mark });
        }

        public void AddMark(int[]? marks)
        {
            if (marks == null || marks.Length == 0) return;

            foreach (var m in marks)
            {
                if (m < 2 || m > 5)
                    throw new ArgumentException($"Оценка {m} недопустима (должна быть от 2 до 5)!");
            }

            int oldLen = _grades.Length;
            Array.Resize(ref _grades, oldLen + marks.Length);
            Array.Copy(marks, 0, _grades, oldLen, marks.Length);

            RecalculateAverage();
        }

        private void RecalculateAverage()
        {
            if (_grades.Length == 0) return;
            double sum = 0;
            foreach (var g in _grades)
            {
                sum += g;
            }
            _averageGrade = sum / _grades.Length;
        }

        public int[] GetGradesUnsafe()
        {
            return _grades;
        }

        public int[] GetGradesSafe()
        {
            return (int[])_grades.Clone();
        }

        public override string ToString()
        {
            return $"[Зачетка: {_id}] {_fullName}, Курс: {_course}, Ср.балл: {_averageGrade:F2}";
        }
    }
}