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
            Item.shoot = Mod.Find<ModProjectile>("BFE5000").Type;
            Item.shootSpeed = 12f;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.rocketDamage);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position += Vector2.Normalize(velocity) * 80;
            return true;
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

