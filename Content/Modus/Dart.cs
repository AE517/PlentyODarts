using Microsoft.Xna.Framework;
using Terraria;

namespace PlentyODarts.Content.Modus
{
    public interface IDart
    {
        void ApplyModus(ModusType current, Player player, Vector2 position, Vector2 velocity)
        {
            int m = current switch
            {
                ModusType.SPLIT => new Split().Modus(player, position, velocity),
                _ => Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    position,
                    velocity,
                    player.HeldItem.shoot,
                    player.HeldItem.damage,
                    player.HeldItem.knockBack
                ),
            };
        }
        //void ApplyShift(Player player);
    }
}
