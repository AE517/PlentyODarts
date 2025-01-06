using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Placeable.Furniture
{
    public class DartStationBasic : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.DartStationBasic>());
            Item.width = 48;
            Item.height = 47;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
    }
}
