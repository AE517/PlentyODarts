using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class FieryDartProj : ModProjectile
    {
        public Color[] dusts = { Color.Red, Color.Yellow, Color.Orange, Color.LightYellow };

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Fiery Dart");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.height = 16;
            p.width = 3;

            p.DamageType = DartDamage.Instance;
            p.knockBack = 1;

            p.aiStyle = 1;
            p.timeLeft = 300;

            p.penetrate = 1;

            p.tileCollide = true;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = false;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            if (Projectile.wet)
            {
                SoundEngine.PlaySound(SoundID.LiquidsWaterLava);
                Projectile.Kill();
            }

            SoundEngine.PlaySound(SoundID.Dig);
            Projectile.Kill();

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                if (target.buffImmune[BuffID.OnFire])
                {
                    target.buffImmune[BuffID.OnFire] = false;
                    target.AddBuff(BuffID.OnFire, 300);
                }

                target.AddBuff(BuffID.OnFire, 600);
            }

            if (target.HasBuff(BuffID.OnFire))
                damageDone += 5;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff(BuffID.OnFire))
                modifiers.FinalDamage += .2f;
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;

            if (Projectile.owner == Main.myPlayer)
            {
                if (player.ZoneUnderworldHeight)
                    Projectile.timeLeft += 2;
                if (player.ZoneSnow)
                    Projectile.timeLeft -= 2;
            }

            int dust = Dust.NewDust(
                Projectile.position + Projectile.velocity,
                Projectile.width,
                Projectile.height,
                DustID.Torch,
                Projectile.velocity.X / 2,
                Projectile.velocity.Y / 2,
                125,
                Main.rand.NextFromList(dusts),
                1
            );

            Vector2 pos = new Vector2(Projectile.Center.X + 20, Projectile.Center.Y);
            Lighting.AddLight(pos, .9f, .5f, .8f);
        }
    }
}
