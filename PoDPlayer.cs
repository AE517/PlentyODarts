using PlentyODarts.Content.Modus;
using PlentyODarts.Content.Shift;
using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts
{
    public class PoDPlayer : ModPlayer
    {
        public IModus CurrentModus { get; protected set; } = new DefaultModus();
        public IShift CurrentShift { get; protected set; } = new DefaultShift();
        public void SetModus(IModus modus)
        {
            CurrentModus = modus ?? new DefaultModus();
        }

        public void SetShift(IShift shift)
        {
            CurrentShift = shift ?? new DefaultShift();
        }

        public override void ResetEffects()
        {
            CurrentModus = new DefaultModus();
            CurrentShift = new DefaultShift();
        }
    }
}
