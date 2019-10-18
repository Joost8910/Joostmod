using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class SuperMissileLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Missile Launcher");
            Tooltip.SetDefault("Fires powerful missiles");
        }
        public override void SetDefaults()
        {
            item.damage = 800;
            item.ranged = true;
            item.width = 24;
            item.height = 16;
            item.noMelee = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.knockBack = 9;
            item.value = 10000000;
            item.rare = 11;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MissileShoot");
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SuperMissile");
            item.shootSpeed = 12f;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            damage = (int)(damage * player.rocketDamage);
        }
        public override void HoldStyle(Player player)
        {
            player.itemLocation.X -= 7f * player.direction;
            player.itemLocation.Y -= 7f * player.gravDir;
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

