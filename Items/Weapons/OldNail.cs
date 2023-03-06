using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class OldNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Old Nail");
			Tooltip.SetDefault("'Years of age and wear have blunted its blade'");
		}
		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 30;
			Item.height = 30;
			Item.noMelee = true;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.knockBack = 3;
			Item.value = 8000;
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.shoot = Mod.Find<ModProjectile>("OldNail").Type;
			Item.shootSpeed = 6f;
		}
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
	}
}

