using Terraria.ModLoader;

namespace JoostMod.DamageClasses
{
    public class MagicThrowingHybrid : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("throwing and magic damage");
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic || damageClass == Throwing || damageClass == Magic)
            {
                return StatInheritanceData.Full;
            }
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return (damageClass == Magic || damageClass == Throwing);
        }
    }
}
