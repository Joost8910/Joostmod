using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
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
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 26;
            Item.height = 30;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true; 
            Item.UseSound = SoundID.Item1;
        }
        public override bool OnPickup(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X - 300, (int)player.Center.Y - 300, Mod.Find<ModNPC>("GrandCactusWormHead").Type);
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.position.Y/16 > Main.worldSurface && !NPC.AnyNPCs(Mod.Find<ModNPC>("AlphaCactusWormHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("GrandCactusWormHead").Type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X - 300, (int)player.Center.Y - 300, Mod.Find<ModNPC>("GrandCactusWormHead").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}
