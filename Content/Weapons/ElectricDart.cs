using PlentyODarts.Content.Projectiles;
using PlentyODarts.Content.Tiles.Furniture;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class ElectricDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 18));
            ItemID.Sets.AnimatesAsSoul[Type] = true;

            DisplayName.Format("Electric Dart");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;

            Item.damage = 18;
            Item.DamageType = DartDamage.Instance;
            Item.knockBack = 2;
            Item.crit = 7;

            Item.value = Item.sellPrice(0, 0, 0, 75);
            Item.rare = ItemRarityID.Blue;

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<ElectricDartProj>();
            Item.shootSpeed = 10;
            Item.maxStack = 999;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(player, ref damage);
            damage += Main.raining ? .5f : 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe(50)
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.GoldBar, 1)
                .AddIngredient(ItemID.SunplateBlock, 5)
                .Register();

            CreateRecipe(50)
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.PlatinumBar, 1)
                .AddIngredient(ItemID.SunplateBlock, 5)
                .Register();
        }
    }
}
