using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace JoostMod.Items.Weapons
{
    public class DirtWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soil Wand");
            Tooltip.SetDefault("Does 1 more damage for every 666 blocks of dirt in your inventory\n" + 
                "Using this weapon consumes dirt equal to 1/20th of the damage bonus");
        }
        public override void SetDefaults()
        {
            item.damage = 1;
            item.magic = true;
			item.mana = 3;
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.knockBack = 2.5f;
            item.autoReuse = true;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 10);
			item.UseSound = SoundID.Item8;
			item.shoot = mod.ProjectileType("DirtBolt");
			item.shootSpeed = 5.5f;
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
        public override bool CanUseItem(Player player)
        {
            int dirt = 0;
            for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
                {
                    dirt += player.inventory[i].stack;
                }
            }
            int amount = (dirt / 666) / 20;
            for (int i = 0; i < 58 && amount > 0; i++)
            {
                if (player.inventory[i].stack > 0 && player.inventory[i].type == ItemID.DirtBlock)
                {
                    if (player.inventory[i].stack >= amount)
                    {
                        player.inventory[i].stack -= amount;
                        amount = 0;
                    }
                    else
                    {
                        amount -= player.inventory[i].stack;
                        player.inventory[i].stack = 0;
                    }
                    if (player.inventory[i].stack <= 0)
                    {
                        player.inventory[i].SetDefaults(0, false);
                    }
                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }
            return base.CanUseItem(player);
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

