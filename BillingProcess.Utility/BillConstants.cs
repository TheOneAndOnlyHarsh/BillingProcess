namespace BillingProcess.Utility
{
    public static class BillConstants
    {
        public const string BillType1 = "Tuition Fee";
        public const string BillType2 = "Library Fee";
        public const string BillType3 = "Exam Fee";

        public const string Student_Role = "Student";

        public const string Admin_Role = "Admin";
    }
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}