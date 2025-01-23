using PlentyODarts.Content.Projectiles;
using PlentyODarts.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class Marnum : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Marnum");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.damage = 25;
            Item.DamageType = DartDamage.Instance;
            Item.knockBack = 7;
            Item.crit = 10;

            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.rare = ItemRarityID.Green;

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            Item.consumable = false;
            Item.shoot = ModContent.ProjectileType<MarnumProj>();
            Item.shootSpeed = 10;
            Item.maxStack = 1;
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
            IDart dart = new Marnum();
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

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += player.CountItem(ItemID.PlatinumOre) / 1000;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Marble, 50)
                .AddIngredient(ItemID.PlatinumBar)
                .Register();
        }
    }
}
