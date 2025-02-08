using PlentyODarts.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class OceanDart : DartWeapon
    {
        protected override bool IsModusApplicable => false;

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Ocean Dart");
        }

        public override void SetDefaults()
        {
            Item.width = 37;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.consumable = true;
            Item.knockBack = 10f;
            Item.damage = 20;
            Item.crit = 3;
            Item.value = Item.buyPrice(0, 0, 0, 50);
            Item.UseSound = SoundID.Item39;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DartDamage.Instance;
            Item.shoot = ModContent.ProjectileType<OceanDartProj>();
            Item.shootSpeed = 15;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
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
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<OceanDart2Proj>(),
                    damage,
                    knockback,
                    player.whoAmI
                );
                return false;
            }

            // Projectile.NewProjectile(source, position, velocity, type, damage, knockback);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(200)
                .AddIngredient(ModContent.ItemType<WoodenDart>(), 50)
                .AddIngredient(ItemID.Seashell, 1)
                .AddCondition(Condition.NearWater)
                .Register();
        }
    }
}
