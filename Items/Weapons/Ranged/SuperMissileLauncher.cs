using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Ranged;
using System;

namespace JoostMod.Items.Weapons.Ranged
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
            Item.shoot = ModContent.ProjectileType<SuperMissile>();
            Item.shootSpeed = 12f;
            Item.GetGlobalItem<JoostGlobalItem>().drawOverArm = true;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.rocketDamage);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            float armRot = player.itemRotation - (float)Math.PI / 2 * player.direction;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRot);
            Vector2 origin = player.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, armRot);
            player.itemLocation = origin - heldItemFrame.Size() / 2f + player.itemRotation.ToRotationVector2() * -16 * player.direction;
         }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.IceCoreX>()
                .Register();
        }
    }
}

