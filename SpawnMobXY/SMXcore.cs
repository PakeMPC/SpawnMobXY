using System;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using System.IO;

namespace SpawnMobXY
{
    [ApiVersion(2, 1)]
    public class SMXcore : TerrariaPlugin
    {
        public static SMXconfig Config;
        public static string ConfigPath = Path.Combine(TShock.SavePath, "SMX_Config.json");
        public static Dictionary<int, DropInfo> CustomDrops = new Dictionary<int, DropInfo>();
        private bool[] processedNpcs = new bool[Main.maxNPCs];

        public SMXcore(Main game) : base(game) { }

        public override string Name => "SpawnMobXY";
        public override string Author => "Newy + PakeMPC";
        public override string Description => "Spawn and define mobs with special properties and personalized drops";
        public override Version Version => typeof(SMXcore).Assembly.GetName().Version;

        public override void Initialize()
        {
            if (!File.Exists(ConfigPath)) { Config = new SMXconfig(); Config.Save(ConfigPath); }
            else Config = SMXconfig.Read(ConfigPath);

            SMXi18n.Language = Config.Language;
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            ServerApi.Hooks.NpcKilled.Register(this, OnNpcKilled);
            Commands.ChatCommands.Add(new Command("spawnmobx.use", SMXcommands.SpawnCommand, "smx"));
        }

        private void OnUpdate(EventArgs args)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active)
                {
                    if (!processedNpcs[i]) { SMXattach.ApplyTo(npc); processedNpcs[i] = true; }
                }
                else
                {
                    if (processedNpcs[i]) { CustomDrops.Remove(i); processedNpcs[i] = false; }
                }
            }
        }

        private void OnNpcKilled(NpcKilledEventArgs args)
        {
            if (CustomDrops.TryGetValue(args.npc.whoAmI, out var drop))
            {
                if (new Random().Next(1, 101) <= drop.chance)
                    Item.NewItem(args.npc.GetItemSource_Loot(), args.npc.position, args.npc.width, args.npc.height, drop.itemId, drop.amount);
                CustomDrops.Remove(args.npc.whoAmI);
            }
        }
    }
}