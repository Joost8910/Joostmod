using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Magic;

namespace JoostMod.Items.Weapons.Magic
{
    public class PalladiumStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Staff");
            Tooltip.SetDefault("Shoots a chunk of crystal at your mouse's location that shatters into 8 shards");
        }
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Magic;
            Item.width = 38;
            Item.height = 38;
            Item.noMelee = true;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.autoReuse = true;
            Item.mana = 20;
            Item.staff[Item.type] = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = 40000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item43;
            Item.shoot = ModContent.ProjectileType<CrystalChunk>();
            Item.shootSpeed = 1f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
            velocity.X = velocity.X * (distance / 30);
            velocity.Y = velocity.Y * (distance / 30);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PalladiumBar, 10)
                .AddIngredient(ItemID.CrystalShard, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}

