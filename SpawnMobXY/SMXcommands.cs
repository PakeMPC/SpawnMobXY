using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TShockAPI;

namespace SpawnMobXY
{
    public static class SMXcommands
    {
        public static void SpawnCommand(CommandArgs args)
        {
            if (args.Parameters.Count < 1) { args.Player.SendErrorMessage(SMXi18n.Get("Usage")); return; }
            string sub = args.Parameters[0].ToLower();

            if (sub == "list")
            {
                args.Player.SendInfoMessage(SMXi18n.Get("ListHeader"));
                foreach (var m in SMXcore.Config.AttachedMobs)
                {
                    string realName = TShock.Utils.GetNPCById(m.NpcId).FullName;
                    args.Player.SendInfoMessage($"- ID {m.NpcId}: {realName} (HP: {m.Health})");
                }
                return;
            }

            if (sub == "detach")
            {
                if (args.Parameters.Count < 2) return;
                var found = TShock.Utils.GetNPCByIdOrName(args.Parameters[1]);
                if (found.Count == 1)
                {
                    SMXcore.Config.AttachedMobs.RemoveAll(m => m.NpcId == found[0].type);
                    SMXcore.Config.Save(SMXcore.ConfigPath);
                    args.Player.SendSuccessMessage(SMXi18n.Get("Detached", found[0].FullName));
                }
                return;
            }

            bool isAttach = (sub == "attach");
            int mobParamIndex = isAttach ? 1 : 0;
            if (args.Parameters.Count <= mobParamIndex) return;

            var target = TShock.Utils.GetNPCByIdOrName(args.Parameters[mobParamIndex]);
            if (target.Count != 1) { args.Player.SendErrorMessage(SMXi18n.Get("NpcNotFound")); return; }
            var baseNpc = target[0];

            int amount = 1;
            int modifierStart = mobParamIndex + 1;
            if (args.Parameters.Count > modifierStart && !args.Parameters[modifierStart].Contains("="))
            {
                int.TryParse(args.Parameters[modifierStart], out amount);
                modifierStart++;
            }

            var stats = ParseParameters(args.Parameters.Skip(modifierStart).ToList());

            if (isAttach)
            {
                SMXcore.Config.AttachedMobs.RemoveAll(m => m.NpcId == baseNpc.type);
                SMXcore.Config.AttachedMobs.Add(new FixedStat { NpcId = baseNpc.type, Health = stats.health, AI = stats.ai, Drop = stats.drop });
                SMXcore.Config.Save(SMXcore.ConfigPath);
                args.Player.SendSuccessMessage(SMXi18n.Get("Attached", baseNpc.FullName));
            }
            else
            {
                float sx, sy;
                if (args.Player.RealPlayer)
                {
                    sx = (stats.x != -1) ? stats.x : args.Player.X + (args.Player.TPlayer.direction * 320);
                    sy = (stats.y != -1) ? stats.y : args.Player.Y;
                }
                else
                {
                    if (stats.x == -1 || stats.y == -1) { args.Player.SendErrorMessage(SMXi18n.Get("ConsoleError")); return; }
                    sx = stats.x; sy = stats.y;
                }

                int sourceIndex = args.Player.RealPlayer ? args.Player.Index : 0;

                for (int i = 0; i < amount; i++)
                {
                    int idx = NPC.NewNPC(NPC.GetBossSpawnSource(sourceIndex), (int)sx, (int)sy, baseNpc.type);
                    var n = Main.npc[idx];

                    if (stats.health > 0) { n.lifeMax = stats.health; n.life = stats.health; }
                    for (int j = 0; j < 4; j++) if (stats.ai[j] != -1f) n.ai[j] = stats.ai[j];
                    if (stats.drop != null) SMXcore.CustomDrops[idx] = stats.drop;

                    n.netUpdate = true;
                    NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, idx);
                }
            }
        }

        private static (int health, float[] ai, DropInfo drop, float x, float y) ParseParameters(List<string> pms)
        {
            int h = -1; float[] a = { -1f, -1f, -1f, -1f }; DropInfo d = null; float x = -1, y = -1;
            foreach (var p in pms)
            {
                if (!p.Contains("=")) continue;
                var s = p.Split('='); string k = s[0].ToLower(), v = s[1];
                switch (k)
                {
                    case "health": int.TryParse(v, out h); break;
                    case "x": float.TryParse(v, out x); x *= 16; break;
                    case "y": float.TryParse(v, out y); y *= 16; break;
                    case "ai0": float.TryParse(v, out a[0]); break;
                    case "ai1": float.TryParse(v, out a[1]); break;
                    case "ai2": float.TryParse(v, out a[2]); break;
                    case "ai3": float.TryParse(v, out a[3]); break;
                    case "drop":
                        var dp = v.Split(':');
                        if (dp.Length == 3) d = new DropInfo { itemId = int.Parse(dp[0]), amount = int.Parse(dp[1]), chance = int.Parse(dp[2]) };
                        break;
                }
            }
            return (h, a, d, x, y);
        }
    }
}