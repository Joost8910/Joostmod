using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PufferfishStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pufferfish Staff");
			Tooltip.SetDefault("Summons a floating pufferfish to fight for you");
		}
		public override void SetDefaults()
		{
			item.damage = 12;
			item.summon = true;
			item.mana = 10;
			item.width = 64;
			item.height = 64;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 3;
			item.value = 27000;
			item.rare = 2;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("fishMinion");
			item.shootSpeed = 7f;
			item.buffType = mod.BuffType("fishMinion");
			item.buffTime = 3600;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                mult *= BattleRodsFishingDamage / player.minionDamage;
            }
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit;
            }
        }
        public float BattleRodsFishingDamage
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>(ModLoader.GetMod("UnuBattleRods")).bobberDamage; }
        }
        public int BattleRodsCrit
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>(ModLoader.GetMod("UnuBattleRods")).bobberCrit; }
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
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return player.altFunctionUse != 2;
		}
		
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}

	}
}


