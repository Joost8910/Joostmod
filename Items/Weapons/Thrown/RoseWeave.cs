using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class RoseWeave : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rose Weave");
            Tooltip.SetDefault("Showers thorns in the air");
        }
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 66;
            Item.height = 66;
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 8;
            Item.value = 300000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ThornShower").Type;
            Item.shootSpeed = 10f;
        }

    }
}
