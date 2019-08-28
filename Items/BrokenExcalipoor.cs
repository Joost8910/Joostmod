using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class BrokenExcalipoor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Excalipoor");
            Tooltip.SetDefault("Easily repairable");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 50;
            item.height = 48;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.value = 10000;
            item.rare = -1;
            item.UseSound = SoundID.Item1;

        }
    }
}

