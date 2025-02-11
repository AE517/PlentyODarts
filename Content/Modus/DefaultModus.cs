using Microsoft.Xna.Framework;
using Terraria;

namespace PlentyODarts.Content.Modus
{
    public interface IModus
    {
        int Modus(Player player, Vector2 position, Vector2 velocity);
    }

    public class DefaultModus : IModus
    {
        /* Acts as a Default Behavior of Modus, when none are equipped*/
        public int Modus(Player player, Vector2 position, Vector2 velocity)
        {
            return 1;
        }
    }
}
