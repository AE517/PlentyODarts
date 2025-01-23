using Microsoft.Xna.Framework;
using PlentyODarts.Content.Modus;
using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content
{
    public abstract class DartWeapon : ModItem, IDart
    {
        public override bool WeaponPrefix() => true;

        public override bool RangedPrefix() => false;
    }

    public interface IDart
    {
        int ApplyModus(ModusType current, Player player, Vector2 position, Vector2 velocity)
        {
            int m;
            return m = current switch
            {
                ModusType.SPLIT => new Split().Modus(player, position, velocity),
                _ => 0,
            };
        }
    }
}
