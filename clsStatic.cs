using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EastSidazFantasy
{
    /// <summary>
    /// Provides an internal structure to sort the query parameter
    /// </summary>
    public class QueryParameter
    {
        private string name = null;
        private string value = null;

        public QueryParameter(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return name; }
        }

        public string Value
        {
            get { return value; }
        }
    }

    public static class clsStatic
    {
        public const string g_sConsumerKey = "dj0yJmk9bk9WUkZhUDhOd2VHJmQ9WVdrOVFYRTBkVTFyTjJzbWNHbzlNVGs1T1RnMk5UYzJNZy0tJnM9Y29uc3VtZXJzZWNyZXQmeD05Nw--";
        public const string g_sConsumerSecret = "f900ab95a43dfd51a58a389eba776e445484f19c";        
        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            Random random = new Random();
            return random.Next(123400, 9999999).ToString();
        }

        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static void ShowError(string sMessage)
        {
            MessageBox.Show(sMessage, "EastSidaz Fantasy Sports", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
