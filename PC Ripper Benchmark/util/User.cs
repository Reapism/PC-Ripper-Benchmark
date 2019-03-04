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

   public partial class User
    {
        string firstName { get; set; }
        string lastName { get; set; }
        string email { get; set; }
        string password { get; set; }
    }
}
