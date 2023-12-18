using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Weapons.Summon
{
    public class GnomeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gnome Staff");
            Tooltip.SetDefault("'With silver beard and crimson hat the gnome warriors fight valiantly for their people'\n" +
                "Summons a Gnome warrior\n" +
                "Hold right click to direct the gnome warriors to block\n" +
                "Gnome warriors cannot block projectiles with higher base damage than double their own damage\n" +
                "Left click while they're blocking to direct them to shield bash");
        }
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 52;
            Item.height = 50;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 64000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<Gnome>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.GnomeBuff>();
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
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GardenGnome)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 8)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}


