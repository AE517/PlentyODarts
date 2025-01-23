using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PlentyODarts.Prefixes
{
    [LegacyName("PointyWeaponPrefix")]
    public class Pointy : DartWeaponPrefix
    {
        public override float damageMod => 1.1f;
    }

    [LegacyName("SharpWeaponPrefix")]
    public class Sharp : DartWeaponPrefix
    {
        public override float damageMod => 1.15f;
    }

    [LegacyName("HastyWeaponPrefix")]
    public class Hasty : DartWeaponPrefix
    {
        public override float shootSpeedMod => 1.3f;
    }

    [LegacyName("BruteWeaponPrefix")]
    public class Brute : DartWeaponPrefix
    {
        public override float knockbackMod => 1.3f;
    }

    [LegacyName("FierceWeaponPrefix")]
    public class Fierce : DartWeaponPrefix
    {
        public override int critMod => 4;
    }

    [LegacyName("MediocreWeaponPrefix")]
    public class Mediocre : DartWeaponPrefix
    {
        public override float damageMod => 0.7f;
        public override float knockbackMod => 0.7f;
        public override float shootSpeedMod => 1.2f;
        public override float useTimeMod => 0.7f;
    }

    [LegacyName("ComplexWeaponPrefix")]
    public class Complex : DartWeaponPrefix
    {
        public override float damageMod => 1.2f;
        public override float useTimeMod => 0.82f;
        public override float shootSpeedMod => 0.93f;
    }

    [LegacyName("DisastrousWeaponPrefix")]
    public class Disastrous : DartWeaponPrefix
    {
        public override float damageMod => 0.4f;
        public override float knockbackMod => 0.4f;
        public override float shootSpeedMod => 0.4f;
        public override float useTimeMod => 1.4f;
    }

    [LegacyName("RebelliousWeaponPrefix")]
    public class Rebellious : DartWeaponPrefix
    {
        public override float knockbackMod => 1.5f;
        public override float damageMod => 1.3f;
        public override int critMod => 7;
    }

    [LegacyName("UnbalancedWeaponPrefix")]
    public class Unbalanced : DartWeaponPrefix
    {
        public override float useTimeMod => 0.85f;
        public override float shootSpeedMod => 1.1f;
    }

    [LegacyName("MaddenedWeaponPrefix")]
    public class Maddened : DartWeaponPrefix
    {
        public override float damageMod => 1.25f;
        public override float knockbackMod => 1.8f;
        public override int critMod => 5;
    }

    [LegacyName("AlegoricWeaponPrefix")]
    public class Alegoric : DartWeaponPrefix
    {
        public override float damageMod => 0.85f;
        public override float knockbackMod => 0.7f;
        public override float shootSpeedMod => 0.8f;
    }

    [LegacyName("QuintessentialWeaponPrefix")]
    public class Quintessential : DartWeaponPrefix
    {
        public override float damageMod => 1.5f;
        public override float knockbackMod => 1.5f;
        public override float shootSpeedMod => 1.5f;
        public override float useTimeMod => 0.5f;
        public override int critMod => 10;
    }

    public class DartWeaponPrefix : ModPrefix
    {
        //stats
        public virtual float damageMod => 1f;
        public virtual float useTimeMod => 1f;
        public virtual float shootSpeedMod => 1f;
        public virtual float knockbackMod => 1f;
        public virtual int critMod => 1;

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
            damageMult = this.damageMod;
            useTimeMult = this.useTimeMod;
            shootSpeedMult = this.shootSpeedMod;
            knockbackMult = this.knockbackMod;
            critBonus = this.critMod;
        }
    }
}
