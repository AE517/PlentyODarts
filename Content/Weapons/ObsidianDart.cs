using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlentyODarts.Content.Projectiles;
using PlentyODarts.Content.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Weapons
{
    public class ObsidianDart : DartWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Obsidian Dart");
        }

        public override void SetDefaults()
        {
            Item.height = 40;
            Item.width = 40;

            Item.damage = 17;
            Item.DamageType = DartDamage.Instance;
            Item.knockBack = 6;
            Item.crit = 5;

            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = ItemRarityID.Blue;

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item39;

            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<ObsidianDartProj>();
            Item.shootSpeed = 12;
            Item.maxStack = 999;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += player.CountItem(ItemID.Obsidian) / 1000;
        }

        public override void PostDrawInWorld(
            SpriteBatch spriteBatch,
            Color lightColor,
            Color alphaColor,
            float rotation,
            float scale,
            int whoAmI
        )
        {
            Player player = Main.LocalPlayer;

            Texture2D texture = ModContent
                .Request<Texture2D>(
                    "PlentyODarts/Content/Weapons/ObsidianDart_Glow",
                    ReLogic.Content.AssetRequestMode.ImmediateLoad
                )
                .Value;

            if (player.HasItem(ItemID.LavaBucket))
            {
                Vector2 pos = new Vector2(
                    Item.Center.X - Main.screenPosition.X,
                    Item.Center.Y - Main.screenPosition.Y
                );

                spriteBatch.Draw(
                    texture,
                    pos,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    new Color(1f, 1f, 1f, .5f),
                    rotation,
                    texture.Size() * .5f,
                    1f,
                    SpriteEffects.None,
                    1f
                );

                Lighting.AddLight(Item.position, Color.OrangeRed.ToVector3());
            }
        }

        public override void PostDrawInInventory(
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Color drawColor,
            Microsoft.Xna.Framework.Color itemColor,
            Microsoft.Xna.Framework.Vector2 origin,
            float scale
        )
        {
            Player player = Main.LocalPlayer;

            Texture2D texture = ModContent
                .Request<Texture2D>(
                    "PlentyODarts/Content/Weapons/ObsidianDartInv_Glow",
                    ReLogic.Content.AssetRequestMode.ImmediateLoad
                )
                .Value;

            if (player.HasItem(ItemID.LavaBucket))
            {
                spriteBatch.Draw(
                    texture,
                    position,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    new Color(1f, 1f, 1f, .5f),
                    0f,
                    texture.Size() * .5f,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(25)
                .AddTile(ModContent.TileType<DartStationBasic>())
                .AddIngredient(ItemID.Obsidian, 5)
                .Register();
        }
    }
}
