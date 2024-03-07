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

class Bot
{
    private static string token = "";
    private static TelegramBotClient? client;
    private static Dictionary<string, int> LIST_OF_USERS = new Dictionary<string, int>() { { "AAA", 000 } };
    // key - никнейм пользователя , value - состояние бота в данный момент времени для данного пользователя

    static void Main()
    {
        int VARIANT = 1; // 1 - ДЛЯ ТИМУРА, 2 - ДЛЯ МЕНЯ
        if (VARIANT == 1) { token = ""; } // ТУТ НАДО ВСТАВИТЬ СВОЙ ТОКЕН ВНУТРИ КАВЫЧЕК
        else
        {
            string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\TOKEN_FILE.txt";
            string? line;
            using (StreamReader reader = new StreamReader(path))
            {
                line = reader.ReadLine();
                reader.Close();
            }
            token = line;
        }

        client = new TelegramBotClient(token);
        client.StartReceiving(WrapUpdate, Error);
        Console.ReadLine();
    }

    private static async Task WrapUpdate(ITelegramBotClient client, Update update, CancellationToken token)
    {
        try
        {
            await Update(client, update, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
    {
        var message = update.Message;

        int check = 0; // ПРОВЕРКА НАЛИЧИЯ ИМЕНИ ПОЛЬЗОВАТЕЛЯ В ТЕЛЕГРАМ + ПРОВЕРКА ПЕРВОГО ЗАКАЗА
        if (message.Chat.Username == null) { message.Chat.Username = message.Chat.Id.ToString(); }

        foreach (var user in LIST_OF_USERS)
        {
            if (message.Chat.Username == user.Key) 
            { 
                check = 1;
                break; 
            }
        }
        if (check == 0) { LIST_OF_USERS.Add(message.Chat.Username, 0); }

        string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\" + message.Chat.Username;

        Console.WriteLine();
        Console.WriteLine($"От пользователя {message.Chat.Username} пришло сообщение с текстом: {message.Text}");
        Console.WriteLine();

        switch (LIST_OF_USERS[message.Chat.Username])
        {
            case 0: // ПЕРВОЕ СООБЩЕНИЕ
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
                    "🎮 Выберите ваш аккаунт 🎮",
                    replyMarkup: replyKeyboardMarkup);

                LINKS_FUNK(message);

                LIST_OF_USERS[message.Chat.Username]++;
                break;


            case 1: // ПРОИЗОШЕЛ ВЫБОР ВАРИАНТА РЕГИОНА

                if (STUPID_CHECK(message) == true)
                {
                    REC_TO_FILE(message);

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

                    if (lineCount == 1) { LIST_OF_USERS[message.Chat.Username]++; }
                    else { LIST_OF_USERS[message.Chat.Username] += 3; }
                }
                
                break;

            case 2: // ПРОИЗОШЕЛ ВЫБОР КОЛИЧЕСТВА ДУБЛОНОВ
                switch (message.Text)
                {
                    case "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄":
                        REBOOT_BOT(message);
                        break;

                    default:
                        if (STUPID_CHECK(message) == true)
                        {
                            REC_TO_FILE(message);

                            string substringToFind = "PlayStation Россия";
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

                            RUSSIA_OR_NOT(message, lastLine);

                            LIST_OF_USERS[message.Chat.Username]++;
                        }

                        break;
                }
                break;

            case 3: // ПРОИЗОШЕЛ ВВОД ЛОГИНА И ПАРОЛЯ ОТ АККАУНТА 
                switch (message.Text)
                {
                    case "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️":
                        LIST_OF_USERS[message.Chat.Username] -= 2;
                        PREVIOUS_STEP(message);
                        break;

                    case "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄":
                        REBOOT_BOT(message);
                        break;

                    default:
                        REC_TO_FILE(message);

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
                            "Пришлите ссылку для связи в соцсетях: ВК или Telegram",
                            replyMarkup: replyKeyboardMarkup3);

                        LIST_OF_USERS[message.Chat.Username]++;
                        break;
                }
                break;

            case 4: //ПРОИЗОШЕЛ ВВОД КОНТАКТНЫХ ДАННЫХ: ВК ИЛИ ТЕЛЕГРАММ   +   БОТ ВЫВОДИТ ДЛЯ ПОЛЬЗОВАТЕЛЯ ИНФОРМАЦИЮ О РЕКВИЗИТАХ ДЛЯ ОПЛАТЫ
                switch (message.Text)
                {
                    case "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️":
                        LIST_OF_USERS[message.Chat.Username] -= 2;
                        PREVIOUS_STEP(message);
                        break;

                    case "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄":
                        REBOOT_BOT(message);
                        break;

                    default:
                        REC_TO_FILE(message);

                        ReplyKeyboardMarkup replyKeyboardMarkup3 = new(new[]
                        {
                            new KeyboardButton[] { "ПЕРЕВОД ВЫПОЛНЕН ✅" },
                            new KeyboardButton[] { "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️" },
                            new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                        })
                        {
                            ResizeKeyboard = true
                        };

                        await client.SendTextMessageAsync(
                            message.Chat.Id,
                            "\nСБП: <code>+79031986580</code> (Сбер, Альфа, Тинькофф)" +
                            "\n" +
                            "\nПОСЛЕ ЗАВЕРШЕНИЯ ОПЛАТЫ НАЖМИТЕ НА КНОПКУ \"ПЕРЕВОД ВЫПОЛНЕН ✅\"",
                            parseMode: ParseMode.Html,
                            replyMarkup: replyKeyboardMarkup3);

                        LIST_OF_USERS[message.Chat.Username]++;

                        break;
                }
                break;

            case 5: // ПРОИЗОШЛО НАЖАТИЕ НА ФИНАЛЬНУЮ КНОПКУ?
                switch (message.Text)
                {
                    case "ПЕРЕВОД ВЫПОЛНЕН ✅":
                        ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
                        {
                            new KeyboardButton[] { "СОЗДАТЬ НОВЫЙ ЗАКАЗ" },
                        })
                        {
                            ResizeKeyboard = true
                        };

                        await client.SendTextMessageAsync(
                            message.Chat.Id,
                            "Спасибо за заказ! В ближайшее время администратор с вами свяжется",
                            replyMarkup: replyKeyboardMarkup4);

                        string lastLine = ""; // ПОИСК ПОСЛЕДНЕЙ СТРОКИ В ФАЙЛЕ ТЕКУЩЕГО ПОЛЬЗОВАТЕЛЯ ДЛЯ ОТПРАВКИ АДМИНИСТРАТОРУ
                        using (StreamReader reader = new StreamReader(path))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                lastLine = line;
                            }
                            reader.Close();
                        }

                        //await client.SendTextMessageAsync(
                        //    5966876209,
                        //    "НОВЫЙ ЗАКАЗ! " + 
                        //    "\n" + lastLine +
                        //    "\nИМЯ ПОЛЬЗОВАТЕЛЯ: " + message.Chat.Username);

                        REC_TO_FILE(message);

                        LIST_OF_USERS[message.Chat.Username] = 0;
                        break;

                    case "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️":
                        int lineCount = 0;
                        using (StreamReader reader = new StreamReader(path))
                        {
                            while (reader.ReadLine() != null)
                            {
                                lineCount++;
                            }
                            reader.Close();
                        }

                        if (lineCount == 1) { LIST_OF_USERS[message.Chat.Username] -= 2; }
                        else { LIST_OF_USERS[message.Chat.Username] -= 3; }

                        PREVIOUS_STEP(message);
                        break;

                    case "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄":
                        REBOOT_BOT(message);
                        break;
                }
                break;
        }
        return;
    }


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

    private static bool STUPID_CHECK(Message message)
    {
        string[] basic = new string[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄", "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️", "1250 ДУБЛОНОВ - 600 ₽",
        "2750 ДУБЛОНОВ - 1180 ₽", "5625 ДУБЛОНОВ - 2310 ₽", "11500 ДУБЛОНОВ - 4620 ₽", "20500 ДУБЛОНОВ - 7120 ₽",
        "30000 ДУБЛОНОВ - 10180 ₽", "47000 ДУБЛОНОВ - 15260 ₽", "🍀 1 ГОРШОК С ЗОЛОТОМ - 600 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 7200 ₽",
        "1250 ДУБЛОНОВ - 480 ₽", "2750 ДУБЛОНОВ - 960 ₽", "5625 ДУБЛОНОВ - 1920 ₽", "11500 ДУБЛОНОВ - 3720 ₽", 
        "20500 ДУБЛОНОВ - 6160 ₽", "30000 ДУБЛОНОВ - 9000 ₽" ,"🍀 1 ГОРШОК С ЗОЛОТОМ - 480 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 6620 ₽",
        "20500 ДУБЛОНОВ - 1500 ₽", "🍀 1 ГОРШОК С ЗОЛОТОМ - 540 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 6980 ₽", "1250 ДУБЛОНОВ - 300 ₽", 
        "2750 ДУБЛОНОВ - 600 ₽", "5625 ДУБЛОНОВ - 1100 ₽", "11500 ДУБЛОНОВ - 2200 ₽", "20500 ДУБЛОНОВ - 3600 ₽", 
        "30000 ДУБЛОНОВ - 4400 ₽", "47000 ДУБЛОНОВ - 6600 ₽", "🍀 1 ГОРШОК С ЗОЛОТОМ - 420 ₽", "🍀 15 ГОРШКОВ С ЗОЛОТОМ - 5600 ₽",
        "PlayStation Россия", "PlayStation Украина", "PlayStation Турция", "Xbox"};
        for (int i=0; i<basic.Length; i++)
        {
            if (message.Text == basic[i])
            {
                return true;
            }
        }
        return false;
    }

    private static async void VARIANT_OF_PURCHASE(Message message)
    {
        // СПАРСИТЬ РЕГИОН ИЗ ТЕКСТОВОГО ФАЙЛА

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

    private static async void RUSSIA_OR_NOT(Message message, string lastLine)
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
                "❗ ВНИМАНИЕ! Все данные присылайте одним сообщением через запятую ❗" +
                "\nПРИМЕР: Логин_От_Аккаунта, Пароль_От_Аккаунта" +
                "\n" +
                "\nЕсли ваш аккаунт \"PlayStation Россия\", то:" +
                "\nВы ранее заходили в свой аккаунт кораблей через мобильное приложение Legends?" +
                "\n   ❶ Если ДА, то пришлите логин и пароль от аккаунта Facebook/Google, используемого для входа" +
                "\n   ❷ Если НЕТ, то пришлите логин и пароль от вашего аккаунта PlayStation Россия",
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
                "❗ ВНИМАНИЕ! Все данные присылайте одним сообщением через запятую ❗" +
                "\nПРИМЕР: Логин_От_Аккаунта, Пароль_От_Аккаунта" +
                "\n" +
                "\nЕсли ваш аккаунт НЕ \"PlayStation Россия\", то:" +
                "\n   ❶ Пришлите логин и пароль от вашего аккаунта PS/Xbox",
                replyMarkup: replyKeyboardMarkup2);
        }
    }

    private static async void REBOOT_BOT(Message message) // ПЕРЕЗАПУСК БОТА С ПЕРВОГО ШАГА
    {
        LIST_OF_USERS[message.Chat.Username] = 0;

        string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\" + message.Chat.Username;
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
            new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
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

    private static async void PREVIOUS_STEP(Message message) // ВОЗВРАЩЕНИЕ НА ПРЕДЫДУЩИЙ ШАГ
    {
        string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\" + message.Chat.Username;
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
                if (lines[lines.Count-1] == line)
                    writer.Write(line);
                else writer.WriteLine(line);
            }
            writer.Close();
        }

        switch (LIST_OF_USERS[message.Chat.Username])
        {
            case 1: // ВЫБОР КОЛИЧЕСТВА ДУБЛОНОВ
                string substringToFind1 = "PlayStation Россия";
                string substringToFind2 = "PlayStation Украина";
                string substringToFind3 = "PlayStation Турция";
                string substringToFind4 = "Xbox";
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

                if (lineCount == 1) { LIST_OF_USERS[message.Chat.Username]++; }
                else { LIST_OF_USERS[message.Chat.Username] += 2; }

                break;

            case 2: // ВВОД ИНФОРМАЦИИ О ПОЛЬЗОВАТЕЛЕ

                string substringToFind = "PlayStation Россия";
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

                RUSSIA_OR_NOT(message, lastLine);

                LIST_OF_USERS[message.Chat.Username]++;

                break;

            case 3:
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
                    "Пришлите ссылку для связи в соцсетях: ВК или Telegram",
                    replyMarkup: replyKeyboardMarkup3);

                LIST_OF_USERS[message.Chat.Username]++;

                break;
        }
        return;
    }

    private static async void REC_TO_FILE(Message message) // ЗАПИСЬ ИНФОРМАЦИИ В ФАЙЛ
    {
        string path1 = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT";
        string mes = message.Text;

        string[] files = Directory.GetFiles(path1);

        int check = 0;

        foreach (string file in files)
        {
            if (file == message.Chat.Username)
            {
                check = 1;
                break;
            }
        }
        if (check == 0)
        {
            using (FileStream fs = System.IO.File.Create(message.Chat.Username)) { fs.Close(); }
        }
        
        string path = path1 + @"\" + message.Chat.Username;


        switch (LIST_OF_USERS[message.Chat.Username])
        {
            case 5:
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteLineAsync(";   " + mes + "   " + DateTime.Now.ToString());
                    writer.Close();
                }
                break;

            default:
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteAsync(";   " + mes);
                    writer.Close();
                }
                break;
        }
    }

    async static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        await Task.Delay(0);
        Console.WriteLine(arg2.Message);
        return;
    }
}