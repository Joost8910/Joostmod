using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class ScrollofDeath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scroll of Death");
			Tooltip.SetDefault("Summon multiple skulls when below half health\n" + 
			"Increases max number of minions by 2\n" + 
			"Increases summon damage by 15%\n" + 
			"Increases minion knockback");
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.rare = ItemRarityID.Cyan;
			Item.value = 400000;
			Item.accessory = true;
			Item.damage = 125;
			Item.DamageType = DamageClass.Summon;
			Item.knockBack = 5.5f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions += 2;
			player.GetKnockback(DamageClass.Summon).Base += 3f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.GetModPlayer<JoostPlayer>().SkullSigil = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PapyrusScarab)
				.AddIngredient<SigilofSkulls>()
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}