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
    private static async void REC_TO_FILE(Message message) // ЗАПИСЬ ИНФОРМАЦИИ В ФАЙЛ СО СПИСКОМ ЗАКАЗОВ ПОЛЬЗОВАТЕЛЯ
    {
        string path1 = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT";
        // string path1 = @"/data/Users";

        string path = path1 + @"\" + message.Chat.Username;
        //string path = path1 + message.Chat.Username;

        switch (LIST_OF_USERS[message.Chat.Username])
        {
            case 1:
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteAsync(message.Text);
                    writer.Close();
                }
                break;
            case 6:
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteLineAsync(";   " + message.Text + "   " + DateTime.Now.ToString());
                    writer.Close();
                }
                break;

            default:
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteAsync(";   " + message.Text);
                    writer.Close();
                }
                break;
        }
    }
}
