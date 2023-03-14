namespace Discount.Grpc.Configurations
{
    public class PostgresDbConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        //public string SearchPath { get; set; }
        //public int MaxPoolSize { get; set; }
        //public int? MinPoolSize { get; set; }
        //public int? KeepAliveInSeconds { get; set; }
        //public RetryPolicyConfig RetryPolicy { get; set; }
    }
}
