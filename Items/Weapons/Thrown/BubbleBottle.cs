using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class BubbleBottle : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble Knife");
            Tooltip.SetDefault("'Bubbles!'");
        }
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Throwing;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 10;
            Item.height = 24;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 2;
            Item.value = 5000;
            Item.rare = Item.RarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("BubbleBottle").Type;
            Item.shootSpeed = 9f;
        }

    }
}

