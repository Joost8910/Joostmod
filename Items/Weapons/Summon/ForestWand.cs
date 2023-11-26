using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Summon
{
    public class ForestWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wand of the Forest");
            Tooltip.SetDefault("Summons a swirling shield of leaves\n" + "Right click to send the leaves outwards");
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 3;
            Item.width = 30;
            Item.height = 32;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 5000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item78;
            Item.shoot = Mod.Find<ModProjectile>("Leaf").Type;
            Item.shootSpeed = 7f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool create = true;
            for (int l = 0; l < 200; l++)
            {
                Projectile p = Main.projectile[l];
                if (p.type == type && p.active && p.owner == player.whoAmI && p.ai[1] != 1)
                {
                    if (player.altFunctionUse == 0)
                    {
                        p.Kill();
                    }
                    else
                    {
                        p.ai[1] = 1f;
                        create = false;
                    }
                    p.netUpdate = true;
                }
            }
            if (create)
            {
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 90f, 0f);
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 180f, 0f);
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 270f, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LivingLeafWall, 8)
                .AddRecipeGroup("Wood", 12)
                .AddTile(TileID.WorkBenches)
                .Register();

        }
    }
}


