using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3
{
    public class Config
    {
        public static readonly string CONNECTION = "Data Source=Bogdan;Initial Catalog=Team3;Integrated Security=True;Encrypt=False;";

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