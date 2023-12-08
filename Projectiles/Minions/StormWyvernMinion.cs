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
	public class StormWyvernMinion : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Wyvern");
			Main.projFrames[Projectile.type] = 5;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targetting
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            float mult = 1f;
            if (player.ownedProjectileCounts[Projectile.type] > 4)
            {
                mult = (player.ownedProjectileCounts[Projectile.type] - 3) * 4 / player.ownedProjectileCounts[Projectile.type];
            }
            damage = (int)(damage * mult);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[Projectile.owner];
            float mult = 1f;
            if (player.ownedProjectileCounts[Projectile.type] > 4)
            {
                mult = (player.ownedProjectileCounts[Projectile.type] - 3) * 4 / player.ownedProjectileCounts[Projectile.type];
            }
            damage = (int)(damage * mult);
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
        int aimWindow = 30;
        public override bool PreAI()
        {
            //CheckActive()
            Player player = Main.player[Projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.dead)
            {
                modPlayer.stormWyvernMinion = false;
            }
            if (modPlayer.stormWyvernMinion)
            {
                Projectile.timeLeft = 2;
            }

            Projectile.frame = (int)Projectile.ai[0];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.ai[0] == 0)
            {
                bool foundWings = false;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == Projectile.owner && p.minion)
                    {
                        if (i != Projectile.whoAmI && p.type == Projectile.type)
                        {
                            if (p.ai[0] == 0)
                            {
                                p.Kill();
                            }
                            if (p.ai[0] == 1)
                            {
                                foundWings = true;
                            }
                        }
                    }
                }
                if (!foundWings)
                {
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 1, Projectile.identity).minionSlots = 0;
                    Projectile.netUpdate = true;
                    /*
                    int latestProj = projectile.whoAmI;
                    for (int i = 1; i < 4; i++)
                    {
                        latestProj = Projectile.NewProjectile(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockback, projectile.owner, i, latestProj);
                        Main.projectile[latestProj].minionSlots = 0;
                        projectile.netUpdate = true;
                    }
                    */
                }

                if (Projectile.velocity.X > 0f)
                {
                    Projectile.spriteDirection = (Projectile.direction = -1);
                }
                else if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = (Projectile.direction = 1);
                }
                if (Projectile.localAI[0] < 0)
                {
                    Projectile.localAI[0]++;
                    if (Projectile.localAI[0] == 0)
                    {
                        SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
                        Dust.NewDustPerfect(Projectile.Center, 178, new Vector2(-Projectile.direction, -3), 0, Color.Yellow, 2);
                    }
                }
                int max = player.ownedProjectileCounts[Projectile.type] * 24;
                bool channeling = Projectile.localAI[0] >= 0 && (player.controlUseTile || Projectile.localAI[0] >= max) && player.HeldItem.shoot == Projectile.type && player.itemTime > 0 && player.itemTime < 4 && !player.noItems && !player.CCed;
                if (Main.myPlayer == Projectile.owner && channeling)
                {
                    float chargeSpeed = 12.5f;
                    player.itemAnimation = 2;
                    player.itemTime = 2;
                    Vector2 dir = Main.MouseWorld - Projectile.Center;
                    if (!player.controlUseTile)
                    {
                        if (player.HasMinionAttackTargetNPC)
                        {
                            dir = Main.npc[player.MinionAttackTargetNPC].Center - Projectile.Center;
                        }
                        else
                        {
                            dir = (Projectile.rotation - 1.57f).ToRotationVector2();
                        }
                    }
                    Vector2 playerDir = player.DirectionTo(Main.MouseWorld);
                    int pDir = 0;
                    if (playerDir.X < 0)
                    {
                        pDir = -1;
                    }
                    if (playerDir.X > 0)
                    {
                        pDir = 1;
                    }
                    player.ChangeDir(pDir);

                    if (Projectile.localAI[0] > 0 || dir.Length() < 150)
                    {
                        Projectile.localAI[0]++;
                    }

                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 55, 0, 0, 0, default(Color), 0.5f);
                    
                    if (Projectile.localAI[0] == 12 || Projectile.localAI[0] == 24)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_lightning_aura_zap_1"), Projectile.Center); // 21
                    }
                    if (Projectile.localAI[0] > 0 && Projectile.localAI[0] == max - 115)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sky_dragons_fury_circle_1"), Projectile.Center); // 224
                    }
                    if (Projectile.localAI[0] < max - 12)
                    {
                        dir.X += pDir * (float)(100 * Math.Sin(MathHelper.ToRadians(Projectile.localAI[0] * 4)));
                        dir.Y += (float)(100 * Math.Cos(MathHelper.ToRadians(Projectile.localAI[0] * 4)));
                        float length = chargeSpeed / dir.Length();
                        dir *= length;
                        if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                        {
                            Projectile.netUpdate = true;
                        }
                        Projectile.velocity.X = (Projectile.velocity.X * 20 + dir.X) / 21;
                        Projectile.velocity.Y = (Projectile.velocity.Y * 20 + dir.Y) / 21;
                    }
                    else if (Projectile.localAI[0] < max)
                    {
                        float length = chargeSpeed / dir.Length();
                        dir *= length;
                        if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                        {
                            Projectile.netUpdate = true;
                        }
                        Projectile.velocity = dir;
                    }
                    if (Projectile.localAI[0] == max)
                    {
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_sky_dragons_fury_shot_0") with { Pitch = 0.7f }, Projectile.Center); // 226
                    }
                    if (Projectile.localAI[0] >= max && Projectile.localAI[0] < max+ aimWindow)
                    {
                        dir.Normalize();
                        Projectile.velocity = -dir;
                        Projectile.rotation = (float)Math.Atan2(-Projectile.velocity.Y, -Projectile.velocity.X) + 1.57f;
                    }
                    else
                    {
                        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                    }
                    if (Projectile.localAI[0] == max+ aimWindow)
                    {
                        SoundEngine.PlaySound(SoundID.Item122, Projectile.Center);
                        dir.Normalize();
                        Projectile.velocity = dir;
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir, Mod.Find<ModProjectile>("StormWyvernMinionZap").Type, (int)(Projectile.damage * player.ownedProjectileCounts[Projectile.type]), Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
                    }
                    if (Projectile.localAI[0] >= max+ aimWindow + 20)
                    {
                        Projectile.localAI[0] = -180;
                    }
                    return false;
                }
                else
                {
                    if (Projectile.localAI[0] > 0)
                    {
                        Projectile.localAI[0] = 0;
                    }
                    Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                    return true;
                }
            }
            else
            {
                Projectile prevSeg = Main.projectile[(int)Projectile.ai[1]];
                if (Projectile.ai[0] == 1)
                {
                    bool foundLegs = false;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == Projectile.owner && p.minion)
                        {
                            if (i != Projectile.whoAmI && p.type == Projectile.type && p.ai[0] == 2)
                            {
                                foundLegs = true;
                                break;
                            }
                        }
                    }
                    if (!foundLegs)
                    {
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 2, Projectile.whoAmI).minionSlots = 0;
                        Projectile.netUpdate = true;
                    }
                }
                if (Projectile.ai[0] == 2)
                {
                    bool foundTail = false;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == Projectile.owner && p.minion)
                        {
                            if (i != Projectile.whoAmI && p.type == Projectile.type && p.ai[0] == 3)
                            {
                                foundTail = true;
                                break;
                            }
                        }
                    }
                    if (!foundTail)
                    {
                        if (prevSeg.ai[0] == 4)
                        {
                            prevSeg.ai[0] = 2;
                            Projectile.ai[0] = 3;
                        }
                        if (prevSeg.ai[0] == 1)
                        {
                            Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 3, Projectile.whoAmI).minionSlots = 0;
                            Projectile.netUpdate = true;
                        }
                    }
                }
                if (!prevSeg.active || prevSeg.owner != Projectile.owner || prevSeg.type != Projectile.type)
                {
                    Projectile.Kill();
                }
                if (prevSeg.localAI[0] > 24)
                {
                    Projectile.localAI[0]++;
                    if (Projectile.localAI[0] == 12 || Projectile.localAI[0] == 24)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_lightning_aura_zap_1"), Projectile.Center); // 21
                    }
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 55, 0, 0, 0, default(Color), 0.5f);
                }
                else
                {
                    Projectile.localAI[0] = 0;
                }
                float dirX = prevSeg.Center.X - Projectile.Center.X;
                float dirY = prevSeg.Center.Y - Projectile.Center.Y;
                Projectile.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - 24) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
                Projectile.spriteDirection = Projectile.direction = dirX > 0 ? -1 : 1;
                Projectile.velocity = Vector2.Zero;
                Projectile.position.X = Projectile.position.X + posX;
                Projectile.position.Y = Projectile.position.Y + posY;
                return false;
            }
        }
        public override void AI()
        {
            float viewDist = 600f;
            float inertia = 20f;
            float chaseAccel = 10f;
            float chargeSpeed = 15f;
            int chargeCool = 120;


            Player player = Main.player[Projectile.owner];
            Vector2 targetPos = Projectile.position;
            float targetDist = viewDist;
            bool target = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if ((distance < targetDist || !target))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                        if (Projectile.localAI[1] < 0)
                        {
                            Projectile.localAI[1] = 0;
                        }
                    }
                }
            }
            else for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(this, false))
                    {
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);
                        if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            target = true;
                        }
                    }
                }
            if (Vector2.Distance(player.Center, Projectile.Center) > (target ? 1000f : 500f))
            {
                Projectile.localAI[1] = -1;
                Projectile.netUpdate = true;
            }
            if (target && Projectile.localAI[1] >= 0f)
            {
                Vector2 direction = targetPos - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
            }
            else
            {
                if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
                {
                    Projectile.localAI[1] = -1;
                }
                float speed = 6f;
                if (Projectile.localAI[1] == -1)
                {
                    speed = 15;
                }
                Vector2 center = Projectile.Center;
                Vector2 direction = player.Center - center;
                Projectile.netUpdate = true;
                direction.X += player.direction * (float)(200 * Math.Cos(MathHelper.ToRadians((float)(Main.time % 180) * 2)));
                direction.Y += (float)(90 * Math.Sin(MathHelper.ToRadians((float)(Main.time % 180) * 2)));
                float distanceTo = direction.Length();
                if (distanceTo > 200f && speed < 9f)
                {
                    speed = 9f;
                }
                if (distanceTo < 100f && Projectile.localAI[1] < 0)
                {
                    Projectile.localAI[1] = 0f;
                    Projectile.netUpdate = true;
                }
                if (distanceTo > 2000f)
                {
                    Projectile.Center = player.Center;
                }
                if (distanceTo > 48f)
                {
                    direction.Normalize();
                    direction *= speed;
                    float temp = inertia / 2f;
                    Projectile.velocity = (Projectile.velocity * temp + direction) / (temp + 1);
                }
                else
                {
                    Projectile.direction = Main.player[Projectile.owner].direction;
                    Projectile.velocity *= (float)Math.Pow(0.98, 40.0 / inertia);
                }
            }

            if (Projectile.localAI[1] > 0)
            {
                Projectile.localAI[1]--;
                Projectile.netUpdate = true;
            }
            if ((int)Projectile.localAI[1] == 0f)
            {
                if (target)
                {
                    if ((targetPos - Projectile.Center).X > 0f)
                    {
                        Projectile.spriteDirection = (Projectile.direction = -1);
                    }
                    else if ((targetPos - Projectile.Center).X < 0f)
                    {
                        Projectile.spriteDirection = (Projectile.direction = 1);
                    }
                    Projectile.velocity = Projectile.DirectionTo(targetPos) * chargeSpeed;
                    Projectile.netUpdate = true;
                    Projectile.localAI[1] = chargeCool;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            int xFrameCount = 2;
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            int frameX = 0;
            if (Projectile.localAI[0] > 0)
            {
                frameX = 1;
            }
            Rectangle rect = new Rectangle(frameX * (tex.Width / xFrameCount), Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), (tex.Width / xFrameCount), (tex.Height / Main.projFrames[Projectile.type]));
            Vector2 vect = new Vector2(((tex.Width / xFrameCount) / 2f), ((tex.Height / Main.projFrames[Projectile.type]) / 2f));

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), rect, lightColor, Projectile.rotation, vect, Projectile.scale, effects, 0);

            int max = Main.player[Projectile.owner].ownedProjectileCounts[Projectile.type] * 24;
            if (Projectile.ai[0] == 0 && Projectile.localAI[0] > 0 && Projectile.localAI[0] < max + aimWindow)
            {
                float scale = Projectile.localAI[0] < max ? Projectile.localAI[0] / max : 1f;
                float rot = MathHelper.ToRadians(Projectile.localAI[0] * 11);
                Texture2D texture = TextureAssets.Projectile[Mod.Find<ModProjectile>("StormWyvernMinionZap").Type].Value;
                rect = new Rectangle((texture.Width / 3) * ((int)Projectile.localAI[0] / 2) % 3, 0, (texture.Width / 3), (texture.Height / 3));
                vect = new Vector2(((texture.Width / 3) / 2f), ((texture.Height / 3) / 2f));
                Vector2 offSet = (Projectile.rotation - 1.57f).ToRotationVector2() * 30;
                Main.EntitySpriteDraw(texture, offSet + new Vector2(Projectile.position.X - Main.screenPosition.X + (Projectile.width / 2f) - (texture.Width / 3) / 2f + vect.X, Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2) - (float)(texture.Height / 3) / 2 + 4f + vect.Y), new Rectangle?(rect), Color.White, rot, vect, scale, effects, 0);

            }
            return false;
        }
    }
}