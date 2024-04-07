//TODO: Make cool like new Night's Edge
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools.Hammers
{
    public class BloodBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Breaker");
        }
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 12;
            Item.useAnimation = 36;
            Item.knockBack = 7;
            Item.value = 54000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.hammer = 80;
            Item.tileBoost = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MoltenHamaxe)
                .AddIngredient<AquaHammer>()
                .AddIngredient<JungleHammer>()
                .AddIngredient(ItemID.FleshGrinder)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}


