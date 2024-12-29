using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts
{
    public class PoDPlayer : ModPlayer
    {
        public ModusType currentModus = ModusType.NONE;

        public override void ResetEffects()
        {
            currentModus = ModusType.NONE;
        }
    }
}
