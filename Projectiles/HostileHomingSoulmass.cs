using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HostileHomingSoulmass : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Homing Soulmass");
		}
		public override void SetDefaults()
		{
			Projectile.width = 36;
			Projectile.height = 36;
			Projectile.aiStyle = -1;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 360;
			Projectile.tileCollide = false;
            Projectile.alpha = 55;
		}
		public override void AI()
		{
			if (Projectile.timeLeft % 2 == 0)
			{
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
			double deg = Projectile.ai[1];
			double rad = deg * (Math.PI / 180);
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Player player = Main.player[host.target];
            if (Projectile.localAI[1] == 0)
            {
                Projectile.position.X = host.Center.X - (int)(Math.Cos(rad) * 100) - Projectile.width / 2;
                Projectile.position.Y = (host.Center.Y - 50) - (int)(Math.Sin(rad) * 100) - Projectile.height / 2;
                Projectile.direction = host.direction;
                if (!host.active || host.type != Mod.Find<ModNPC>("Spectre").Type)
                {
                    Projectile.Kill();
                }
            }
            Projectile.spriteDirection = Projectile.direction;
            Vector2 move = Vector2.Zero;
            float distance = 800f;
            bool target = false;
            for (int k = 0; k < 255; k++)
            {
                if (Projectile.Distance(player.Center) > distance || !player.active || player.dead)
                {
                    player = Main.player[k];
                }
                if (player.active && !player.dead && Collision.CanHit(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, player.position, player.width, player.height))
                {
                    Vector2 newMove = player.Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            Projectile.rotation = (float)rad + (Projectile.timeLeft * -12 * (float)Math.PI / 180f * Projectile.direction);
            if (Projectile.timeLeft < 270 && Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1) && Projectile.localAI[1] == 0)
            {
                Projectile.velocity = Projectile.DirectionTo(player.Center) * 8;
                Projectile.localAI[0] = Projectile.velocity.Length();
                SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);
                Projectile.localAI[1] = 1;
                Projectile.ai[1] = 0;
                Projectile.tileCollide = true;
            }
            if (Projectile.localAI[1] == 1)
            {
                if (target)
                {
                    if (move.Length() > Projectile.localAI[0] && Projectile.localAI[0] > 0)
                    {
                        move *= Projectile.localAI[0] / move.Length();
                    }
                    float home = 30f;
                    Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                }
                if (Projectile.velocity.Length() < Projectile.localAI[0] && Projectile.localAI[0] > 0)
                {
                    Projectile.velocity *= (Projectile.localAI[0] / Projectile.velocity.Length());
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(255, 255, 255, 150);
            sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.Kill();
        }
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 12; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 20;
            height = 20;
            return true;
        }
    }
}

