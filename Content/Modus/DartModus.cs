using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Modus
{
    public abstract class DartModus : ModItem, IModus
    {
        public abstract int Modus(Player player, Vector2 position, Vector2 velocity);

        protected int ModusProjectile(
            Player player,
            Vector2 position,
            Vector2 velocity,
            int type,
            int damage,
            float knockback
        )
        {
            return Projectile.NewProjectile(
                player.GetSource_FromThis(),
                position,
                velocity,
                type,
                damage,
                knockback,
                player.whoAmI
            );
        }

        public override bool CanRightClick()
        {
            Player player = Main.LocalPlayer;
            int maxAccIndex = 5 + player.extraAccessorySlots;

            for (int i = 13; i < 13 + maxAccIndex; i++)
            {
                if (player.armor[i].type == Item.type)
                    return false;
            }

            if (CheckModus().accessory != null)
                return true;

            return base.CanRightClick();
        }

        public override void RightClick(Player player)
        {
            var (index, accessory) = CheckModus();

            if (accessory != null)
            {
                Main.LocalPlayer.QuickSpawnItem(player.GetSource_FromThis(), accessory);
                Main.LocalPlayer.armor[index] = Item.Clone();
            }
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int index = CheckModus().index;

                if (index != -1)
                {
                    return slot == index;
                }
            }

            return base.CanEquipAccessory(player, slot, false);
        }

        protected (int index, Item accessory) CheckModus()
        {
            Player player = Main.LocalPlayer;
            int maxAccIndex = 5 + player.extraAccessorySlots;

            for (int i = 3; i < 3 + maxAccIndex; i++)
            {
                Item acc = player.armor[i];

                if (!acc.IsAir && acc.ModItem is DartModus && acc != this.Item)
                {
                    return (i, acc);
                }
            }
            return (-1, null);
        }
    }
}
