using PlentyODarts.Content.Projectiles;
using PlentyODarts.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class Aurpura : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Aurpura");
        }

        public override void SetDefaults()
        {
            Item i = Item;

            i.width = 40;
            i.height = 40;

            i.damage = 20;
            i.DamageType = DartDamage.Instance;
            i.knockBack = 7;
            i.crit = 10;

            i.value = Item.sellPrice(0, 0, 2, 0);
            i.rare = ItemRarityID.Green;

            i.noMelee = true;
            i.noUseGraphic = true;
            i.useAnimation = 17;
            i.useTime = 17;
            i.useStyle = ItemUseStyleID.Swing;
            i.UseSound = SoundID.Item39;

            i.consumable = false;
            i.shoot = ModContent.ProjectileType<AurpuraProj>();
            i.shootSpeed = 10;
            i.maxStack = 1;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(player, ref damage);

            damage += player.CountItem(ItemID.GoldOre) / 1000;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Granite, 50)
                .AddIngredient(ItemID.GoldBar)
                .Register();
        }
    }
}
