using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Globals
{
    public class PoDProjectile : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            Player player = Main.LocalPlayer;
        }
    }
}
