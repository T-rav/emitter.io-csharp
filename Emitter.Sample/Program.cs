using System;
using System.Text;
using Emitter.Messages;
using Emitter.Utility;

namespace Emitter.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var channelKey = "VP1suiS7cEfori1zoME_fd8Zm0arr4D6";
            var channel = "test/";
            var url = "192.168.86.247";

            // =============================

            //var channelKey = "bmJA7A3D3soLAyagVMwGCB9EffKBsuGa"; 
            //var channel = "staging/solver/status/";
            //var url = "playerfirst-staging-emitter.azurewebsites.net";

            using (var emitter = Connection.Establish(channelKey, url, 80, false))
            {
                emitter.Error += (object sender, Exception e) => { Console.WriteLine("Error:" + e.Message); };
                emitter.PresenceSubscribe(channelKey, channel, false, (PresenceEvent e) => { Console.WriteLine("Presence event " + e.Event + "."); });
                emitter.PresenceStatus(channelKey, channel, (PresenceEvent e) => { Console.WriteLine("Presence event " + e.Event + "."); });

                // Handle chat messages
                emitter.Subscribe(channel,
                    (chan, msg) => { Console.WriteLine("GOT > " + Encoding.UTF8.GetString(msg)); },
                    Options.WithFrom(DateTime.UtcNow.AddHours(-1)),
                    Options.WithUntil(DateTime.UtcNow),
                    Options.WithLast(10_000));

                emitter.Link(channelKey, channel, "L0", true);
                emitter.PublishWithLink("L0", "Link test");

                emitter.Me += (MeResponse me) => { Console.WriteLine(me.MyId); };
                emitter.MeInfo();

                string text = "";
                Console.WriteLine("Type to chat or type 'q' to exit...");
                do
                {
                    text = Console.ReadLine();
                    emitter.Publish(channelKey, channel, text, Options.WithTTL(3600));
                }
                while (text != "q");
            }
        }


    }
}
