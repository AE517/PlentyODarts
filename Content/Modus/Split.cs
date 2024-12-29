using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Modus
{
    public class Split : ModItem, IModus
    {
        public int Modus(Player player, Vector2 position, Vector2 velocity)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    position,
                    velocity * i,
                    player.HeldItem.shoot,
                    player.HeldItem.damage,
                    player.HeldItem.knockBack
                );
            }
            return 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Modus - Split");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 29;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PoDPlayer>().currentModus = ModusType.SPLIT;
        }
    }
}
