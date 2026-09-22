using Terraria;
using Terraria.ModLoader;

namespace PlentyODarts.Prefixes
{
    [LegacyName("PointyWeaponPrefix")]
    public class Pointy : DartWeaponPrefix
    {
        public override float DamageMod => 1.1f;
    }

    [LegacyName("SharpWeaponPrefix")]
    public class Sharp : DartWeaponPrefix
    {
        public override float DamageMod => 1.15f;
    }

    [LegacyName("HastyWeaponPrefix")]
    public class Hasty : DartWeaponPrefix
    {
        public override float ShootSpeedMod => 1.3f;
    }

    [LegacyName("BruteWeaponPrefix")]
    public class Brute : DartWeaponPrefix
    {
        public override float KnockbackMod => 1.3f;
    }

    [LegacyName("FierceWeaponPrefix")]
    public class Fierce : DartWeaponPrefix
    {
        public override int CritMod => 4;
    }

    [LegacyName("MediocreWeaponPrefix")]
    public class Mediocre : DartWeaponPrefix
    {
        public override float DamageMod => 0.7f;
        public override float KnockbackMod => 0.7f;
        public override float ShootSpeedMod => 1.2f;
        public override float UseTimeMod => 0.7f;
    }

    [LegacyName("ComplexWeaponPrefix")]
    public class Complex : DartWeaponPrefix
    {
        public override float DamageMod => 1.2f;
        public override float UseTimeMod => 0.82f;
        public override float ShootSpeedMod => 0.93f;
    }

    [LegacyName("DisastrousWeaponPrefix")]
    public class Disastrous : DartWeaponPrefix
    {
        public override float DamageMod => 0.4f;
        public override float KnockbackMod => 0.4f;
        public override float ShootSpeedMod => 0.4f;
        public override float UseTimeMod => 1.4f;
    }

    [LegacyName("RebelliousWeaponPrefix")]
    public class Rebellious : DartWeaponPrefix
    {
        public override float KnockbackMod => 1.5f;
        public override float DamageMod => 1.3f;
        public override int CritMod => 7;
    }

    [LegacyName("UnbalancedWeaponPrefix")]
    public class Unbalanced : DartWeaponPrefix
    {
        public override float UseTimeMod => 0.85f;
        public override float ShootSpeedMod => 1.1f;
    }

    [LegacyName("MaddenedWeaponPrefix")]
    public class Maddened : DartWeaponPrefix
    {
        public override float DamageMod => 1.25f;
        public override float KnockbackMod => 1.8f;
        public override int CritMod => 5;
    }

    [LegacyName("AllegoricWeaponPrefix")]
    public class Allegoric : DartWeaponPrefix
    {
        public override float DamageMod => 0.85f;
        public override float KnockbackMod => 0.7f;
        public override float ShootSpeedMod => 0.8f;
    }

    [LegacyName("QuintessentialWeaponPrefix")]
    public class Quintessential : DartWeaponPrefix
    {
        public override float DamageMod => 1.5f;
        public override float KnockbackMod => 1.5f;
        public override float ShootSpeedMod => 1.5f;
        public override float UseTimeMod => 0.5f;
        public override int CritMod => 10;
    }

    public class DartWeaponPrefix : ModPrefix
    {
        //stats
        public virtual float DamageMod => 1f;
        public virtual float UseTimeMod => 1f;
        public virtual float ShootSpeedMod => 1f;
        public virtual float KnockbackMod => 1f;
        public virtual int CritMod => 1;

        public override PrefixCategory Category => PrefixCategory.AnyWeapon;

        public override bool CanRoll(Item item) =>
            item.CountsAsClass<ThrowingDamageClass>()
            && (item.maxStack == 1 || item.AllowReforgeForStackableItem)
            && GetType() != typeof(DartWeaponPrefix);

        //Apply
        public override void SetStats(
            ref float damageMult,
            ref float knockbackMult,
            ref float useTimeMult,
            ref float scaleMult,
            ref float shootSpeedMult,
            ref float manaMult,
            ref int critBonus
        )
        {
            damageMult = DamageMod;
            useTimeMult = UseTimeMod;
            shootSpeedMult = ShootSpeedMod;
            knockbackMult = KnockbackMod;
            critBonus = CritMod;
        }
    }
}
