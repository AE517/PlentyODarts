//using System;
using PlentyODarts.Content.Weapons;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Projectiles
{
    public class WoodenDartProj : DartProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 24;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DartDamage.Instance;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;

            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.Kill();

            if (Main.rand.Next(100) < 15)
            {
                Item item = new(ModContent.ItemType<WoodenDart>());
                Item.NewItem(Projectile.GetSource_FromThis(), Projectile.Center, item);
            }
            return false;
        }
    }
}
