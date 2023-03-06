using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{ 
    public class EnergyFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Orb");
			Tooltip.SetDefault("If this is in your inventory something went wrong.\n" + 
            "It's meant to do a thing when picked up like hearts\n" + 
            "Consume it for its effect since picking it up didn't work");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 3));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item8;
        }
        public override bool OnPickup(Player player)
        {
            player.AddBuff(Mod.Find<ModBuff>("EnergyBuff").Type, 600);
            player.statLife += 10;
            player.statMana += 10;
            player.HealEffect(10, true);
            player.ManaEffect(10);
            SoundEngine.PlaySound(SoundID.Item8, player.Center);
            SoundEngine.PlaySound(SoundID.Grab, player.Center);
            return false;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.AddBuff(Mod.Find<ModBuff>("EnergyBuff").Type, 300);
            player.statLife += 5;
            player.statMana += 5;
            player.HealEffect(5, true);
            player.ManaEffect(5);
            return null;
        }
    }
}
