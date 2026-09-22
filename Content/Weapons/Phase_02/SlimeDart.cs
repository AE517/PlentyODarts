using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using PlentyODarts.Content.Projectiles.Phase_02;

namespace PlentyODarts.Content.Weapons.Phase_02;

public class SlimeDart : DartWeapon
{
    public override void SetStaticDefaults()
    {
        DisplayName.Format("Slimy Dart");
    }

    public override void SetDefaults()
    {
        Item i = Item;

        i.width = 40;
        i.height = 40;

        i.damage = 20;
        i.DamageType = DartDamage.Instance;
        i.knockBack = 0.5f;
        i.crit = 5;

        i.value = Item.sellPrice(0, 0, 0, 15);
        i.rare = ItemRarityID.Green;

        i.noMelee = true;
        i.noUseGraphic = true;
        i.useTime = 17;
        i.useAnimation = 17;
        i.useStyle = ItemUseStyleID.Swing;
        i.UseSound = SoundID.Item39;

        i.consumable = true;
        i.shoot = ModContent.ProjectileType<SlimeDartProj>();
        i.shootSpeed = 10;
        i.maxStack = 999;
    }

    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
    {
        if (player.ZoneDesert || player.ZoneUnderworldHeight) damage /= 2;
        if (Main.slimeRain) damage *= 2;
    }

    public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
    {
        if (player.ZoneDesert || player.ZoneUnderworldHeight) knockback *= 2;
    }

    public override void AddRecipes()
    {
        CreateRecipe(10)
            .AddTile(TileID.Solidifier)
            .AddIngredient(ItemID.Gel, 20)
            .AddIngredient(ItemID.CopperBar)
            .Register();
        
        CreateRecipe(10)
            .AddTile(TileID.Solidifier)
            .AddIngredient(ItemID.Gel, 20)
            .AddIngredient(ItemID.TinBar)
            .Register();
            
    }
}