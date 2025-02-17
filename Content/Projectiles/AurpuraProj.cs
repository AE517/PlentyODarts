using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class AurpuraProj : DartProjectile
    {
        private const int BufferSize = 20;
        private Vector2[] posBuffer = new Vector2[BufferSize];
        private int Tail = 0;
        private bool BufferFull;

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Aurpura Dart");
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

            p.penetrate = 2;
            p.tileCollide = true;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = false;

            DrawOriginOffsetY = -4;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            float minDist = 900f;
            int index = 0;

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CanBeChasedBy(Projectile, false) && npc != target)
                {
                    float dist = (Projectile.position - npc.position).Length();

                    if (dist < minDist)
                    {
                        minDist = dist;
                        index = npc.whoAmI;
                    }
                }
            }

            Vector2 dartVelocity;
            Vector2 critDartVelocity;

            if (hit.Crit)
            {
                if (minDist < 900f)
                    dartVelocity = Main.npc[index].Center - Projectile.Center;
                else
                    dartVelocity = -Projectile.velocity;

                critDartVelocity = Main.npc[index + 1].Center - Projectile.Center;
                dartVelocity = Main.npc[index].Center - Projectile.Center;

                dartVelocity.Normalize();
                dartVelocity *= 10f;

                critDartVelocity.Normalize();
                critDartVelocity *= 20f;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position,
                    dartVelocity,
                    ModContent.ProjectileType<AurpuraProj2>(),
                    (int)(Projectile.damage * 1.1f),
                    2,
                    Main.myPlayer,
                    0,
                    0
                );

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position,
                    critDartVelocity,
                    ModContent.ProjectileType<AurpuraProj2>(),
                    Projectile.damage * 2,
                    2,
                    Main.myPlayer,
                    0,
                    0
                );
            }
            else
            {
                if (minDist < 900f)
                    dartVelocity = Main.npc[index].Center - Projectile.Center;
                else
                    dartVelocity = -Projectile.velocity;

                dartVelocity.Normalize();
                dartVelocity *= 20f;

                int dart = Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position,
                    dartVelocity,
                    ModContent.ProjectileType<AurpuraProj2>(),
                    (int)(Projectile.damage * 1.1f),
                    2,
                    Main.myPlayer,
                    0,
                    0
                );
            }
        }

        public override void AI()
        {
            base.AI();

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
                    DustID.PurpleTorch,
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

    public class AurpuraProj2 : DartProjectile
    {
        private const int BufferSize = 20;
        private Vector2[] posBuffer = new Vector2[BufferSize];
        private int Tail = 0;
        private bool BufferFull;

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Aurpura Dart");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 10;
            p.height = 10;
            p.scale = 1;

            p.DamageType = DartDamage.Instance;

            p.aiStyle = 1;
            p.timeLeft = 600;

            p.penetrate = 6;
            p.tileCollide = false;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = false;

            DrawOriginOffsetY = -4;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            base.AI();

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
                    DustID.YellowTorch,
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
