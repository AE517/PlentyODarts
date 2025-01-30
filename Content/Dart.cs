using Microsoft.Xna.Framework;
using PlentyODarts.Content.Modus;
using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content
{
    public abstract class DartWeapon : ModItem
    {
        public virtual bool isModusApplicable => true;

        public override bool WeaponPrefix() => true;

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
            ModusType modus = player.GetModPlayer<PoDPlayer>().currentModus;
            if (isModusApplicable)
            {
                return applyModus(modus, player, position, velocity);
            }
            return true;
        }

        public bool applyModus(ModusType modus, Player player, Vector2 position, Vector2 velocity)
        {
            switch (modus)
            {
                case ModusType.SPLIT:
                {
                    new Split().Modus(player, position, velocity);
                    return false;
                }

                default:
                    return true;
            }
        }
    }
}
