using JoostMod.Items;
using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Prefixes
{
	public class AccessoryPrefix : ModPrefix
	{
		private byte meleeDamage = 0;
        private byte thrownDamage = 0;
        private byte rangedDamage = 0;
        private byte magicDamage = 0;
        private byte summonDamage = 0; 
        private byte maxHealth = 0;
        private byte lifeRegen = 0;
        private byte fishingPower = 0;

        public override float RollChance(Item item)
        {
            return 1f;
        }
        public override bool CanRoll(Item item)
        {
            return true;
        }
        public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

        public AccessoryPrefix()
		{
		}

		public AccessoryPrefix(byte meleeDamage, byte thrownDamage, byte rangedDamage, byte magicDamage, byte summonDamage, byte maxHealth, byte lifeRegen, byte fishingPower)
		{
            this.meleeDamage = meleeDamage;
            this.thrownDamage = thrownDamage;
            this.rangedDamage = rangedDamage;
            this.magicDamage = magicDamage;
            this.summonDamage = summonDamage;
            this.maxHealth = maxHealth;
            this.lifeRegen = lifeRegen;
            this.fishingPower = fishingPower;
		}

		public override bool IsLoadingEnabled(Mod mod)
		{
			if (base.IsLoadingEnabled(ref name))
			{
				Mod.AddPrefix("Grognak's", new AccessoryPrefix(6, 0, 0, 0, 0, 0, 0, 0));
                Mod.AddPrefix("Gnunderson's", new AccessoryPrefix(0, 6, 0, 0, 0, 0, 0, 0));
                Mod.AddPrefix("Boook's", new AccessoryPrefix(0, 0, 6, 0, 0, 0, 0, 0));
                Mod.AddPrefix("David's", new AccessoryPrefix(0, 0, 0, 6, 0, 0, 0, 0));
                Mod.AddPrefix("Larkus's", new AccessoryPrefix(0, 0, 0, 0, 6, 0, 0, 0));
                Mod.AddPrefix("Hearty", new AccessoryPrefix(0, 0, 0, 0, 0, 25, 0, 0));
                Mod.AddPrefix("Rejuvenating", new AccessoryPrefix(0, 0, 0, 0, 0, 0, 1, 0));
                Mod.AddPrefix("Uncle Carius's", new AccessoryPrefix(0, 0, 0, 0, 0, 0, 0, 5));
            }
			return false;
		}
        public override void Apply(Item item)
        {
            item.GetGlobalItem<JoostGlobalItem>().meleeDamage = meleeDamage;
            item.GetGlobalItem<JoostGlobalItem>().thrownDamage = thrownDamage;
            item.GetGlobalItem<JoostGlobalItem>().rangedDamage = rangedDamage;
            item.GetGlobalItem<JoostGlobalItem>().magicDamage = magicDamage;
            item.GetGlobalItem<JoostGlobalItem>().summonDamage = summonDamage;
            item.GetGlobalItem<JoostGlobalItem>().maxHealth = maxHealth;
            item.GetGlobalItem<JoostGlobalItem>().lifeRegen = lifeRegen;
            item.GetGlobalItem<JoostGlobalItem>().fishingPower = fishingPower;
        }
        public override void ModifyValue(ref float valueMult)
		{
            float multiplier = 1f * (1 + meleeDamage * 0.034f) * (1 + thrownDamage * 0.034f) * (1 + rangedDamage * 0.034f) * (1 + magicDamage * 0.034f) * (1 + summonDamage * 0.034f) * (1 + maxHealth * 0.008f) * (1 + lifeRegen * 0.2f) * (1 + fishingPower * 0.04f);
			valueMult *= multiplier;
		}
	}
}