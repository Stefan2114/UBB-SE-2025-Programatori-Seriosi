using System;

namespace Team3
{
    public class Config
    {
        public static readonly string CONNECTION = "Server=DESKTOP-KQ6N16D;Database=Team3;Integrated Security=True;";

        private static Config? _instance;
        private static readonly object _lock = new object();

        public string Username { get; set; } = string.Empty;

        private Config() { }

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Config();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
