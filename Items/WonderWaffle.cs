using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items
{
    public class WonderWaffle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wonder Waffle");
            Tooltip.SetDefault("'The amazing Wonder Waffle!'\n" +
            "'The special secret ingredient makes this the most delicious waffle found anywhere!'\n" +
            "'Wonder Waffle is a registered trademark of Travellers Enterprise'\n" + 
            "Grants a random status effect\n" + 
            "DISCLAIMER: Some buffs do not function without certain requirements, so the Wonder Waffle may appear to do nothing");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 42;
            item.height = 42;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 2;
            item.knockBack = 5;
            item.value = 1500;
            item.rare = 3;
            item.noMelee = true;
            item.UseSound = SoundID.Item2;
            item.consumable = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(174 + (int)(Main.DiscoR * 0.315f), 102 + (int)(Main.DiscoR * 0.375f), (int)(Main.DiscoR * 0.467f));
                }
            }
        }
        public override bool UseItem(Player player)
        {
            int buffCount = BuffLoader.BuffCount;
            int buffID = Main.rand.Next(buffCount);
            int length = 1800 + Main.rand.Next(1800);
            /*if (Main.debuff[buffID])
            {
                length = 600 + Main.rand.Next(600);
            }*/
            while (buffID == BuffID.DrillMount && !NPC.downedGolemBoss)
            {
                Main.NewText("You can't get the drill containment unit from the Wonder Waffle until after beating the Golem", Color.OrangeRed);
                buffID = Main.rand.Next(buffCount);
            }
            while (ModContent.GetModBuff(buffID).Name == "PrimordiaCurse")
            {
                Main.NewText("Prevented " + Lang.GetBuffName(buffID) + " from crashing the game", Color.OrangeRed);
                buffID = Main.rand.Next(buffCount);
            }
            player.AddBuff(buffID, length, false);
            return true;
        }
    }
}

