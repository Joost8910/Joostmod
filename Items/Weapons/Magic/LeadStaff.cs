using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
{
    public class LeadStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Staff");
        }
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Magic;
            Item.width = 42;
            Item.height = 40;
            Item.noMelee = true;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.autoReuse = true;
            Item.mana = 5;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 4;
            Item.value = 5000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item43;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Star>();
            Item.shootSpeed = 22f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            float speed = velocity.Length();
            velocity = position.DirectionTo(Main.MouseWorld) * speed;
            Vector2 targetPos = Main.MouseWorld;
            Vector2 dir = (position - Main.MouseWorld).SafeNormalize(new Vector2(0f, -1f));
            while (targetPos.Y > position.Y && WorldGen.SolidTile(targetPos.ToTileCoordinates()))
            {
                targetPos += dir * 16f;
            }
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position, velocity, type, damage, knockback, player.whoAmI, 0f, targetPos.Y, 1f);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 10)
                .AddIngredient(ItemID.FallenStar, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}

