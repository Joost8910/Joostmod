using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Weapons.Summon
{
    public class SkellyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skeleton Staff");
            Tooltip.SetDefault("Summons a mini Skeleton to fight for you\n" +
            "Mini Skeletons have a 20% chance to throw an empowered bone");
        }
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 25000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<SkellyMinion>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.SkellyMinionBuff>();
            Item.buffTime = 3600;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            return player.altFunctionUse != 2;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(false);
            }
            return base.UseItem(player);
        }
    }
}


