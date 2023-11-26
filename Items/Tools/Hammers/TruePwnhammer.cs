using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools.Hammers
{
    public class TruePwnhammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Pwnhammer");
        }
        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 52;
            Item.useTime = 8;
            Item.useAnimation = 24;
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
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X + 50 * player.direction * Item.scale, player.Center.Y - 40 * player.gravDir, 3f * player.direction, 0f, Mod.Find<ModProjectile>("LightWave").Type, Item.damage, Item.knockBack, player.whoAmI, player.gravDir);
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
                .AddIngredient(ItemID.Pwnhammer)
                .AddIngredient<Materials.BrokenHeroHammer>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


