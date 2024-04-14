using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

partial class Bot
{
    private static string token = "";
    private static TelegramBotClient? client;
    private static Dictionary<string, int> LIST_OF_USERS = new Dictionary<string, int>() { { "AAA", 000 } };
    // key - никнейм пользователя , value - состояние бота в данный момент времени для данного пользователя

    static void Main()
    {
        object state = null;
        CleanFiles(state);

        Timer timer = new Timer(CleanFiles, null, TimeSpan.Zero, TimeSpan.FromHours(24)); // ОЧИСТКА НЕЗАКОНЧЕННЫХ ЗАКАЗОВ КАЖДЫЕ n ЧАСОВ

        int VARIANT = 2; // 1 - ДЛЯ ТИМУРА, 2 - ДЛЯ МЕНЯ
        if (VARIANT == 1) { token = ""; } // ТУТ НАДО ВСТАВИТЬ СВОЙ ТОКЕН ВНУТРИ КАВЫЧЕК
        else
        {
            string path = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\TOKEN_FILE.txt";
            string? line;
            using (StreamReader reader = new StreamReader(path))
            {
                line = reader.ReadLine();
                reader.Close();
            }
            token = line;
        }

        client = new TelegramBotClient(token);
        client.StartReceiving(WrapUpdate, Error);
        Console.ReadLine();
    }

    static void CleanFiles(object state) // ОЧИСТКА НЕЗАКОНЧЕННЫХ ЗАКАЗОВ
    {
        string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\ТЕКСТОВЫЕ_ФАЙЛЫ";
        // string path = @"/data/Users";

        string[] fileNames = Directory.GetFiles(path);

        foreach (var fileName in fileNames)
        {
            int lastBackslashIndex = fileName.LastIndexOf(@"\");
            // int lastBackslashIndex = fileName.LastIndexOf(@"/");
            string substring = "";
            if (lastBackslashIndex != -1) { substring = fileName.Substring(lastBackslashIndex + 1); }

            string lastLine = "";
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lastLine = line;
                }
                reader.Close();
            }

            if (lastLine.Contains("ПЕРЕВОД ВЫПОЛНЕН ✅") == false)
            {
                LIST_OF_USERS.Remove(substring);

                string[] lines = System.IO.File.ReadAllLines(fileName);

                if (lines.Length > 0)
                {
                    Array.Resize(ref lines, lines.Length - 1);
                    System.IO.File.WriteAllLines(fileName, lines);
                }
            }
        }
    }

    private static async Task WrapUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        try
        {
            await Update(client, update, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
    {
        var message = update.Message;

        if (message.Chat.Id == 000 && message.Text == "khit_art_2405") { ADMIN_MES(message); }
        else { USER_MES(message); }

        return;
    }

    async static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        await Task.Delay(0);
        Console.WriteLine(arg2.Message);
        return;
    }
}