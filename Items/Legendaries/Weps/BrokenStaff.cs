using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Magic;

namespace JoostMod.Items.Legendaries.Weps
{
    public class BrokenStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Staff of David");
            /* Tooltip.SetDefault("'Staff of the legendary David'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Unleashes a defense-ignoring, rapidly damaging laser\n" +
            "Right click to fire three magical bolts"); */
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<StaffofDavid>();
        }
        public override void SetDefaults()
        {
            Item.damage = 2;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 15;
            Item.rare = ItemRarityID.Gray;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<BrokenLaser>();
            Item.value = 0;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
    }
}
