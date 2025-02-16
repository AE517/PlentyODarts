using PlentyODarts.Content.Projectiles.GemDarts;
using PlentyODarts.Content.Tiles.Furniture;
using PlentyODarts.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons.GemDarts
{
    public class AmberDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Amber Dart");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.damage = 21;
            Item.crit = 4;
            Item.knockBack = 3;
            Item.DamageType = DartDamage.Instance;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 0, 95);

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            // Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<AmberDartProj>();
            Item.shootSpeed = 13;
            // Item.maxStack = 999;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            DamageUtils utils = new DamageUtils();
            int count = player.CountItem(ItemID.Amber);

            damage = StatModifier.Default with { Flat = utils.GemDartMod(count, 90) };
        }

        public override void AddRecipes()
        {
            CreateRecipe(15)
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Amber, 1)
                .AddIngredient(ItemID.FossilOre, 1)
                .Register();
        }
    }
}
