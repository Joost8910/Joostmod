using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod.Prefixes
{
    public class UniversalPrefix : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.AnyWeapon;
        public virtual float damage => 0;
        public virtual float knockback => 0;
        public virtual float speed => 0;
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
            critBonus = crit;
        }
    }
    public class DecisivePrefix : UniversalPrefix
    {
        public override float knockback => -0.1f;
        public override int crit => 18;
        public override bool CanRoll(Item item)
        {
            return item.damage > 0;
        }
    }
    /* 1.3 code
        public override PrefixCategory Category => PrefixCategory.AnyWeapon;

        //Changing Decisive from 20% crit to 18% and reduced knockback by 10%, so it is more like Ruthless's design
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            critBonus = 18;
            knockbackMult = 0.9f;
        }

		private float damage = 0;
        private float speed = 0;
        private byte crit = 0;
        private float knockback = 0;

        public override float RollChance(Item item)
        {
            return 1f;
        }
        public override bool CanRoll(Item item)
        {
            return item.damage > 1;
        }
    */
    /*
    public class UniversalPrefix : ModPrefix
	{
        public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }
        public UniversalPrefix()
		{
		}

		public UniversalPrefix(float damage, float speed, byte crit, float knockback)
		{
            this.damage = damage;
            this.speed = speed;
            this.crit = crit;
            this.knockback = knockback;
		}

		public override bool IsLoadingEnabled(Mod mod)
		{
			if (base.IsLoadingEnabled(ref name))
			{
				Mod.AddPrefix("Decisive", new UniversalPrefix(1f, 1f, 20, 1f));
			}
			return false;
		}

		public override void Apply(Item item)
		{
            item.damage = (int)(item.damage * damage);
            item.useTime = (int)(item.useTime * (1 / speed));
            item.useAnimation = (int)(item.useAnimation * (1 / speed));
            item.reuseDelay = (int)(item.reuseDelay * (1 / speed));
            item.crit += crit;
            item.knockBack *= knockback;
        }

		public override void ModifyValue(ref float valueMult)
		{
            valueMult = damage * speed * (1 + crit * 0.01f) * knockback;
		}
    }
    */
}