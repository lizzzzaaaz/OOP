namespace Lab2_OOP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ScholarshipRule noMoney = new NoScholarship();
            ScholarshipRule baseScholarship = new AcademicScholarship();
            ScholarshipRule topScholarship = new EnhancedScholarship();

            Student st1 = new BudgetStudent("0193479", "Иванов Иван Иванович", 2, new int[] { 5, 5, 5, 4 }, baseScholarship);
            Student st2 = new ContractStudent("0193480", "Петрова Анна Сергеевна", 1, new int[] { 3, 4, 4 }, noMoney);
            Student st3 = new ExchangeStudent("0193481", "Сидоров Алексей Владимирович", 3, new int[] { 5, 5, 5, 5 }, topScholarship);

            Group group = new Group("ИС-21", 5);
            group.AddStudent(st1);
            group.AddStudent(st2);
            group.AddStudent(st3);

            System.Console.WriteLine(group);
            foreach (var st in group.GetStudentsSafe())
            {
                System.Console.WriteLine($"   - {st}");
            }

            st2.SetScholarshipRule(baseScholarship);
            st2.AddMark(5);
            st1.Promote();
            group.RemoveStudent("0193480");

            System.Console.WriteLine("\nПосле изменений:");
            System.Console.WriteLine(group);
            foreach (var st in group.GetStudentsSafe())
            {
                System.Console.WriteLine($"   - {st}");
            }

            System.Console.ReadKey();
        }
    }
}