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
    private static async void PREVIOUS_STEP(Message message) // ВОЗВРАЩЕНИЕ НА ПРЕДЫДУЩИЙ ШАГ
    {
        string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\ТЕКСТОВЫЕ_ФАЙЛЫ\" + message.Chat.Username;
        // string path = @"/data/Users/" + message.Chat.Username;

        List<string> lines = new List<string>(System.IO.File.ReadAllLines(path));
        char endSymbol = ';';
        int endIndex = lines[lines.Count - 1].LastIndexOf(endSymbol);
        if (endIndex >= 0)
        {
            lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, endIndex);
        }

        using (StreamWriter writer = new StreamWriter(path, false))
        {
            foreach (string line in lines)
            {
                if (lines[lines.Count - 1] == line)
                    writer.Write(line);
                else writer.WriteLine(line);
            }
            writer.Close();
        }

        switch (LIST_OF_USERS[message.Chat.Username])
        {
            case 1: // ПРОИЗОШЕЛ ВЫБОР ВАРИАНТА РЕГИОНА
                string substringToFind1 = "PlayStation Россия";
                string substringToFind2 = "PlayStation Украина";
                string substringToFind3 = "PlayStation Турция";
                string substringToFind4 = "Xbox";
                string substringToFind5 = "iOS/Android";
                string lastLine = "";
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lastLine = line;
                    }
                    reader.Close();
                }

                if (lastLine.Contains(substringToFind1)) { message.Text = substringToFind1; }
                else if (lastLine.Contains(substringToFind2)) { message.Text = substringToFind2; }
                else if (lastLine.Contains(substringToFind3)) { message.Text = substringToFind3; }
                else if (lastLine.Contains(substringToFind4)) { message.Text = substringToFind4; }
                else if (lastLine.Contains(substringToFind5)) { message.Text = substringToFind5; }

                VARIANT_OF_PURCHASE(message);

                int lineCount = 0;
                using (StreamReader reader = new StreamReader(path))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                    reader.Close();
                }

                LIST_OF_USERS[message.Chat.Username]++;

                break;

            case 2: // ПРОИЗОШЕЛ ВЫБОР КОЛИЧЕСТВА ДУБЛОНОВ
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
                    "Напишите цифрой количество наборов данного вида, которые хотели бы приобрести",
                    replyMarkup: replyKeyboardMarkup1);

                LIST_OF_USERS[message.Chat.Username]++;

                break;

            case 3: // ПРОИЗОШЕЛ ВВОД КОЛИЧЕСТВА ЗАКАЗОВ ОДНОГО ТИПА
                lastLine = "";
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lastLine = line;
                    }
                    reader.Close();
                }

                if (LINE_COUNT(path) == 1) { RUSSIA_OR_NOT(message, lastLine); } // ОППРЕДЕЛЕНИЕ ВЫБРАННОГО РЕГИОНА ПОЛЬЗОВАТЕЛЕМ
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
                        "Пришлите ссылку для связи в соцсетях: ВК или Telegram",
                        replyMarkup: replyKeyboardMarkup2);
                }

                if (LINE_COUNT(path) == 1) { LIST_OF_USERS[message.Chat.Username]++; }
                else { LIST_OF_USERS[message.Chat.Username] += 3; }

                break;

            case 4:
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
                    "Пришлите пароль от вашего аккаунта",
                    replyMarkup: replyKeyboardMarkup3);

                LIST_OF_USERS[message.Chat.Username]++;

                break;

            case 5: // ПРОИЗОШЕЛ ВВОД ПАРОЛЯ ОТ АККАУНТА 
                ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
                {
                    new KeyboardButton[] { "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️" },
                    new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                })
                {
                    ResizeKeyboard = true
                };

                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Пришлите ТЕКСТОВУЮ ссылку для связи в соцсетях: ВК или Telegram",
                    replyMarkup: replyKeyboardMarkup4);

                LIST_OF_USERS[message.Chat.Username]++;

                break;
        }
        return;
    }
}
