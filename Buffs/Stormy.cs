using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class Stormy : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormy");
			Description.SetDefault("Best girl");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<JoostPlayer>().stormy = true;
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Stormy").Type] <= 0 && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Stormy").Type, 0, 0f, player.whoAmI);
			}
		}
	}
}