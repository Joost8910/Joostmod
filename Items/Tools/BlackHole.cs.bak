using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools
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
            Item.width = 28;
            Item.height = 30;
            Item.noMelee = true;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.reuseDelay = 5;
            Item.autoReuse = true;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 0;
            Item.rare = ItemRarityID.Red;
            Item.shoot = ModContent.ProjectileType<Projectiles.BlackHole>();
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
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage = 1;
        }
    }
}

