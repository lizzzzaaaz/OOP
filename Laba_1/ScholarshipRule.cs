namespace Lab2_OOP
{
    public interface ScholarshipRule
    {
        double CalculateScholarship(double averageGrade);
    }

    public class NoScholarship : ScholarshipRule
    {
        public double CalculateScholarship(double avg)
        {
            return 0.0;
        }
    }

    public class AcademicScholarship : ScholarshipRule
    {
        public double CalculateScholarship(double avg)
        {
            if (avg >= 4.0)
            {
                return 2500.0;
            }
            else
            {
                return 0.0;
            }
        }
    }

    public class EnhancedScholarship : ScholarshipRule
    {
        public double CalculateScholarship(double avg)
        {
            if (avg == 5.0)
            {
                return 6000.0;
            }
            else
            {
                return 3000.0;
            }
        }
    }
}