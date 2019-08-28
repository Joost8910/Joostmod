using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
	public class SoulSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Spear");
			Tooltip.SetDefault("Fire a piercing soul spear\n" + "Goes through blocks for a short distance");
		}
		public override void SetDefaults()
		{
			item.damage = 250;
			item.thrown = true;
			item.mana = 20;
			item.width = 36;
			item.height = 36;
			item.useTime = 20;
			item.useAnimation = 40;
			item.useStyle = 4;
			item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 6.5f;
			item.value = 500000;
			item.rare = 8;
			item.autoReuse = true;
			item.UseSound = SoundID.Item8;
			item.shoot = mod.ProjectileType("SoulSpear");
			item.shootSpeed = 15f;
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
            list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " throwing and magic damage"));
        }
        public override void HoldItem(Player player)
        {
            if (player.itemAnimation > 1 && player.itemAnimation > item.useTime)
            {
                int dustType = 92;
                Vector2 pos = player.MountedCenter + (new Vector2((6 * player.direction) - 9, -16));
                int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.noGravity = true;
            }
            base.HoldItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.itemAnimation > item.useTime)
            {
                Main.PlaySound(42, player.Center, 203);
                return false;
            }
            Main.PlaySound(2, player.Center, 28);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
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
                switch (rand.Next(12))
                {
                    case 1:
                        return PrefixID.Rapid;
                    case 2:
                        return PrefixID.Hasty;
                    case 3:
                        return PrefixID.Intimidating;
                    case 4:
                        return PrefixID.Deadly2;
                    case 5:
                        return PrefixID.Staunch;
                    case 6:
                        return PrefixID.Awful;
                    case 7:
                        return PrefixID.Lethargic;
                    case 8:
                        return PrefixID.Awkward;
                    case 9:
                        return PrefixID.Powerful;
                    case 10:
                        return PrefixID.Frenzying;
                    case 11:
                        return PrefixID.Sighted;
                    default:
                        return PrefixID.Unreal;
                }
            }
            return base.ChoosePrefix(rand);
        }
    }
}


