using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class GrabThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impact");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.alpha = 75;
            Projectile.light = 0.7f;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (Projectile.ai[0] >= 0 && Main.npc[(int)Projectile.ai[0]].active)
            {
                NPC target = Main.npc[(int)Projectile.ai[0]];
                Projectile.scale = (target.width + target.height) / 2f / 20f;
                Projectile.direction = target.velocity.X > 0 ? 1 : target.velocity.X < 0 ? -1 : Projectile.direction;
                Projectile.position = target.Center - Projectile.Size / 2;
                Projectile.velocity = target.velocity / 2;
                if (target.velocity.X == 0 || target.velocity.Y == 0 || Projectile.timeLeft < 175 && Collision.SolidCollision(target.position - new Vector2(2, 2), target.width + 4, target.height + 4))
                {
                    Projectile.ai[0] = -1;
                    Projectile.ai[1] = -1;
                    Projectile.timeLeft = 2;
                    Projectile.velocity = Vector2.Zero;
                }
            }
            else if (Projectile.ai[1] >= 0 && Main.player[(int)Projectile.ai[1]].active)
            {
                Player target = Main.player[(int)Projectile.ai[1]];
                target.mount.Dismount(target);
                Projectile.direction = target.velocity.X > 0 ? 1 : target.velocity.X < 0 ? -1 : Projectile.direction;
                Projectile.position = target.Center - Projectile.Size / 2;
                Projectile.velocity = target.velocity / 2;
                if (target.velocity.X == 0 || target.velocity.Y == 0)
                {
                    Projectile.ai[0] = -1;
                    Projectile.ai[1] = -1;
                    Projectile.timeLeft = 2;
                    Projectile.velocity = Vector2.Zero;
                }
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (Projectile.ai[0] >= 0)
            {
                NPC target = Main.npc[(int)Projectile.ai[0]];
                hitbox = target.getRect();
            }
            else if (Projectile.ai[1] >= 0)
            {
                Player target = Main.player[(int)Projectile.ai[1]];
                hitbox = target.getRect();
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.whoAmI == (int)Projectile.ai[0])
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override bool CanHitPvp(Player target)
        {
            if (target.whoAmI == (int)Projectile.ai[1])
            {
                return false;
            }
            return base.CanHitPvp(target);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] >= 0)
            {
                NPC npc = Main.npc[(int)Projectile.ai[0]];
                Main.player[Projectile.owner].ApplyDamageToNPC(npc, damage / 2, 0, Projectile.direction, false);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            hitDirection = Projectile.direction;
            /*
            if (projectile.scale > 1)
            {
                damage = (int)(damage * projectile.scale);
            }
            */
            crit = true;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            /*
            if (projectile.scale > 1)
            {
                damage = (int)(damage * projectile.scale);
            }
            */
            crit = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.direction == -1)
            {
                effects = SpriteEffects.FlipVertically;
            }
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            }
            return false;
        }
    }
}

