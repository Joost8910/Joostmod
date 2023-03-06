using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
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
            Item.maxStack = 20;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 28;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 2;
            Item.value = 5000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item3;
            Item.potion = true;
            Item.healLife = 150;
            Item.healMana = 150;
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
            CreateRecipe()
.AddIngredient(Mod.Find<ModItem>("GreaterRestorationPotion").Type)
.AddIngredient(ItemID.SoulofLight)
.AddIngredient(ItemID.Ectoplasm)
.AddTile(TileID.Bottles)
.Register();
        }

    }
}

