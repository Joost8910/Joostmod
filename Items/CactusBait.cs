using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class CactusBait : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Succulent Cactus Meal");
            Tooltip.SetDefault("Summons the Alpha Cactus Worm while in the underground desert");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 4;
            item.noMelee = true; 
            item.value = 15000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
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
            if (player.position.Y/16 > Main.worldSurface && !NPC.AnyNPCs(mod.NPCType("IdleCactusWorm")) && !NPC.AnyNPCs(mod.NPCType("AlphaCactusWormHead")) && !NPC.AnyNPCs(mod.NPCType("GrandCactusWormHead")))
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
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("AlphaCactusWormHead"));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SucculentCactus", 10);
            recipe.AddIngredient(ItemID.Bone, 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

