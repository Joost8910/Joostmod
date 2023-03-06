using JoostMod.Items;
using JoostMod.Items.Materials;
using JoostMod.Items.Rewards;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod
{
    public class JoostRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //LegacyMisc.37 is "Any"
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ModContent.ItemType<Sapling>()), new int[]
            {
                ModContent.ItemType<Sapling>(),
                ModContent.ItemType<ShieldSapling>(),
                ModContent.ItemType<GlowSapling>(),
                ModContent.ItemType<SpelunkerSapling>(),
                ModContent.ItemType<BowSapling>(),
                ModContent.ItemType<SwordSapling>(),
                ModContent.ItemType<StaffSapling>(),
                ModContent.ItemType<HatchetSapling>(),
                ModContent.ItemType<FishingSapling>()
            });
            RecipeGroup cpgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CobaltBar), new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup mogroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.MythrilBar), new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup atgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.AdamantiteBar), new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("JoostMod:Saplings", group);
            RecipeGroup.RegisterGroup("JoostMod:AnyCobalt", cpgroup);
            RecipeGroup.RegisterGroup("JoostMod:AnyMythril", mogroup);
            RecipeGroup.RegisterGroup("JoostMod:AnyAdamantite", atgroup);
        }

        public override void AddRecipes()
        {
            //TODO: Add a material drop from Empress of Light to this recipe.
            Recipe.Create(ItemID.RodofDiscord)
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddIngredient(ItemID.SoulofLight, 25)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.MoneyTrough)
                .AddIngredient(ItemID.PiggyBank)
                .AddIngredient(ItemID.GoldCoin, 10)
                .AddIngredient(ItemID.Feather, 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.DPSMeter)
                .AddRecipeGroup("IronBar", 6)
                .AddIngredient(ItemID.Wire, 30)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.LivingLoom)
                .AddRecipeGroup("Wood", 25)
                .AddTile(TileID.LeafBlock)
                .Register();

            //TODO: Remove these once shimmer makes this obsolete

            Recipe.Create(ItemID.RangerEmblem)
                .AddIngredient(ItemID.WarriorEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.SorcererEmblem)
                .AddIngredient(ItemID.WarriorEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.SummonerEmblem)
                .AddIngredient(ItemID.WarriorEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.WarriorEmblem)
                .AddIngredient(ItemID.RangerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.SorcererEmblem)
                .AddIngredient(ItemID.RangerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.SummonerEmblem)
                .AddIngredient(ItemID.RangerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.WarriorEmblem)
                .AddIngredient(ItemID.SorcererEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.RangerEmblem)
                .AddIngredient(ItemID.SorcererEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.SummonerEmblem)
                .AddIngredient(ItemID.SorcererEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.WarriorEmblem)
                .AddIngredient(ItemID.SummonerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.RangerEmblem)
                .AddIngredient(ItemID.SummonerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.SorcererEmblem)
                .AddIngredient(ItemID.SummonerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.AncientBattleArmorMaterial)
                .AddIngredient<DesertCore>(), 2)
                .AddIngredient(ItemID.DarkShard)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            Recipe.Create(ItemID.SpelunkerGlowstick, 50)
                .AddIngredient(ItemID.Glowstick, 50)
                .AddIngredient(ItemID.SpelunkerPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ItemID.Leather)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            Recipe.Create(ItemID.HotlineFishingHook)
                .AddIngredient<FireEssence>(), 25)
                .AddIngredient(ItemID.MechanicsRod)
                .AddTile<Tiles.ElementalForge>()
                .Register();
            Recipe.Create(ItemID.BottomlessBucket)
                .AddIngredient<WaterEssence>(), 25)
                .AddIngredient(ItemID.WaterBucket)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}