using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class CactoidCompact : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactoid Compact");
            Tooltip.SetDefault("Summons a cactoid\n" + "Fished in the hardmode desert with the Cactoid Commendation equipped");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 3;
            Item.useAnimation = 3;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true; 
            Item.value = 5000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44;
            Item.autoReuse = true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    num++;
                }
            }
            if (num >= 200)
            {
                return false;
            }
            if (Main.netMode != 1)
            {
                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y, Mod.Find<ModNPC>("Cactoid").Type);
            }
            return true;
        }
    }
}

