using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Legendaries.Weps
{
    public class BrokenTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Larkus's Tome");
            /* Tooltip.SetDefault("'Tome of the legendary Larkus'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Summons a Spirit of Power to protect you\n" +
            "Increases your max number of minions\n" +
            "Hold Right Click to charge a more powerful blast\n" +
            "(Cooldown based on how long it's charged)"); */
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<LarkusTome>();
        }
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 5;
            Item.width = 28;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 0;
            Item.rare = ItemRarityID.Gray;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<PowerSpiritBlast>();
            Item.shootSpeed = 0f;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return player.altFunctionUse != 2;
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            if (player.altFunctionUse == 2)
            {
                mult = 0f;
            }
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(false);
            }
            return null;
        }

    }
}


