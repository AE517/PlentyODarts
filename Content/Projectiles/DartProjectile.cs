using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public abstract class DartProjectile : ModProjectile
    {
        protected virtual bool IsShiftApplicable => true;

        /// Some Shifts alter only the projectile stats (mostly defined at the OnSpawn hook) like velocity and damage, while others
        /// do alterations on it's behavior and effects, this ApplyStatus and ApplyBehavior separates
        /// those modifications to avoid unpredictable or undesired continuous behavior.
        public static bool ApplyStatusShift(Projectile projectile, Player player)
        {
            return !player.GetModPlayer<PoDPlayer>().CurrentShift.Shift(projectile, player);
        }

        public override void OnSpawn(Terraria.DataStructures.IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];

            if (IsShiftApplicable)
                ApplyStatusShift(Projectile, player);
        }
    }
}
