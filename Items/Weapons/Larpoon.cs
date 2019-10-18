using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace JoostMod.Items.Weapons
{
	public class Larpoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Larpoon");
			Tooltip.SetDefault("Launches a harpoon that shoots lasers at enemies\n" + 
                "Fished in the ocean after a mechanical boss has been defeated\n" + 
                "'Live-Action-Role-Playing Harpoon' - Loki");
		}
		public override void SetDefaults()
		{
			item.damage = 42;
			item.ranged = true;
			item.width = 46;
			item.height = 22;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 6, 5, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Larpoon"); 
			item.shootSpeed = 18f;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                mult *= BattleRodsFishingDamage / player.rangedDamage;
            }
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.rangedCrit;
            }
        }
        public float BattleRodsFishingDamage
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberDamage; }
        }
        public int BattleRodsCrit
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberCrit; }
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                Player player = Main.player[Main.myPlayer];
                int dmg = list.FindIndex(x => x.Name == "Damage");
                list.RemoveAt(dmg);
                list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " Fishing damage"));
            }
        }

        public override bool CanUseItem(Player player)      
        {
			return player.ownedProjectileCounts[mod.ProjectileType("Larpoon")] == 0;
        }
	}
}
