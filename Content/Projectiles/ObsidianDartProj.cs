using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class ObsidianDartProj : DartProjectile
    {
        private bool charged = false;

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Obsidian Dart");
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

            DrawOffsetX = -4;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            if (charged)
                explode(Projectile.damage, 75, 75);

            if (Main.player[Projectile.owner].HasItem(ItemID.LavaBucket))
                explode(Projectile.damage / 2, 50, 50);

            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 600);
            if (charged)
            {
                explode((int)(Projectile.damage * 1.5f), 150, 150);
            }

            if (Main.LocalPlayer.HasItem(ItemID.LavaBucket))
                explode((int)(Projectile.damage * 0.9f), 50, 50);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (charged)
            {
                modifiers.SetCrit();
            }
        }

        public override void AI()
        {
            base.AI();

            Tile tile = Framing.GetTileSafely(Projectile.position);
            if (tile.LiquidType == LiquidID.Lava)
            {
                charged = true;
                SoundEngine.PlaySound(SoundID.Item20.WithPitchOffset(0.7f), Projectile.position);

                Projectile.velocity.X =
                    Math.Abs(Projectile.velocity.X - Projectile.oldVelocity.X) > float.Epsilon
                        ? -Projectile.oldVelocity.X * 2
                        : Projectile.velocity.X;
                Projectile.velocity.Y =
                    Math.Abs(Projectile.velocity.Y - Projectile.oldVelocity.Y) > float.Epsilon
                        ? -Projectile.oldVelocity.Y * 2
                        : Projectile.velocity.Y;
            }

            Player player = Main.player[Projectile.owner];
            if (charged || player.HasItem(ItemID.LavaBucket))
                Lighting.AddLight(Projectile.Center, Color.OrangeRed.ToVector3());

            if (charged)
            {
                int dust = Dust.NewDust(
                    Projectile.oldPosition,
                    1,
                    1,
                    DustID.Torch,
                    Projectile.velocity.X,
                    Projectile.velocity.Y,
                    0,
                    default,
                    1
                );
                // Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= Main.rand.NextFloat(-0.4f, 0.4f);
                Main.dust[dust].noLight = true;
            }
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D texture = ModContent
                .Request<Texture2D>(
                    "PlentyODarts/Content/Projectiles/Glowmasks/ObsidianDartProj_Glow",
                    ReLogic.Content.AssetRequestMode.ImmediateLoad
                )
                .Value;

            Vector2 p = new Vector2(
                Projectile.Center.X - Main.screenPosition.X,
                Projectile.Center.Y - Main.screenPosition.Y
            );

            if (charged || Main.player[Projectile.owner].HasItem(ItemID.LavaBucket))
            {
                DrawOriginOffsetY = -10;
                Main.EntitySpriteDraw(
                    texture,
                    p,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    Projectile.rotation,
                    texture.Size() * .5f,
                    1f,
                    (Projectile.spriteDirection != 1)
                        ? SpriteEffects.FlipHorizontally
                        : SpriteEffects.None,
                    0f
                );
            }
        }

        void explode(int damage, int width, int height)
        {
            SoundEngine.PlaySound(SoundID.Item89, Projectile.position);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Rectangle rect = new Rectangle(
                (int)Projectile.Center.X - width / 2,
                (int)Projectile.Center.Y - height / 2,
                width,
                height
            );
            Array.ForEach(
                Main.npc,
                npc =>
                {
                    if (npc.Hitbox.Intersects(rect) && !npc.townNPC)
                    {
                        npc.SimpleStrikeNPC(Projectile.damage, 1);
                        if (charged)
                            npc.AddBuff(Main.hardMode ? BuffID.OnFire3 : BuffID.OnFire, 600);
                    }
                }
            );
            makeSmoke();
            for (int i = 0; i <= 20; i++)
            {
                Vector2 v = new Vector2(
                    Main.rand.NextFloat(-2f, 2f) * 5,
                    Main.rand.NextFloat(-2f, 2f) * 5
                );

                Dust d = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Torch,
                    v,
                    0,
                    default,
                    Main.rand.NextFloat(.9f, 1.5f)
                );

                d.noGravity = true;
            }
        }

        void makeSmoke()
        {
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
                smokeGore.velocity -= Vector2.One;
            }
        }
    }
}
