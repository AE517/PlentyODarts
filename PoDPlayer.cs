using PlentyODarts.Content.Modus;
using Terraria.ModLoader;

namespace PlentyODarts
{
    public class PoDPlayer : ModPlayer
    {
        public IModus CurrentModus { get; protected set; } = new DefaultModus();
        public ShiftType currentShift = ShiftType.NONE;

        public void SetModus(IModus modus)
        {
            CurrentModus = modus ?? new DefaultModus();
        }

        public override void ResetEffects()
        {
            CurrentModus = new DefaultModus();
            currentShift = ShiftType.NONE;
        }
    }
}
