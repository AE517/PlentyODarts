using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Modus
{
    public class Split : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Modus - Split");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 29;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PoDPlayer>().currentModus = ModusType.SPLIT;
        }
    }
}
