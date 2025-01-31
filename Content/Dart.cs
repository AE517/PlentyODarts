using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content
{
    public abstract class DartWeapon : ModItem
    {
        protected virtual bool IsModusApplicable => true;

        //Ensures Reforge prefixes to actually work also including the Dart class directed ones;
        public override bool WeaponPrefix() => true;

        //Despite being a Throwing based class it does not benefits from ranged prefixes;
        public override bool RangedPrefix() => false;

        public override bool Shoot(
            Player player,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Vector2 position,
            Vector2 velocity,
            int type,
            int damage,
            float knockback
        )
        {
            if (IsModusApplicable)
            {
                return ApplyModus(player, position, velocity);
            }
            return true;
        }

        private static bool ApplyModus(Player player, Vector2 position, Vector2 velocity)
        {
            return player.GetModPlayer<PoDPlayer>().CurrentModus.Modus(player, position, velocity)
                != 0;
        }
    }
}
