using MessageAPIViewModel.Authentication;
using Newtonsoft.Json;
using System.Text;

namespace MessageEncrypt_Ex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }
        static async Task MainAsync()
        {
            int choice = 0;
            while (choice != -1)
            {
                Console.WriteLine("----------------CHAT------------");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("-1. Exit");
                Console.WriteLine("Enter your choice: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 2:
                            await CreateAccounAsync();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private static async Task CreateAccounAsync()
        {
            Console.WriteLine("----------------Create account--------------");
            Console.WriteLine("Enter usename: ");
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

            HttpClient client = new HttpClient();

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

    }
}
