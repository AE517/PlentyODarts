using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class ElectricDartProj : DartProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Electric Dart");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.width = 10;
            p.height = 10;
            p.scale = 1f;

            p.DamageType = DartDamage.Instance;

            p.aiStyle = 1;
            p.timeLeft = 720;

            p.penetrate = 1;
            p.tileCollide = true;
            p.ignoreWater = true;

            p.friendly = true;
            p.hostile = false;

            AIType = ProjectileID.WoodenArrowFriendly;

            DrawOffsetX = -1;
            DrawOriginOffsetY = -2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                SoundEngine.PlaySound(SoundID.Item93.WithVolumeScale(.25f), Projectile.position);
                if (target.buffImmune[BuffID.Electrified])
                {
                    target.buffImmune[BuffID.Electrified] = false;
                    target.AddBuff(BuffID.Electrified, 300);
                }

                if (target.ai[0] == 300)
                {
                    target.buffImmune[BuffID.Electrified] = true;
                }

                if (target.HasBuff(BuffID.Electrified))
                    target.ai[0] = 0;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff(BuffID.Wet))
                modifiers.SetCrit();

            if (Main.raining)
                modifiers.SetCrit();
        }

        public override void AI()
        {
            base.AI();

            int dust = Dust.NewDust(
                Projectile.position,
                Projectile.width / 2,
                Projectile.height / 2,
                DustID.Electric,
                0,
                0,
                125,
                Color.LightGoldenrodYellow,
                .5f
            );

            Main.dust[dust].noGravity = true;
        }
    }
}
