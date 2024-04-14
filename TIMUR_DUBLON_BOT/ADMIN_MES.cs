using OpenQA.Selenium.Remote;
using System.IO;
using System.IO.Pipes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

partial class Bot
{
    private static async void ADMIN_MES(Message message)
    {
        if (message.Text == "khit_art_2405")
        {
            string path1 = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\РАССЫЛКА.txt";
            string path2 = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\СПИСОК_ID_ПОЛЬЗОВАТЕЛЕЙ.txt";
            string path3 = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\РАССЫЛКА.jpg";

            // string path1 = @"/data/Pictures/РАССЫЛКА.txt"; // ПУТЬ К ФАЙЛУ С ТЕКСТОМ РАССЫЛКИ
            // string path2 = @"/data/Pictures/ID"; // ПУТЬ К ФАЙЛУ СО СПИСКОМ ПОЛЬЗОВАТЕЛЕЙ ДЛЯ РАССЫЛКИ
            // string path3 = @"/data/Users/РАССЫЛКА.png";

            string text = "";
            using (StreamReader reader = new StreamReader(path1)) { text = await reader.ReadToEndAsync(); reader.Close(); }

            using (StreamReader reader = new StreamReader(path2))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (FileStream fileStream = new FileStream(path3, FileMode.Open))
                        {
                            string uploadUrl = $"https://api.telegram.org/bot{token}/sendPhoto?chat_id={line}&caption={text}";
                            MultipartFormDataContent formData = new MultipartFormDataContent();
                            formData.Add(new StreamContent(fileStream), "photo", Path.GetFileName(path3));
                            HttpResponseMessage photoResponse = await client.PostAsync(uploadUrl, formData);

                            if (!photoResponse.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"Ошибка при отправке сообщения с изображением пользователю с ID {line}");
                            }
                            else
                            {
                                Console.WriteLine($"Сообщение с изображением успешно отправлено пользователю с ID {line}");
                            }

                            fileStream.Close();
                        }
                    }
                }
                reader.Close();
            }
        }
    }
}
