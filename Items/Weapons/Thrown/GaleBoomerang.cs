using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class GaleBoomerang : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gale Boomerang");
            // Tooltip.SetDefault("A piercing boomerang that picks up enemies and items");
        }
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 30;
            Item.height = 62;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7f;
            Item.value = 250000;
            Item.rare = ItemRarityID.LightRed;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>();
            Item.shootSpeed = 9.5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Rectangle rect = new Rectangle((int)Main.MouseWorld.X - 32, (int)Main.MouseWorld.Y - 32, 64, 64);
            int t = 0;
            for (int n = 0; n < 200; n++)
            {
                NPC target = Main.npc[n];
                if (target.active && !target.friendly && !target.immortal && rect.Intersects(target.getRect()))
                {
                    t = n + 1;
                    for (int d = 0; d < 12; d++)
                    {
                        Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.SnowBlock, 0, 0f, 50, default, 1.5f);
                        dust.noGravity = true;
                        dust.velocity = target.DirectionTo(dust.position) * 3;
                        dust.position = target.Center;
                    }
                    break;
                }
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, t, t);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] >= Item.stack)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.AirEssence>(50)
                .AddIngredient(ItemID.WoodenBoomerang)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

