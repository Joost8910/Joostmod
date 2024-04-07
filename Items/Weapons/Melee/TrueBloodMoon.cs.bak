using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class TrueBloodMoon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Blood Moon");
        }
        public override void SetDefaults()
        {
            Item.damage = 154;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 44;
            Item.useAnimation = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 10;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.channel = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TrueBloodMoon>();
            Item.shootSpeed = 16f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position.Y -= Item.scale * 46 - 46;
            damage /= 2;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BloodMoon>()
                .AddIngredient<Materials.BrokenHeroFlail>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}

