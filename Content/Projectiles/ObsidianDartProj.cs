using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class ObsidianDartProj : ModProjectile
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
            if (charged && !Projectile.lavaWet)
            {
                Projectile.Kill();
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                Rectangle rect = new Rectangle(
                    (int)Projectile.Center.X - 75,
                    (int)Projectile.Center.Y - 75,
                    100,
                    100
                );
                Array.ForEach(
                    Main.npc,
                    npc =>
                    {
                        if (npc.Hitbox.Intersects(rect))
                            npc.SimpleStrikeNPC(Projectile.damage, 1);
                    }
                );
                makeSmoke();
            }
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 600);
            if (charged)
            {
                hit.Crit = true;
                explode((int)(Projectile.damage * 1.5f), 150);
            }

            if (Main.LocalPlayer.HasItem(ItemID.LavaBucket))
                explode((int)(Projectile.damage * 0.9f), 50);
        }

        public override void AI()
        {
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

            Projectile.damage += charged ? 2 : 0;

            Player player = Main.LocalPlayer;
            if (charged || player.HasItem(ItemID.LavaBucket))
            {
                int dust = Dust.NewDust(
                    Projectile.position,
                    2,
                    2,
                    DustID.Torch,
                    Projectile.velocity.X,
                    Projectile.velocity.Y,
                    0,
                    default,
                    1
                );
            }
        }

        void explode(int damage, int area)
        {
            SoundEngine.PlaySound(SoundID.Item89, Projectile.position);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Rectangle rect = new Rectangle(
                (int)Projectile.Center.X - area,
                (int)Projectile.Center.Y - area,
                100,
                100
            );
            Array.ForEach(
                Main.npc,
                npc =>
                {
                    if (npc.Hitbox.Intersects(rect))
                    {
                        npc.SimpleStrikeNPC(Projectile.damage, 1);
                        if (charged)
                            npc.AddBuff(Main.hardMode ? BuffID.OnFire3 : BuffID.OnFire, 600);
                    }
                }
            );
            makeSmoke();
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
    }
}
