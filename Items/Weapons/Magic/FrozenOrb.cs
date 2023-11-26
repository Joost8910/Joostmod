using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
{
    public class FrozenOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Orb");
            Tooltip.SetDefault("Creates an orb of ice that fires icicles");
        }
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 30;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.value = 300000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item120;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("FrozenOrb").Type;
            Item.shootSpeed = 6f;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }
    }
}

