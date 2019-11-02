using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
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
            item.maxStack = 999;
            item.consumable = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 3;
            item.useAnimation = 3;
            item.useStyle = 4;
            item.noMelee = true; 
            item.value = 5000;
            item.rare = 2;
            item.UseSound = SoundID.Item44;
            item.autoReuse = true;
        }
        public override bool UseItem(Player player)
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
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Cactoid"));
            }
            return true;
        }
    }
}

