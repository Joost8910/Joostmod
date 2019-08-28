using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Prefixes
{
	public class UniversalPrefix : ModPrefix
	{
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

		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				mod.AddPrefix("Decisive", new UniversalPrefix(1f, 1f, 20, 1f));
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
}