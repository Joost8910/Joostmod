using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class HellstoneShuriken : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Glove");
            Tooltip.SetDefault("Rapidly throws flaming shurikens");
        }
        public override void SetDefaults()
        {
            item.damage = 19;
            item.thrown = true;
            item.width = 26;
            item.height = 28;
            item.useTime = 11;
            item.useAnimation = 11;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 20000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("HellstoneShuriken");
            item.shootSpeed = 14f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}

