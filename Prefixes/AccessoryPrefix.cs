using JoostMod.Items;
using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Prefixes
{
    public class AccessoryPrefix : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public virtual byte meleeDamage => 0;
        public virtual byte thrownDamage => 0;
        public virtual byte rangedDamage => 0;
        public virtual byte magicDamage => 0;
        public virtual byte summonDamage => 0;
        public virtual byte maxHealth => 0;
        public virtual byte lifeRegen => 0;
        public virtual byte fishingPower => 0;
        public override bool CanRoll(Item item)
        {
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
    public class GrognakPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grognak's");
        }
        public override byte meleeDamage => 6;
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    public class GnundersonPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gnunderson's");
        }
        public override byte thrownDamage => 6;
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    public class BoookPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boook's");
        }
        public override byte rangedDamage => 6;
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    public class DavidPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("David's");
        }
        public override byte magicDamage => 6;
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    public class LarkusPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Larkus's");
        }
        public override byte summonDamage => 6; 
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    public class UncleCariusPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uncle Carius's");
        }
        public override byte fishingPower => 5;
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    public class HeartyPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hearty");
        }
        public override byte maxHealth => 25;
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    public class RejuvenatingPrefix : AccessoryPrefix
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rejuvenating");
        }
        public override byte lifeRegen => 1;
        public override bool CanRoll(Item item)
        {
            return true;
        }
    }
    /* 1.3 code
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
    */
}