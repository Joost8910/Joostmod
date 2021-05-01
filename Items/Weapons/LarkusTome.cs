using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    public class LarkusTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Larkus's Tome");
            Tooltip.SetDefault("'Tome of the legendary Larkus'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Summons a Spirit of Power to protect you\n" +
            "Increases your max number of minions\n" +
            "Hold Right Click to charge a more powerful blast\n" +
            "(Cooldown based on how long it's charged)");
        }
        public override void SetDefaults()
        {
            item.damage = 100;
            item.summon = true;
            item.mana = 10;
            item.width = 28;
            item.height = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 4;
            item.useTurn = true;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 300000;
            item.rare = 9;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("PowerSpirit");
            item.shootSpeed = 7f;
            item.buffType = mod.BuffType("PowerSpirit");
            item.buffTime = 3600;
            item.GetGlobalItem<JoostGlobalItem>().glowmaskTex = mod.GetTexture("Items/Weapons/LarkusTome_Glow");
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(null, "EvilStone");
            recipe.AddIngredient(null, "SkullStone");
            recipe.AddIngredient(null, "JungleStone");
            recipe.AddIngredient(null, "InfernoStone");
            recipe.AddTile(null, "ShrineOfLegends");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.75f)));
                }
            }
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().legendOwn = true;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= JoostGlobalItem.LegendaryDamage() * 0.07f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
            if (player.ownedProjectileCounts[type] > 0)
            {
                float slots = 0;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.owner == player.whoAmI)
                    {
                        slots += proj.minionSlots;
                        if (proj.type == type)
                        {
                            float slotAmount = proj.minionSlots;
                            if (slots <= player.maxMinions - 1)
                            {
                                slotAmount++;
                            }
                            proj.Kill();
                            Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, knockBack, player.whoAmI).minionSlots = slotAmount;
                            break;
                        }
                    }
                }
                return false;
            }
            return player.altFunctionUse != 2;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }

    }
}


