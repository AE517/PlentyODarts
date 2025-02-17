using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Shift
{
    public interface IShift
    {
        bool Shift(Projectile projectile, Player player);
    }

    public class DefaultShift : IShift
    {
        public bool Shift(Projectile projectile, Player player) => false;
    }
}
