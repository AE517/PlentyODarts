using Terraria;
using Terraria.ID;

namespace PlentyODarts.Content.Shift
{

    public class HyperSpeedEffect : IShift
    {
        public bool Shift(Projectile projectile, Player player)
        {
            projectile.velocity *= 1.2f;
            projectile.damage /= 2;
            return true;
        }
    }
    
    public class HyperSpeed : DartShift
    {

        private static readonly IShift Effect = new HyperSpeedEffect();
        public override void SetStaticDefaults()
        {
            DisplayName.Format("Shift - Hyperspeed");
        }

        public override void SetDefaults()
        {
            Item.height = 30;
            Item.width = 22;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PoDPlayer>().SetShift(Effect);
        }
    }
}
