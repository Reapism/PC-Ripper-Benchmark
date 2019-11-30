using Microsoft.VisualBasic;

namespace PC_Ripper_Benchmark.Functions
{

    /// <summary>
    /// The <see cref="RipperDialog"/> class.
    /// <para> </para>
    /// Represents all the functions for 
    /// creating dialog boxes.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperDialog
    {

        /// <summary>
        /// Displays and returns the string in the <see cref="Interaction.InputBox"/>.
        /// </summary>
        /// <param name="prompt">The prompt of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="title">The title of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="defaultResponse">The default response of the <see cref="Interaction.InputBox"/>.</param>
        /// <returns></returns>

        public static string InputBox(string prompt, string title, string defaultResponse)
        {
            return Interaction.InputBox(prompt, title, defaultResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt">The prompt of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="title">The title of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="defaultResponse">The default response of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="output">The output of the ulong.</param>
        /// <returns></returns>

        public static bool InputBox(string prompt, string title, string defaultResponse, out ulong output)
        {
            try
            {
                bool b = ulong.TryParse(Interaction.InputBox(prompt, title, defaultResponse), out output);
                return b;
            }
            catch
            {
                output = 0;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt">The prompt of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="title">The title of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="defaultResponse">The default response of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="output">The output of the byte.</param>
        /// <returns></returns>

        public static bool InputBox(string prompt, string title, string defaultResponse, out byte output)
        {
            try
            {
                bool b = byte.TryParse(Interaction.InputBox(prompt, title, defaultResponse), out output);
                return b;
            }
            catch
            {
                output = 0;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt">The prompt of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="title">The title of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="defaultResponse">The default response of the <see cref="Interaction.InputBox"/>.</param>
        /// <param name="output">The output of the int.</param>
        /// <returns></returns>

        public static bool InputBox(string prompt, string title, string defaultResponse, out int output)
        {
            try
            {
                bool b = int.TryParse(Interaction.InputBox(prompt, title, defaultResponse), out output);
                return b;
            }
            catch
            {
                output = 0;
                return false;
            }
        }
    }
}
