namespace WebApplication2.Conversor
{
    public static class Conversor
    {
        public static int AgeCalculator(DateTime birthdate)
        {
            int age = DateTime.Now.Year - birthdate.Year;
            return age;
        }   
    }
}
