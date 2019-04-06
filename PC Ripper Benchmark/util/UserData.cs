namespace PC_Ripper_Benchmark.util {
    /// <summary>
    /// The <see cref="UserData"/> class.
    /// <para></para>Object class to be created when a user signs up
    /// Author: David Hartglass (c), all rights reserved.
    /// </summary>

    public class UserData {

        /// <summary>
        /// Represents a guest <see cref="UserData"/>.
        /// </summary>
        /// <returns></returns>

        public static UserData GetGuestUser() {
            UserData u = new UserData {
                Email = "GuestEmail",
                FirstName = "Guest",
                LastName = "Guest",
                IsAdvanced = UserSkill.Advanced,
                IsLight = true,
                Password = "guest",
                PhoneNumber = "guest number",
                SecurityQuestion = "guest security question",
                SecurityQuestionAnswer = "guest",
                UserType = TypeOfUser.HighPerformance
            };

            return u;
        }

        /// <summary>
        /// Represents a <see langword="null"/> <see cref="UserData"/>.
        /// <para>Used to check if an error was made
        /// connecting to the DB.</para>
        /// </summary>
        /// <returns></returns>

        public static UserData GetNullUser() {
            UserData u = new UserData {
                Email = "NULL",
                FirstName = "NULL",
                LastName = "NULL",
                IsAdvanced = UserSkill.Advanced,
                IsLight = true,
                Password = "NULL",
                PhoneNumber = "NULL",
                SecurityQuestion = "NULL",
                SecurityQuestionAnswer = "NULL",
                UserType = TypeOfUser.HighPerformance
            };

            return u;
        }

        public enum UserSkill {

            Beginner = 1024,

            Advanced = 2048
        }

        public enum TypeOfUser {
            /// <summary>
            /// Represents a casual user.
            /// </summary>
            Casual = 16,

            /// <summary>
            /// Represents a websurfing user.
            /// </summary>
            Websurfer = 32,

            /// <summary>
            /// Represents a user who games or use high performance task.
            /// </summary>
            HighPerformance = 64,

            /// <summary>
            /// Represents a user who does a lot of video editing, also
            /// requires decent performance.
            /// </summary>
            Video = 128
        }

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
        public string Password {  get; set; }

        /// <summary>
        /// The phone number for the account.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The security question for the account.
        /// </summary>
        public string SecurityQuestion { get; set; }

        /// <summary>
        /// The security question answer for the account.
        /// </summary>
        public string SecurityQuestionAnswer { get; set; }

        /// <summary>
        /// Determines where the user is advanced
        /// or beginner.
        /// </summary>
        public UserSkill IsAdvanced { get; set; } 

        /// <summary>
        /// Represents the type of use the user
        /// uses their PC.
        /// </summary>
        public TypeOfUser UserType { get; set; }

        /// <summary>
        /// Determines whether the theme is in
        /// light mode or dark mode.
        /// </summary>
        public bool IsLight { get; set; }
    }
}
