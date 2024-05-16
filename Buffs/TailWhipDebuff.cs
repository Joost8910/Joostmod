using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace JoostMod.Buffs
{
	public class TailWhipDebuff : ModBuff
	{
        public static readonly int TagDamage = 7;
		public override void SetStaticDefaults()
		{
            BuffID.Sets.IsATagBuff[Type] = true;
		}
    }
	public class TailWhipDebuffNPC : GlobalNPC
	{
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {// Only player attacks should benefit from this buff, hence the NPC and trap checks.
            if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
                return;


            // SummonTagDamageMultiplier scales down tag damage for some specific minion and sentry projectiles for balance purposes.
            var projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
            if (npc.HasBuff<TailWhipDebuff>())
            {
                // Apply a flat bonus to every hit
                modifiers.FlatBonusDamage += TailWhipDebuff.TagDamage * projTagMultiplier;
            }
        }
    }
}
