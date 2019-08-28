using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{ 
    public class EnergyFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Orb");
			Tooltip.SetDefault("If this is in your inventory something went wrong.\n" + 
            "It's meant to do a thing when picked up like hearts\n" + 
            "Consume it for its effect since picking it up didn't work");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 3));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.width = 32;
            item.height = 32;
            item.rare = 2;
            item.consumable = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 4;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item8;
        }
        public override bool OnPickup(Player player)
        {
            player.AddBuff(mod.BuffType("EnergyBuff"), 300);
            player.statLife += 5;
            player.statMana += 5;
            player.HealEffect(5, true);
            player.ManaEffect(5);
            Main.PlaySound(2, player.Center, 8);
            Main.PlaySound(7, player.Center);
            return false;
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType("EnergyBuff"), 300);
            player.statLife += 5;
            player.statMana += 5;
            player.HealEffect(5, true);
            player.ManaEffect(5);
            return true;
        }
    }
}
