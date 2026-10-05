namespace Lab1_OOP
{
    public class Group
    {
        private string _groupName;
        private Student?[] _students;
        private int _count;

        public Group(string groupName, int capacity = 30)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentException("Название группы не может быть пустым!");

            if (capacity <= 0)
                throw new ArgumentException("Вместимость группы должна быть больше 0!");

            _groupName = groupName;
            _students = new Student?[capacity];
            _count = 0;
        }

        public string GetGroupName()
        {
            return _groupName;
        }

        public int GetCount()
        {
            return _count;
        }

        public bool AddStudent(Student? student)
        {
            if (student == null) return false;

            for (int i = 0; i < _count; i++)
            {
                if (_students[i] != null && _students[i]!.GetId() == student.GetId())
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
            if (string.IsNullOrWhiteSpace(id)) return false;

            int index = -1;
            for (int i = 0; i < _count; i++)
            {
                if (_students[i] != null && _students[i]!.GetId() == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return false;

            for (int i = index; i < _count - 1; i++)
            {
                _students[i] = _students[i + 1];
            }

            _students[_count - 1] = null;
            _count--;

            return true;
        }

        public Student? FindStudent(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            for (int i = 0; i < _count; i++)
            {
                if (_students[i] != null && _students[i]!.GetId() == id)
                {
                    return _students[i];
                }
            }

            return null;
        }

        public double GetGroupAverage()
        {
            if (_count == 0) return 0.0;

            double sum = 0;
            for (int i = 0; i < _count; i++)
            {
                sum += _students[i]!.GetAverageGrade();
            }

            return sum / _count;
        }

        public Student[] GetStudentsSafe()
        {
            Student[] result = new Student[_count];
            for (int i = 0; i < _count; i++)
            {
                result[i] = _students[i]!;
            }
            return result;
        }

        public Student[] GetHonorsStudents()
        {
            int honorsCount = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_students[i] != null && _students[i]!.GetAverageGrade() == 5.0)
                {
                    honorsCount++;
                }
            }

            Student[] result = new Student[honorsCount];

            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_students[i] != null && _students[i]!.GetAverageGrade() == 5.0)
                {
                    result[index] = _students[i]!;
                    index++;
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