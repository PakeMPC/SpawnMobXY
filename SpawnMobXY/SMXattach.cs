using System.Linq;
using Terraria;
using TShockAPI;

namespace SpawnMobXY
{
    public static class SMXattach
    {
        public static void ApplyTo(NPC npc)
        {
            var template = SMXcore.Config.AttachedMobs.FirstOrDefault(m => m.NpcId == npc.type);
            if (template == null) return;

            if (template.Health > 0) { npc.lifeMax = template.Health; npc.life = template.Health; }
            for (int j = 0; j < 4; j++) if (template.AI[j] != -1f) npc.ai[j] = template.AI[j];
            if (template.Drop != null) SMXcore.CustomDrops[npc.whoAmI] = template.Drop;

            npc.netUpdate = true;
            NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, npc.whoAmI);
        }
    }
}