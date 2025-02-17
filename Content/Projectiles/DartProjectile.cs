using PlentyODarts.Content.Shift;
using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public abstract class DartProjectile : ModProjectile
    {
        protected virtual bool IsShiftApplicable => true;

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            IShift shift = player.GetModPlayer<PoDPlayer>().CurrentShift;

            if (shift == new HyperSpeed())
                modifiers.FlatBonusDamage += -.5f;

            if (shift == new HyperForce())
                modifiers.FlatBonusDamage += 1f;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (IsShiftApplicable)
                ApplyShift(Projectile, player);
        }

        public static bool ApplyShift(Projectile projectile, Player player)
        {
            return !player.GetModPlayer<PoDPlayer>().CurrentShift.Shift(projectile, player);
        }
    }
}
