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
        //string substringToFind1 = "PlayStation Россия";
        //string substringToFind2 = "PlayStation Украина";
        //string substringToFind3 = "PlayStation Турция";
        //string substringToFind4 = "Xbox";
        //string substringToFind5 = "iOS/Android";

        string substringToFind1 = "PlayStation Россия / PlayStation Украина";
        string substringToFind2 = "PlayStation Турция";
        string substringToFind3 = "Xbox";
        string substringToFind4 = "iOS/Android";


        if (lastLine != null && lastLine.Contains(substringToFind1))
        {
            ReplyKeyboardMarkup replyKeyboardMarkup1 = new(new[]
            {
                new KeyboardButton[] { "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️" },
                new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
            })
            {
                ResizeKeyboard = true
            };

            await client.SendTextMessageAsync(
                message.Chat.Id,
                "\nВы ранее заходили в свой аккаунт кораблей через мобильное приложение Legends?" +
                "\n   ❶ Если ДА, то пришлите логин от аккаунта Facebook/Google, используемого для входа, " +
                "\n      с указанием способа входа - Facebook или Google" +
                "\n   ❷ Если НЕТ, то пришлите логин от вашего аккаунта PlayStation",
                replyMarkup: replyKeyboardMarkup1);
        }

        else if (lastLine != null && lastLine.Contains(substringToFind2))
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
                "\n❶ Пришлите логин от вашего аккаунта PS Турция",
                replyMarkup: replyKeyboardMarkup2);
        }
        else if (lastLine != null && lastLine.Contains(substringToFind3))
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
                "\nВы ранее заходили в свой аккаунт кораблей через мобильное приложение Legends?" +
                "\n   ❶ Если ДА, то пришлите логин от аккаунта Facebook/Google, используемого для входа, " +
                "\n      с указанием способа входа - Facebook или Google" +
                "\n   ❷ Если НЕТ, то пришлите логин от вашего аккаунта Xbox",
                replyMarkup: replyKeyboardMarkup2);
        }
        else if (lastLine != null && lastLine.Contains(substringToFind4))
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
                "\nПришлите логин от аккаунта Facebook/Google, используемого для входа, " +
                "\nс указанием способа входа - Facebook или Google",
                replyMarkup: replyKeyboardMarkup2);
        }
    }
}
