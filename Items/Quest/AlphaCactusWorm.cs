using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class AlphaCactusWorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extra Succulent Cactus");
			Tooltip.SetDefault("Not actually a quest item\n" +  
            "If this is in your inventory something went wrong.\n" + 
            "It's meant to do a thing when picked up like hearts\n" + 
            "Consume it for its effect since picking it up didn't work");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 26;
            item.height = 30;
            item.uniqueStack = true;
            item.rare = -11;
            item.consumable = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 4;
            item.noMelee = true; 
            item.UseSound = SoundID.Item1;
        }
        public override bool OnPickup(Player player)
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)player.Center.X - 300, (int)player.Center.Y - 300, mod.NPCType("GrandCactusWormHead"));
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.position.Y/16 > Main.worldSurface && !NPC.AnyNPCs(mod.NPCType("AlphaCactusWormHead")) && !NPC.AnyNPCs(mod.NPCType("GrandCactusWormHead")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool UseItem(Player player)
        {
            NPC.NewNPC((int)player.Center.X - 300, (int)player.Center.Y - 300, mod.NPCType("GrandCactusWormHead"));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}
