using JoostMod.Prefixes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class ActualFlamingMace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Actual Flaming Mace");
            Tooltip.SetDefault(//"Not to be confused with the flail labeled 'Flaming Mace'\n" +
                "Hold attack to charge the swing\n" +
                "Charged attacks reduce enemy defense\n" +
                "Sets enemies on fire");
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.4f;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item7;
            Item.channel = true;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ActualFlamingMace>();
            Item.shootSpeed = 1f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ActualMace>()
                .AddIngredient(ItemID.Torch, 99)
                .Register();
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}

