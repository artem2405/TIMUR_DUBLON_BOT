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

        if (message.Chat.Id == 507922621 && message.Text == "khit_art_2405") { ADMIN_MES(message); }
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