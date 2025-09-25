using PlentyODarts.Content.Projectiles.GemDarts;
using PlentyODarts.Content.Tiles.Furniture;
using PlentyODarts.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons.GemDarts
{
    public class EmeraldDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Emerald Dart");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.damage = 19;
            Item.crit = 4;
            Item.knockBack = 3;
            Item.DamageType = DartDamage.Instance;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 0, 85);

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            // Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<EmeraldDartProj>();
            Item.shootSpeed = 12;
            // Item.maxStack = 999;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            DamageUtils utils = new();
            int count = player.CountItem(ItemID.Emerald);

            damage = StatModifier.Default with { Flat = utils.GemDartMod(count, 80) };
        }

        public override void AddRecipes()
        {
            CreateRecipe(25)
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Emerald, 5)
                .Register();
        }
    }
}
