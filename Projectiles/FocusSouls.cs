using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class FocusSouls : ModProjectile
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
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 114;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.magic = true;
        }
        public override void AI()
        {
	        Player player = Main.player[projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.velocity = Vector2.Zero;
            projectile.localAI[0]++;
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    //projectile.ai[0]++;
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - projectile.Center;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector13;
                    projectile.netUpdate = true;
                }
            }
            if (projectile.localAI[0] == 15)
            {
                Main.PlaySound(42, projectile.Center, 202);
            }
            if (projectile.localAI[0] < 30)
            {
                projectile.alpha -= 6;
            }
            if (projectile.localAI[0] > 30 && projectile.localAI[0] < 33 && player.channel && !player.noItems && !player.CCed)
            {
                projectile.localAI[0] = 31;
            }
            if (projectile.localAI[0] == 33)
            {
                Main.PlaySound(42, projectile.Center, 164);
                double rot = projectile.rotation + (-35f * (Math.PI / 180));
                Vector2 pos1 = projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);
                rot = projectile.rotation + (35f * (Math.PI / 180));
                Vector2 pos2 = projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = projectile.rotation + (145f * (Math.PI / 180));
                Vector2 pos3 = projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = projectile.rotation + (-145f * (Math.PI / 180));
                Vector2 pos4 = projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);

                Vector2 mousePos = Main.MouseWorld;
                if (Main.myPlayer == projectile.owner)
                {
                    mousePos = Main.MouseWorld;
                }

                Vector2 dir1 = (mousePos - pos1);
                dir1.Normalize();
                Vector2 dir2 = (mousePos - pos2);
                dir2.Normalize();
                Vector2 dir3 = (mousePos - pos3);
                dir3.Normalize();
                Vector2 dir4 = (mousePos - pos4);
                dir4.Normalize();

                Projectile.NewProjectile(pos1, dir1, mod.ProjectileType("FocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner);
                Projectile.NewProjectile(pos2, dir2, mod.ProjectileType("FocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner);
                Projectile.NewProjectile(pos3, dir3, mod.ProjectileType("FocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner);
                Projectile.NewProjectile(pos4, dir4, mod.ProjectileType("FocusSoulBeam"), projectile.damage, projectile.knockBack, projectile.owner);
                projectile.netUpdate = true;
            }
            if (projectile.localAI[0] == 60)
            {
                Main.PlaySound(42, projectile.Center, 217);
                Main.PlaySound(42, projectile.Center, 222);
                Main.PlaySound(42, projectile.Center, 44);
            }
            if (projectile.timeLeft <= 10)
            {
                projectile.alpha += 16;
            }
            if (projectile.localAI[0] < 33)
            {
                projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
                projectile.spriteDirection = projectile.direction;
                projectile.position = (center - projectile.velocity) - projectile.Size / 2f + new Vector2(0, -50 * player.gravDir);
                projectile.localAI[1] = projectile.velocity.ToRotation() + 1.57f;
                player.ChangeDir(projectile.direction);
                player.heldProj = projectile.whoAmI;
                player.itemTime = 10;
                player.itemAnimation = 10;
                projectile.timeLeft = 130;
            }
            else
            {
                projectile.velocity = Vector2.Zero;
            }
            projectile.rotation = projectile.localAI[1];
        }
        public override bool CanHitPvp(Player target)
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
            //color.A = (byte)projectile.alpha;
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return false;
        }
    }
}
