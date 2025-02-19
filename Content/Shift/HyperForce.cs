using Terraria;
using Terraria.ID;

namespace PlentyODarts.Content.Shift
{
    public class HyperForce : DartShift
    {
        public override bool Shift(Projectile projectile, Player player)
        {
            projectile.velocity *= .5f;
            projectile.damage = player.HeldItem.damage * 2;
            return true;
        }

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
            player.GetModPlayer<PoDPlayer>().SetShift(new HyperForce());
        }
    }
}
