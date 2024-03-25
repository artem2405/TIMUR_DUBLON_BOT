using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

partial class Bot
{
    private static async void RASSYLKA_DATABASE(Message message)
    {
        string path1 = @"C:\Users\artem\Desktop\PROGS\💻💻💻ФАЙЛЫ ДЛЯ TIMUR_DUBLON_BOT💻💻💻\СПИСОК_ID_ПОЛЬЗОВАТЕЛЕЙ.txt";
        // string path1 = @"/data/Pictures/ID";

        List<string> lines = new List<string>();
        using (StreamReader reader = new StreamReader(path1))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            reader.Close();
        }

        int check = 0;
        foreach (string line in lines) // ОПРЕДЕЛЕНИЕ НАЛИЧИЯ ID ПОЛЬЗОВАТЕЛЯ В ФАЙЛЕ
        {
            if (line == message.Chat.Id.ToString())
            {
                check = 1;
                break;
            }
        }
        if (check == 0) // СОЗДАНИЕ НОВОГО ФАЙЛА ДЛЯ ПОЛЬЗОВАТЕЛЯ + ДОБАВЛЕНИЕ В ФАЙЛ СО СПИСКОМ ВСЕХ ПОЛЬЗОВАТЕЛЕЙ
        {
            using (FileStream fs = System.IO.File.Create(message.Chat.Username)) { fs.Close(); }

            using (StreamWriter writer = new StreamWriter(path1, true))
            {
                await writer.WriteLineAsync(message.Chat.Id.ToString());
                writer.Close();
            }
        }
    }
}