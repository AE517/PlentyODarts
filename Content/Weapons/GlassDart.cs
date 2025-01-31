using PlentyODarts.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class GlassDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Glass Dart");
            Tooltip.Format("Why?");
        }

        public override void SetDefaults()
        {
            Item i = Item;

            i.damage = 10;
            i.DamageType = DartDamage.Instance;
            i.crit = 10;
            i.knockBack = 2;
            i.noMelee = true;

            i.width = 29;
            i.height = 64;
            i.scale = 1;

            i.useStyle = ItemUseStyleID.Swing;
            i.useTime = 15;
            i.useAnimation = 15;
            i.UseSound = SoundID.Item39;
            i.noUseGraphic = true;

            i.consumable = true;
            i.maxStack = 999;
            i.shoot = ModContent.ProjectileType<GlassDartProj>();
            i.shootSpeed = 13;
        }

        public override void AddRecipes()
        {
            CreateRecipe(10).AddIngredient(ItemID.Glass).AddTile(TileID.GlassKiln).Register();
        }
    }
}
