using Microsoft.Xna.Framework;
using PlentyODarts.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class FieryDart : ModItem, IDart
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Fiery Dart");
        }

        public override void SetDefaults()
        {
            Item i = Item;

            i.height = 32;
            i.width = 32;

            i.damage = 15;
            i.crit = 5;
            i.knockBack = 5;
            i.DamageType = DartDamage.Instance;

            i.noMelee = true;
            i.noUseGraphic = true;
            i.useAnimation = 15;
            i.useTime = 15;
            i.useStyle = ItemUseStyleID.Swing;
            i.UseSound = SoundID.Item39;

            i.value = Item.sellPrice(0, 0, 0, 50);
            i.rare = ItemRarityID.Green;

            i.consumable = true;
            i.shoot = ModContent.ProjectileType<FieryDartProj>();
            i.shootSpeed = 15;
            i.maxStack = 999;
        }

        public override bool Shoot(
            Player player,
            EntitySource_ItemUse_WithAmmo source,
            Vector2 position,
            Vector2 velocity,
            int type,
            int damage,
            float knockback
        )
        {
            IDart dart = new FieryDart();
            dart.ApplyModus(
                player.GetModPlayer<PoDPlayer>().currentModus,
                player,
                position,
                velocity
            );

            return false;
        }

        public override void PostUpdate()
        {
            Vector2 pos = new Vector2(Item.Center.X + 20, Item.Center.Y);
            Lighting.AddLight(pos, .9f, .3f, .7f);
        }

        public override void AddRecipes()
        {
            CreateRecipe(150)
                .AddIngredient(ModContent.ItemType<WoodenDart>(), 50)
                .AddIngredient(ItemID.Torch)
                .Register();
        }
    }
}
