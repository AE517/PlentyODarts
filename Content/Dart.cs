using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content
{
    /// <summary>
    /// A Subclass of the Vanilla Throwing Class inheriting ModItem.
    /// This class works as Base for any Dart in the mod.
    /// As a subclass it'll inherit every already in-place effect and modifiers present, but also
    /// providing access to unique modifiers and Modus.
    /// </summary>
    public abstract class DartWeapon : ModItem
    {
        /// <summary>
        /// This gives access to Modus behavior changing effects on the Shoot hook, it is enabled by default.
        /// If false, the Shoot hook will follow any previous (or default) logic inside it.
        /// </summary>
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

        /// <summary>
        /// Applies the current equiped Modus to the Dart in use (HeldItem) changing it's present Shoot logic.
        /// </summary>
        /// <param name="player">Actual player (Projectile.owner) using the held Dart.</param>
        /// <param name="position">Dart Spawn Projectile initial position (defined upon shooting).</param>
        /// <param name="velocity">Dart Spawn Projectile initial speed (defined upon shooting at the Item.shootspeed in SetDefault + any modifier present).</param>
        private static bool ApplyModus(Player player, Vector2 position, Vector2 velocity)
        {
            return player.GetModPlayer<PoDPlayer>().CurrentModus.Modus(player, position, velocity)
                != 0;
        }
    }
}
