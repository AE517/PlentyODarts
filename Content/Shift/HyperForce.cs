using Terraria;
using Terraria.ID;

namespace PlentyODarts.Content.Shift
{

    public class HyperForceEffect : IShift
    {
        public bool Shift(Projectile projectile, Player player)
        {
            projectile.velocity *= .5f;
            projectile.damage *= 2;
            return true;
        }
    }
    
    public class HyperForce : DartShift
    {
        private static readonly IShift Effect = new HyperForceEffect();

        public override void SetStaticDefaults()
        {
            DisplayName.Format("Shift - Hyperforce");
        }

        public override void SetDefaults()
        {
            Item.height = 26;
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
