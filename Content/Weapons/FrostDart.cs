using PlentyODarts.Content.Modus;
using PlentyODarts.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class FrostDart : ModItem, IDart
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Frost Dart");
            Tooltip.Format("Chilly...");
        }

        public override void SetDefaults()
        {
            Item i = Item;

            i.width = 17;
            i.height = 64;

            i.damage = 15;
            i.DamageType = DartDamage.Instance;
            i.knockBack = 3;
            i.crit = 3;

            i.useStyle = ItemUseStyleID.Swing;
            i.useAnimation = 15;
            i.useTime = 15;
            i.noMelee = true;
            i.noUseGraphic = true;
            i.UseSound = SoundID.Item39;

            i.shoot = ModContent.ProjectileType<FrostDartProj>();
            i.shootSpeed = 15;
            i.consumable = true;
            i.maxStack = 999;

            i.rare = ItemRarityID.Green;
            i.value = Item.buyPrice(0, 0, 0, 50);
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
            IDart dart = new FrostDart();
            dart.ApplyModus(
                player.GetModPlayer<PoDPlayer>().currentModus,
                player,
                position,
                velocity
            );
            return false;
        }

        public override void ModifyShootStats(
            Player player,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        )
        {
            if (player.ZoneSnow)
            {
                velocity *= 5f;
                damage += (int)(damage * .5f);
            }

            if (player.ZoneUnderworldHeight)
                damage -= (int)(damage * .5f);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.ZoneSnow)
                damage += 0.5f;
            if (player.ZoneUnderworldHeight)
                damage -= 0.5f;
            if (player.ZoneBeach || player.ZoneDesert)
                damage -= 0.3f;
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (player.ZoneSnow)
                crit = 10;
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
            if (player.ZoneSnow)
                knockback += 2f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(200)
                .AddIngredient(ModContent.ItemType<WoodenDart>(), 50)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddTile(TileID.IceMachine)
                .Register();

            CreateRecipe(100)
                .AddIngredient(ModContent.ItemType<WoodenDart>(), 50)
                .AddIngredient(ItemID.SnowBlock, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
