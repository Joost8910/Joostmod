using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class HavelArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havel's Armor");
            Tooltip.SetDefault("Grants immunity to knockback\n" + 
                "7% increased melee damage\n" +
                "10% reduced movement speed");
            ArmorIDs.Body.Sets.HidesTopSkin[Item.bodySlot] = true;
            //ArmorIDs.Body.Sets.shouldersAreAlwaysInTheBack[Item.bodySlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = 300000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 30;
        }
        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.GetModPlayer<JoostPlayer>().skirtTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Skirt");
            player.GetModPlayer<JoostPlayer>().betterShoulderTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_BetterShoulder");
            //player.waist = (sbyte)EquipLoader.GetEquipSlot(Mod, "HavelArmor", EquipType.Waist);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.07f;
            player.moveSpeed *= 0.9f;
            player.maxRunSpeed *= 0.9f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.9f;
            player.noKnockback = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 200)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 8)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 8)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 8)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}