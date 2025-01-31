using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Modus
{
    public abstract class DartModus : IModus
    {
        public abstract int Modus(Player player, Vector2 position, Vector2 velocity);

        protected int ModusProjectile(
            Player player,
            Vector2 position,
            Vector2 velocity,
            int type,
            int damage,
            float knockback
        )
        {
            return Projectile.NewProjectile(
                player.GetSource_FromThis(),
                position,
                velocity,
                type,
                damage,
                knockback,
                player.whoAmI
            );
        }
    }
}
