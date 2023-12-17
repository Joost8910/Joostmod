using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Legendaries.Weps
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
            Item.damage = 100;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 28;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 300000;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<PowerSpirit>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.PowerSpirit>();
            Item.buffTime = 3600;
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskTex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/Weapons/LarkusTome_Glow");
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddIngredient<EvilStone>()
                .AddIngredient<SkullStone>()
                .AddIngredient<JungleStone>()
                .AddIngredient<InfernoStone>()
                .AddTile<Tiles.ShrineOfLegends>()
                .Register();
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, (int)(51 + Main.DiscoG * 0.75f));
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
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage *= JoostGlobalItem.LegendaryDamage() * 0.07f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
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
                            Projectile.NewProjectileDirect(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI).minionSlots = slotAmount;
                            break;
                        }
                    }
                }
                return false;
            }
            return player.altFunctionUse != 2;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(false);
            }
            return null;
        }

    }
}


