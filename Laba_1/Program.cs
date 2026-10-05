//using System;

namespace Lab1_OOP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Демонтсрация работы классов Student и Group\n");

            Student st1 = new Student("0193479", "Иванов Иван Иванович", 2, 4.5, new int[] { 4, 5, 5, 4 });
            Student st2 = new Student("0193480", "Петрова Анна Сергеевна"); 
            Student st3 = new Student("0193481", "Сидоров Алексей Владимирович", 2, 3.8, new int[] { 3, 4, 4, 4 });

            Group group = new Group("ИС1", 2); 
            group.AddStudent(st1);
            group.AddStudent(st2);
            group.AddStudent(st3); 

            Console.WriteLine("1. Группа после создания:");
            Console.WriteLine(group.ToString());
            foreach (var st in group.GetStudentsSafe())
            {
                Console.WriteLine($"   - {st}");
            }

            Console.WriteLine("\n2. Добавим оценки (перезагрузка AddMark):");
            st1.AddMark(5);                      
            st2.AddMark(new int[] { 5, 5, 4 });  

            Console.WriteLine($"   Обновленный st1: {st1}");
            Console.WriteLine($"   Обновленный st2: {st2}");
            Console.WriteLine($"   Новый средний балл группы: {group.GetGroupAverage():F2}");

           
            Console.WriteLine("\n3. Демонстрация защитного копирования:");

            int[] unsafeGrades = st1.GetGradesUnsafe();
            Console.WriteLine($"   Оценки st1 до порчи снаружи: {string.Join(", ", unsafeGrades)}");
            unsafeGrades[0] = 2; 
            Console.WriteLine($"   Оценки st1 после изменения [0]=2 снаружи: {string.Join(", ", st1.GetGradesUnsafe())}");

            int[] safeGrades = st1.GetGradesSafe();
            safeGrades[0] = 5;
            Console.WriteLine($"   Попытка изменить safeGrades[0]=5. Данные внутри объекта останутся прежними: {string.Join(", ", st1.GetGradesSafe())}");

            Console.WriteLine("\n4. Отчисление студента:");
            Console.WriteLine($"   Отчисляем студента c ID 0193479");
            bool removed = group.RemoveStudent("0193479");
            Console.WriteLine($"   Успешно отчислен: {removed}");
            Console.WriteLine($"   Итоговое состояние группы: {group}");

            Console.WriteLine("\nПроверка обработки ошибок:");
            try
            {
                Console.WriteLine("   Попытка создать студента с некорректным 9 курсом...");
                Student badStudent = new Student("21-ИС-99", "Ошибочный Студент", 9, 7.0, null);
                Console.WriteLine("   Студент успешно создан!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"   Перехвачена ошибка: {ex.Message}");
            }

            Console.WriteLine("\nПрограмма успешно завершила работу.");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}