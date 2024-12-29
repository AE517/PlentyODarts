using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace PlentyODarts.Content.Modus
{
    public interface IModus
    {
        int Modus(Player player, Vector2 position, Vector2 velocity);
    }
}
