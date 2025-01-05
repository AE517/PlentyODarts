using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Shift
{
    public class HyperSpeed : ModItem, IShift
    {
        public int Shift(Projectile projectile, Player player)
        {
            projectile.velocity *= 1.05f;
            projectile.damage = player.HeldItem.damage / 2;
            return 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Shift - Hyperspeed");
        }

        public override void SetDefaults()
        {
            Item.height = 36;
            Item.width = 36;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PoDPlayer>().currentShift = ShiftType.HYPERSPEED;
        }
    }
}
