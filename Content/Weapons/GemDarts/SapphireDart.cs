using PlentyODarts.Content.Projectiles.GemDarts;
using PlentyODarts.Content.Tiles.Furniture;
using PlentyODarts.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons.GemDarts
{
    public class SapphireDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Sapphire Dart");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.damage = 18;
            Item.crit = 4;
            Item.knockBack = 3;
            Item.DamageType = DartDamage.Instance;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            // Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<SapphireDartProj>();
            Item.shootSpeed = 10;
            // Item.maxStack = 999;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            DamageUtils utils = new();
            int count = player.CountItem(ItemID.Sapphire);

            damage = StatModifier.Default with { Flat = utils.GemDartMod(count, 70) };
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Sapphire, 5)
                .Register();
        }
    }
}
