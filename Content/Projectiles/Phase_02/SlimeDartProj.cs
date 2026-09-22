using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
//using Terraria.ModLoader;

using PlentyODarts.Utils;
using Terraria.GameContent;

namespace PlentyODarts.Content.Projectiles.Phase_02;

public class SlimeDartProj : DartProjectile
{
    protected override bool IsShiftApplicable => false;

    private int _bounces = 6;

    public override void SetStaticDefaults()
    {
        DisplayName.Format("Slimy Dart");
    }

    public override void SetDefaults()
    {
        Projectile p = Projectile;

        p.width = 14;
        p.height = 14;
        p.scale = 1;

        p.DamageType = DartDamage.Instance;
        p.CritChance = 10;

        p.aiStyle = ProjAIStyleID.Arrow;
        p.timeLeft = 600;

        p.penetrate = 1;
        p.tileCollide = true;
        p.ignoreWater = false;
        p.lavaWet = true;

        p.friendly = true;
        p.hostile = false;

        AIType = ProjectileID.WoodenArrowFriendly;

        DrawOriginOffsetY = -5;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        _bounces--;

        if (_bounces == 0) Projectile.Kill();
        else
        {
            Collision.HitTiles(
                Projectile.position,
                Projectile.velocity,
                Projectile.width,
                Projectile.height);
            
            DartUtils.SoundFxVolume(SoundID.Item56, Projectile.position, 0.7f, 0.1f);
            
            Projectile.velocity = DartUtils.Bounce(Projectile.velocity, oldVelocity);
        }
        
        return false;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (target.HasBuff(BuffID.OnFire)) target.buffTime[BuffID.OnFire] += 60;
        else target.AddBuff(BuffID.Slimed, 180);
        
    }

    public override void AI()
    {
        base.AI();
        
        for (var i = 0; i < 2; i++)
        {
            var dust = Dust.NewDust(
                Projectile.position + Projectile.velocity,
                1,
                1,
                DustID.Water,
                Projectile.velocity.X,
                Projectile.velocity.Y);

            Main.dust[dust].fadeIn *= 1.2f;
        }
    }
}