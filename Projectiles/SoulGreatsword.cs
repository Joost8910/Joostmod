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
	public class SoulGreatsword : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Greatsword");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			Projectile.width = 160;
			Projectile.height = 160;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 104;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
	        Player player = Main.player[Projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            Projectile.velocity = Vector2.Zero;
            Projectile.direction = player.direction * (int)player.gravDir;
            float speed = player.GetAttackSpeed(DamageClass.Melee) / 2;
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
                speed = ((50f / player.inventory[player.selectedItem].useTime) / player.GetAttackSpeed(DamageClass.Melee)) / 2;
                Projectile.width = (int)(160 * Projectile.scale);
                Projectile.height = (int)(160 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling && Main.myPlayer == Projectile.owner)
            {
                Vector2 vector13 = Main.MouseWorld - center;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * (float)player.direction;
                }
                if (vector13.X > 0)
                {
                    Projectile.direction = (int)player.gravDir;
                    Projectile.netUpdate = true;
                }
                else
                {
                    Projectile.direction = -(int)player.gravDir;
                    Projectile.netUpdate = true;
                }
            }
            player.ChangeDir(Projectile.direction * (int)player.gravDir);
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.localAI[1] > 0 && Projectile.ai[1] > 0 && !player.mount.Active)
            {
                Projectile.localNPCHitCooldown = (int)(40f * (1f - speed));
                player.fullRotationOrigin = player.Center - player.position;
                if (Projectile.ai[1] < 180)
                {
                    player.fullRotation = (float)(Projectile.ai[1] * Math.PI / 180 * player.direction * (int)player.gravDir);
                }
                else
                {
                    player.fullRotation = (float)(90 + (player.direction * (int)player.gravDir < 0 ? 90 : 0) + ((20 - Projectile.timeLeft) * 25) * Math.PI / 180 * player.direction * (int)player.gravDir);
                }
            }
            double rad = (player.fullRotation - 1.83f) + (Projectile.ai[1] * 0.0174f * Projectile.direction);
            if (player.gravDir < 0)
            {
                rad += 3.14f;
            }
            Projectile.rotation = (float)rad;
            if (Projectile.direction == -1)
            {
                rad -= 1.045;
                Projectile.rotation = (float)rad - 1.57f;
            }
            double dist = -110 * Projectile.scale * Projectile.direction;
            Projectile.position.X = center.X + (0 * player.direction) - (int)(Math.Cos(rad - 0.785f) * dist) - (Projectile.width / 2);
            Projectile.position.Y = center.Y + (0) - (int)(Math.Sin(rad - 0.785f) * dist) - (Projectile.height / 2);
            Projectile.ai[0] += speed;
            if (Projectile.ai[0] < 30)
            {
                Projectile.alpha -= (int)(7 * speed);
            }
            if (Projectile.ai[0] > 30 && Projectile.soundDelay >= 0)
            {
                Projectile.soundDelay = -60;
                SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
            }
            if (channeling && Projectile.ai[0] > 30)
            {
                Projectile.ai[0] = 40;
            }
            if (Projectile.ai[0] <= 42)
            {
                Projectile.timeLeft = 22;
            }
            if (Projectile.ai[0] > 42 && Projectile.localAI[0] <= 0)
            {
                if (player.velocity.Y != 0 && player.velocity.X * player.direction > 0)
                {
                    Projectile.localAI[1] = 1;
                }
                Projectile.localAI[0] = 1;
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
            }
            if (Projectile.timeLeft <= 20)
            {
                if (Projectile.ai[1] < 180)
                {
                    Projectile.timeLeft = 20;
                    Projectile.ai[1] += 15 * speed;
                }
                else
                {
                    Projectile.alpha += 16;
                    if (Projectile.timeLeft <= 1)
                    {
                        player.fullRotation = 0;
                    }
                }
            }
            player.heldProj = Projectile.whoAmI;
            player.itemTime = (int)((50f / speed) - ((Projectile.ai[1] / 15f) * 4f / speed));
            player.itemAnimation = (int)((50f / speed) - ((Projectile.ai[1] / 15f) * 4f / speed));
            if (player.itemTime < 1)
            {
                player.itemTime = 1;
            }
            if (player.itemAnimation < 1)
            {
                player.itemAnimation = 1;
            }
            player.ChangeDir(Projectile.direction * (int)player.gravDir);
            /*
            float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
            Vector2 unit = rot.ToRotationVector2();
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            for (int i = 0; i < 7; i++)
            {
                Dust.NewDustPerfect(vector + unit * i * 32, 20);
            }*/
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.timeLeft <= 20 && (Projectile.timeLeft > 4 || Projectile.localAI[1] > 0))
            {
                Player player = Main.player[Projectile.owner];
                float rot = Projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 224 * Projectile.scale, 52 * Projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.timeLeft <= 20 && Projectile.timeLeft > 10)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.timeLeft <= 20 && Projectile.timeLeft > 10)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = Color.White * (1f - (Projectile.alpha / 255f));
            if (Projectile.ai[1] > 0)
            {
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                    Color color2 = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
                    Main.EntitySpriteDraw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                }
            }
            //color.A = (byte)projectile.alpha;
            spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            return false;
        }
    }
}
