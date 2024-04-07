using Terraria.ModLoader;

namespace JoostMod.DamageClasses
{
    public class MagicMeleeHybrid : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("melee and magic damage");
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic || damageClass == Melee || damageClass == Magic)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return (damageClass == Magic || damageClass == Melee);
        }
    }
}
