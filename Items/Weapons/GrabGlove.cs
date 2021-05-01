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
            item.damage = 9;
            item.melee = true;
            item.width = 26;
            item.height = 24;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.knockBack = 5;
            item.value = 100000;
            item.rare = 1;
            item.UseSound = SoundID.Item7;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GrabGlove");
            item.shootSpeed = 1f;
            item.channel = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}


