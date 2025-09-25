using Terraria.ModLoader;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace PlentyODarts
#pragma warning restore IDE0130 // Namespace does not match folder structure
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
