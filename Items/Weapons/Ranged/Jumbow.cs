using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Ranged
{
    public class Jumbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jumbow");
            Tooltip.SetDefault("'It's huge!'");
        }
        public override void SetDefaults()
        {
            Item.damage = 555;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 96;
            Item.height = 198;
            Item.noMelee = true;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.reuseDelay = 5;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 10;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = Mod.Find<ModProjectile>("Jumbow").Type;
            Item.shootSpeed = 16f;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.arrowDamage);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.Cactustoken>()
                .Register();
        }
    }
}

