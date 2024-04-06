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
        string substringToFind1 = "PlayStation Россия";
        string substringToFind2 = "iOS/Android";

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
                "\n   ❷ Если НЕТ, то пришлите логин от вашего аккаунта PlayStation Россия",
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
                "\nЕсли ваш аккаунт \"iOS/Android\", то:" +
                "\n   ❶ В настройках игры привяжите пустой аккаунт Xbox" +
                "\n   ❷ Пришлите логин от вашего аккаунта Xbox",
                replyMarkup: replyKeyboardMarkup2);
        }
        else
        {
            ReplyKeyboardMarkup replyKeyboardMarkup3 = new(new[]
            {
                new KeyboardButton[] { "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️" },
                new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
            })
            {
                ResizeKeyboard = true
            };

            await client.SendTextMessageAsync(
                message.Chat.Id,
                "\n❶ Пришлите логин от вашего аккаунта PS/Xbox",
                replyMarkup: replyKeyboardMarkup3);
        }
    }
}
