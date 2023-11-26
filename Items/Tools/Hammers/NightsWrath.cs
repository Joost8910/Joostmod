using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools.Hammers
{
    public class NightsWrath : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Night's Wrath");
        }
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.knockBack = 7;
            Item.value = 54000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.hammer = 80;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.tileBoost = 1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MoltenHamaxe)
                .AddIngredient<AquaHammer>()
                .AddIngredient<JungleHammer>()
                .AddIngredient(ItemID.TheBreaker)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}


