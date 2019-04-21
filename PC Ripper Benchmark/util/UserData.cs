namespace PC_Ripper_Benchmark.util {
    /// <summary>
    /// The <see cref="UserData"/> class.
    /// <para></para>Object class to be created when a user signs up
    /// Author: David Hartglass (c), all rights reserved.
    /// </summary>

    public class UserData {

        #region Instance member(s), enum(s)

        /// <summary>
        /// The <see cref="UserSkill"/> type.
        /// <para>Beginner or advanced?</para>
        /// </summary>

        public enum UserSkill {

            /// <summary>
            /// Represents a beginner
            /// user.
            /// </summary>
            Beginner = 1024,

            /// <summary>
            /// Represents an advanced
            /// user.
            /// </summary>
            Advanced = 2048
        }

        /// <summary>
        /// The <see cref="TypeOfUser"/> type.
        /// <para>Casual, etc.</para>
        /// </summary>

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

        #endregion

        #region Constructor(s)

        #endregion

        #region Static Function(s)

        /// <summary>
        /// Represents a guest <see cref="UserData"/>.
        /// <para>Check if a user is a guest by checking
        /// the email is "guest".</para>
        /// </summary>
        /// <returns></returns>

        public static UserData GetGuestUser() {
            UserData u = new UserData {
                Email = "guest",
                FirstName = "Guest",
                LastName = "User",
                IsAdvanced = UserSkill.Beginner,
                IsLight = true,
                Password = "guest",
                PhoneNumber = "guest number",
                SecurityQuestion = "guest security question",
                SecurityQuestionAnswer = "guest",
                UserType = TypeOfUser.Casual
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
                IsAdvanced = UserSkill.Beginner,
                IsLight = true,
                Password = "NULL",
                PhoneNumber = "NULL",
                SecurityQuestion = "NULL",
                SecurityQuestionAnswer = "NULL",
                UserType = TypeOfUser.HighPerformance
            };

            return u;
        }

        #endregion

        #region Member function(s)

        /// <summary>
        /// Represents the number that equals
        /// your <see cref="UserSkill"/> + <see cref="TypeOfUser"/>
        /// evaluated into the <see langword="uint"/> they represent.
        /// </summary>
        /// <returns></returns>

        private uint GetTotalNumber() =>
             GetUserSkillInt() + GetTypeOfUserInt();

        /// <summary>
        /// Returns the <see langword="uint"/> representation of
        /// a <see cref="UserSkill"/>.
        /// </summary>
        /// <returns></returns>

        private uint GetUserSkillInt() {
            uint num = 0;

            switch (IsAdvanced) {
                case UserSkill.Beginner: {
                    num += (int)UserSkill.Beginner;
                    break;
                }

                case UserSkill.Advanced: {
                    num += (int)UserSkill.Advanced;
                    break;
                }
            }

            return num;
        }

        /// <summary>
        /// Returns the string representation of
        /// a <see cref="UserSkill"/>.
        /// </summary>
        /// <returns></returns>

        public string GetUserSkillString() {
            string name = string.Empty;

            switch (IsAdvanced) {
                case UserSkill.Beginner: {
                    name = "Beginner";
                    break;
                }

                case UserSkill.Advanced: {
                    name = "Advanced";
                    break;
                }
            }

            return name;
        }

        /// <summary>
        /// Returns the <see langword="uint"/> representation of
        /// a <see cref="TypeOfUser"/>.
        /// </summary>
        /// <returns></returns>

        private uint GetTypeOfUserInt() {
            uint num = 0;

            switch (UserType) {
                case TypeOfUser.Casual: {
                    num += (int)TypeOfUser.Casual;
                    break;
                }

                case TypeOfUser.Websurfer: {
                    num += (int)TypeOfUser.Websurfer;
                    break;
                }

                case TypeOfUser.HighPerformance: {
                    num += (int)TypeOfUser.HighPerformance;
                    break;
                }

                case TypeOfUser.Video: {
                    num += (int)TypeOfUser.Video;
                    break;
                }
            }

            return num;
        }

        /// <summary>
        /// Returns the string representation of
        /// a <see cref="TypeOfUser"/>.
        /// </summary>
        /// <returns></returns>

        public string GetTypeOfUserString() {
            string name = string.Empty;

            switch (UserType) {
                case TypeOfUser.Casual: {
                    name = "Casual";
                    break;
                }

                case TypeOfUser.Websurfer: {
                    name = "Web Surfer";
                    break;
                }

                case TypeOfUser.HighPerformance: {
                    name = "High Performance";
                    break;
                }

                case TypeOfUser.Video: {
                    name = "Video Editing";
                    break;
                }
            }

            return name;
        }


        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        #endregion

        #region Properties

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
        public string Password { get; set; }

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

        #endregion
    }
}
