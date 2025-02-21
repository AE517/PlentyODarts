using PlentyODarts.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Accessories
{
    public class BasicPrecisionLens : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Basic Precision Lens");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<DartDamage>() += 0.05f;
            player.GetCritChance<DartDamage>() += 5;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.MarbleBlock, 10)
                .AddIngredient(ItemID.Emerald, 3)
                .Register();
        }
    }
}
