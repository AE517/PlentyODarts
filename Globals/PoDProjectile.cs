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
            if (projectile.DamageType == DartDamage.Instance)
            {
                Player player = Main.LocalPlayer;
                ShiftType shift = player.GetModPlayer<PoDPlayer>().currentShift;

                int S = shift switch
                {
                    ShiftType.HYPERSPEED => new HyperSpeed().Shift(projectile, player),
                    _ => 0,
                };
            }
        }
    }
}
