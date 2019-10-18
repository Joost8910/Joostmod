using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrognakHammer : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 17;
            projectile.ownerHitCheck = true;
        }
        public override bool? CanHitNPC(NPC target)
		{
            Player player = Main.player[projectile.owner];
			return !target.friendly && (projectile.ai[1] < 226 || player.velocity.Y * player.gravDir > 9);
		}
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
            Player player = Main.player[projectile.owner];
			if (n.active && !n.friendly && !n.dontTakeDamage && n.type != 488 && !n.boss && player.velocity.Y * player.gravDir > 9)
			{
				n.velocity.Y = (knockback + Math.Abs(player.velocity.Y)) * player.gravDir * n.knockBackResist;
			}
		}
        public override bool CanHitPvp(Player target)
        {
            Player player = Main.player[projectile.owner];
            return (projectile.ai[1] < 226 || player.velocity.Y * player.gravDir > 9);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y = (projectile.knockBack + Math.Abs(player.velocity.Y)) * player.gravDir;
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.scale = player.inventory[player.selectedItem].scale;
            projectile.width = (int)(64 * projectile.scale);
            projectile.height = (int)(64 * projectile.scale);
            bool channeling = !player.noItems && !player.CCed;
            if(channeling)
            {
                float scaleFactor6 = 1f;
                if (player.inventory[player.selectedItem].shoot == projectile.type)
                {
                    scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                }
                projectile.velocity.Y = 0;
                projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
                projectile.velocity.X = projectile.direction;
            }
            else
            {
                player.fullRotation = 0f;
                projectile.Kill();
            }
            if (projectile.ai[1] < 1)
            {
                player.velocity.Y = -18f * player.gravDir;
                player.fallStart = (int)(player.position.Y / 16f);
            }
            if (projectile.ai[1] % 51 == 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 7);
            }
            if (projectile.ai[1] < 226)
            {
                projectile.ai[1] += 3;
            }
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.mount.Dismount(player);
            if (player.gravDir == -1)
            {
                projectile.rotation = -(projectile.ai[1]-2) * 0.0174f * 7f * projectile.direction - (float)(1.566 * player.direction);
                projectile.position = (player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f) + new Vector2((float)Math.Cos(projectile.rotation + 1.566 + (2.349 * projectile.direction))*35*projectile.scale, (float)Math.Sin(projectile.rotation + 1.566 + (2.349 * projectile.direction))*35*projectile.scale);
                player.fullRotation = (float)(projectile.rotation - (3.14 * player.direction));
            }
            else
            {
                projectile.rotation = (projectile.ai[1]-2) * 0.0174f * 7f * projectile.direction;
                projectile.position = (player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f) + new Vector2((float)Math.Cos(projectile.rotation + 1.566 + (2.349 * projectile.direction))*35*projectile.scale, (float)Math.Sin(projectile.rotation + 1.566 + (2.349 * projectile.direction))*35*projectile.scale);
		    	player.fullRotation = (float)(projectile.rotation - (1.566 * player.direction));
            }
            player.fullRotationOrigin = player.Center - player.position;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = (int)(player.itemAnimationMax * 0.6);
            player.itemAnimation = (int)(player.itemAnimationMax * 0.6);
            player.itemRotation = 0;
            player.portalPhysicsFlag = true;
            if (projectile.ai[1] >= 226 && player.velocity.Y == 0)
            {
                player.fullRotation = 0f;
                projectile.Kill();
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16.0));
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
		{
            Player player = Main.player[projectile.owner];
            Vector2 posi = new Vector2(player.Center.X + 64 * projectile.direction * projectile.scale, player.position.Y+player.height+4);
			Point pos = posi.ToTileCoordinates();
			Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
			for (int d = 0; d < 80; d++)
			{
				Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-10, 10)*0.3f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -6)*0.4f;
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 70);	
            if (player.gravDir == -1)
            {
                Projectile.NewProjectile(player.Center.X + 48 * projectile.direction * projectile.scale, projectile.Center.Y, projectile.direction * 8f, 0f, mod.ProjectileType("GrogWaveFlipped"), (int)(projectile.damage * 1.33f), projectile.knockBack * 2.5f, projectile.owner);
            }
            else
            {
			    Projectile.NewProjectile(player.Center.X + 48 * projectile.direction * projectile.scale, projectile.Center.Y, projectile.direction * 8f, 0f, mod.ProjectileType("GrogWave"), (int)(projectile.damage * 1.67f), projectile.knockBack * 2.5f, projectile.owner);			
            }
            player.fullRotation = 0f;		
		}
    }
}