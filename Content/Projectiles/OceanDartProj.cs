using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class OceanDartProj : DartProjectile
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

            p.DamageType = DartDamage.Instance;
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
                _ = Dust.NewDust(
                    new Vector2(
                        Projectile.position.X + Main.rand.Next(0, 30),
                        Projectile.position.Y + Main.rand.Next(0, 30)
                    ),
                    Projectile.width,
                    Projectile.height,
                    DustID.Water,
                    Projectile.velocity.X / 2,
                    Projectile.velocity.Y / 2,
                    0,
                    Color.DarkBlue,
                    1
                );
            }
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Wet, 300);
            if (target.buffImmune[BuffID.Wet])
                target.buffImmune[BuffID.Wet] = false;

            if (target.HasBuff(BuffID.Wet))
            {
                Projectile.velocity *= 1.02f;
                Projectile.penetrate += 1;
            }

            Projectile.Kill();
        }

        public override void AI()
        {
            base.AI();

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
                int dust = Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Water,
                    0,
                    0,
                    0
                );

                Main.dust[dust].noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
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
