using PlentyODarts.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class PoisonDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Poisoned Dart");
            Tooltip.Format("It stings...");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1;
            Item.rare = ItemRarityID.Green;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item39;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.DamageType = DartDamage.Instance;
            Item.damage = 20;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 0, 0, 70);
            Item.crit = 5;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.shootSpeed = 15;
            Item.shoot = ModContent.ProjectileType<PoisonDartProj>();
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
            IDart dart = new PoisonDart();
            ModusType m = player.GetModPlayer<PoDPlayer>().currentModus;
            if (m != ModusType.NONE)
            {
                dart.ApplyModus(
                    player.GetModPlayer<PoDPlayer>().currentModus,
                    player,
                    position,
                    velocity
                );
                return false;
            }
            else
                return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(150)
                .AddIngredient(ModContent.ItemType<WoodenDart>(), 50)
                .AddIngredient(ItemID.Stinger)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, .3f, 1f, .3f);
        }
    }
}
