using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class MarnumProj : ModProjectile
    {
        private const int BufferSize = 20;
        private Vector2[] posBuffer = new Vector2[BufferSize];
        private int Tail = 0;
        private bool BufferFull;
        private int bounces = 4;

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Marnum Dart");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 10;
            p.height = 10;
            p.scale = 1;

            p.DamageType = DartDamage.Instance;
            p.CritChance = 10;

            p.aiStyle = 1;
            p.timeLeft = 600;

            p.penetrate = 1;
            p.tileCollide = true;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = false;

            DrawOriginOffsetY = -4;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounces--;

            if (bounces == 0)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(
                    Projectile.position,
                    Projectile.velocity,
                    Projectile.width,
                    Projectile.height
                );
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                Projectile.aiStyle = 2;

                Projectile.velocity.X =
                    Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon
                        ? -oldVelocity.X
                        : Projectile.velocity.X;
                Projectile.velocity.Y =
                    Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon
                        ? -oldVelocity.Y
                        : Projectile.velocity.Y;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.position,
                -Projectile.velocity,
                ModContent.ProjectileType<MarnumGhost>(),
                (int)(Projectile.damage * 1.2),
                7f,
                Main.myPlayer
            );
            Projectile.Kill();
        }

        public override void AI()
        {
            posBuffer[Tail] = Projectile.position;
            Tail++;
            if (Tail >= BufferSize)
            {
                BufferFull = true;
                Tail = 0;
            }

            int dustAmt = BufferFull ? BufferSize : Tail;

            for (int i = 0; i < dustAmt; i++)
            {
                int dust = Dust.NewDust(
                    posBuffer[i] + Projectile.velocity,
                    1,
                    1,
                    DustID.WhiteTorch,
                    Projectile.velocity.X / 1.5f,
                    Projectile.velocity.Y / 1.5f
                );
                Main.dust[dust].velocity *= 0;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].fadeIn *= 1.8f;
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
