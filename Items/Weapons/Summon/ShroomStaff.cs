using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Summon
{
    public class ShroomStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroom Staff");
            Tooltip.SetDefault("Summons a giant mushroom that creates homing spores\n" +
            "The mushroom knocks enemies upwards as it sprouts");
        }
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 15;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 10;
            Item.value = 75000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item44;
            Item.shoot = Mod.Find<ModProjectile>("ShroomSentry").Type;
            Item.sentry = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld - new Vector2(0, 50);
            velocity.Y = 48;
            return true;
        }
    }
}


