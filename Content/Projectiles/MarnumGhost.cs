using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class MarnumGhost : ModProjectile
    {
        private int bounces = 4;

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Marnum Ghost");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 5;
            p.height = 5;
            p.scale = 1;
            p.alpha = 55;

            p.DamageType = DartDamage.Instance;
            p.CritChance = 10;

            p.aiStyle = 2;
            p.timeLeft = 600;

            p.penetrate = 2;
            p.tileCollide = true;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = false;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            bounces--;
            Vector2 oldVelocity = Projectile.oldVelocity;

            if (bounces == 0)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                Projectile.Kill();
            }
            else
            {
                Projectile.velocity.X =
                    Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon
                        ? -oldVelocity.X * 1.02f
                        : Projectile.velocity.X;
                Projectile.velocity.Y =
                    Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon
                        ? -oldVelocity.Y * 1.02f
                        : Projectile.velocity.Y;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D t = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(
                t,
                Projectile.Center - Main.screenPosition,
                null,
                Projectile.GetAlpha(lightColor),
                Projectile.rotation,
                t.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0
            );
            return false;
        }
    }
}
