using Terraria.ModLoader;

namespace JoostMod.DamageClasses
{
    public class MeleeThrowingHybrid : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("melee and throwing damage");
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic || damageClass == Throwing || damageClass == Melee)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return (damageClass == Melee || damageClass == Throwing);
        }
    }
}
