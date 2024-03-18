using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    internal class Util
    {
 
        public static string DefaultHashedPassword()
        {
            SHA256 sha = SHA256.Create();

            //Convert the input string to a byte array and compute the hash.
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes("Password@123"));

            //create a new string buuilder to colllect the bytes and create a string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static string HashPassword(string password)
        {
          SHA256 sha = SHA256.Create();
          
            //Convert the input string to a byte array and compute the hash.
             byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            //create a new string buuilder to colllect the bytes and create a string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
