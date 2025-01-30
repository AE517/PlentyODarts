using PlentyODarts.Content.Shift;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Globals
{
    public class PoDProjectile : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            Player player = Main.LocalPlayer;
            ShiftType shift = player.GetModPlayer<PoDPlayer>().currentShift;
            if (shift != ShiftType.NONE)
            {
                switch (shift)
                {
                    case ShiftType.HYPERSPEED:
                    {
                        new HyperSpeed().Shift(projectile, player);
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}
