using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Shift
{
    public abstract class DartShift : ModItem, IShift
    {
        public abstract bool Shift(Projectile projectile, Player player);

        public override bool CanRightClick()
        {
            Player player = Main.LocalPlayer;

            int maxAccIndex = 5 + player.extraAccessorySlots;

            for (int i = 13; i < 13 + maxAccIndex; i++)
            {
                if (player.armor[i].type == Item.type)
                    return false;
            }

            if (CheckShift().accessory != null)
                return true;

            return base.CanRightClick();
        }

        public override void RightClick(Player player)
        {
            var (index, accessory) = CheckShift();

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
                int index = CheckShift().index;

                if (index != -1)
                    return slot == index;
            }

            return base.CanEquipAccessory(player, slot, false);
        }

        protected (int index, Item accessory) CheckShift()
        {
            Player player = Main.LocalPlayer;

            int maxAccIndex = 5 + player.extraAccessorySlots;

            for (int i = 3; i < 3 + maxAccIndex; i++)
            {
                Item acc = player.armor[i];

                if (!acc.IsAir && acc.ModItem is DartShift && acc != this.Item)
                    return (i, acc);
            }

            return (-1, null);
        }
    }
}
