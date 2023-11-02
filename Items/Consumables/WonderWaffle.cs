using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Consumables
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
            "Disclaimer: Some buffs do not function without certain requirements, so the Wonder Waffle may appear to do nothing\n" +
            "WARNING: May have unintended disasterous effects when paired with other mods. Proceed at your own risk.");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 2;
            Item.knockBack = 5;
            Item.value = 1997;
            Item.rare = ItemRarityID.Orange;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(174 + (int)(Main.DiscoR * 0.315f), 102 + (int)(Main.DiscoR * 0.375f), (int)(Main.DiscoR * 0.467f));
                }
            }
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
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
                if (Main.netMode != NetmodeID.Server)
                    Main.NewText("You can't get the drill containment unit from the Wonder Waffle until after beating the Golem", Color.OrangeRed);
                buffID = Main.rand.Next(buffCount);
            }
            while (ModContent.GetModBuff(buffID).Name == "PrimordiaCurse")
            {
                Mod.Logger.Info("Wonder Waffle detected " + Lang.GetBuffName(buffID) + ", skipping to prevent crash");
                buffID = Main.rand.Next(buffCount);
            }
            Mod.Logger.Info("Wonder Waffle granted " + Lang.GetBuffName(buffID));
            player.AddBuff(buffID, length, false);
            return true;
        }
    }
}

