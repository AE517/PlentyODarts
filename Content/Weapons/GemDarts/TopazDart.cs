using PlentyODarts.Content.Projectiles.GemDarts;
using PlentyODarts.Content.Tiles.Furniture;
using PlentyODarts.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons.GemDarts
{
    public class TopazDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Topaz Dart");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.damage = 16;
            Item.crit = 4;
            Item.knockBack = 3;
            Item.DamageType = DartDamage.Instance;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 65);

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            // Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<TopazDartProj>();
            Item.shootSpeed = 8;
            // Item.maxStack = 999;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            DamageUtils utils = new();
            int count = player.CountItem(ItemID.Topaz);

            damage = StatModifier.Default with { Flat = utils.GemDartMod(count, 60) };
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Topaz, 5)
                .Register();
        }
    }
}
