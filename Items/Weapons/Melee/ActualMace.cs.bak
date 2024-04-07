using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class ActualMace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Actual Mace");
            Tooltip.SetDefault(//"Not to be confused with the flail labeled 'Mace'\n" +
                "Hold attack to charge the swing\n" +
                "Charged attacks reduce enemy defense\n" +
                "Can be upgraded with torches");
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
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ActualMace>();
            Item.shootSpeed = 1f;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}

