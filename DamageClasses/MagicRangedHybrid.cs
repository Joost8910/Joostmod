using Terraria.ModLoader;

namespace JoostMod.DamageClasses
{
    public class MagicRangedHybrid : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("ranged and magic damage");
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic || damageClass == Ranged || damageClass == Magic)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return (damageClass == Magic || damageClass == Ranged);
        }
    }
}
