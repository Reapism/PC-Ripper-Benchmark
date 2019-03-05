namespace PC_Ripper_Benchmark.util {
    /// <summary>
    /// The <see cref="UserData"/> class.
    /// <para></para>Object class to be created when a user signs up
    /// Author: David Hartglass (c), all rights reserved.
    /// </summary>

    public class UserData {
        /// <summary>
        /// The firstname for the users account.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user account.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email of the user account.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password for the account.
        /// </summary>
        public string Password { get; }
    }
}
