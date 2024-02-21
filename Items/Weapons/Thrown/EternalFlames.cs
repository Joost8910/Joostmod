using JoostMod.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class EternalFlames : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Flames");
            Tooltip.SetDefault("Right click for a special targeted attack\n" +
                "'Got it memorized?'");
        }
        public override void SetDefaults()
        {
            Item.damage = 56;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.EternalFlame>();
            Item.shootSpeed = 13f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<EternalFlame2>(), damage, knockback, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<EternalFlame2>(), damage, knockback, player.whoAmI, -Main.MouseWorld.X, Main.MouseWorld.Y);
            }
            else
            {
                float sp = velocity.Length();
                float mDist = player.Distance(Main.MouseWorld);
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, sp, mDist);
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 2 && player.ownedProjectileCounts[ModContent.ProjectileType<EternalFlame2>()] < 1;
        }
        public override bool AltFunctionUse(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}


