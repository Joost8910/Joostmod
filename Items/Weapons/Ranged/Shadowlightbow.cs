using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Ranged;

namespace JoostMod.Items.Weapons.Ranged
{
    public class Shadowlightbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowlight Bow");
            Tooltip.SetDefault("Does not consume ammo\n" + "'Find your inner pieces'");
        }
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 144000;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Darklightarrow>();
            Item.shootSpeed = 16f;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.arrowDamage);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LightShard, 1)
                .AddIngredient(ItemID.DarkShard, 1)
                .AddIngredient(ItemID.SoulofLight, 7)
                .AddIngredient(ItemID.SoulofNight, 7)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

