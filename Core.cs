﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServerInfo
{
    internal class Core
    {
        public static string gameVersion = "1.0.23";
        public static string ip = "45.77.65.193";
        public static double frequency = 2.5;

        public static string displayContent = "";
        public static HttpClient client = new HttpClient();

        public static void Main()
        {
            while (true)
            {
                Server serverData = GetServerData(ip);

                if (serverData != null)
                {
                    UpdateDisplay(serverData);
                }
                

                System.Threading.Thread.Sleep((int)(frequency * 1000));
            }
        }

        public static void UpdateDisplay(Server server)
        {
            string newContent = $@"
version/updated      {server.Version} / {server.Updated:yyyy-MM-dd HH:mm:ss}
name                 {server.Name}
map/gameMode         {server.MapLabel} ({server.MapId}) / {server.GameModeLabel} ({server.GameMode})
ip/port/slots        {server.Ip}:{server.Port} / {server.Slots}/{server.MaxSlots}
PW/Secured/PTVV      {server.BPasswordProtected.ToString().ToLower()} / {server.BSecured.ToString().ToLower()} / {server.BPavTVVOIP.ToString().ToLower()}
";

            newContent = newContent.Trim();

            if (newContent != displayContent)
            {
                Console.Clear();
                Console.WriteLine(newContent);
                displayContent = newContent;
            }
        }

        public static Server? GetServerData(string ip)
        {
            string json = client.GetStringAsync("https://prod-crossplay-pavlov-ms.vankrupt.net/servers/v2/list/" + gameVersion + "/windows/0/0/0/ALL").Result;

            try
            {
                // Deserialisierung
                Root root = JsonConvert.DeserializeObject<Root>(json);

                // Ausgabe der Daten
                if (root != null && root.Servers != null)
                {
                    foreach (var server in root.Servers)
                    {
                        if (server.Ip == ip)
                        {
                            return server;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler bei der Verarbeitung der JSON-Daten: " + ex.Message);
            }

            return null;
            
        }
    }

    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slots")]
        public int Slots { get; set; }

        [JsonProperty("maxSlots")]
        public int MaxSlots { get; set; }

        [JsonProperty("mapId")]
        public string MapId { get; set; }

        [JsonProperty("mapLabel")]
        public string MapLabel { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("bPasswordProtected")]
        public bool BPasswordProtected { get; set; }

        [JsonProperty("bSecured")]
        public bool BSecured { get; set; }

        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("bPavTVVOIP")]
        public bool BPavTVVOIP { get; set; }

        [JsonProperty("gameModeLabel")]
        public string GameModeLabel { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
    }

    public class Root
    {
        [JsonProperty("servers")]
        public List<Server> Servers { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}