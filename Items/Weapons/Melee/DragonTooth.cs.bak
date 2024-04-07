using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class DragonTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Tooth");
            Tooltip.SetDefault("Increases defense by 10 while held\n" +
                "Hold attack to charge the swing\n" +
                "Unleash a charged swing while falling to do a plunging attack");
        }
        public override void SetDefaults()
        {
            Item.damage = 188;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 106;
            Item.height = 110;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.reuseDelay = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 15;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.DragonTooth>();
            Item.shootSpeed = 10f;
        }
        public override void HoldItem(Player player)
        {
            player.statDefense += 10;
        }

        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 100)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

