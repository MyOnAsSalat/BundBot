using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace BUNDbot
{
    class Program
    {
        public static readonly string[] gimn = "Союз нерушимый республик свободных Сплотила навеки Великая Русь Да здравствует созданный волей народов Единый могучий Советский Союз Славься Отечество наше свободное Дружбы народов надёжный оплот Знамя Советское знамя народное Пусть от победы к победе ведёт".ToUpper().Split(" ");
        static TelegramBotClient bot = new TelegramBotClient("789946714:AAGfbet4D-yGwO4u1nOF0pppKZT32UwGDZc");
        static Dictionary<long, Room> rooms = new Dictionary<long, Room>();
        static void Main()
        {
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            Console.ReadKey();
        }

        static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine(rooms.Count);
            Console.WriteLine(e.Message.Text);
            if (rooms.ContainsKey(e.Message.Chat.Id))
            {
                var result = rooms[e.Message.Chat.Id].NextMessage(e.Message.Text);
                if (result == null) return;
                else bot.SendTextMessageAsync(e.Message.Chat,result);
            }
            else
            {
                var room = new Room();
                rooms.Add(e.Message.Chat.Id,room);
                var result = room.NextMessage(e.Message.Text);
                if (result == null) return;
                else bot.SendTextMessageAsync(e.Message.Chat, result);
            }
        }

        
    }

    public class Room
    {
        private bool isBund = false;
        private int stage = 0;

        private readonly string[] gimn;
        public string NextMessage(string message)
        {
            Console.WriteLine("test");
            if (!isBund)
            {
                if (message != "БУНД") return null;
                else return WhileBund();
            }
            else
            {
              return WhileBund(message);
            }          
        }

        string WhileBund(string message = "БУНД")
        {
            try
            {
                if (message == "БУНД")
            {
                isBund = true;
                stage++;
                return "Вам нужно будет пропеть гимн по очереди со мной!\n \n \n"
                       + String.Join(" ",Program.gimn) + "\n \n \nЯ начну - СОЮЗ" ;
            }

            if (message == Program.gimn[stage])
            {
                
                    stage++;
                    var response = Program.gimn[stage];
                    stage++;
                    return response;
               
            }
            else
            {
                return "Чё пиздишь, уёбок, пой гимн со мной: " + Program.gimn[stage];
            }
            }


            catch (Exception e)
            {
                isBund = false;
                stage = 0;
                return "Допели, молдцом!";
            }

        }

    }
}
