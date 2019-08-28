using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class EndlessSoulArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Soul Arrow");
            Tooltip.SetDefault("Has a slight homing effect");
        }
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.ranged = true;
            item.damage = 14;
            item.width = 32;
            item.height = 32;
            item.consumable = false;
            item.knockBack = 2.5f;
            item.value = 1000;
            item.rare = 8;
            item.shoot = mod.ProjectileType("SoulArrow");
            item.shootSpeed = 9f;
            item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SoulArrow"), 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
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
    }
}

