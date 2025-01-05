using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Shift
{
    public interface IShift
    {
        int Shift(Projectile projectile, Player player);
    }
}
