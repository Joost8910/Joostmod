using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools
{
    public class CheatManipulationTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Greater Manipulation");
            Tooltip.SetDefault("--Cheat Item--\n" + 
                "Allows you to pick up and move NPCs\n" +
                "Right click while holding the NPC to rapidly damage the NPC\n" +
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
            Item.shoot = Mod.Find<ModProjectile>("CheatManipulation").Type;
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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            damage = 25;
            return true;
        }
    }
}

