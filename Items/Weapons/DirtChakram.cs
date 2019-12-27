using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Weapons
{
    public class DirtChakram : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soil Chakram");
            Tooltip.SetDefault("Does 1 more damage for every 666 blocks of dirt in your inventory\n" + 
                "Stacks up to 3");
        }
        public override void SetDefaults()
        {
            item.damage = 1;
            item.thrown = true;
            item.width = 18;
            item.height = 32;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 1;
            item.knockBack = 0;
            item.rare = 2;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.maxStack = 3;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 0, 0, 10);
            item.UseSound = SoundID.Item1;
			item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("DirtChakram");
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[item.shoot] >= item.stack)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(151, 107, 75);
                }
            }
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            int dirt = 0;
            for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
                {
                    dirt += player.inventory[i].stack;
                }
            }
            flat = (dirt / 666f);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 666);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

