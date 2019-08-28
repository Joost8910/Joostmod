using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class SoulArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Arrow");
            Tooltip.SetDefault("Has a slight homing effect");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.ranged = true;
            item.damage = 14;
            item.width = 30;
            item.height = 60;
            item.consumable = true;
            item.knockBack = 2.5f;
            item.value = 150;
            item.rare = 8;
            item.shoot = mod.ProjectileType("SoulArrow");
            item.shootSpeed = 9f;
            item.ammo = AmmoID.Arrow;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.magicDamage - 1f);
            mult *= player.magicDamageMult;
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.magicCrit;
            crit /= 2;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " ranged and magic damage"));
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 75);
            recipe.AddRecipe();
        }
    }
}

