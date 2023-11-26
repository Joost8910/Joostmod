using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class DivineMirror : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Divine Mirror");
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public bool Collides(Vector2 ellipseCenter, Vector2 ellipseDim, Vector2 boxPos, Vector2 boxDim)
        {
            float x = 0f; //ellipse center
            float y = 0f; //ellipse center
            if (boxPos.X > ellipseCenter.X)
            {
                x = boxPos.X - ellipseCenter.X; //left corner
            }
            else if (boxPos.X + boxDim.X < ellipseCenter.X)
            {
                x = boxPos.X + boxDim.X - ellipseCenter.X; //right corner
            }
            if (boxPos.Y > ellipseCenter.Y)
            {
                y = boxPos.Y - ellipseCenter.Y; //top corner
            }
            else if (boxPos.Y + boxDim.Y < ellipseCenter.Y)
            {
                y = boxPos.Y + boxDim.Y - ellipseCenter.Y; //bottom corner
            }
            float a = ellipseDim.X / 2f;
            float b = ellipseDim.Y / 2f;
            return (x * x) / (a * a) + (y * y) / (b * b) < 1 && (Projectile.direction * x / 3 - y > 0) && (Projectile.direction * -x / 3 - y < 0); //point collision detection
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 90 || !player.active || player.dead || player.noItems || player.CCed)
            {
                Projectile.Kill();
            }
            if (Projectile.ai[0] < 15)
            {
                Projectile.frame = (int)(Projectile.ai[0] / 3); 
            }
            else
            {
                Projectile.frame = 4;
            }
            if (Projectile.ai[0] == 20)
            {
                SoundEngine.PlaySound(SoundID.Item6.WithPitchOffset(-0.3f), Projectile.Center);
            }
            if (Projectile.ai[0] > 20)
            {
                float op = (Projectile.ai[0] - 20) / 60f;
                if (Projectile.ai[0] > 80)
                {
                    op = (Projectile.ai[0] - 75) / 5f;
                }
                if (Projectile.ai[0] > 84)
                {
                    op = (90 - Projectile.ai[0]) / 5f;
                }
                Vector3 color = new Vector3(1f, 0.41f, 0.77f);
                Vector2 lightPos = new Vector2(Projectile.Center.X + (16 * Projectile.direction), Projectile.Center.Y);
                Lighting.AddLight(lightPos, color * op);
            }
            if (Projectile.ai[0] == 80)
            {
                SoundEngine.PlaySound(SoundID.Item29.WithPitchOffset(-0.3f), Projectile.Center);
            }
            if (Projectile.ai[0] >= 80 && Projectile.ai[0] <= 86)
            { 
                Vector2 center =new Vector2(Projectile.Center.X + (-8 * Projectile.direction), Projectile.Center.Y);
                Vector2 dim = new Vector2(144, 144);
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC n = Main.npc[i];
                    if (n.active && n.knockBackResist > 0 && !n.boss && n.type != NPCID.TargetDummy && Projectile.CanHit(n))
                    {
                        if (Collides(center, dim, n.position, n.Size))
                        {
                            for (int x = (int)n.position.X; x < (int)n.position.X + n.width; x += 4)
                            {
                                for (int y = (int)n.position.Y; y < (int)n.position.Y + n.height; y += 4)
                                {
                                    Dust.NewDustPerfect(new Vector2(x, y), 71, null, 0, default(Color), 1.5f).noGravity = true;
                                }
                            }
                            n.timeLeft = NPC.activeTime;
                            n.position = new Vector2(Main.spawnTileX * 16 - n.width / 2, Main.spawnTileY * 16 - n.height);
                            for (int x = (int)n.position.X; x < (int)n.position.X + n.width; x += 4)
                            {
                                for (int y = (int)n.position.Y; y < (int)n.position.Y + n.height; y += 4)
                                {
                                    Dust.NewDustDirect(new Vector2(x, y), 1, 1, 71);
                                }
                            }
                        }
                    }
                }
                /*
                for (int i = -150; i <= 150; i += 2)
                {
                    for (int j = -150; j <= 150; j += 2)
                    {
                        Vector2 pos = new Vector2(center.X + i, center.Y + j);
                        if (Collides(center, dim, pos, new Vector2(1, 1)) && Collision.CanHit(projectile.Center + new Vector2(12 * projectile.direction, 0), 1, 1, pos, 1, 1))
                        {
                            Dust.NewDustPerfect(pos, 71, Vector2.Zero, 0, default(Color), 1f).noGravity = true;
                        }
                    }
                }
                */
            }
            if (Math.Abs(player.velocity.X) > 3f)
            {
                player.velocity.X *= 0.9f;
            }
            if (player.velocity.Y * player.gravDir < -3f)
            {
                player.velocity.Y *= 0.9f;
            }
            Projectile.position = player.MountedCenter - Projectile.Size / 2f;
            Projectile.direction = Math.Sign(Projectile.velocity.X);
            player.direction = Projectile.direction;
            Projectile.velocity.Y = 0;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = 0;
            Projectile.timeLeft = 2;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = 0;
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            SpriteEffects effects = Projectile.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D tex = Mod.Assets.Request<Texture2D>("Projectiles/DivineMirror_Light").Value;
            float op = (Projectile.ai[0] - 20) / 60f;
            if (Projectile.ai[0] > 80)
            {
                op = (Projectile.ai[0] - 75) / 5f;
            }
            if (Projectile.ai[0] > 84)
            {
                op = (90 - Projectile.ai[0]) / 5f;
            }
            Color color = Color.White * op;
            Rectangle rect = new Rectangle(0, 0, 2, tex.Height);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(12 * Projectile.spriteDirection, Projectile.gfxOffY), new Rectangle?(rect), color, Projectile.rotation, new Vector2(1, tex.Height / 2), Projectile.scale, effects, 0);

            if (Projectile.ai[0] > 80)
            {
                rect = new Rectangle(0, 0, tex.Width, tex.Height);
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2((11 + tex.Width / 2) * Projectile.spriteDirection, Projectile.gfxOffY), new Rectangle?(rect), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            }
        }
    }
}