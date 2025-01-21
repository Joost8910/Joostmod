using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Melee;

namespace JoostMod.Items.Legendaries.Weps
{
    public class BrokenHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Warhammer of Grognak");
            /* Tooltip.SetDefault("'Warhammer of the legendary Grognak'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Right click for a strong attack\n" +
            "Hold UP during strong attack to unleash a mighty shockwave attack\n" +
            "(5 second Cooldown)\n" +
            "Does not function as a tool hammer"); */
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<GrognakHammer>();
        }
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 62;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.rare = ItemRarityID.Gray;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = false;
            Item.value = 0;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.BrokenHammer>();
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
    }
}

