using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SlimeHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Helm");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.buyPrice(0, 0, 60, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 4;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SlimeCoat>() && legs.type == ModContent.ItemType<SlimeLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Allows you to stick to surfaces\n" +
                "Press the Armor Ability key to become slimed\n" +
                "While slimed, you take 20% less damage and stick to enemies, but cannot use items";
            player.GetModPlayer<JoostPlayer>().slimeArmor = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SlimeBlock, 20)
                .AddTile(TileID.Solidifier)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Gel, 20)
                .AddTile(TileID.Solidifier)
                .Register();
        }
    }
}