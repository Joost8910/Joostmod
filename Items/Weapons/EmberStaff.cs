using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class EmberStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ember Staff");
            Tooltip.SetDefault("Summons an Ember to fight for you");
        }
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 100;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item44;
            Item.shoot = Mod.Find<ModProjectile>("EmberMinion").Type;
            Item.shootSpeed = 7f;
            Item.buffType = Mod.Find<ModBuff>("EmberMinion").Type;
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
                .AddIngredient(ItemID.Torch)
                .AddIngredient(ItemID.StoneBlock)
                .AddRecipeGroup("Wood", 12)
                .AddTile(TileID.WorkBenches)
                .Register();

        }
    }
}


