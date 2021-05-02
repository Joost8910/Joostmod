using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
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
            item.damage = 100;
            item.melee = true;
            item.width = 64;
            item.height = 62;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.knockBack = 7;
            item.rare = 9;
            item.UseSound = SoundID.Item7;
            item.autoReuse = true;
            item.value = 300000;
            item.shootSpeed = 1f;
            item.shoot = mod.ProjectileType("GrognakHammer");
            item.noMelee = true;
            item.noUseGraphic = true;
            item.GetGlobalItem<JoostGlobalItem>().glowmaskTex = ModContent.GetTexture("JoostMod/Items/Weapons/GrognakHammerGem");
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
            recipe.AddIngredient(ItemID.GoldHammer);
            recipe.AddIngredient(null, "EvilStone");
            recipe.AddIngredient(null, "SkullStone");
            recipe.AddIngredient(null, "JungleStone");
            recipe.AddIngredient(null, "InfernoStone");
            recipe.AddTile(null, "ShrineOfLegends");
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumHammer);
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
            mult *= JoostGlobalItem.LegendaryDamage() * 0.18f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 0)
            {
                item.useStyle = 1;
            }
            else
            {
                item.useStyle = 5;
            }
            return base.CanUseItem(player);
        }
    }
}

