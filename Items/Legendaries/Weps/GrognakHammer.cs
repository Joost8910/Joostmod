using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Melee;

namespace JoostMod.Items.Legendaries.Weps
{
    public class GrognakHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
            Tooltip.SetDefault("'Warhammer of the legendary Grognak'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Right click for a strong attack\n" +
            "Hold UP during strong attack to unleash a mighty shockwave attack\n" +
            "(5 second Cooldown)\n" +
            "Does not function as a tool hammer");
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 62;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 7;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.value = 300000;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.GrognakHammer>();
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Gem");
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
                .AddIngredient(ItemID.GoldHammer)
                .AddIngredient<EvilStone>()
                .AddIngredient<SkullStone>()
                .AddIngredient<JungleStone>()
                .AddIngredient<InfernoStone>()
                .AddTile<Tiles.ShrineOfLegends>()
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.PlatinumHammer)
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
            damage *= JoostGlobalItem.LegendaryDamage() * 0.18f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 0)
            {
                Item.useStyle = ItemUseStyleID.Swing;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Shoot;
            }
            return base.CanUseItem(player);
        }
    }
}

