using PlentyODarts.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace PlentyODarts.Content.Projectiles.Phase_02.GemDarts
{
    public class AmethystDartProj : DartProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Amethyst Dart");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 10;
            p.height = 10;
            p.scale = 1;

            p.DamageType = DartDamage.Instance;

            p.aiStyle = 1;
            p.timeLeft = 1200;

            p.penetrate = 1;
            p.tileCollide = true;
            p.ignoreWater = true;

            p.friendly = true;
            p.hostile = false;

            AIType = ProjectileID.WoodenArrowFriendly;

            DrawOffsetX = -1;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            Projectile.Kill();
            DartUtils.SoundFxVolume(SoundID.Dig, Projectile.position);
            return false;
        }

        public override void AI()
        {
            base.AI();
            int dust = Dust.NewDust(
                Projectile.position,
                2,
                2,
                DustID.GemAmethyst,
                0,
                0,
                125,
                default
            );
            Main.dust[dust].noGravity = true;
        }
    }
}
