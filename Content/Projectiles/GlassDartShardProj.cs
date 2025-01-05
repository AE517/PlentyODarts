using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class GlassDartShardProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Glass Shard");
        }

        public override void SetDefaults()
        {
            Projectile p = Projectile;

            p.height = 10;
            p.width = 10;
            p.scale = .9f;

            p.DamageType = DartDamage.Instance;
            p.knockBack = 1;
            p.CritChance = 2;

            p.aiStyle = 2;
            p.timeLeft = 600;

            p.penetrate = 1;

            p.tileCollide = true;
            p.ignoreWater = false;

            p.friendly = true;
            p.hostile = true;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
                target.AddBuff(BuffID.Bleeding, 300);
            Projectile.Kill();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Bleeding, 300);
            Projectile.Kill();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff(BuffID.Bleeding))
                modifiers.FinalDamage += .5f;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (target.HasBuff(BuffID.Bleeding))
                modifiers.FinalDamage += .5f;
        }
    }
}
