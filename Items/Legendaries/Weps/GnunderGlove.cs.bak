//TODO: Change up the method for throwing the giant shuriken, the dodge jump makes aiming a slow multihit frustrating
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Thrown;

namespace JoostMod.Items.Legendaries.Weps
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class GnunderGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gnunderson's Glove");
            Tooltip.SetDefault("'Glove of the legendary Gnunderson'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Rapidly throws shurikens\n" +
            "Right click for an evasive jump that throws many shurikens\n" +
            "Hold UP during the jump to throw a giant shuriken\n" +
            "(4 Second Cooldown");
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 30;
            Item.height = 28;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item7;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Gnunderken>();
            Item.shootSpeed = 10f;
            Item.value = 300000;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Leather, 10)
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
            damage *= JoostGlobalItem.LegendaryDamage() * 0.08f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 0)
            {
                Item.reuseDelay = 0;
                Item.useTime = 7;
                Item.useAnimation = 7;
                Item.useStyle = ItemUseStyleID.Swing;
            }
            else
            {
                player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
                if (player.velocity.Y == 0 || player.wingTime > 0 || player.rocketTime > 0 || player.jump > 0)
                {
                    player.jump = 0;
                    player.velocity.Y = -player.gravDir * 12;
                    player.velocity.X = -player.direction * 6;
                    player.fallStart = (int)(player.position.Y / 16f);
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_ghastly_glaive_pierce_0"), player.Center);
                    //Trackable sound 204, but my list equates that to the book? That aint right. what was it meant to be
                    if (player.immuneTime < 20)
                    {
                        player.immune = true;
                        player.immuneTime = 20;
                    }
                }

                Item.reuseDelay = 40;
                Item.useAnimation = 40;
                Item.useTime = 1;
                Item.useStyle = ItemUseStyleID.Shoot;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 0)
            {
                float spread = 15f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                double baseAngle = Math.Atan2(velocity.X, velocity.Y);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
                velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.altFunctionUse != 0)
            {
                if (player.itemAnimation < 20)
                {
                    if (player.itemAnimation == 19 && (player.wingTime > 0 || player.rocketTime > 0 || player.jump > 0))
                    {
                        player.wingTime -= 20;
                        player.rocketTime -= 3;
                        player.velocity.Y = -player.gravDir * 6;
                    }
                    if (modPlayer.LegendCool <= 0 && player.controlUp && player.itemAnimation == 19)
                    {
                        player.itemAnimation = 0;
                        player.reuseDelay = 60;
                        modPlayer.LegendCool = 240;
                        SoundEngine.PlaySound(SoundID.Item19, position);
                        Projectile.NewProjectile(source, position.X, position.Y, velocity.X / 3.5f, velocity.Y / 3.5f, ModContent.ProjectileType<GiantGnunderken>(), damage * 3, knockback * 3, player.whoAmI, 0.0f, 0.0f);
                    }
                    else
                    {

                        float spread = 90f * 0.0174f;
                        float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                        double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
                        double deltaAngle = spread / 20f;
                        double offsetAngle;
                        int dir = player.direction < 0 ? player.itemAnimation : 20 - player.itemAnimation;
                        SoundEngine.PlaySound(SoundID.Item19, player.Center);
                        offsetAngle = startAngle + deltaAngle * dir;
                        Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
                    }
                }
            }
            return player.altFunctionUse == 0;
        }
    }
}

