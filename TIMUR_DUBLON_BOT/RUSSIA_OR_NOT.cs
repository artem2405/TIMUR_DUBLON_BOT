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
    private static async void RUSSIA_OR_NOT(Message message, string lastLine) // ПРОВЕРКА АККАУНТА: PS RU ИЛИ КАКОЙ-ТО ДРУГОЙ
    {
        string substringToFind = "PlayStation Россия";

        if (lastLine != null && lastLine.Contains(substringToFind))
        {
            ReplyKeyboardMarkup replyKeyboardMarkup2 = new(new[]
            {
                new KeyboardButton[] { "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️" },
                new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
            })
            {
                ResizeKeyboard = true
            };

            await client.SendTextMessageAsync(
                message.Chat.Id,
                //"❗ ВНИМАНИЕ! Все данные присылайте одним сообщением через запятую ❗" +
                //"\nПРИМЕР: Логин_От_Аккаунта, Пароль_От_Аккаунта" +
                //"\n" +
                "\nЕсли ваш аккаунт \"PlayStation Россия\", то:" +
                "\nВы ранее заходили в свой аккаунт кораблей через мобильное приложение Legends?" +
                "\n   ❶ Если ДА, то пришлите логин от аккаунта Facebook/Google, используемого для входа" +
                "\n   ❷ Если НЕТ, то пришлите логин от вашего аккаунта PlayStation Россия",
                replyMarkup: replyKeyboardMarkup2);
        }
        else
        {
            ReplyKeyboardMarkup replyKeyboardMarkup2 = new(new[]
            {
                new KeyboardButton[] { "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️" },
                new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
            })
            {
                ResizeKeyboard = true
            };

            await client.SendTextMessageAsync(
                message.Chat.Id,
                //"❗ ВНИМАНИЕ! Все данные присылайте одним сообщением через запятую ❗" +
                //"\nПРИМЕР: Логин_От_Аккаунта, Пароль_От_Аккаунта" +
                //"\n" +
                "\nЕсли ваш аккаунт НЕ \"PlayStation Россия\", то:" +
                "\n   ❶ Пришлите логин от вашего аккаунта PS/Xbox",
                replyMarkup: replyKeyboardMarkup2);
        }
    }
}
