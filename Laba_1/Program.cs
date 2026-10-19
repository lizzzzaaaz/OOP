

namespace Lab2_OOP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ScholarshipRule noMoney = new NoScholarship();
            ScholarshipRule baseScholarship = new AcademicScholarship();
            ScholarshipRule topScholarship = new EnhancedScholarship();

            List<Achievement> achievements1 = new List<Achievement>();
            achievements1.Add(new Achievement("Победитель олимпиады по программированию"));

            List<Achievement> achievements2 = new List<Achievement>();
            achievements2.Add(new Achievement("Активное участие в студенческом совете"));
            achievements2.Add(new Achievement("Спортивные достижения"));

            Student st1 = new BudgetStudent("0193479", "Иванов Иван Иванович", 2, new int[] { 5, 5, 5, 4 }, baseScholarship, achievements1);
            Student st2 = new ContractStudent("0193480", "Петрова Анна Сергеевна", 1, new int[] { 3, 4, 4 }, noMoney);
            Student st3 = new ExchangeStudent("0193481", "Сидоров Алексей Владимирович", 3, new int[] { 5, 5, 5, 5 }, topScholarship);

        
            TargetStudent st4 = new TargetStudent("0193482", "Смирнов Дмитрий Павлович", 2, new int[] { 4, 4, 5, 4 }, baseScholarship, "ООО «ПромГазСервис»", 3, achievements2);

       
            Group group = new Group("ИС-21", 5);
            group.AddStudent(st1);
            group.AddStudent(st2);
            group.AddStudent(st3);
            group.AddStudent(st4);

  
            Console.WriteLine(group);
            foreach (var st in group.GetStudentsSafe())
            {
                Console.WriteLine($"   - {st}");
            }

           
            st2.SetScholarshipRule(baseScholarship);
            st2.AddMark(5);
            st1.Promote();

        
            st4.AddAchievement(new Achievement("Патент на разработку"));

    
            group.RemoveStudent("0193480");

            Console.WriteLine("\nПосле изменений:");
            Console.WriteLine(group);
            foreach (var st in group.GetStudentsSafe())
            {
                Console.WriteLine($"   - {st}");
            }

            Console.ReadKey();
        }
    }
}