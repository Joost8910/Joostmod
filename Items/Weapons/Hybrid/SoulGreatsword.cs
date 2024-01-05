using JoostMod.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Hybrid
{
    public class SoulGreatsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Greatsword");
            Tooltip.SetDefault("Attack with a mighty greatsword formed from souls");
        }
        public override void SetDefaults()
        {
            Item.damage = 320;
            Item.DamageType = ModContent.GetInstance<MagicMeleeHybrid>();
            Item.mana = 25;
            Item.width = 160;
            Item.height = 160;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.reuseDelay = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hybrid.SoulGreatsword>();
            Item.shootSpeed = 10f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool MeleePrefix()
        {
            return Main.rand.NextBool(2);
        }
        public override bool WeaponPrefix()
        {
            return false;
        }
        public override bool MagicPrefix()
        {
            return true;
        }
    }
}