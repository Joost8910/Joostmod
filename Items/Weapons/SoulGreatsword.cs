using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    public class SoulGreatsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Greatsword");
            Tooltip.SetDefault("Attack with a mighty greatsword formed from souls");
        }
		public override void SetDefaults()
		{
			item.damage = 300;
			item.melee = true;
            item.mana = 40;
			item.width = 160;
			item.height = 160;
			item.useTime = 50;
			item.useAnimation = 50;
			item.reuseDelay = 5;
			item.useStyle = 1;
			item.knockBack = 10;
			item.value = 500000;
			item.rare = 8;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("SoulGreatsword");
			item.shootSpeed = 10f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.magicDamage - 1f);
            mult *= player.magicDamageMult;
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.magicCrit;
            crit /= 2;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " melee and magic damage"));
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (Main.rand.NextBool(3))
            {
                switch (rand.Next(12))
                {
                    case 1:
                        return PrefixID.Mystic;
                    case 2:
                        return PrefixID.Adept;
                    case 3:
                        return PrefixID.Masterful;
                    case 4:
                        return PrefixID.Inept;
                    case 5:
                        return PrefixID.Ignorant;
                    case 6:
                        return PrefixID.Deranged;
                    case 7:
                        return PrefixID.Intense;
                    case 8:
                        return PrefixID.Taboo;
                    case 9:
                        return PrefixID.Celestial;
                    case 10:
                        return PrefixID.Furious;
                    case 11:
                        return PrefixID.Manic;
                    default:
                        return PrefixID.Mythical;
                }
            }
            else if (Main.rand.NextBool(2))
            {
                switch (rand.Next(18))
                {
                    case 1:
                        return PrefixID.Large;
                    case 2:
                        return PrefixID.Massive;
                    case 3:
                        return PrefixID.Dangerous;
                    case 4:
                        return PrefixID.Savage;
                    case 5:
                        return PrefixID.Sharp;
                    case 6:
                        return PrefixID.Pointy;
                    case 7:
                        return PrefixID.Tiny;
                    case 8:
                        return PrefixID.Terrible;
                    case 9:
                        return PrefixID.Small;
                    case 10:
                        return PrefixID.Dull;
                    case 11:
                        return PrefixID.Unhappy;
                    case 12:
                        return PrefixID.Bulky;
                    case 13:
                        return PrefixID.Shameful;
                    case 14:
                        return PrefixID.Heavy;
                    case 15:
                        return PrefixID.Light;
                    case 16:
                        return mod.PrefixType("Impractically Oversized");
                    case 17:
                        return mod.PrefixType("Miniature");
                    default:
                        return PrefixID.Legendary;
                }
            }
            return base.ChoosePrefix(rand);
        }
    }
}