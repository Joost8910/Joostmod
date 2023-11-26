using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools.Hammers
{
    public class TerraFirma : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Firma");
        }
        public override void SetDefaults()
        {
            Item.damage = 140;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 58;
            Item.useTime = 5;
            Item.useAnimation = 25;
            Item.knockBack = 11;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.hammer = 120;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.tileBoost = 7;
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
                    //SoundEngine.PlaySound(42, player.position, 207 + Main.rand.Next(3));
                    //I get a feeling my trackable sounds id list goes wrong somewhere. My gut says it uses the monk staff sounds
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X + 64 * player.direction * Item.scale, player.Center.Y - 40 * player.gravDir, 10f, 0f, Mod.Find<ModProjectile>("TerraWave").Type, Item.damage, Item.knockBack, player.whoAmI, player.gravDir);
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X + 64 * player.direction * Item.scale, player.Center.Y - 40 * player.gravDir, -10f, 0f, Mod.Find<ModProjectile>("TerraWave").Type, Item.damage, Item.knockBack, player.whoAmI, player.gravDir);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, player.Center);
                    //SoundEngine.PlaySound(42, player.position, 213 + Main.rand.Next(4));
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<TrueNightsWrath>()
                .AddIngredient<TruePwnhammer>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient<TrueBloodBreaker>()
                .AddIngredient<TruePwnhammer>()
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }
    }
}


