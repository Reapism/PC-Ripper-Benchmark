using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Ripper_Benchmark.util
{
    /// <summary>
    /// The <see cref="User"/> class.
    /// <para></para>Object class to be created when a user signs up
    /// Author: David Hartglass (c), all rights reserved.
    /// </summary>
    /// 

   public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
    }
}
