using System;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class DoomCannonHostile : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doom Cannon");
			Main.projFrames[Projectile.type] = 12;
		}
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 666;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool PreAI()
        {
            NPC owner = Main.npc[(int)Projectile.ai[0]];
            Vector2 vector = owner.Center;
            bool channeling = owner.active && owner.life > 0 && owner.ai[2] == 4 && owner.type == Mod.Find<ModNPC>("SkeletonDemoman").Type && Projectile.ai[1] < 660;
            if (channeling)
            {
                Vector2 vector13 = Main.player[owner.target].Center - vector;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * owner.direction;
                }
                if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity = vector13;
            }
            else
            {
                Projectile.Kill();
            }

            if (Projectile.ai[1] < 60)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.ai[1] < 120)
            {
                Projectile.frame = 1;
            }
            else if (Projectile.ai[1] < 180)
            {
                Projectile.frame = 2;
            }
            else if (Projectile.ai[1] < 240)
            {
                Projectile.frame = 3;
            }
            else if (Projectile.ai[1] < 300)
            {
                Projectile.frame = 4;
            }
            else if (Projectile.ai[1] < 360)
            {
                Projectile.frame = 5;
            }
            else if (Projectile.ai[1] < 420)
            {
                Projectile.frame = 6;
            }
            else if (Projectile.ai[1] < 480)
            {
                Projectile.frame = 7;
            }
            else if (Projectile.ai[1] < 540)
            {
                Projectile.frame = 8;
            }
            else if (Projectile.ai[1] < 600)
            {
                Projectile.frame = 9;
            }
            else if (Projectile.ai[1] < 660)
            {
                Projectile.frame = 10;
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] % 60 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item75, Projectile.position);
                if (Projectile.ai[1] >= 360)
                {
                    SoundEngine.PlaySound(SoundID.Item114, Projectile.position);
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.position = vector - Projectile.Size / 2 + new Vector2(-14, 10) + new Vector2((float)Math.Cos(Projectile.rotation - (Math.PI / 2)), (float)Math.Sin(Projectile.rotation - (Math.PI / 2))) * 14;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            owner.direction = Projectile.direction;
            return false;
        }
        public override void Kill(int timeLeft)
        {
            NPC owner = Main.npc[(int)Projectile.ai[0]];
            if (owner.active && owner.life > 0 && owner.ai[2] == 4 && owner.type == Mod.Find<ModNPC>("SkeletonDemoman").Type && Projectile.ai[1] >= 660)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
                SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 pos = owner.Center + Projectile.velocity * 140;
                    float speed = 7;
                    int type = Mod.Find<ModProjectile>("DoomSkullHostile").Type;
                    if (float.IsNaN(Projectile.velocity.X) || float.IsNaN(Projectile.velocity.Y))
                    {
                        Projectile.velocity = -Vector2.UnitY;
                    }
                    Projectile.NewProjectile(pos, Projectile.velocity * speed, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }
    }
}