using OData.Business.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OData.Business.Utilities
{
    public static class Extension
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static T Deserialize<T>(this string SerializedJSONString)
        {
            if (!IsValidJson(SerializedJSONString))
            {
                throw new ArgumentException("Invalid Json");
            }

#pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<T>(SerializedJSONString);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static bool IsValidJson(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || 
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) 
            {
                try
                {
                    JToken? obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void Display(this PeopleModel model)
        {
            StringBuilder output = new StringBuilder();
            output = output.AppendLine("\r\n\r\n" +
               $"First Name: {model.FirstName}" +
               $"Middle Name: {model.MiddleName} " +
               $"Last Name: {model.LastName} " +
               $"User Name: {model.UserName}" +
               $"\r\n\r\n" +
               $"Gender:{model.Gender} " +
               $"Age:{model.Age}" +
               $"\r\n\r\n" +
               $"Favorite Feature:{model.FavoriteFeature}");

            output.AppendLine($"Emails :");
            foreach (string? item in model.Emails)
            {
                output.AppendLine(item);
            }

            output.AppendLine($"Features :");
            foreach (string? item in model.Features)
            {
                output.AppendLine(item);
            }

            output.AppendLine($"Address Info:");

            foreach (AddressModel? item in model.AddressInfo)
            {
                output.AppendLine($"Address : {item.Address}");
                output.AppendLine($"City :");
                output.AppendLine($" Name: {item.City.Name}" +
                        $" CountryRegion: {item.City.CountryRegion} " +
                        $" Region: {item.City.Region} ");
            }
            Console.WriteLine($"{output.ToString()}");
        }

        public static void Display(this PeopleRowModel model)
        {
            Console.WriteLine("\r\n\r\n{0,10} {1,20} {2,30}\n", "User Name", "First Name", "Last Name");
            foreach (PeopleModel? item in model.Data)
            {
                Console.WriteLine("\r\n\r\n{0,10} {1,20} {2,30:N1}\n", item.UserName, item.FirstName, item.LastName);
            }
        }

      
        public static string ReplaceWhitespace(this string input, string replacement = "")
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}
