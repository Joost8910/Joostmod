using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
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
            Item.maxStack = 999;
            Item.DamageType = DamageClass.Ranged;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 14;
            Item.width = 30;
            Item.height = 60;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = 150;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = Mod.Find<ModProjectile>("SoulArrow").Type;
            Item.shootSpeed = 9f;
            Item.ammo = AmmoID.Arrow;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.magicDamage - 1f);
            mult *= player.magicDamageMult;
        }
        */
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += player.GetCritChance(DamageClass.Magic);
            crit /= 2;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " ranged and magic damage"));
        }
        public override void AddRecipes()
        {
            CreateRecipe(75)
                .AddIngredient(ItemID.SpectreBar)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

