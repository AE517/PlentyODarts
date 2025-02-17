using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class FrostDartProj : DartProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Frost Dart");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 4;
            p.height = 4;

            p.DamageType = DartDamage.Instance;

            p.aiStyle = 1;
            p.timeLeft = 300;

            p.penetrate = 1;

            p.tileCollide = true;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = false;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);

            for (int i = 0; i <= 15; i++)
            {
                int dust = Dust.NewDust(
                    new Vector2(
                        Projectile.position.X + Main.rand.Next(0, 20),
                        Projectile.position.Y + Main.rand.Next(0, 20)
                    ),
                    Projectile.width,
                    Projectile.height,
                    DustID.Ice,
                    Projectile.velocity.X / 2,
                    Projectile.velocity.Y / 2,
                    0,
                    Color.LightBlue,
                    1
                );

                Main.dust[dust].noGravity = true;
            }

            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                if (target.buffImmune[BuffID.Frostburn])
                {
                    target.buffImmune[BuffID.Frostburn] = false;
                    target.AddBuff(BuffID.Frostburn, 300);
                }

                target.AddBuff(BuffID.Frostburn, 600);

                if (target.HasBuff(BuffID.Wet))
                    target.AddBuff(BuffID.Slow, 300);

                SoundEngine.PlaySound(SoundID.Shatter, target.position);
            }
        }

        public override void AI()
        {
            base.AI();

            Player player = Main.LocalPlayer;
            if (Projectile.owner == Main.myPlayer)
            {
                if (player.ZoneSnow)
                    Projectile.timeLeft += 2;
                if (player.ZoneDesert || player.ZoneBeach || player.ZoneUndergroundDesert)
                    Projectile.timeLeft -= 2;
            }
        }
    }
}
