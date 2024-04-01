using MessageAPIViewModel.Authentication;
using Newtonsoft.Json;
using System.Text;

namespace MessageEncrypt_Ex
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await MainAsync();
        }

        static async Task MainAsync()
        {
            int choice = 0;
            while (choice != -1)
            {
                DisplayMenu();
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            await LoginAsync();
                            break;
                        case 2:
                            await CreateAccountAsync();
                            break;
                        case -1:
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("----------------CHAT------------");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("-1. Exit");
            Console.WriteLine("Enter your choice: ");
        }

        private static async Task CreateAccountAsync()
        {
            Console.WriteLine("----------------Create account--------------");
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Enter re-password: ");
            string repassword = Console.ReadLine();
            SignUpAPIViewModel data = new SignUpAPIViewModel()
            {
                Password = password,
                RePassword = repassword,
                Username = username
            };

            using HttpClient client = new HttpClient();

            string json = JsonConvert.SerializeObject(data);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7089/api/authentication/signup", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Error");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

        private static async Task LoginAsync()
        {
            Console.WriteLine("---------------- Login --------------");

            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            LoginAPIViewModel loginData = new LoginAPIViewModel()
            {
                Username = username,
                Password = password
            };

            using HttpClient client = new HttpClient();

            string json = JsonConvert.SerializeObject(loginData);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7089/api/authentication/login", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();// http content ở dạng bytes, dùng lệnh này để chuyển nó sang string 
                                                                       // rồi in ra trả cho user
                Console.WriteLine("Login successful. Token: " + token);
            }
            else
            {
                Console.WriteLine("Login failed. Invalid username or password.");
            }
        }
    }
}
