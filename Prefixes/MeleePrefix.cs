using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Prefixes
{

    public class MeleePrefix : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Melee;
        public virtual float damage => 0;
        public virtual float knockback => 0;
        public virtual float speed => 0;
        public virtual float size => 0;
        public virtual int crit => 0;
        public override bool CanRoll(Item item)
        {
            return false;
        }
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 1f + damage;
            knockbackMult = 1f + knockback;
            useTimeMult = 1f - speed;
            scaleMult = 1f + size;
            critBonus = crit;
        }
    }
    public class ImpracticallyOversizedPrefix : MeleePrefix
    {
        public override float damage => 0.15f;
        public override float speed => -0.5f;
        public override float knockback => -0.3f;
        public override float size => -0.5f;
        public override bool CanRoll(Item item)
        {
            return item.damage > 0 && !(item.pick > 0 || item.hammer > 0 || item.axe > 0);
        }
    }
    public class MiniaturePrefix : MeleePrefix
    {
        public override float damage => -0.2f;
        public override float speed => 0.333f;
        public override float knockback => 0.3f;
        public override float size => 1f;
        public override bool CanRoll(Item item)
        {
            return item.damage > 1 && item.useTime > 4;
        }
    }
    /* 1.3 code
	public class MeleePrefix : ModPrefix
	{
		private float damage = 0;
        private float speed = 0;
        private byte crit = 0;
        private float size = 0;
        private float knockback = 0;

        public override PrefixCategory Category { get { return PrefixCategory.Melee; } }
        public MeleePrefix()
		{
		}
        public MeleePrefix(float damage, float speed, byte crit, float size, float knockback)
		{
            this.damage = damage;
            this.speed = speed;
            this.crit = crit;
            this.size = size;
            this.knockback = knockback;
		}
        public override bool IsLoadingEnabled(Mod mod)
        {
            if (base.IsLoadingEnabled(ref name))
            {
                Mod.AddPrefix("Impractically Oversized", new MeleePrefix(1.15f, 0.666f, 0, 2f, 1.3f));
                Mod.AddPrefix("Miniature", new MeleePrefix(0.8f, 1.5f, 0, 0.5f, 0.7f));
            }
            return false;
        }
        
        public override float RollChance(Item item)
        {
            return 1f;
        }

        public override bool CanRoll(Item item)
        {
            if (speed < 1f && (item.pick > 0 || item.hammer > 0 || item.axe > 0))
            {
                return false;
            }
            return (item.damage > 1 || damage >= 1f) && (item.useTime > 4 || speed < 1f);
        }
        public override void Apply(Item item)
		{
            item.damage = (int)(item.damage * damage);
            item.useTime = (int)(item.useTime * (1 / speed));
            item.useAnimation = (int)(item.useAnimation * (1 / speed));
            item.reuseDelay = (int)(item.reuseDelay * (1 / speed));
            item.crit += crit;
            item.scale *= size;
            item.knockback *= knockback;
        }

        public override void ModifyValue(ref float valueMult)
		{
            valueMult = damage * speed * (1 + crit * 0.01f) * knockback;
		}
	}
    */
}