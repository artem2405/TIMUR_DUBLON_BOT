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
    private static bool STUPID_CHECK(Message message) // ЗАЩИТА НА СЛУЧАЙ ВВОДА НЕ ПРАВИЛЬНОЙ ИНФОРМАЦИИ ПРИ ВЫБОРЕ РЕГИОНА И ВИДА ПОКУПКИ
    {
        string[] basic = new string[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄", "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️", "1250 ДУБЛОНОВ - 600 ₽",
        "2750 ДУБЛОНОВ - 1180 ₽", "5625 ДУБЛОНОВ - 2310 ₽", "11500 ДУБЛОНОВ - 4620 ₽", "20500 ДУБЛОНОВ - 7120 ₽",
        "30000 ДУБЛОНОВ - 10180 ₽", "47000 ДУБЛОНОВ - 15260 ₽", "🍀 1 ГОРШОК С ЗОЛОТОМ - 600 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 7200 ₽",
        "1250 ДУБЛОНОВ - 480 ₽", "2750 ДУБЛОНОВ - 960 ₽", "5625 ДУБЛОНОВ - 1920 ₽", "11500 ДУБЛОНОВ - 3720 ₽",
        "20500 ДУБЛОНОВ - 6160 ₽", "30000 ДУБЛОНОВ - 9000 ₽" ,"🍀 1 ГОРШОК С ЗОЛОТОМ - 480 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 6620 ₽",
        "20500 ДУБЛОНОВ - 1500 ₽", "🍀 1 ГОРШОК С ЗОЛОТОМ - 540 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 6980 ₽",
        "1250 ДУБЛОНОВ - 420 ₽",
        "2750 ДУБЛОНОВ - 880 ₽", "5625 ДУБЛОНОВ - 1760 ₽", "11500 ДУБЛОНОВ - 3500 ₽", "20500 ДУБЛОНОВ - 6040 ₽",
        "30000 ДУБЛОНОВ - 8440 ₽", "47000 ДУБЛОНОВ - 12640 ₽", "🍀 1 ГОРШОК С ЗОЛОТОМ - 420 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 5600 ₽",
        "PlayStation Россия", "PlayStation Украина", "PlayStation Турция", "Xbox", "iOS/Android"};
        for (int i = 0; i < basic.Length; i++)
        {
            if (message.Text == basic[i])
            {
                return true;
            }
        }
        return false;
    }
}
