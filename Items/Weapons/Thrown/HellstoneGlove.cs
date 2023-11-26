using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class HellstoneGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Glove");
            Tooltip.SetDefault("Rapidly throws flaming shurikens");
        }
        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 26;
            Item.height = 28;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = 20000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shoot = Mod.Find<ModProjectile>("HellstoneShuriken").Type;
            Item.shootSpeed = 14f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}

