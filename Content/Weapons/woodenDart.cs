using Microsoft.Xna.Framework;
using PlentyODarts.Content.Modus;
using PlentyODarts.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class WoodenDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Wooden Dart");
            Tooltip.Format("Good and reliable");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.consumable = true;
            Item.knockBack = 5f;
            Item.damage = 10;
            Item.crit = 4;
            Item.value = Item.buyPrice(0, 0, 0, 10);
            Item.UseSound = SoundID.Item39;
            Item.rare = ItemRarityID.White;
            Item.maxStack = 999;
            Item.shoot = ModContent.ProjectileType<WoodenDartProj>();
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DartDamage.Instance;
            Item.shootSpeed = 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddRecipeGroup(RecipeGroupID.Wood, 5)
                .AddRecipeGroup(RecipeGroupID.IronBar, 1)
                .Register();
        }
    }
}
