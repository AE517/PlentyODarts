using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using PlentyODarts.Utils;

namespace PlentyODarts.Content.Modus
{
    public class SplitBehavior : IModus
    {
        public int Modus(Player player, Vector2 position, Vector2 velocity)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                _ = DartUtils.SpawnModusProjectile(
                    player,
                    position,
                    velocity * i,
                    player.HeldItem.shoot,
                    player.HeldItem.damage,
                    player.HeldItem.knockBack
                );
            }

            return 0;
        }
    }
public class Split : DartModus
{

    private static readonly IModus Behavior = new SplitBehavior();
    public override void SetStaticDefaults()
        {
            DisplayName.Format("Modus - Split");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 12;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PoDPlayer>().SetModus(Behavior);
        }
    }
}
