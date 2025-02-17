using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class PoisonDartProj : DartProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Poisoned Dart");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 25;
            Projectile.scale = 1f;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DartDamage.Instance;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                if (target.buffImmune[BuffID.Poisoned])
                {
                    target.buffImmune[BuffID.Poisoned] = false;
                    target.AddBuff(BuffID.Poisoned, 300);
                }

                target.AddBuff(BuffID.Poisoned, 600);
            }
            else
            {
                target.AddBuff(BuffID.Poisoned, 300);
            }
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.Kill();

            for (int i = 0; i <= 12; i++)
            {
                int dust = Dust.NewDust(
                    new Vector2(
                        Projectile.position.X + Main.rand.Next(0, 30),
                        Projectile.position.Y + Main.rand.Next(0, 30)
                    ),
                    Projectile.width,
                    Projectile.height,
                    DustID.JungleSpore,
                    Projectile.velocity.X / 2,
                    Projectile.velocity.Y / 2,
                    0,
                    Color.White,
                    1
                );

                Main.dust[dust].noGravity = true;
            }

            Projectile.Kill();

            return false;
        }

        public override void AI()
        {
            base.AI();

            Lighting.AddLight(Projectile.Center, .3f, 1f, .3f);
        }
    }
}
