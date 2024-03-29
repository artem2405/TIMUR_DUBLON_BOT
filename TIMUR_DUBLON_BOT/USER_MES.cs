﻿using Newtonsoft.Json.Linq;
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
using static System.Net.WebRequestMethods;

partial class Bot
{
    private static async void USER_MES(Message message)
    {
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

        RASSYLKA_DATABASE(message); // ЗАПИСЬ ПОЛЬЗОВАТЕЛЯ В СПИСОК ПОЛЬЗОВАТЕЛЕЙ ДЛЯ РАССЫЛКИ

        string path = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\" + message.Chat.Username;
        // string path = @"/data/Users" + message.Chat.Username;

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
                    new KeyboardButton[] { "iOS/Android" }
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


            case 1: // ПРОИЗОШЕЛ ВЫБОР РЕГИОНА

                if (STUPID_CHECK(message) == true)
                {
                    REC_TO_FILE(message);

                    VARIANT_OF_PURCHASE(message);

                    if (LINE_COUNT(path) == 1) { LIST_OF_USERS[message.Chat.Username]++; }
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

            case 3: // ПРОИЗОШЕЛ ВВОД ЛОГИНА ОТ АККАУНТА 
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
                            "Пришлите пароль от вашего аккаунта",
                            replyMarkup: replyKeyboardMarkup3);

                        LIST_OF_USERS[message.Chat.Username]++;
                        break;
                }
                break;

            case 4: // ПРОИЗОШЕЛ ВВОД ПАРОЛЯ ОТ АККАУНТА 
                switch (message.Text)
                {
                    case "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️":
                        if (LINE_COUNT(path) == 1) { LIST_OF_USERS[message.Chat.Username] -= 2; }
                        else { LIST_OF_USERS[message.Chat.Username] -= 3; }

                        PREVIOUS_STEP(message);
                        break;

                    case "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄":
                        REBOOT_BOT(message);
                        break;

                    default:

                        if ((LINE_COUNT(path) == 1) || (STUPID_CHECK(message) == true && LINE_COUNT(path) > 1))
                        {
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
                                "Пришлите ТЕКСТОВУЮ ссылку для связи в соцсетях: ВК или Telegram",
                                replyMarkup: replyKeyboardMarkup3);

                            LIST_OF_USERS[message.Chat.Username]++;
                        }
                        break;
                }
                break;

            case 5: //ПРОИЗОШЕЛ ВВОД КОНТАКТНЫХ ДАННЫХ: ВК ИЛИ ТЕЛЕГРАММ   +   БОТ ВЫВОДИТ ДЛЯ ПОЛЬЗОВАТЕЛЯ ИНФОРМАЦИЮ О РЕКВИЗИТАХ ДЛЯ ОПЛАТЫ
                int i = 0;
                if (message.Type == MessageType.Photo && i == 1)
                {
                    //PhotoSize photo = message.Photo[message.Photo.Length - 1];
                    //string path1 = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT" + message.Chat.Username + @"_ССЫЛКА.jpg"; // В ПЕРВЫЕ КАВЫЧКИ ВСТАВИТЬ ПУТЬ ДИРРЕКТОРИИ

                    //using (FileStream stream = new FileStream(path, FileMode.Create))
                    //{
                    //    await client.DownloadFileAsync(photo.FileId, stream);
                    //    stream.Close();
                    //}
                }
                else
                {
                    switch (message.Text)
                    {
                        case "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️":
                            if (LINE_COUNT(path) == 1) { LIST_OF_USERS[message.Chat.Username] -= 2; }
                            else { LIST_OF_USERS[message.Chat.Username] -= 4; }

                            PREVIOUS_STEP(message);
                            break;

                        case "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄":
                            REBOOT_BOT(message);
                            break;

                        default:
                            REC_TO_FILE(message);

                            char targetChar = '₽';
                            string fileContent = System.IO.File.ReadAllText(path);
                            int Index = fileContent.IndexOf(targetChar);
                            int startIndex = fileContent.LastIndexOf('-', Index);
                            string parsedString = fileContent.Substring(startIndex + 1, Index - startIndex - 1);

                            ReplyKeyboardMarkup replyKeyboardMarkup3 = new(new[]
                            {
                                // new KeyboardButton[] { "ПЕРЕВОД ВЫПОЛНЕН ✅" },
                                new KeyboardButton[] { "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️" },
                                new KeyboardButton[] { "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄" },
                            })
                            {
                                ResizeKeyboard = true
                            };

                            await client.SendTextMessageAsync(
                                message.Chat.Id,
                                "СУММА К ОПЛАТЕ:<code>" + parsedString + "</code>₽" +
                                "\n" +
                                "\nСБП: <code>+79031986580</code> (Сбер, Альфа, Тинькофф)" +
                                "\n" +
                                //"\nПОСЛЕ ЗАВЕРШЕНИЯ ОПЛАТЫ НАЖМИТЕ НА КНОПКУ \"ПЕРЕВОД ВЫПОЛНЕН ✅\"",
                                "\nПОСЛЕ ЗАВЕРШЕНИЯ ОПЛАТЫ ОТПРАВЬТЕ СКРИНШОТ ОПЛАТЫ",
                                parseMode: ParseMode.Html,
                                replyMarkup: replyKeyboardMarkup3);

                            LIST_OF_USERS[message.Chat.Username]++;

                            break;
                    }
                }
                break;

            case 6: // ПРОИЗОШЛО НАЖАТИЕ НА ФИНАЛЬНУЮ КНОПКУ?
                i = 1;
                if ((message.Type == MessageType.Photo || message.Type == MessageType.Document) && i == 1)
                {
                    Random rnd = new Random();
                    string path1 = @"C:\Users\artem\Desktop\PROGS\TIMUR_DUBLON_BOT\" + message.Chat.Username + @"_ССЫЛКА_" + rnd.Next(0, 1000000) + ".jpg";

                    if (message.Type == MessageType.Photo)
                    {
                        string fileId = message.Photo[message.Photo.Length - 1].FileId;

                        using (FileStream stream = new FileStream(path1, FileMode.Create))
                        {
                            await client.GetInfoAndDownloadFileAsync(fileId, stream);
                            stream.Close();
                        }
                    }
                    if (message.Type == MessageType.Document)
                    {
                        string fileId = message.Document.FileId;

                        using (FileStream stream = new FileStream(path1, FileMode.Create))
                        {
                            await client.GetInfoAndDownloadFileAsync(fileId, stream);
                            stream.Close();
                        }
                    }

                    ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
                    {
                        new KeyboardButton[] { "🆕 СОЗДАТЬ НОВЫЙ ЗАКАЗ 🆕" },
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

                    using (FileStream stream = new FileStream(path1, FileMode.Open))
                    {
                        InputFileStream input = new InputFileStream(stream);
                        await client.SendPhotoAsync(
                            chatId: 432771577, // ВВЕСТИ СВОЙ АЙДИ ИЗ ТЕЛЕГРАМА
                            photo: input,
                            caption:
                            "НОВЫЙ ЗАКАЗ! " +
                            "\n" + lastLine +
                            "\nИМЯ ПОЛЬЗОВАТЕЛЯ: " + message.Chat.Username + ";  ID ПОЛЬЗОВАТЕЛЯ: " + message.Chat.Id,
                            parseMode: ParseMode.Html);
                    }

                    //await client.SendTextMessageAsync(
                    //    ,
                    //    "НОВЫЙ ЗАКАЗ! " + 
                    //    "\n" + lastLine +
                    //    "\nИМЯ ПОЛЬЗОВАТЕЛЯ: " + message.Chat.Username + ";  ID ПОЛЬЗОВАТЕЛЯ: " + message.Chat.Id);

                    REC_TO_FILE(message);

                    LIST_OF_USERS[message.Chat.Username] = 0;
                }
                else
                {
                    switch (message.Text)
                    {
                        //case "ПЕРЕВОД ВЫПОЛНЕН ✅":
                        //    ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
                        //    {
                        //        new KeyboardButton[] { "🆕 СОЗДАТЬ НОВЫЙ ЗАКАЗ 🆕" },
                        //    })
                        //    {
                        //        ResizeKeyboard = true
                        //    };

                        //    await client.SendTextMessageAsync(
                        //        message.Chat.Id,
                        //        "Спасибо за заказ! В ближайшее время администратор с вами свяжется",
                        //        replyMarkup: replyKeyboardMarkup4);

                        //    string lastLine = ""; // ПОИСК ПОСЛЕДНЕЙ СТРОКИ В ФАЙЛЕ ТЕКУЩЕГО ПОЛЬЗОВАТЕЛЯ ДЛЯ ОТПРАВКИ АДМИНИСТРАТОРУ
                        //    using (StreamReader reader = new StreamReader(path))
                        //    {
                        //        string line;
                        //        while ((line = reader.ReadLine()) != null)
                        //        {
                        //            lastLine = line;
                        //        }
                        //        reader.Close();
                        //    }

                        //    //await client.SendTextMessageAsync(
                        //    //    ,
                        //    //    "НОВЫЙ ЗАКАЗ! " + 
                        //    //    "\n" + lastLine +
                        //    //    "\nИМЯ ПОЛЬЗОВАТЕЛЯ: " + message.Chat.Username + ";  ID ПОЛЬЗОВАТЕЛЯ: " + message.Chat.Id);

                        //    REC_TO_FILE(message);

                        //    LIST_OF_USERS[message.Chat.Username] = 0;
                        //    break;

                        case "↩️ ВЕРНУТЬСЯ НА ПРЕДЫДУЩИЙ ШАГ ↩️":
                            LIST_OF_USERS[message.Chat.Username] -= 2;
                            PREVIOUS_STEP(message);
                            break;

                        case "🔄 ПЕРЕЗАПУСТИТЬ БОТА 🔄":
                            REBOOT_BOT(message);
                            break;
                    }
                }
                
                break;
        }
    }
}
