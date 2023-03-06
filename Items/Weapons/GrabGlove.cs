using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class GrabGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brawler's Glove");
            Tooltip.SetDefault("Punches enemies with your bare hands\n" +
                "Hold right click to grab an enemy\n" +
                "Left click while grabbing to pummel\n" +
                "Release right click to throw");
        }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 26;
            Item.height = 24;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 5;
            Item.value = 100000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("GrabGlove").Type;
            Item.shootSpeed = 1f;
            Item.channel = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}


