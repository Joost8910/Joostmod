using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class Gnome : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome");
			Main.projFrames[projectile.type] = 7;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 14;
			projectile.height = 30;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			inertia = 8f;
			shoot = mod.ProjectileType("GnomeSpear");
            chaseDist = 30;
			shootSpeed = 7.5f;
			spacingMult = 0.75f;
			shootCool = 40f;
            grounded = true;
            jump = true;
            predict = true;
            maxShootDist = 400;
            chaseAccel = 12;
        }
		public override void FlyingDust()
        {
            projectile.rotation = 0;
            Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 0, Color.Red, 0.7f);
		}
		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.Gnome = false;
			}
			if (modPlayer.Gnome)
			{
				projectile.timeLeft = 2;
			}
		}
        public override void ShootEffects()
        {
            shootAI0 = projectile.whoAmI;
        }
        public override void SelectFrame(Vector2 tPos)
        {
			projectile.frameCounter++;
			if (projectile.frameCounter * Math.Abs(projectile.velocity.X) >= 16 && projectile.ai[0] != 1f && Math.Abs(projectile.velocity.X) > 0.1f)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 7;
			}
			if (projectile.ai[0] == 1f)
			{
				projectile.frame = 6;
			}
			else if (Math.Abs(projectile.velocity.X) <= 0.1f)
			{
				projectile.frame = 1;
			}
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)projectile.localAI[0]);
            writer.Write((short)projectile.localAI[1]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            projectile.localAI[0] = reader.ReadInt16();
            projectile.localAI[1] = reader.ReadInt16();
        }
        public override bool PreAI()
        {
            int bashTime = 10;
            int numPrev = 0;
            int numNext = 0;
            int prevGnomeIndex = -1;
            for (int k = 0; k < projectile.whoAmI; k++)
            {
                if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type)
                {
                    prevGnomeIndex = k;
                    numPrev++;
                }
            }
            for (int k = projectile.whoAmI; k < Main.maxProjectiles; k++)
            {
                if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type)
                {
                    numNext++;
                }
            }
            projectile.localAI[0] = 0;
            Player player = Main.player[projectile.owner];
            if (player.HeldItem.type == mod.ItemType("GnomeStaff") && !player.noItems && !player.CCed && !player.dead)
            {
                if (player.controlUseTile && player.itemTime > 0 && player.itemTime < 5)
                {
                    player.itemTime = 2;
                    player.itemAnimation = 2;
                    projectile.localAI[0] = 1;
                    if (Main.LocalPlayer == player && Main.mouseLeft && (int)projectile.localAI[1] == 0)
                    {
                        projectile.localAI[1] = shootCool + bashTime + numPrev;
                        projectile.velocity.X = -projectile.spriteDirection * player.moveSpeed * 15;
                        projectile.netUpdate = true;
                    }
                }
            }
            if ((int)projectile.localAI[0] > 0 && (int)projectile.localAI[1] >= 0 && (int)projectile.localAI[1] <= shootCool)
            {
                SelectFrame(Vector2.Zero);
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 mousePos = Main.MouseWorld + player.velocity + new Vector2(((10 + numPrev * projectile.width * spacingMult) * -player.direction), 0);
                    Vector2 dir = mousePos - projectile.Center;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * player.direction;
                    }
                    player.direction = Math.Sign(Main.MouseWorld.X - player.Center.X);
                    float speed = Math.Max(chaseAccel, player.maxRunSpeed);
                    speed = projectile.Distance(mousePos) >= speed ? (projectile.Distance(mousePos) >= speed * 30 ? projectile.Distance(mousePos) / 30 : speed) : projectile.Distance(mousePos);
                    dir *= speed;
                    if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    bool justJumped = false;
                    if (dir.X != 0 && projectile.velocity.X == 0f && projectile.velocity.Y >= 0f)
                    {
                        projectile.velocity.Y -= 3f;
                        justJumped = true;
                    }
                    projectile.velocity.X = dir.X + player.velocity.X;
                    Vector2 ground = mousePos;
                    for (int i = 0; i < 100; i++)
                    {
                        Point tilePos = ground.ToTileCoordinates();
                        Tile tile = Main.tile[tilePos.X, tilePos.Y];
                        if (tile.active() && !tile.inActive() && Main.tileSolid[tile.type])
                        {
                            ground = tilePos.ToWorldCoordinates();
                        }
                        else
                        {
                            ground.Y += 16;
                        }
                    }
                    if (mousePos.Y > projectile.position.Y + projectile.height)
                    {
                        fallThroughPlat = true;
                    }
                    else
                    {
                        fallThroughPlat = false;
                    }
                    if (prevGnomeIndex > 0 && mousePos.Y < ground.Y - 16 - projectile.height * numPrev && (Math.Abs(projectile.Center.X - mousePos.X) < 20 || Math.Abs(projectile.Center.X - Main.projectile[prevGnomeIndex].Center.X) < 20))
                    {
                        Projectile prevGnome = Main.projectile[prevGnomeIndex];
                        projectile.position.X = prevGnome.position.X;
                        projectile.position.Y = prevGnome.position.Y - projectile.height;
                        projectile.velocity = Vector2.Zero;
                        projectile.frame = 0;
                    }
                    else if (Main.MouseWorld.Y < projectile.position.Y - projectile.height * numNext && (projectile.velocity.Y == 0 || justJumped))
                    {
                        projectile.velocity.Y = -(float)Math.Sqrt(2 * 0.25f * Math.Abs(projectile.Center.Y - (Main.MouseWorld.Y - 20 + projectile.height * numNext)));
                    }
                    projectile.direction = Math.Sign(projectile.Center.X - player.Center.X);
                    if (!Collision.CanHitLine(projectile.position, projectile.width, projectile.height, mousePos, 1, 1))
                    {
                        if (projectile.velocity.Y < 0 && mousePos.Y < projectile.position.Y)
                        {
                            projectile.tileCollide = false;
                        }
                        else
                        {
                            projectile.tileCollide = true;
                        }
                        if (projectile.velocity.Y >= 0 && mousePos.Y > projectile.position.Y + projectile.height + 8)
                        {
                            projectile.position.Y++;
                        }
                    }
                    else
                    {
                        projectile.tileCollide = true;
                    }
                }
                projectile.spriteDirection = -projectile.direction;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    float colPoint = 0;
                    if (proj.active && proj.hostile && Collision.CheckAABBvLineCollision(projectile.position, projectile.Size, proj.Center, proj.Center + proj.velocity * (proj.extraUpdates + 1), (proj.width + proj.height) / 2, ref colPoint) /*proj.getRect().Intersects(projectile.getRect())*/)
                    {
                        if (proj.damage <= projectile.damage && (proj.aiStyle == 1 || proj.aiStyle == 2 || proj.aiStyle == 8 || proj.aiStyle == 21 || proj.aiStyle == 24 || proj.aiStyle == 28 || proj.aiStyle == 29 || proj.aiStyle == 131))
                        {
                            proj.Kill();
                            if (Math.Sign(proj.velocity.X) != projectile.direction)
                            {
                                Main.PlaySound(3, (int)projectile.Center.X, (int)projectile.Center.Y, 4);
                            }
                            else
                            {
                                projectile.velocity = new Vector2(proj.velocity.X, -5);
                                projectile.localAI[1] = -shootCool;
                                Main.PlaySound(3, (int)projectile.Center.X, (int)projectile.Center.Y, 4, 1, -0.15f);
                            }
                        }
                        else
                        {
                            projectile.velocity = new Vector2(proj.velocity.X, -5);
                            projectile.localAI[1] = -shootCool;
                            Main.PlaySound(3, (int)projectile.Center.X, (int)projectile.Center.Y, 4, 1, -0.3f);
                        }
                    }
                }
            }
            if ((int)projectile.localAI[1] < 0)
            {
                projectile.tileCollide = true;
                projectile.localAI[1]++;
            }
            if ((int)projectile.localAI[1] > 0)
            {
                projectile.tileCollide = true;
                projectile.localAI[1]--;
                if (player.controlUseTile)
                {
                    player.itemTime = 4;
                    player.itemAnimation = 4;
                }
                if ((int)projectile.localAI[1] == shootCool + bashTime)
                {
                    Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 213, 0.9f, 0.3f);
                }
            }
            if ((int)projectile.localAI[1] != 0 || projectile.localAI[0] > 0)
            {
                projectile.localAI[0] = 1;
                projectile.velocity.Y += 0.25f;
                CheckActive();
                return false;
            }
            return base.PreAI();
        }
        public override bool MinionContactDamage()
        {
            return projectile.localAI[0] == 1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Math.Sign(projectile.Center.X - target.Center.X) == projectile.direction)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            hitDirection = projectile.direction;
            if (projectile.localAI[1] <= shootCool)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            float KB = projectile.knockBack;
            if (projectile.localAI[1] <= shootCool)
            {
                knockback = 0;
                crit = false;

                for (int i = 0; i < 100; i++)
                {
                    if (Main.combatText[i].active && Main.combatText[i].color == CombatText.DamagedHostile && Main.combatText[i].text == damage.ToString() && projectile.Distance(Main.combatText[i].position) < 250)
                    {
                        Main.combatText[i].active = false;
                        break;
                    }
                }
                //CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkGreen, "BLOCKED", true, false);
                if (target.life < target.lifeMax)
                {
                    target.life++;
                }
            }
            else
            {
                KB *= 2;
            }
            if (target.knockBackResist > 0)
            {
                target.velocity.X = -projectile.spriteDirection * (KB + Math.Abs(projectile.velocity.X));
                target.velocity.Y--;
                if (!target.noTileCollide)
                {
                    float push = projectile.Center.X + 16;
                    if (projectile.direction < 0)
                    {
                        push = (projectile.Center.X - 16) - target.width;
                    }
                    Vector2 pos = target.position;
                    pos.X = push + projectile.velocity.X;
                    if (Collision.SolidCollision(pos, target.width, target.height))
                    {
                        projectile.velocity.X = -projectile.direction * KB;
                        projectile.localAI[1] = 0;
                    }
                }
                if (Main.netMode != 0)
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write((byte)JoostModMessageType.NPCpos);
                    packet.Write(target.whoAmI);
                    packet.WriteVector2(target.position);
                    packet.WriteVector2(target.velocity);
                    ModPacket netMessage = packet;
                    netMessage.Send();
                }
            }
            else
            {
                if ((target.velocity.X < 0 && projectile.direction > 0) || (target.velocity.X > 0 && projectile.direction < 0))
                {
                    projectile.velocity.X = -projectile.direction * (KB + Math.Abs(target.velocity.X));
                }
                else
                {
                    projectile.velocity.X = -projectile.direction * KB;
                }
                projectile.localAI[1] = shootCool;
            }
            Main.PlaySound(3, (int)projectile.Center.X, (int)projectile.Center.Y, 4, 1);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Texture2D tex = Main.projectileTexture[projectile.type];
            Rectangle rectangle = new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), tex.Width, (tex.Height / Main.projFrames[projectile.type]));

            float yOff = projectile.height - rectangle.Height + 4;
            float armYOff = yOff + ((projectile.frame == 2 || projectile.frame == 6) ? -6 : -4);
            Texture2D armTex = ModContent.GetTexture("JoostMod/Projectiles/Minions/Gnome_Arm");
            int armFrame = 0;
            Rectangle armRect = new Rectangle(0, armFrame * armTex.Height / 6, armTex.Width, armTex.Height / 6);

            Texture2D shieldTex = ModContent.GetTexture("JoostMod/Projectiles/Minions/Gnome_Shield");
            int shieldFrame = (int)projectile.localAI[0];
            Rectangle shieldRect = new Rectangle(0, shieldFrame * shieldTex.Height / 2, shieldTex.Width, shieldTex.Height / 2);

            if (shieldFrame == 0)
            {
                spriteBatch.Draw(shieldTex, projectile.Center - Main.screenPosition + new Vector2(0, projectile.height / 2 + armYOff + shieldRect.Height / 2 - 5), new Rectangle?(shieldRect), lightColor, projectile.rotation, new Vector2(shieldTex.Width * 0.5f, shieldTex.Height * 0.5f), projectile.scale, effects, 0f);
                spriteBatch.Draw(armTex, projectile.Center - Main.screenPosition + new Vector2(10 * -projectile.spriteDirection, tex.Height / 2 - projectile.height + armYOff), new Rectangle?(armRect), lightColor, projectile.rotation, new Vector2(armTex.Width * 0.5f, armTex.Height * 0.5f), projectile.scale, effects, 0f);
                spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, tex.Height / 2 - projectile.height / 2 + yOff), new Rectangle?(rectangle), lightColor, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            }
            else
            {
                spriteBatch.Draw(armTex, projectile.Center - Main.screenPosition + new Vector2(10 * -projectile.spriteDirection, tex.Height / 2 - projectile.height + armYOff), new Rectangle?(armRect), lightColor, projectile.rotation, new Vector2(armTex.Width * 0.5f, armTex.Height * 0.5f), projectile.scale, effects, 0f);
                spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, tex.Height / 2 - projectile.height / 2 + yOff), new Rectangle?(rectangle), lightColor, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
                spriteBatch.Draw(shieldTex, projectile.Center - Main.screenPosition + new Vector2(0, projectile.height / 2 + armYOff + shieldRect.Height / 2 - 5), new Rectangle?(shieldRect), lightColor, projectile.rotation, new Vector2(shieldTex.Width * 0.5f, shieldTex.Height * 0.5f), projectile.scale, effects, 0f);
            }
            armFrame = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.type == shoot && (int)p.ai[0] == projectile.whoAmI)
                {
                    if (p.Center.Y + 9 < projectile.Center.Y - 10)
                    {
                        armFrame = 3;
                    }
                    else if (p.Center.Y + 9 > projectile.Center.Y + 10)
                    {
                        armFrame = 5;
                    }
                    else if (p.direction != projectile.direction || p.timeLeft > 13)
                    {
                        armFrame = 1;
                    }
                    else
                    {
                        armFrame = 4;
                    }
                }
            }
            armRect = new Rectangle(0, armFrame * armTex.Height / 6, armTex.Width, armTex.Height / 6);
            spriteBatch.Draw(armTex, projectile.Center - Main.screenPosition + new Vector2(0f, tex.Height / 2 - projectile.height + armYOff), new Rectangle?(armRect), lightColor, projectile.rotation, new Vector2(armTex.Width * 0.5f, armTex.Height * 0.5f), projectile.scale, effects, 0f);

            return false;
        }
    }
}


