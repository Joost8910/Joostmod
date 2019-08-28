using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class BootCactus : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus");
			Main.projFrames[projectile.type] = 5;
            ProjectileID.Sets.DontAttachHideToAlpha[projectile.type] = true;
        }
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 61;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = 4;
			projectile.timeLeft = 300;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
            projectile.hide = true;
            projectile.aiStyle = 0;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 18;
			height = 56;
			fallThrough = false;
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.ai[0] += projectile.ai[0] < 1 ? 1 : 0;
			return false;
		}
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type != mod.NPCType("Cactus Person") && projectile.ai[0] >= 24 && !target.friendly && (target.damage > 0 || projectile.ai[1] <= 0))
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override void AI()
		{
			projectile.velocity.X = 0;
	        projectile.rotation = 0;
			if (projectile.ai[0] == 6)
			{
				projectile.frame = 1;
			}
			if (projectile.ai[0] == 12)
			{
				projectile.frame = 2;
			}
			if (projectile.ai[0] == 18)
			{
				projectile.frame = 3;
			}
			if (projectile.ai[0] == 24)
			{
				projectile.frame = 4;
			}
			if (projectile.timeLeft < 24)
			{
				projectile.ai[0]--;
			}
			else if (projectile.ai[0] > 0 && projectile.ai[0] < 24)
			{
				projectile.ai[0]++;
			}
		}
	}
}