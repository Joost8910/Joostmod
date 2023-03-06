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
            Item.damage = 150;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 12;
            Item.noMelee = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 5;
            Item.mana = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.UseSound = SoundID.Item7;
            Item.shoot = Mod.Find<ModProjectile>("IceBeamCannon").Type;
            Item.shootSpeed = 16f;
            Item.autoReuse = true;
        }
        public override bool CanUseItem(Player player)
        {
            if ((player.ownedProjectileCounts[Item.shoot]) >= Item.stack)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.IceCoreX>()
                .Register();
        }

    }
}
