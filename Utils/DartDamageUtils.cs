using System;
using Terraria;

namespace PlentyODarts.Utils
{
    public class DamageUtils
    {
        public float mod = 0f;

        internal float BossCounterMod()
        {
            mod += NPC.downedSlimeKing.ToInt();
            mod += NPC.downedBoss1.ToInt();
            mod += NPC.downedBoss2.ToInt();
            mod += NPC.downedQueenBee.ToInt();
            mod += NPC.downedBoss3.ToInt();
            mod += NPC.downedDeerclops.ToInt();
            mod += Main.hardMode.ToInt();
            mod += NPC.downedQueenSlime.ToInt();
            mod += NPC.downedMechBoss1.ToInt();
            mod += NPC.downedMechBoss2.ToInt();
            mod += NPC.downedMechBoss3.ToInt();
            mod += NPC.downedPlantBoss.ToInt();
            mod += NPC.downedGolemBoss.ToInt();
            mod += NPC.downedFishron.ToInt();
            mod += NPC.downedEmpressOfLight.ToInt();
            mod += NPC.downedAncientCultist.ToInt();

            float avg = mod / (float)16;
            return avg;
        }

        public float GemDartMod(int gemCount, float damageCap)
        {
            float bonus = gemCount * 0.4f;
            return Math.Min(gemCount, damageCap);
        }
    }
}
