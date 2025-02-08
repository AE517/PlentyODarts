using PlentyODarts.Content.Projectiles;
using PlentyODarts.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class Marnum : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Marnum");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.damage = 20;
            Item.DamageType = DartDamage.Instance;
            Item.knockBack = 7;
            Item.crit = 10;

            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.rare = ItemRarityID.Green;

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            Item.consumable = false;
            Item.shoot = ModContent.ProjectileType<MarnumProj>();
            Item.shootSpeed = 10;
            Item.maxStack = 1;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += player.CountItem(ItemID.PlatinumOre) / 1000;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Marble, 25)
                .AddIngredient(ItemID.PlatinumBar)
                .Register();
        }
    }
}
