using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GrogWave2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
			Main.projFrames[Projectile.type] = 10;
		}
		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 56;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
            //projectile.extraUpdates = 1;
            Projectile.timeLeft = 11;
			Projectile.tileCollide = false;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 20;
		}
        public override bool PreAI()
        {
            Projectile.direction = (int)Projectile.ai[0];
            Projectile.scale = Projectile.ai[1];
            Projectile.spriteDirection = Projectile.direction;
            Projectile.width = (int)(18 * Projectile.scale);
            if (Projectile.timeLeft > 10)
            {
                Projectile.frame = 0;
                Projectile.height = (int)(56 * Projectile.scale);
            }
            if (Projectile.timeLeft == 10)
            {
                Projectile.position.Y = Projectile.position.Y - 26*Projectile.scale;
                Projectile.frame = 1;
                Projectile.height = (int)(82 * Projectile.scale);
            }
            if (Projectile.timeLeft == 9)
            {
                Projectile.position.Y = Projectile.position.Y - 6 * Projectile.scale;
                Projectile.frame = 2;
                Projectile.height = (int)(88 * Projectile.scale);
            }
            if (Projectile.timeLeft == 8)
            {
                Projectile.frame = 3;
            }
            if (Projectile.timeLeft == 7)
            {
                Projectile.position.Y = Projectile.position.Y + 6 * Projectile.scale;
                Projectile.frame = 4;
                Projectile.height = (int)(82 * Projectile.scale);
            }
            if (Projectile.timeLeft == 6)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.scale;
                Projectile.frame = 5;
                Projectile.height = (int)(66 * Projectile.scale);
            }
            if (Projectile.timeLeft == 5)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.scale;
                Projectile.frame = 6;
                Projectile.height = (int)(50 * Projectile.scale);
            }
            if (Projectile.timeLeft == 4)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.scale;
                Projectile.frame = 7;
                Projectile.height = (int)(34 * Projectile.scale);
            }
            if (Projectile.timeLeft == 3)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.scale;
                Projectile.frame = 8;
                Projectile.height = (int)(18 * Projectile.scale);
            }
            if (Projectile.timeLeft <= 2)
            {
                Projectile.position.Y = Projectile.position.Y + 8 * Projectile.scale;
                Projectile.frame = 9;
                Projectile.height = (int)(10 * Projectile.scale);
            }
            return base.PreAI();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y -= Projectile.knockBack * target.knockBackResist * Projectile.scale;
            if (target.knockBackResist > 0)
            {
                target.velocity.X = 0;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= Projectile.knockBack * Projectile.scale;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * Projectile.scale);
            knockback = 0;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * Projectile.scale);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rectangle = new Rectangle(0,  Projectile.frame * 90, texture.Width, (texture.Height / Main.projFrames[Projectile.type]));
            Vector2 vector = new Vector2((texture.Width / 2f), ((texture.Height / Main.projFrames[Projectile.type]) / 2f));

            sb.Draw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, color, Projectile.rotation, vector, Projectile.scale, effects, 0f);
            return false;
        }
    }
}
