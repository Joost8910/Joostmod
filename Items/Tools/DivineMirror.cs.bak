using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools
{
    public class DivineMirror : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Divine Mirror");
            Tooltip.SetDefault("Hold out the mirror to charge a flash of divine light\n" +
                "Creatures caught in the light get sent to the world's spawn\n" +
                "Ineffective against bosses or knockback-immune enemies\n" +
                "Cannot be used while you have mana sickness");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.noMelee = true;
            Item.mana = 40;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.reuseDelay = 15;
            Item.autoReuse = false;
            //item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 0;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ModContent.ProjectileType<Projectiles.DivineMirror>();
            Item.shootSpeed = 1;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1 && !player.manaSick;
        }
    }
}

