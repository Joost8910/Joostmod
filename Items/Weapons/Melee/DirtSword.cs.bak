using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace JoostMod.Items.Weapons.Melee
{
    public class DirtSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soil Sword");
            Tooltip.SetDefault("Does 1 more damage for every 666 blocks of dirt in your inventory\n" +
                "Using this weapon consumes dirt equal to 1/20th of the damage bonus");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.value = Item.sellPrice(0, 0, 0, 10);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(151, 107, 75);
                }
            }
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int dirt = 0;
            for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
                {
                    dirt += player.inventory[i].stack;
                }
            }
            damage.Flat = dirt / 666f;
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
            int amount = dirt / 666 / 20;
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
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 666)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}

