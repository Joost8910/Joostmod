using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Mounts;

namespace JoostMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
	public class BlazingAnklet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Anklets");
			Tooltip.SetDefault("Increases running speed by 25%\n" +
                "For every 10mph you're running at, ranged crit chance increases by 1%\n" +
                "Greatly increases running speed while on fire blocks\n" + 
                "Grants immunity to fire blocks");
		}
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 24;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.fireWalk = true;
            player.moveSpeed *= 1.25f;
            player.maxRunSpeed *= 1.25f;
            if (!player.mount.Active || player.mount._type == ModContent.MountType<FierySoles>())
                player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.25f;
            player.GetModPlayer<JoostPlayer>().blazeAnklet = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.FireEssence>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 3)
                .AddRecipeGroup("JoostMod:AnyMythril", 3)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}