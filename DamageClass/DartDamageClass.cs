using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts
{
    public class DartDamage : DamageClass
    {
        internal static DartDamage Instance;

        public override void Load() => Instance = this;

        public override void Unload() => Instance = null;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Throwing || damageClass == Generic)
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass) =>
            damageClass == Throwing;
    }
}
