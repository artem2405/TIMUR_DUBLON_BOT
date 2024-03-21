using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

partial class Bot
{
    private static async void ADMIN_MES(Message message)
    {
        if (message.Text == "khit_art_2405") // ПАРОЛЬ ДЛЯ АКТИВАЦИИ РАССЫЛКИ
        {
            string path1 = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\РАССЫЛКА.txt";
            string path2 = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\СПИСОК_ID_ПОЛЬЗОВАТЕЛЕЙ.txt";

            //string path1 = @""; // ПУТЬ К ФАЙЛУ С ТЕКСТОМ РАССЫЛКИ
            //string path2 = @""; // ПУТЬ К ФАЙЛУ СО СПИСКОМ ПОЛЬЗОВАТЕЛЕЙ ДЛЯ РАССЫЛКИ

            string text = "";
            using (StreamReader reader = new StreamReader(path1)) { text = await reader.ReadToEndAsync(); reader.Close(); }

            using (StreamReader reader = new StreamReader(path2)) 
            {
                string line;
                string path = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\РАССЫЛКА.jpg";
                while ((line = reader.ReadLine()) != null)
                {
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        InputFileStream input = new InputFileStream(stream);
                        await client.SendPhotoAsync(
                            chatId: line,
                            photo: input,
                            caption: text,
                            parseMode: ParseMode.Html);
                    }
                }
                reader.Close();
            }
        }
    }
}
