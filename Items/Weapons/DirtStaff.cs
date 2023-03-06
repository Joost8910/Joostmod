using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace JoostMod.Items.Weapons
{
    public class DirtStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soil Staff");
            Tooltip.SetDefault("Does 1 more damage for every 666 blocks of dirt in your inventory\n" +
            "Summons a soil spirit to fight for you\n" +
            "Using this weapon consumes dirt equal half of the damage bonus");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
            Item.width = 46;
            Item.height = 46;
            Item.noMelee = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = true;
            Item.value = Item.sellPrice(0, 0, 0, 10);
			Item.UseSound = SoundID.Item44;
			Item.shoot = Mod.Find<ModProjectile>("DirtMinion").Type;
			Item.shootSpeed = 7f;
			Item.buffType = Mod.Find<ModBuff>("DirtMinion").Type;
			Item.buffTime = 3600;
        }
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			position = Main.MouseWorld;
			return player.altFunctionUse != 2;
		}

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                int dirt = 0;
                for (int i = 0; i < 58; i++)
                {
                    if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
                    {
                        dirt += player.inventory[i].stack;
                    }
                }
                int amount = (dirt / 666) / 2;
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
            }
            return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(false);
			}
			return base.UseItem(player);
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
            damage.Flat = (dirt / 666f);
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

