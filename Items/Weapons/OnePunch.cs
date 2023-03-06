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
			Item.damage = 1;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 28;
			Item.height = 28;
			Item.useTime = 6;
			Item.useAnimation = 6;
			Item.useStyle = 3;
			Item.channel = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.knockBack = 50;
			Item.value = 0;
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item7;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.shoot = Mod.Find<ModProjectile>("OnePunch").Type;
			Item.shootSpeed = 10;
		}
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.OverrideColor = new Color(255, 0, 0);
				}
			}
			int dmg = list.FindIndex(x => x.Name == "Damage");
			list.RemoveAt(dmg);
			list.Insert(dmg, new TooltipLine(Mod, "Damage", "Infinite damage"));
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

