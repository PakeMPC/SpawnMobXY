using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SpawnMobXY
{
    public class DropInfo
    {
        public int itemId;
        public int amount;
        public int chance;
    }

    public class FixedStat
    {
        public int NpcId { get; set; }
        public int Health { get; set; } = -1;
        public float[] AI { get; set; } = new float[4] { -1f, -1f, -1f, -1f };
        public DropInfo Drop { get; set; }
    }

    public class SMXconfig
    {
        public string Language { get; set; } = "es";
        public List<FixedStat> AttachedMobs { get; set; } = new List<FixedStat>();

        public static SMXconfig Read(string path)
        {
            if (!File.Exists(path)) return new SMXconfig();
            return JsonConvert.DeserializeObject<SMXconfig>(File.ReadAllText(path));
        }

        public void Save(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented));
        }
    }
}