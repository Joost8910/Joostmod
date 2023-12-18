using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class Gnome : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome");
			Main.projFrames[Projectile.type] = 7;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 14;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			inertia = 8f;
			shoot = ModContent.ProjectileType<GnomeSpear>();
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
            Projectile.rotation = 0;
            Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 0, Color.Red, 0.7f);
		}
		public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.Gnome = false;
			}
			if (modPlayer.Gnome)
			{
				Projectile.timeLeft = 2;
			}
		}
        public override void ShootEffects()
        {
            shootAI0 = Projectile.whoAmI;
        }
        public override void SelectFrame(Vector2 tPos)
        {
			Projectile.frameCounter++;
			if (Projectile.frameCounter * Math.Abs(Projectile.velocity.X) >= 16 && Projectile.ai[0] != 1f && Math.Abs(Projectile.velocity.X) > 0.1f)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 7;
			}
			if (Projectile.ai[0] == 1f)
			{
				Projectile.frame = 6;
			}
			else if (Math.Abs(Projectile.velocity.X) <= 0.1f)
			{
				Projectile.frame = 1;
			}
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)Projectile.localAI[0]);
            writer.Write((short)Projectile.localAI[1]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadInt16();
            Projectile.localAI[1] = reader.ReadInt16();
        }
        public override bool PreAI()
        {
            int bashTime = 10;
            int numPrev = 0;
            int numNext = 0;
            int prevGnomeIndex = -1;
            for (int k = 0; k < Projectile.whoAmI; k++)
            {
                if (Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
                {
                    prevGnomeIndex = k;
                    numPrev++;
                }
            }
            for (int k = Projectile.whoAmI; k < Main.maxProjectiles; k++)
            {
                if (Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
                {
                    numNext++;
                }
            }
            Projectile.localAI[0] = 0;
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type == Mod.Find<ModItem>("GnomeStaff").Type && !player.noItems && !player.CCed && !player.dead)
            {
                if (player.controlUseTile && player.itemTime > 0 && player.itemTime < 5)
                {
                    player.itemTime = 2;
                    player.itemAnimation = 2;
                    Projectile.localAI[0] = 1;
                    if (Main.LocalPlayer == player && Main.mouseLeft && (int)Projectile.localAI[1] == 0)
                    {
                        Projectile.localAI[1] = shootCool + bashTime + numPrev;
                        Projectile.velocity.X = -Projectile.spriteDirection * player.moveSpeed * 15;
                        Projectile.netUpdate = true;
                    }
                }
            }
            if ((int)Projectile.localAI[0] > 0 && (int)Projectile.localAI[1] >= 0 && (int)Projectile.localAI[1] <= shootCool)
            {
                SelectFrame(Vector2.Zero);
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 mousePos = Main.MouseWorld + player.velocity + new Vector2(((10 + numPrev * Projectile.width * spacingMult) * -player.direction), 0);
                    Vector2 dir = mousePos - Projectile.Center;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * player.direction;
                    }
                    player.direction = Math.Sign(Main.MouseWorld.X - player.Center.X);
                    float speed = Math.Max(chaseAccel, player.maxRunSpeed);
                    speed = Projectile.Distance(mousePos) >= speed ? (Projectile.Distance(mousePos) >= speed * 30 ? Projectile.Distance(mousePos) / 30 : speed) : Projectile.Distance(mousePos);
                    dir *= speed;
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    bool justJumped = false;
                    if (dir.X != 0 && Projectile.velocity.X == 0f && Projectile.velocity.Y >= 0f)
                    {
                        Projectile.velocity.Y -= 3f;
                        justJumped = true;
                    }
                    Projectile.velocity.X = dir.X + player.velocity.X;
                    Vector2 ground = mousePos;
                    for (int i = 0; i < 100; i++)
                    {
                        Point tilePos = ground.ToTileCoordinates();
                        Tile tile = Main.tile[tilePos.X, tilePos.Y];
                        if (tile.HasTile && !tile.IsActuated && Main.tileSolid[tile.TileType])
                        {
                            ground = tilePos.ToWorldCoordinates();
                        }
                        else
                        {
                            ground.Y += 16;
                        }
                    }
                    if (mousePos.Y > Projectile.position.Y + Projectile.height)
                    {
                        fallThroughPlat = true;
                    }
                    else
                    {
                        fallThroughPlat = false;
                    }
                    if (prevGnomeIndex > 0 && mousePos.Y < ground.Y - 16 - Projectile.height * numPrev && (Math.Abs(Projectile.Center.X - mousePos.X) < 20 || Math.Abs(Projectile.Center.X - Main.projectile[prevGnomeIndex].Center.X) < 20))
                    {
                        Projectile prevGnome = Main.projectile[prevGnomeIndex];
                        Projectile.position.X = prevGnome.position.X;
                        Projectile.position.Y = prevGnome.position.Y - Projectile.height;
                        Projectile.velocity = Vector2.Zero;
                        Projectile.frame = 0;
                    }
                    else if (Main.MouseWorld.Y < Projectile.position.Y - Projectile.height * numNext && (Projectile.velocity.Y == 0 || justJumped))
                    {
                        Projectile.velocity.Y = -(float)Math.Sqrt(2 * 0.25f * Math.Abs(Projectile.Center.Y - (Main.MouseWorld.Y - 20 + Projectile.height * numNext)));
                    }
                    Projectile.direction = Math.Sign(Projectile.Center.X - player.Center.X);
                    if (!Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, mousePos, 1, 1))
                    {
                        if (Projectile.velocity.Y < 0 && mousePos.Y < Projectile.position.Y)
                        {
                            Projectile.tileCollide = false;
                        }
                        else
                        {
                            Projectile.tileCollide = true;
                        }
                        if (Projectile.velocity.Y >= 0 && mousePos.Y > Projectile.position.Y + Projectile.height + 8)
                        {
                            Projectile.position.Y++;
                        }
                    }
                    else
                    {
                        Projectile.tileCollide = true;
                    }
                }
                Projectile.spriteDirection = -Projectile.direction;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    float colPoint = 0;
                    if (proj.active && proj.hostile && Collision.CheckAABBvLineCollision(Projectile.position, Projectile.Size, proj.Center, proj.Center + proj.velocity * (proj.extraUpdates + 1), (proj.width + proj.height) / 2, ref colPoint) /*proj.getRect().Intersects(projectile.getRect())*/)
                    {
                        if (proj.damage <= Projectile.damage && (proj.aiStyle == 1 || proj.aiStyle == 2 || proj.aiStyle == 8 || proj.aiStyle == 21 || proj.aiStyle == 24 || proj.aiStyle == 28 || proj.aiStyle == 29 || proj.aiStyle == 131))
                        {
                            proj.Kill();
                            if (Math.Sign(proj.velocity.X) != Projectile.direction)
                            {
                                SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.Center);
                            }
                            else
                            {
                                Projectile.velocity = new Vector2(proj.velocity.X, -5);
                                Projectile.localAI[1] = -shootCool;
                                SoundEngine.PlaySound(SoundID.NPCHit4.WithPitchOffset(-0.15f), Projectile.Center);
                            }
                        }
                        else
                        {
                            Projectile.velocity = new Vector2(proj.velocity.X, -5);
                            Projectile.localAI[1] = -shootCool;
                            SoundEngine.PlaySound(SoundID.NPCHit4.WithPitchOffset(-0.3f), Projectile.Center);
                        }
                    }
                }
            }
            if ((int)Projectile.localAI[1] < 0)
            {
                Projectile.tileCollide = true;
                Projectile.localAI[1]++;
            }
            if ((int)Projectile.localAI[1] > 0)
            {
                Projectile.tileCollide = true;
                Projectile.localAI[1]--;
                if (player.controlUseTile)
                {
                    player.itemTime = 4;
                    player.itemAnimation = 4;
                }
                if ((int)Projectile.localAI[1] == shootCool + bashTime)
                {
                    SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_swing_0") with { Volume = 0.9f, Pitch = 1.3f}, Projectile.Center); // 213
                }
            }
            if ((int)Projectile.localAI[1] != 0 || Projectile.localAI[0] > 0)
            {
                Projectile.localAI[0] = 1;
                Projectile.velocity.Y += 0.25f;
                CheckActive();
                return false;
            }
            return base.PreAI();
        }
        public override bool MinionContactDamage()
        {
            return Projectile.localAI[0] == 1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Math.Sign(Projectile.Center.X - target.Center.X) == Projectile.direction)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            hitDirection = Projectile.direction;
            if (Projectile.localAI[1] <= shootCool)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            float KB = Projectile.knockBack;
            if (Projectile.localAI[1] <= shootCool)
            {
                knockback = 0;
                crit = false;

                for (int i = 0; i < 100; i++)
                {
                    if (Main.combatText[i].active && Main.combatText[i].color == CombatText.DamagedHostile && Main.combatText[i].text == damage.ToString() && Projectile.Distance(Main.combatText[i].position) < 250)
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
                target.velocity.X = -Projectile.spriteDirection * (KB + Math.Abs(Projectile.velocity.X));
                target.velocity.Y--;
                if (!target.noTileCollide)
                {
                    float push = Projectile.Center.X + 16;
                    if (Projectile.direction < 0)
                    {
                        push = (Projectile.Center.X - 16) - target.width;
                    }
                    Vector2 pos = target.position;
                    pos.X = push + Projectile.velocity.X;
                    if (Collision.SolidCollision(pos, target.width, target.height))
                    {
                        Projectile.velocity.X = -Projectile.direction * KB;
                        Projectile.localAI[1] = 0;
                    }
                }
                if (Main.netMode != 0)
                {
                    ModPacket packet = Mod.GetPacket();
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
                if ((target.velocity.X < 0 && Projectile.direction > 0) || (target.velocity.X > 0 && Projectile.direction < 0))
                {
                    Projectile.velocity.X = -Projectile.direction * (KB + Math.Abs(target.velocity.X));
                }
                else
                {
                    Projectile.velocity.X = -Projectile.direction * KB;
                }
                Projectile.localAI[1] = shootCool;
            }
            SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.Center);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rectangle = new Rectangle(0, Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), tex.Width, (tex.Height / Main.projFrames[Projectile.type]));

            float yOff = Projectile.height - rectangle.Height + 4;
            float armYOff = yOff + ((Projectile.frame == 2 || Projectile.frame == 6) ? -6 : -4);
            Texture2D armTex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/Minions/Gnome_Arm");
            int armFrame = 0;
            Rectangle armRect = new Rectangle(0, armFrame * armTex.Height / 6, armTex.Width, armTex.Height / 6);

            Texture2D shieldTex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/Minions/Gnome_Shield");
            int shieldFrame = (int)Projectile.localAI[0];
            Rectangle shieldRect = new Rectangle(0, shieldFrame * shieldTex.Height / 2, shieldTex.Width, shieldTex.Height / 2);

            if (shieldFrame == 0)
            {
                Main.EntitySpriteDraw(shieldTex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.height / 2 + armYOff + shieldRect.Height / 2 - 5), new Rectangle?(shieldRect), lightColor, Projectile.rotation, new Vector2(shieldTex.Width * 0.5f, shieldTex.Height * 0.5f), Projectile.scale, effects, 0);
                Main.EntitySpriteDraw(armTex, Projectile.Center - Main.screenPosition + new Vector2(10 * -Projectile.spriteDirection, tex.Height / 2 - Projectile.height + armYOff), new Rectangle?(armRect), lightColor, Projectile.rotation, new Vector2(armTex.Width * 0.5f, armTex.Height * 0.5f), Projectile.scale, effects, 0);
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, tex.Height / 2 - Projectile.height / 2 + yOff), new Rectangle?(rectangle), lightColor, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            }
            else
            {
                Main.EntitySpriteDraw(armTex, Projectile.Center - Main.screenPosition + new Vector2(10 * -Projectile.spriteDirection, tex.Height / 2 - Projectile.height + armYOff), new Rectangle?(armRect), lightColor, Projectile.rotation, new Vector2(armTex.Width * 0.5f, armTex.Height * 0.5f), Projectile.scale, effects, 0);
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, tex.Height / 2 - Projectile.height / 2 + yOff), new Rectangle?(rectangle), lightColor, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
                Main.EntitySpriteDraw(shieldTex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.height / 2 + armYOff + shieldRect.Height / 2 - 5), new Rectangle?(shieldRect), lightColor, Projectile.rotation, new Vector2(shieldTex.Width * 0.5f, shieldTex.Height * 0.5f), Projectile.scale, effects, 0);
            }
            armFrame = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.type == shoot && (int)p.ai[0] == Projectile.whoAmI)
                {
                    if (p.Center.Y + 9 < Projectile.Center.Y - 10)
                    {
                        armFrame = 3;
                    }
                    else if (p.Center.Y + 9 > Projectile.Center.Y + 10)
                    {
                        armFrame = 5;
                    }
                    else if (p.direction != Projectile.direction || p.timeLeft > 13)
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
            Main.EntitySpriteDraw(armTex, Projectile.Center - Main.screenPosition + new Vector2(0, tex.Height / 2 - Projectile.height + armYOff), new Rectangle?(armRect), lightColor, Projectile.rotation, new Vector2(armTex.Width * 0.5f, armTex.Height * 0.5f), Projectile.scale, effects, 0);

            return false;
        }
    }
}


