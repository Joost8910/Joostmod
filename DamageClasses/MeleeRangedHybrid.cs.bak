using Terraria.ModLoader;

namespace JoostMod.DamageClasses
{
    public class MeleeRangedHybrid : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("melee and ranged damage");
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic || damageClass == Ranged || damageClass == Melee)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return (damageClass == Melee || damageClass == Ranged);
        }
    }
}
