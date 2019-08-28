using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class FrostEmberStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostfire Staff");
            Tooltip.SetDefault("Summons an Frostfire Ember to fight for you");
        }
        public override void SetDefaults()
        {
            item.damage = 7;
            item.summon = true;
            item.mana = 10;
            item.width = 36;
            item.height = 36;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 500;
            item.rare = 2;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("FrostEmberMinion");
            item.shootSpeed = 7f;
            item.buffType = mod.BuffType("FrostEmberMinion");
            item.buffTime = 3600;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            return player.altFunctionUse != 2;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceTorch);
            recipe.AddIngredient(ItemID.IceBlock);
            recipe.AddIngredient(ItemID.BorealWood, 12);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


