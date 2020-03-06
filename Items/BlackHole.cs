using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class BlackHole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Hole Tome");
            Tooltip.SetDefault("--Cheat Item--\n" + 
                "Creates a controllable black hole that sucks in creatures and annihilates them\n" +
                "Life regenerates and infinite immunity frames while held");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.noMelee = true;
            item.useTime = 5;
            item.useAnimation = 5;
            item.reuseDelay = 5;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 10;
            item.shoot = mod.ProjectileType("BlackHole");
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
        }
        public override void HoldItem(Player player)
        {
            player.immune = true;
            player.immuneNoBlink = true;
            player.immuneTime = 20;
            player.noFallDmg = true;
            player.statLife++;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            damage = 1;
            return true;
        }
    }
}

