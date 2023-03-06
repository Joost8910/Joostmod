using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Prefixes
{
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
}