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
            case "PlayStation Россия":
                ReplyKeyboardMarkup replyKeyboardMarkup1 = new(new[]
                {
                    new KeyboardButton[] { "1250 ДУБЛОНОВ - 600 ₽", "2750 ДУБЛОНОВ - 1180 ₽" },
                    new KeyboardButton[] { "5625 ДУБЛОНОВ - 2310 ₽", "11500 ДУБЛОНОВ - 4620 ₽" },
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 7120 ₽", "30000 ДУБЛОНОВ - 10180 ₽" },
                    new KeyboardButton[] { "47000 ДУБЛОНОВ - 15260 ₽" },
                    new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 600 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 7200 ₽" },
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
            case "PlayStation Украина":
                ReplyKeyboardMarkup replyKeyboardMarkup2 = new(new[]
                {
                    new KeyboardButton[] { "1250 ДУБЛОНОВ - 480 ₽", "2750 ДУБЛОНОВ - 960 ₽" },
                    new KeyboardButton[] { "5625 ДУБЛОНОВ - 1920 ₽", "11500 ДУБЛОНОВ - 3720 ₽" },
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 6160 ₽", "30000 ДУБЛОНОВ - 9000 ₽" },
                    new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 480 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 6620 ₽" },
                    new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                })
                {
                    ResizeKeyboard = true
                };
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Выберите количество дублонов",
                    replyMarkup: replyKeyboardMarkup2);
                break;
            case "PlayStation Турция":
                ReplyKeyboardMarkup replyKeyboardMarkup3 = new(new[]
                {
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 1500 ₽" },
                    new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 540 ₽" },
                    new KeyboardButton[] { "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 6980 ₽" },
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
                    new KeyboardButton[] { "1250 ДУБЛОНОВ - 300 ₽", "2750 ДУБЛОНОВ - 600 ₽"},
                    new KeyboardButton[] { "5625 ДУБЛОНОВ - 1100 ₽", "11500 ДУБЛОНОВ - 2200 ₽" },
                    new KeyboardButton[] { "20500 ДУБЛОНОВ - 3600 ₽", "30000 ДУБЛОНОВ - 4400 ₽" },
                    new KeyboardButton[] { "47000 ДУБЛОНОВ - 6600 ₽" },
                    new KeyboardButton[] { "🍀 1 ГОРШОК С ЗОЛОТОМ - 420 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 5600 ₽"  },
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
        }
    }
}