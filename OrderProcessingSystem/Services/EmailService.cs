namespace OrderProcessingSystem.Services
{
    public static class EmailService
    {
        public static void Send(string to, string subject, string body)
        {
            Console.WriteLine($"\n[Email sent to: {to}]");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}\n");
        }
    }
}
