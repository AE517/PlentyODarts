using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class OceanDart2Proj : DartProjectile
    {
        protected override bool IsShiftApplicable => false;

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Ocean Dart");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 18;
            p.height = 25;
            p.scale = 1;

            p.DamageType = DamageClass.Generic;
            p.CritChance = 5;
            p.knockBack = 5;

            p.aiStyle = 1;
            p.timeLeft = 600;

            p.penetrate = 1;
            p.tileCollide = true;
            p.ignoreWater = true;

            p.friendly = true;
            p.hostile = false;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            for (int i = 0; i <= 12; i++)
            {
                int dust = Dust.NewDust(
                    new Vector2(
                        Projectile.position.X + Main.rand.Next(0, 30),
                        Projectile.position.Y + Main.rand.Next(0, 30)
                    ),
                    Projectile.width,
                    Projectile.height,
                    DustID.Torch,
                    Projectile.velocity.X / 2,
                    Projectile.velocity.Y / 2,
                    0,
                    Color.OrangeRed,
                    1
                );
            }
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.HasBuff(BuffID.Wet))
            {
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                target.DelBuff(target.FindBuffIndex(BuffID.Wet));

                for (int k = 0; k < 2; k++)
                {
                    float speedMulti = 0.4f;
                    if (k == 1)
                    {
                        speedMulti = 0.8f;
                    }

                    var smokeGore = Gore.NewGoreDirect(
                        Projectile.GetSource_Death(),
                        Projectile.position,
                        default,
                        Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1)
                    );
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity += Vector2.One;
                    smokeGore = Gore.NewGoreDirect(
                        Projectile.GetSource_Death(),
                        Projectile.position,
                        default,
                        Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1)
                    );
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity.X -= 1f;
                    smokeGore.velocity.Y += 1f;
                    smokeGore.behindTiles = true;
                    smokeGore = Gore.NewGoreDirect(
                        Projectile.GetSource_Death(),
                        Projectile.position,
                        default,
                        Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1)
                    );
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity.X += 1f;
                    smokeGore.velocity.Y -= 1f;
                    smokeGore = Gore.NewGoreDirect(
                        Projectile.GetSource_Death(),
                        Projectile.position,
                        default,
                        Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1)
                    );
                    smokeGore.velocity *= speedMulti;
                    smokeGore.velocity -= Vector2.One;
                }
            }

            Projectile.Kill();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff(BuffID.Wet))
            {
                if (Main.raining)
                    modifiers.FinalDamage += 1.7f;

                modifiers.FinalDamage += .7f;
            }
        }

        public override void AI()
        {
            if (Projectile.wet)
            {
                Projectile.damage += 2;
            }

            if (Main.raining)
            {
                Projectile.timeLeft += 2;
                Projectile.damage += 2;
            }

            for (int i = 0; i <= 3; i++)
            {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Torch,
                    0,
                    0,
                    0,
                    Color.OrangeRed
                );
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                Vector2 drawPos =
                    (Projectile.oldPos[k] - Main.screenPosition)
                    + drawOrigin
                    + new Vector2(0f, Projectile.gfxOffY);

                Color color =
                    Projectile.GetAlpha(lightColor)
                    * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

                Main.EntitySpriteDraw(
                    texture,
                    drawPos,
                    null,
                    color,
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale,
                    SpriteEffects.None,
                    0
                );
            }

            return true;
        }
    }
}
