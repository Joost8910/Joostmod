using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Melee;

namespace JoostMod.Items.Tools.Hammers
{
    public class TrueNightsWrath : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Night's Wrath");
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 9;
            Item.useAnimation = 36;
            Item.knockBack = 9;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.hammer = 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.tileBoost = 2;
            Item.autoReuse = true;
            Item.useTurn = true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.itemAnimation <= Item.useTime)
            {
                if (player.velocity.Y == 0)
                {
                    SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, player.Center);
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X + 64 * player.direction * Item.scale, player.Center.Y - 40 * player.gravDir, 3f * player.direction, 0f, ModContent.ProjectileType<NightWave>(), Item.damage, Item.knockBack, player.whoAmI, player.gravDir);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, player.Center);
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<NightsWrath>()
                .AddIngredient<Materials.BrokenHeroHammer>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


