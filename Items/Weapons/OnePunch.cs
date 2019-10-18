using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOff)]
    public class OnePunch : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Ultimate Fist");
			Tooltip.SetDefault("--Cheat Item--\n" + 
			"Charges the ultimate fist attack\n" + 
            "Full charge one-shots nearly anything\n" + 
            "Life regenerates and infinite immunity frames while held");
		}
		public override void SetDefaults()
		{
			item.damage = 1;
			item.melee = true;
			item.width = 28;
			item.height = 28;
			item.useTime = 6;
			item.useAnimation = 6;
            item.useStyle = 3;
            item.channel = true;
			item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
			item.knockBack = 50;
			item.value = 0;
			item.rare = 10;
			item.UseSound = SoundID.Item7;
			item.autoReuse = true;
			item.useTurn = true;
            item.shoot = mod.ProjectileType("OnePunch");
            item.shootSpeed = 10;
		}
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color(255, 0, 0);
				}
            }
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(mod, "Damage", "Infinite damage"));
        }
		public override void HoldItem(Player player)
		{
			player.immune = true;
			player.immuneNoBlink = true;
			player.immuneTime = 20;
            player.noFallDmg = true;
			player.GetModPlayer<JoostPlayer>().SaitamaOwn = true;
			player.statLife++;
		}
        /*public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			damage += target.life + target.defense;
			Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 100);
		}*/
    }
}

