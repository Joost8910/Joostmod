using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Ranged
{
    public class BFE5000 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The BFE 5000");
            Tooltip.SetDefault("'I want it huge, unstable, prone to overheating and a good chance it will blow up if you look at it from the wrong angle.'\n" +
            "'I didn't become a kerbonaut for an easy ride, dammit!'");
        }
        public override void SetDefaults()
        {
            Item.damage = 136;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 88;
            Item.noMelee = true;
            Item.useTime = 85;
            Item.useAnimation = 85;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 15;
            Item.value = 100000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.BFE5000>();
            Item.shootSpeed = 12f;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.rocketDamage);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += Vector2.Normalize(velocity) * 80;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<KerbalKannon>()
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddIngredient(ItemID.SoulofMight, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }
    }
}

