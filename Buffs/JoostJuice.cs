using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class JoostJuice : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Joost Juice");
			Description.SetDefault("Effects of Well fed, Regeneration, Swiftness, Ironskin\n" + 
			"Heartreach, Lifeforce, Endurance, Rage, Wrath, Warmth, and Summoning");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.wellFed = true;
			player.lifeRegen += 4;
			player.moveSpeed += 0.25f;
			player.statDefense += 8;
			player.lifeMagnet = true;
			player.lifeForce = true;
			player.statLifeMax2 += player.statLifeMax / 5 / 20 * 20;
			player.endurance += 0.1f;
			player.meleeCrit += 10;
			player.rangedCrit += 10;
			player.magicCrit += 10;
			player.thrownCrit += 10;
			player.thrownDamage += 0.1f;
			player.meleeDamage += 0.1f;
			player.rangedDamage += 0.1f;
			player.magicDamage += 0.1f;
			player.minionDamage += 0.1f;
			player.resistCold = true;
			player.maxMinions++;
			player.buffImmune[BuffID.WellFed] = true;
			player.buffImmune[BuffID.Regeneration] = true;
			player.buffImmune[BuffID.Swiftness] = true;
			player.buffImmune[BuffID.Ironskin] = true;
			player.buffImmune[BuffID.Heartreach] = true;
			player.buffImmune[BuffID.Lifeforce] = true;
			player.buffImmune[BuffID.Endurance] = true;
			player.buffImmune[BuffID.Rage] = true;
			player.buffImmune[BuffID.Wrath] = true;
			player.buffImmune[BuffID.Warmth] = true;
			player.buffImmune[BuffID.Summoning] = true;
		}
	}
}
