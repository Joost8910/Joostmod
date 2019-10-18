using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class IceBeam : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
            Tooltip.SetDefault("Hold to charge a shot!\n" + "Sucks in items from larger distances while charging");
        }
        public override void SetDefaults()
        {
            item.damage = 150;
            item.magic = true;
            item.width = 24;
            item.height = 12;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.reuseDelay = 5;
            item.mana = 10;
            item.useStyle = 5;
            item.knockBack = 4;
            item.value = 10000000;
            item.rare = 11;
            item.noUseGraphic = true;
            item.channel = true;
            item.UseSound = SoundID.Item7;
            item.shoot = mod.ProjectileType("IceBeamCannon");
            item.shootSpeed = 16f;
            item.autoReuse = true;
        }
        public override bool CanUseItem(Player player)
        {
            if ((player.ownedProjectileCounts[item.shoot]) >= item.stack)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IceCoreX", 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}

