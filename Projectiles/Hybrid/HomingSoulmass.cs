using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hybrid
{
    public class HomingSoulmass : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Homing Soulmass");
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 6000;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.MagicSummonHybrid;
            Projectile.alpha = 55;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 2 == 0)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            double deg = Projectile.ai[0];
            double rad = deg * (Math.PI / 180);
            Projectile.ai[1]++;
            Player player = Main.player[Projectile.owner];
            if (player.gravDir < 0)
            {
                rad += Math.PI;
            }
            if (Projectile.localAI[1] == 0)
            {
                Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * 50) - Projectile.width / 2;
                Projectile.position.Y = player.Center.Y - 16 * player.gravDir - (int)(Math.Sin(rad) * 50) - Projectile.height / 2;
                Projectile.direction = player.direction * (int)player.gravDir;
            }
            Projectile.spriteDirection = Projectile.direction;
            Vector2 move = Vector2.Zero;
            float distance = 800f;
            bool target = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                Vector2 newMove = npc.Center - Projectile.Center;
                float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                if (distanceTo < distance)
                {
                    move = newMove;
                    distance = distanceTo;
                    target = true;
                }
            }
            else for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.active && !npc.dontTakeDamage && npc.lifeMax > 5 && npc.CanBeChasedBy(this, false) && Collision.CanHit(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, npc.position, npc.width, npc.height))
                    {
                        Vector2 newMove = npc.Center - Projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                    }
                }
            Projectile.rotation = (float)rad + Projectile.timeLeft * -12 * (float)Math.PI / 180f * Projectile.direction;
            if (target && Projectile.ai[1] > 90 && Collision.CanHitLine(Projectile.Center, 1, 1, move + Projectile.Center, 1, 1) && Projectile.localAI[1] == 0)
            {
                Projectile.velocity = Projectile.DirectionTo(move + Projectile.Center) * 8;
                Projectile.localAI[0] = Projectile.velocity.Length();
                SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);
                Projectile.localAI[1] = 1;
                Projectile.ai[0] = 0;
                Projectile.tileCollide = true;
                Projectile.timeLeft = 600;
            }
            if (Projectile.localAI[1] == 1)
            {
                if (target)
                {
                    if (move.Length() > Projectile.localAI[0] && Projectile.localAI[0] > 0)
                    {
                        move *= Projectile.localAI[0] / move.Length();
                    }
                    float home = 15f;
                    Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                }
                if (Projectile.velocity.Length() < Projectile.localAI[0] && Projectile.localAI[0] > 0)
                {
                    Projectile.velocity *= Projectile.localAI[0] / Projectile.velocity.Length();
                }
            }
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
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Color.White;
            color *= 0.9f;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
    }
}

