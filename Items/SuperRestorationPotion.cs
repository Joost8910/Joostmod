using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class SuperRestorationPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Restoration Potion");
            Tooltip.SetDefault("Reduced potion cooldown");
        }
        public override void SetDefaults()
        {
            item.maxStack = 20;
            item.consumable = true;
            item.width = 20;
            item.height = 28;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 2;
            item.value = 5000;
            item.rare = 5;
            item.UseSound = SoundID.Item3;
            item.potion = true;
            item.healLife = 150;
            item.healMana = 150;
        }
        public override bool ConsumeItem(Player player)
        {
            player.ClearBuff(21);
            player.potionDelay = player.restorationDelayTime;
            player.AddBuff(21, player.potionDelay, true);
            return base.ConsumeItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GreaterRestorationPotion"));
            recipe.AddIngredient(ItemID.SoulofLight);
            recipe.AddIngredient(ItemID.Ectoplasm);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}

