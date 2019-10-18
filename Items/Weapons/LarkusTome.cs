using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


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
            "Increases your max number of minions");
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
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 200000;
            item.rare = 9;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("PowerSpirit");
            item.shootSpeed = 7f;
            item.buffType = mod.BuffType("PowerSpirit");
            item.buffTime = 3600;
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
                    line2.overrideColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.5f)));
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().legendOwn = true;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= JoostGlobalItem.LegendaryDamage() * 0.05f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
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


