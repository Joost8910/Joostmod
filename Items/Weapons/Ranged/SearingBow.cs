using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Ranged
{
    public class SearingBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Longbow");
            Tooltip.SetDefault("Transforms wooden arrows into Volcanic Arrows\n" +
                "Volcanic Arrows deal 50% more damage and drop a trail of damaging lava droplets\n" +
                "Right clicking nocks additional arrows\n" +
                "Can nock up to 5 arrows");
        }
        public override void SetDefaults()
        {
            Item.damage = 48;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 52;
            Item.height = 36;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4.5f;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.SearingBow>();
            Item.shootSpeed = 13f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] <= 0;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<Projectiles.Ranged.SearingBow>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MoltenFury)
                .AddIngredient<Materials.FireEssence>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

