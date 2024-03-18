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
    private static async void LINKS_FUNK(Message message) // ФУНКЦИЯ, ВЫДАЮЩАЯ ССЫЛКУ НА ОТЗЫВЫ И ПОДДЕРЖКУ
    {
        InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithUrl( text: "ПОДДЕРЖКА", url: "https://t.me/SERCH016"),
            },
            new[]
            {
                InlineKeyboardButton.WithUrl( text: "ОТЗЫВЫ", url: "https://t.me/wowslegendsdoubloons/21"),
            },
            new[]
            {
                InlineKeyboardButton.WithUrl( text: "Игры и подписки PS", url: "https://t.me/psforjoy"),
            },
        });

        Message sentMessage = await client.SendTextMessageAsync(
        message.Chat.Id,
        text: "⚙️ Дополнительно ⚙️",
        replyMarkup: inlineKeyboard);
    }
}
