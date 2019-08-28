using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GrogWave2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
			Main.projFrames[projectile.type] = 10;
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 56;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
            //projectile.extraUpdates = 1;
            projectile.timeLeft = 11;
			projectile.tileCollide = false;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
        public override bool PreAI()
        {
            projectile.direction = (int)projectile.ai[0];
            projectile.scale = projectile.ai[1];
            projectile.spriteDirection = projectile.direction;
            projectile.width = (int)(18 * projectile.scale);
            if (projectile.timeLeft > 10)
            {
                projectile.frame = 0;
                projectile.height = (int)(56 * projectile.scale);
            }
            if (projectile.timeLeft == 10)
            {
                projectile.position.Y = projectile.position.Y - 26*projectile.scale;
                projectile.frame = 1;
                projectile.height = (int)(82 * projectile.scale);
            }
            if (projectile.timeLeft == 9)
            {
                projectile.position.Y = projectile.position.Y - 6 * projectile.scale;
                projectile.frame = 2;
                projectile.height = (int)(88 * projectile.scale);
            }
            if (projectile.timeLeft == 8)
            {
                projectile.frame = 3;
            }
            if (projectile.timeLeft == 7)
            {
                projectile.position.Y = projectile.position.Y + 6 * projectile.scale;
                projectile.frame = 4;
                projectile.height = (int)(82 * projectile.scale);
            }
            if (projectile.timeLeft == 6)
            {
                projectile.position.Y = projectile.position.Y + 16 * projectile.scale;
                projectile.frame = 5;
                projectile.height = (int)(66 * projectile.scale);
            }
            if (projectile.timeLeft == 5)
            {
                projectile.position.Y = projectile.position.Y + 16 * projectile.scale;
                projectile.frame = 6;
                projectile.height = (int)(50 * projectile.scale);
            }
            if (projectile.timeLeft == 4)
            {
                projectile.position.Y = projectile.position.Y + 16 * projectile.scale;
                projectile.frame = 7;
                projectile.height = (int)(34 * projectile.scale);
            }
            if (projectile.timeLeft == 3)
            {
                projectile.position.Y = projectile.position.Y + 16 * projectile.scale;
                projectile.frame = 8;
                projectile.height = (int)(18 * projectile.scale);
            }
            if (projectile.timeLeft <= 2)
            {
                projectile.position.Y = projectile.position.Y + 8 * projectile.scale;
                projectile.frame = 9;
                projectile.height = (int)(10 * projectile.scale);
            }
            return base.PreAI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y -= projectile.knockBack * target.knockBackResist * projectile.scale;
            if (target.knockBackResist > 0)
            {
                target.velocity.X = 0;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= projectile.knockBack * projectile.scale;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * projectile.scale);
            knockback = 0;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * projectile.scale);
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Color.White;
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle rectangle = new Rectangle(0,  projectile.frame * 90, texture.Width, (texture.Height / Main.projFrames[projectile.type]));
            Vector2 vector = new Vector2((texture.Width / 2f), ((texture.Height / Main.projFrames[projectile.type]) / 2f));

            sb.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color, projectile.rotation, vector, projectile.scale, effects, 0f);
            return false;
        }
    }
}
