using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class PowerSpirit : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit of Power");
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 22;
			projectile.height = 22;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			inertia = 15f;
			chaseAccel = 35f;
			chaseDist = 30f;
			spacingMult = 0.75f;
			shoot = mod.ProjectileType("PowerSpiritBlast");
            shootSpeed = 0;
			shootCool = 60f;
            rapidRate = 2;
            noCollide = true;
        }
        public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.dead)
			{
				modPlayer.powerSpirit = false;
			}
			if (modPlayer.powerSpirit)
			{
				projectile.timeLeft = 2;
			}
		}
        public override void SelectFrame(Vector2 dir)
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 160)
			{
				projectile.frameCounter = 0;
            }
            projectile.frame = (projectile.frameCounter / 5) % 4;
            projectile.rotation = 0;
            if (projectile.minionSlots > 7)
            {
                projectile.minionSlots = 7;
            }
            rapidAmount = (int)projectile.minionSlots;
            Lighting.AddLight(projectile.Center, new Vector3(0.35f, 1f, 0.6f) * (projectile.minionSlots * 0.2f) * (1 + ((projectile.localAI[0] - 60) / 100f)));
        }
        public override void ShootEffects()
        {
            projectile.frameCounter++;
            damageMult = 1f + (projectile.minionSlots * 0.05f);
            Main.PlaySound(SoundID.Item8, projectile.Center);
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.HeldItem.type == mod.ItemType("LarkusTome") && !player.noItems && !player.CCed && !player.dead)
            {
                if (player.controlUseTile && player.itemTime > 0 && player.itemTime < 4 && modPlayer.LegendCool <= 0)
                {
                    player.itemTime = 2;
                    player.itemAnimation = 2;
                    if (projectile.localAI[0] <= 300f)
                    {
                        projectile.localAI[0]++;
                    }

                    if (projectile.localAI[0] > 60)
                    {
                        if (Main.myPlayer == projectile.owner)
                        {
                            Vector2 dir = Main.MouseWorld - projectile.Center;
                            dir.Normalize();
                            if (dir.HasNaNs())
                            {
                                dir = Vector2.UnitX * (float)player.direction;
                            }
                            float speed = projectile.Distance(Main.MouseWorld) >= chaseAccel ? chaseAccel : projectile.Distance(Main.MouseWorld);
                            dir *= speed;
                            if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                            {
                                projectile.netUpdate = true;
                            }
                            projectile.velocity = dir;
                        }
                        if (projectile.localAI[0] % 20 == 0)
                        {
                            Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 8, 1.2f, (projectile.localAI[0] / 100f) - 1.5f);
                        }
                        if (projectile.localAI[0] % (int)(12 - (projectile.localAI[0] / 30)) == 0)
                        {
                            projectile.frameCounter++;
                        }
                        CheckActive();
                        SelectFrame(Vector2.Zero);
                        return false;
                    }
                    else
                    {
                        return base.PreAI();
                    }
                }
                else if (projectile.localAI[0] > 60)
                {
                    modPlayer.LegendCool = (int)projectile.localAI[0];
                    projectile.velocity = Vector2.Zero;
                    float scale = 1 + ((projectile.localAI[0] - 60) / 100f);
                    if (rapidAmount <= 1)
                    {
                        if (projectile.localAI[1] == 0f)
                        {
                            if (Main.myPlayer == projectile.owner)
                            {
                                ShootEffects();
                                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, shoot, (int)(projectile.damage * damageMult * scale), projectile.knockBack, Main.myPlayer, scale, 14 + 7 * scale);
                                Main.projectile[proj].netUpdate = true;
                                projectile.netUpdate = true;
                                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 203, scale * 0.3f, 0.2f);
                            }
                            projectile.localAI[0] = 0;
                            projectile.ai[1] = 0f;
                            return base.PreAI();
                        }
                    }
                    else
                    {
                        if (projectile.localAI[1] <= 0)
                        {
                            if (projectile.localAI[1] % (rapidRate * 2) == 0 && Main.myPlayer == projectile.owner)
                            {
                                ShootEffects();
                                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, shoot, (int)(projectile.damage * damageMult * scale), projectile.knockBack, Main.myPlayer, scale, 14 + 7 * scale);
                                Main.projectile[proj].netUpdate = true;
                                projectile.netUpdate = true;
                                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 203, scale * 0.3f, 0.2f);
                            }
                            projectile.localAI[1]--;
                            if (projectile.localAI[1] <= -rapidAmount * rapidRate * 2)
                            {
                                projectile.localAI[0] = 0;
                                projectile.localAI[1] = 0;
                                projectile.ai[1] = 0f;
                                return base.PreAI();
                            }
                        }
                    }
                    projectile.frameCounter++;
                    CheckActive();
                    SelectFrame(Vector2.Zero);
                    return false;
                }
            }
            projectile.localAI[0] = 0;
            projectile.localAI[1] = 0;
            return base.PreAI();
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            if (projectile.localAI[0] > 60)
            {
                float bright = 1 + (projectile.localAI[0] / 300f);
                color = color * bright;
            }

            Texture2D tex = Main.projectileTexture[projectile.type];
            Rectangle rectangle = new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), tex.Width, (tex.Height / Main.projFrames[projectile.type]));

            Texture2D tex2 = mod.GetTexture("Projectiles/Minions/PowerSpirit_T1");
            Rectangle rectangle2 = new Rectangle(0, 0, tex2.Width, (tex2.Height / 8));

            Texture2D tex3 = mod.GetTexture("Projectiles/Minions/PowerSpirit_T2");
            Rectangle rectangle3 = new Rectangle(0, 0, tex3.Width, (tex3.Height / 16));
            
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, tex.Height / 2 - projectile.height / 2), new Rectangle?(rectangle), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            for (int i = 1; i < projectile.minionSlots; i++)
            {
                if (i <= 2)
                {
                    rectangle2.Y = (((projectile.frameCounter / 5) + i * 4) % 8) * rectangle2.Height;
                    sb.Draw(tex2, projectile.Center - Main.screenPosition + new Vector2(0f, tex2.Height / 2 - projectile.height / 2), new Rectangle?(rectangle2), color, projectile.rotation, new Vector2(tex2.Width / 2, tex2.Height / 2), projectile.scale, effects, 0f);
                }
                else
                {
                    int m = (projectile.minionSlots == 5) ? 8 : ((projectile.minionSlots == 6) ? 5 : 4);
                    rectangle3.Y = (((projectile.frameCounter / 2) + i * m) % 16) * rectangle3.Height;
                    sb.Draw(tex3, projectile.Center - Main.screenPosition + new Vector2(0f, tex3.Height / 2 - projectile.height + 1), new Rectangle?(rectangle3), color, projectile.rotation, new Vector2(tex3.Width / 2, tex3.Height / 2), projectile.scale, effects, 0f);
                }
            }


            return false;
        }
    }
}


