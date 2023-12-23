using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using JoostMod.Projectiles.Thrown;

namespace JoostMod.Items.Weapons.Thrown
{
    public class PowerBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Bomb");
            Tooltip.SetDefault("Explodes into a powerful heat wave");
        }
        public override void SetDefaults()
        {
            Item.damage = 500;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 0;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = new SoundStyle("JoostMod/Sounds/Custom/LayBomb");
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.PowerBomb>();
            Item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<PowerBombExplosion>()] + player.ownedProjectileCounts[ModContent.ProjectileType<PowerBombExplosion2>()] >= 1)
            {
                return false;
            }
            else return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
            velocity.Normalize();
            velocity.X = velocity.X * (distance / 60);
            velocity.Y = velocity.Y * (distance / 60);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.IceCoreX>()
                .Register();
        }

    }
}
