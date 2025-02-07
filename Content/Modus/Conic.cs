using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Content.Modus
{
    public class Conic : DartModus
    {
        public override int Modus(Player player, Vector2 position, Vector2 velocity)
        {
            for (int i = -1; i <= 1; i++)
            {
                int p = ModusProjectile(
                    player,
                    position,
                    velocity.RotatedBy(i * 0.2f),
                    player.HeldItem.shoot,
                    player.HeldItem.damage,
                    player.HeldItem.knockBack
                );
            }
            return 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Modus - Conic");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PoDPlayer>().SetModus(new Conic());
        }
    }
}
