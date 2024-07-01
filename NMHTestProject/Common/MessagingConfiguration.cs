namespace NMHTestProject.Common
{
    public class MessagingConfiguration
    {
        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string VirtualHost { get; set; } = default!;

        public string HostName { get; set; } = default!;

        public string ClientProviderName { get; set; } = default!;

        public string QueueName { get; set; } = default!;

        public int Port { get; set; }
    }
}
