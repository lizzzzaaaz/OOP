namespace Lab2_OOP
{
    public class Group
    {
        private string _groupName;
        private Student?[] _students;
        private int _count;

        public Group(string groupName, int capacity = 30)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentException("Название группы не может быть пустым!");
            }

            if (capacity <= 0)
            {
                throw new ArgumentException("Вместимость группы должна быть больше 0!");
            }

            _groupName = groupName;
            _students = new Student?[capacity];
            _count = 0;
        }

        public bool AddStudent(Student? student)
        {
            if (student == null)
            {
                return false;
            }

            for (int i = 0; i < _count; i++)
            {
                Student? existing = _students[i];
                if (existing != null && existing.GetId() == student.GetId())
                {
                    throw new InvalidOperationException($"Студент с зачёткой '{student.GetId()}' уже зачислен в группу!");
                }
            }

            if (_count >= _students.Length)
            {
                Array.Resize(ref _students, _students.Length * 2);
            }

            _students[_count] = student;
            _count++;
            return true;
        }

        public bool RemoveStudent(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            int index = -1;
            for (int i = 0; i < _count; i++)
            {
                Student? existing = _students[i];
                if (existing != null && existing.GetId() == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return false;
            }

            for (int i = index; i < _count - 1; i++)
            {
                _students[i] = _students[i + 1];
            }

            _students[_count - 1] = null;
            _count--;
            return true;
        }

        public double GetGroupAverage()
        {
            if (_count == 0)
            {
                return 0.0;
            }

            double sum = 0;
            for (int i = 0; i < _count; i++)
            {
                Student? existing = _students[i];
                if (existing != null)
                {
                    sum += existing.GetAverageGrade();
                }
            }
            return sum / _count;
        }

        public Student[] GetStudentsSafe()
        {
            Student[] result = new Student[_count];
            for (int i = 0; i < _count; i++)
            {
                Student? existing = _students[i];
                if (existing != null)
                {
                    result[i] = existing;
                }
            }
            return result;
        }

        public override string ToString()
        {
            return $"Группа {_groupName} (Студентов: {_count}, Ср.балл группы: {GetGroupAverage():F2})";
        }
    }
}