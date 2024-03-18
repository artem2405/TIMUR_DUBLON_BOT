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
    private static int LINE_COUNT(string path)
    {
        int lineCount = 0;
        using (StreamReader reader = new StreamReader(path))
        {
            while (reader.ReadLine() != null)
            {
                lineCount++;
            }
            reader.Close();
        }
        return lineCount;
    }
}
