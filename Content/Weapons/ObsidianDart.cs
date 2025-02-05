using PlentyODarts.Content.Projectiles;
using PlentyODarts.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class ObsidianDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Obsidian Dart");
        }

        public override void SetDefaults()
        {
            Item.height = 40;
            Item.width = 40;

            Item.damage = 17;
            Item.DamageType = DartDamage.Instance;
            Item.knockBack = 6;
            Item.crit = 5;

            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = ItemRarityID.Blue;

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<ObsidianDartProj>();
            Item.shootSpeed = 10;
            Item.maxStack = 999;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += player.CountItem(ItemID.Obsidian) / 1000;
        }

        public override void AddRecipes()
        {
            CreateRecipe(25)
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Obsidian, 5)
                .Register();
        }
    }
}
