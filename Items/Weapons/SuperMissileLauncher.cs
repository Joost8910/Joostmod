using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Item.damage = 800;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 16;
            Item.noMelee = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 9;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = new SoundStyle("JoostMod/Sounds/Custom/SuperMissileShoot");
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SuperMissile").Type;
            Item.shootSpeed = 12f;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.rocketDamage);
        }
        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X -= 7f * player.direction;
            player.itemLocation.Y -= 7f * player.gravDir;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.IceCoreX>()
                .Register();
        }
    }
}

