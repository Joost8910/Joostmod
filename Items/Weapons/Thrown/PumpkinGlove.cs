using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class PumpkinGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpkin Glove");
            Tooltip.SetDefault("Throws a pumpkin that explodes into homing pumpkins");
        }
        public override void SetDefaults()
        {
            Item.damage = 48;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8;
            Item.value = 120000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Pumpkin2").Type;
            Item.shootSpeed = 8f;
        }
    }
}


