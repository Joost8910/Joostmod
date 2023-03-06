using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
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
            Item.maxStack = 999;
            Item.width = 50;
            Item.height = 48;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.value = 5000;
            Item.rare = -1;
            Item.UseSound = SoundID.Item1;
            Item.consumable = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(106, 63, 202);
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Enkidu").Type);
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Gilgamesh2"));
            if (Main.netMode != 1)
            {
                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X - 1500, (int)player.Center.Y - 200, Mod.Find<ModNPC>("Gilgamesh2").Type, 0, 610, 0, 0, 1);
            }
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Enkidu").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            
            Main.NewText("<Gilgamesh> Oho, so you want a rematch?", 225, 25, 25);
            Main.NewText("Then let us skip phase 1 and jump right into the real fight!", 225, 25, 25);
            Main.NewText("<Enkidu> You better hope you're good at dodging", 25, 225, 25);
            
            return null;
        }
    }
}

