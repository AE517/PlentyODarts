using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles.GemDarts
{
    public class RubyDartProj : DartProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Ruby Dart");
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

            DrawOffsetX = -3;
            DrawOriginOffsetX = -1;
            DrawOriginOffsetY = -4;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override void AI()
        {
            base.AI();

            int dust = Dust.NewDust(Projectile.position, 2, 2, DustID.GemRuby, 0, 0, 125, default);
            Main.dust[dust].noGravity = true;
        }
    }
}
