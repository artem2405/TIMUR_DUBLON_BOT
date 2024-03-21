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
    private static async void REBOOT_BOT(Message message) // ПЕРЕЗАПУСК БОТА С ПЕРВОГО ШАГА
    {
        LIST_OF_USERS[message.Chat.Username] = 0;

        string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\" + message.Chat.Username;
        // string path = @"" + message.Chat.Username;

        List<string> lines = new List<string>(System.IO.File.ReadAllLines(path));
        lines.RemoveAt(lines.Count - 1);
        System.IO.File.WriteAllLines(path, lines);

        await client.SendTextMessageAsync(message.Chat.Id, "🔄 БОТ ПЕРЕЗАПУЩЕН 🔄");

        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "PlayStation Россия" },
            new KeyboardButton[] { "PlayStation Украина" },
            new KeyboardButton[] { "PlayStation Турция" },
            new KeyboardButton[] { "Xbox" },
        })
        {
            ResizeKeyboard = true
        };

        await client.SendTextMessageAsync(
            message.Chat.Id,
            "Выберите ваш аккаунт",
            replyMarkup: replyKeyboardMarkup);

        LINKS_FUNK(message);

        LIST_OF_USERS[message.Chat.Username]++;
    }
}
