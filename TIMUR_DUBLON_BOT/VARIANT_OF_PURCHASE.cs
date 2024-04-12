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
    private static async void VARIANT_OF_PURCHASE(Message message) // ВАРИАНТЫ ВОЗМОЖНОГО ЗАКАЗА ДЛЯ КАЖДОГО ИЗ РЕГИОНОВ
    {
        switch (message.Text)
        {
            case "PlayStation Россия / PlayStation Украина":
                ReplyKeyboardMarkup replyKeyboardMarkup1 = new(new[]
                {
                    new KeyboardButton[] { "1250 ДУБЛОНОВ - 490 ₽", "2750 ДУБЛОНОВ - 990 ₽" },
                    new KeyboardButton[] { "5625 ДУБЛОНОВ - 1810 ₽", "11500 ДУБЛОНОВ - 3580 ₽" },
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 6330 ₽", "30000 ДУБЛОНОВ - 8300 ₽" },
                    new KeyboardButton[] { "47000 ДУБЛОНОВ - 11550 ₽" },
                    // new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 600 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 7200 ₽" },
                    new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                })
                {
                    ResizeKeyboard = true
                };
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите количество дублонов",
                    replyMarkup: replyKeyboardMarkup1);
                break;

            case "PlayStation Турция":
                ReplyKeyboardMarkup replyKeyboardMarkup3 = new(new[]
                {
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 1500 ₽" },
                    // new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 540 ₽" },
                    // new KeyboardButton[] { "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 6980 ₽" },
                    new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                })
                {
                    ResizeKeyboard = true
                };
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите количество дублонов",
                    replyMarkup: replyKeyboardMarkup3);
                break;

            case "Xbox":
                ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
                {
                    new KeyboardButton[] { "1250 ДУБЛОНОВ - 490 ₽", "2750 ДУБЛОНОВ - 990 ₽" },
                    new KeyboardButton[] { "5625 ДУБЛОНОВ - 1810 ₽", "11500 ДУБЛОНОВ - 3580 ₽" },
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 6330 ₽", "30000 ДУБЛОНОВ - 8300 ₽" },
                    new KeyboardButton[] { "47000 ДУБЛОНОВ - 11550 ₽" },
                    // new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 420 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 5600 ₽"  },
                    new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                })
                {
                    ResizeKeyboard = true
                };
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите количество дублонов",
                    replyMarkup: replyKeyboardMarkup4);
                break;

            case "iOS/Android":
                ReplyKeyboardMarkup replyKeyboardMarkup5 = new(new[]
{
                    new KeyboardButton[] { "1250 ДУБЛОНОВ - 490 ₽", "2750 ДУБЛОНОВ - 990 ₽" },
                    new KeyboardButton[] { "5625 ДУБЛОНОВ - 1810 ₽", "11500 ДУБЛОНОВ - 3580 ₽" },
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 6330 ₽", "30000 ДУБЛОНОВ - 8300 ₽" },
                    new KeyboardButton[] { "47000 ДУБЛОНОВ - 11550 ₽" },
                    // new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 420 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 5600 ₽"  },
                    new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                })
                {
                    ResizeKeyboard = true
                };
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите количество дублонов",
                    replyMarkup: replyKeyboardMarkup5);
                break;
        }
    }
}