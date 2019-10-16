using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class BrokenExcalipoor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Excalipoor");
            //Tooltip.SetDefault("Easily repairable");
            Tooltip.SetDefault("Demand a rematch with Gilgamesh and Enkidu");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 50;
            item.height = 48;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.value = 5000;
            item.rare = -1;
            item.UseSound = SoundID.Item1;
            item.consumable = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(106, 63, 202);
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2")) && !NPC.AnyNPCs(mod.NPCType("Enkidu"));
        }
        public override bool UseItem(Player player)
        {
            //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Gilgamesh2"));
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)player.Center.X - 1500, (int)player.Center.Y - 200, mod.NPCType("Gilgamesh2"), 0, 610, 0, 0, 1);
            }
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Enkidu"));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            
            Main.NewText("<Gilgamesh> Oho, so you want a rematch?", 225, 25, 25);
            Main.NewText("Then let us skip phase 1 and jump right into the real fight!", 225, 25, 25);
            Main.NewText("<Enkidu> You better hope you're good at dodging", 25, 225, 25);
            
            return true;
        }
    }
}

