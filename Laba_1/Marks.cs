namespace Lab2_OOP
{
    public class Marks
    {
        private int[] _grades;

        public Marks(int[]? initialGrades)
        {
            if (initialGrades != null)
            {
                foreach (var m in initialGrades)
                {
                    if (m < 2 || m > 5)
                    {
                        throw new ArgumentException($"Оценка {m} недопустима! Должна быть от 2 до 5.");
                    }
                }
                _grades = (int[])initialGrades.Clone();
            }
            else
            {
                _grades = new int[0];
            }
        }

        public void AddMark(int mark)
        {
            if (mark < 2 || mark > 5)
            {
                throw new ArgumentException($"Оценка {mark} недопустима! Должна быть от 2 до 5.");
            }

            int oldLen = _grades.Length;
            Array.Resize(ref _grades, oldLen + 1);
            _grades[oldLen] = mark;
        }

        public double GetAverage()
        {
            if (_grades.Length == 0)
            {
                return 0.0;
            }

            double sum = 0;
            foreach (var g in _grades)
            {
                sum += g;
            }
            return sum / _grades.Length;
        }

        public int[] GetCopy()
        {
            return (int[])_grades.Clone();
        }
    }
}