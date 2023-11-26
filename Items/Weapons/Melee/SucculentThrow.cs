using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class SucculentThrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Succulent Throw");
            Tooltip.SetDefault("Picks up a hit enemy\n" +
                "Does not work on enemies immune to knockback");
        }
        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 26;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.channel = true;
            Item.value = 100000;
            Item.rare = ItemRarityID.Green;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("SucculentThrow").Type;
            Item.shootSpeed = 11f;
        }
    }
}

