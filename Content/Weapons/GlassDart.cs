using PlentyODarts.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class GlassDart : ModItem, IDart
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
            i.shootSpeed = 16;
        }

        public override bool Shoot(
            Player player,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 velocity,
            int type,
            int damage,
            float knockback
        )
        {
            IDart dart = new GlassDart();
            dart.ApplyModus(
                player.GetModPlayer<PoDPlayer>().currentModus,
                player,
                position,
                velocity
            );
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(10).AddIngredient(ItemID.Glass).AddTile(TileID.GlassKiln).Register();
        }
    }
}
