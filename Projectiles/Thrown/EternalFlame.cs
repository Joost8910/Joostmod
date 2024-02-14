using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class EternalFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Flame");
        }
        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 78;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 16;
            Projectile.extraUpdates = 1;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 34;
            height = 34;
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] > 0)
            {
                int t = Math.Max((int)(Projectile.ai[1] / Projectile.ai[0]), 2);
                if (Projectile.timeLeft % 4 == 0)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 3f).noGravity = true;
                }
                if (Projectile.localAI[0] < t)
                {
                    Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                }
                Projectile.rotation = Projectile.timeLeft * (float)Math.PI / 16f * Projectile.direction;
                Projectile.localAI[0]++;
                if (Projectile.localAI[0] >= t)
                {
                    if (Projectile.localAI[0] > t + 60)
                    {
                        Projectile.velocity = Projectile.DirectionTo(player.MountedCenter) * Projectile.ai[0] * 2;
                    }
                    Vector2 move = player.MountedCenter - Projectile.Center;
                    if (Projectile.localAI[0] == t)
                    {
                        int x = Projectile.velocity.X > 0 ? 1 : -1;
                        int y = Projectile.velocity.Y > 0 ? 1 : -1;
                        move = new Vector2(x, -y) * 100;
                    }
                    if (move.Length() > 12 * Projectile.ai[0] && Projectile.localAI[0] < t + 5)
                    {
                        Projectile.localAI[0] = t + 5;
                    }
                    if (Projectile.localAI[0] > t + 15)
                    {
                        Projectile.tileCollide = false;
                    }
                    float home = Projectile.localAI[0] < t + 5 ? 10f : 6f;
                    if (move.Length() > Projectile.ai[0])
                    {
                        move *= Projectile.ai[0] / move.Length();
                    }
                    else
                    {
                        Projectile.Kill();
                    }
                    Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;

                    if (Projectile.velocity.Length() < Projectile.ai[0])
                    {
                        Projectile.velocity *= Projectile.ai[0] / Projectile.velocity.Length();
                    }
                }
            }
            else
            {
                Projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(BuffID.OnFire3, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(BuffID.OnFire3, 300);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            return false;
        }

    }
    public class EternalFlame2 : ModProjectile
    {
        public override string Texture => "JoostMod/Projectiles/Thrown/EternalFlame";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Flame");
        }
        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 78;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 16;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 34;
            height = 34;
            return true;
        }
        static int windup = 30;
        static int start = windup + 15;
        static int charge = start + 60;
        static int reversal = charge + 14;
        static int thro = reversal + 13;
        static int launch = thro + 15;
        static int hover = launch + 60;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            bool alt = Projectile.ai[0] < 0;
            Vector2 targetPos = new Vector2(Math.Abs(Projectile.ai[0]), Projectile.ai[1]);
            int tNPC = (int)Projectile.localAI[1] - 1;
            if (Projectile.localAI[0] == 0)
            {
                if (Projectile.localAI[1] == 0)
                {
                    float distance = 0;
                    for (int i = 0; i < 200; i++)
                    {
                        NPC target = Main.npc[i];
                        Rectangle tRect = new Rectangle((int)targetPos.X - Projectile.width / 2, (int)targetPos.Y - Projectile.height / 2, Projectile.width, Projectile.height);
                        if (target.active && target.Hitbox.Intersects(tRect) && !target.friendly && !target.dontTakeDamage)
                        {
                            float distanceTo = target.Distance(targetPos);
                            if (distanceTo < distance || tNPC < 0)
                            {
                                distance = distanceTo;
                                tNPC = i;
                                Projectile.localAI[1] = i + 1;
                            }
                        }
                    }
                    if (tNPC >= 0)
                    {
                        NPC target = Main.npc[tNPC];
                        Projectile.ai[0] = alt ? -target.Center.X : target.Center.X;
                        Projectile.ai[1] = target.Center.X;
                        targetPos = target.Center;
                    }
                }
                if (!alt)
                {
                    Dust.NewDustPerfect(targetPos, DustID.GemTopaz, Vector2.Zero, 30, Color.Yellow, 1.5f).noGravity = true;
                    for (int d = 0; d < 15; d++)
                    {
                        Vector2 p = new Vector2(10, 0).RotatedBy((d + 1.5) * Math.PI / 18f);
                        Dust.NewDustPerfect(targetPos + p, DustID.GemTopaz, Vector2.Zero, 30, Color.Yellow, 1f).noGravity = true;
                        Dust.NewDustPerfect(targetPos - p, DustID.GemTopaz, Vector2.Zero, 30, Color.Yellow, 1f).noGravity = true;
                    }
                }
            }
            if (tNPC >= 0)
            {
                NPC target = Main.npc[tNPC];
                if (target.active)
                {
                    Projectile.ai[0] = alt ? -target.Center.X : target.Center.X;
                    Projectile.ai[1] = target.Center.X;
                    targetPos = target.Center;
                }
                else
                {
                    Projectile.localAI[1] = 0;
                }
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] < launch)
            {
                float rot = 0;
                if (Projectile.localAI[0] < windup)
                {
                    rot = Projectile.localAI[0] * (120f / windup) * (float)Math.PI / 180f * player.direction;
                }
                else if (Projectile.localAI[0] < reversal)
                {
                    rot = (float)Math.PI / 1.5f * player.direction;
                }
                else if (Projectile.localAI[0] < thro)
                {
                    rot = (120f - (Projectile.localAI[0] - reversal) * (120 / (thro - reversal))) * (float)Math.PI / 180f * player.direction;
                }
                float armRot = rot - (float)Math.PI / 2 * player.direction;
                if (!alt)
                {
                    player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRot);
                    player.heldProj = Projectile.whoAmI;
                }
                else
                {
                    player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, armRot);
                }
                if (Projectile.localAI[0] < thro)
                {
                    Vector2 armPos = alt ? player.GetBackHandPosition(Player.CompositeArmStretchAmount.Full, armRot) : player.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, armRot);
                    Projectile.Center = armPos;
                    Projectile.rotation = rot;
                }
            }
            if (Projectile.localAI[0] >= thro)
            {
                Projectile.rotation -= (float)Math.PI / 16f * Projectile.spriteDirection;
                if (Projectile.timeLeft % 4 == 0)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 4f).noGravity = true;
                }
            }
            if (Projectile.localAI[0] < start)
            {
                Projectile.spriteDirection = targetPos.X - player.Center.X > 0 ? 1 : -1;
                if (!alt)
                {
                    player.velocity.X *= 0.5f;
                    player.direction = Projectile.spriteDirection;
                }
            }
            else if (Projectile.localAI[0] < charge)
            {
                if (Projectile.localAI[0] == start)
                {
                    SoundEngine.PlaySound(SoundID.Item7.WithPitchOffset(-0.15f), Projectile.Center);
                }
                Projectile.spriteDirection = targetPos.X - player.Center.X > 0 ? 1 : -1;
                if (!alt)
                {
                    player.direction = Projectile.spriteDirection;
                    player.velocity.X = 15 * Projectile.spriteDirection;
                }
                if (Math.Abs(targetPos.X - player.Center.X) < 60)
                {
                    Projectile.localAI[0] = charge;
                    player.immune = true;
                    player.immuneTime = 45;
                }
                if (Projectile.timeLeft % 3 == 0)
                {
                    Dust.NewDustDirect(new Vector2(player.position.X - 4f, player.position.Y + (player.gravDir > 0 ? (float)player.height + 2f : -2f)), player.width + 8, 4, DustID.Flare, 0, 0, 100, default, 2f).noGravity = true;
                }
            }
            else if (Projectile.localAI[0] < hover)
            {
                if (Projectile.localAI[0] <= reversal)
                {
                    if (!alt)
                    {
                        player.direction = targetPos.X - player.Center.X > 0 ? 1 : -1;
                        player.velocity.X = 20 * Projectile.spriteDirection;
                        if (Projectile.localAI[0] == reversal)
                        {
                            player.velocity.X = 4 * Projectile.spriteDirection;
                            SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_ghastly_glaive_pierce_2"), Projectile.Center);
                        }
                    }
                }
                else if (Projectile.localAI[0] >= thro)
                {
                    float speed = 24f;
                    if (Projectile.localAI[0] == thro)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_betsy_fireball_shot_1"), Projectile.Center);
                        Projectile.spriteDirection = targetPos.X - player.Center.X > 0 ? 1 : -1;
                        Projectile.velocity.X = Projectile.spriteDirection * speed;
                        Projectile.tileCollide = true;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<EternalFire>(), Projectile.damage, 0, Projectile.owner, Projectile.whoAmI);
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<EternalFire>(), Projectile.damage, 0, Projectile.owner, Projectile.whoAmI, 180);
                        }
                    }
                    float home = alt ? 13f : 8f;
                    if (Projectile.localAI[0] >= launch)
                    {
                        speed = Projectile.velocity.Length();
                        home = alt ? 10f : 6f;
                    }
                    Vector2 move = targetPos - Projectile.Center;
                    if (move.Length() > speed)
                    {
                        move *= speed / move.Length();
                    }
                    Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                }
            }
            else
            {
                Projectile.tileCollide = false;
                float speed = 24f;
                if (Projectile.localAI[0] > hover + 60)
                {
                    Projectile.velocity = Projectile.DirectionTo(player.MountedCenter) * speed * 2;
                }
                Vector2 move = player.MountedCenter - Projectile.Center;
                float home = 4f;
                if (move.Length() > speed)
                {
                    move *= speed / move.Length();
                }
                else
                {
                    Projectile.Kill();
                }
                Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;

                if (Projectile.velocity.Length() < speed)
                {
                    Projectile.velocity *= speed / Projectile.velocity.Length();
                }
            }
        }
        public override bool? CanDamage()
        {
            if (Projectile.localAI[0] < start)
            {
                return false;
            }
            return base.CanDamage();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Item10.WithVolumeScale(0.5f), Projectile.Center);
            return false;
        }

    }
}

