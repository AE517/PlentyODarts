using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class GlassDartProj : DartProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Glass Dart");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 5;
            p.height = 5;
            p.scale = 1;

            p.DamageType = DartDamage.Instance;
            p.CritChance = 5;
            p.knockBack = 2;

            p.aiStyle = 1;
            p.timeLeft = 600;

            p.penetrate = 1;
            p.tileCollide = true;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = false;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            CreateShards(oldVelocity);
            SoundEngine.PlaySound(SoundID.Shatter);
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                SoundEngine.PlaySound(SoundID.Shatter);
                if (target.buffImmune[BuffID.Bleeding])
                {
                    target.buffImmune[BuffID.Bleeding] = false;
                    target.AddBuff(BuffID.Bleeding, 300);
                }

                target.AddBuff(BuffID.Bleeding, 600);
                CreateShards(Projectile.oldVelocity);
            }

            Projectile.Kill();
        }

        public void CreateShards(Vector2 velocity)
        {
            int shards = Main.rand.Next(0, 10);

            for (int i = 0; i <= shards; i++)
            {
                float rotation = MathHelper.ToRadians(Main.rand.Next(-360, 360));
                Vector2 newVel = velocity.RotatedByRandom(rotation);

                newVel *= 1f - Main.rand.NextFloat(0.5f);

                int proj = Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position,
                    newVel,
                    ModContent.ProjectileType<GlassDartShardProj>(),
                    1,
                    1
                );
            }
        }
    }
}
