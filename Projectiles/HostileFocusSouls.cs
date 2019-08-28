using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HostileFocusSouls : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Focus Souls");
        }
		public override void SetDefaults()
		{
			projectile.width = 104;
			projectile.height = 104;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 114;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.magic = true;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)projectile.ai[0]];
            Vector2 center = host.Center;
            projectile.velocity.X = projectile.direction;
            projectile.velocity.Y = 0;
            projectile.localAI[0]++;
            if (!host.active || host.type != mod.NPCType("Spectre"))
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] == 55)
            {
                Main.PlaySound(42, projectile.Center, 202);
            }
            if (projectile.localAI[0] < 70)
            {
                projectile.alpha -= 3;
            }
            if (projectile.localAI[0] == 70)
            {
                double rot = projectile.rotation + (-35f * (Math.PI / 180));
                Vector2 pos1 = projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);
                rot = projectile.rotation + (35f * (Math.PI / 180));
                Vector2 pos2 = projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = projectile.rotation + (145f * (Math.PI / 180));
                Vector2 pos3 = projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = projectile.rotation + (-145f * (Math.PI / 180));
                Vector2 pos4 = projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);

                Vector2 targetPos = projectile.Center + new Vector2(projectile.direction * 1200, 0);

                Vector2 dir1 = (targetPos - pos1);
                dir1.Normalize();
                Vector2 dir2 = (targetPos - pos2);
                dir2.Normalize();
                Vector2 dir3 = (targetPos - pos3);
                dir3.Normalize();
                Vector2 dir4 = (targetPos - pos4);
                dir4.Normalize();

                Projectile.NewProjectile(pos1, dir1, mod.ProjectileType("HostileFocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner, host.whoAmI);
                Projectile.NewProjectile(pos2, dir2, mod.ProjectileType("HostileFocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner, host.whoAmI);
                Projectile.NewProjectile(pos3, dir3, mod.ProjectileType("HostileFocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner, host.whoAmI);
                Projectile.NewProjectile(pos4, dir4, mod.ProjectileType("HostileFocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner, host.whoAmI);
            }
            if (projectile.localAI[0] == 100)
            {
                Main.PlaySound(42, projectile.Center, 217);
                Main.PlaySound(42, projectile.Center, 222);
                Main.PlaySound(42, projectile.Center, 44);
            }
            if (projectile.timeLeft <= 10)
            {
                projectile.alpha += 16;
            }
            if (projectile.localAI[0] < 70)
            {
                projectile.direction = host.direction;
                projectile.spriteDirection = projectile.direction;
                projectile.velocity.X = projectile.direction;
                projectile.position = (center - projectile.velocity) - projectile.Size / 2f + new Vector2(host.direction * 60, 0);
                projectile.localAI[1] = projectile.velocity.ToRotation() + 1.57f;
                projectile.timeLeft = 130;
            }
            else
            {
                projectile.velocity = Vector2.Zero;
            }
            projectile.rotation = projectile.localAI[1];
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Color.White * (1f - (projectile.alpha / 255f));
            color.A = 150;
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return false;
        }
    }
}
