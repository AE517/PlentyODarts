using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Modus
{
    public interface IDart
    {
        void ApplyModus(ModusType current, Player player, Vector2 position, Vector2 velocity)
        {
            switch (current)
            {
                case ModusType.SPLIT:
                    for (int i = 0; i <= 1; i++)
                    {
                        if (i == 1)
                        {
                            Projectile.NewProjectile(
                                player.GetSource_FromThis(),
                                position,
                                velocity,
                                player.HeldItem.shoot,
                                player.HeldItem.damage,
                                player.HeldItem.knockBack
                            );
                        }
                        else
                        {
                            Projectile.NewProjectile(
                                player.GetSource_FromThis(),
                                position,
                                -velocity,
                                player.HeldItem.shoot,
                                player.HeldItem.damage,
                                1
                            );
                        }
                    }
                    break;

                default:
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        position,
                        velocity,
                        player.HeldItem.shoot,
                        player.HeldItem.damage,
                        player.HeldItem.knockBack
                    );
                    break;
            }
        }
        //void ApplyShift(Player player);
    }
}
