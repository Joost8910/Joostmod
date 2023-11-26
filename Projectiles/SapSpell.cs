using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SapSpell : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sap");
            Main.projFrames[Projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.alpha = 30;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
            Projectile.extraUpdates = 1;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 6;
            height = 6;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return Projectile.timeLeft <= 10;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("Sap").Type, 1800);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("Sap").Type, 1800);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 90)
            {
                Projectile.timeLeft -= 10;
            }
            return false;
        }
        public override void AI()
        {
            Vector2 targetPos = new Vector2(Projectile.ai[0], Projectile.ai[1]);
            int tNPC = (int)Projectile.localAI[1] - 1;
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = Projectile.velocity.Length();
                if (Projectile.localAI[1] == 0)
                {
                    float distance = 0;
                    for (int i = 0; i < 200; i++)
                    {
                        NPC target = Main.npc[i];
                        Rectangle tRect = new Rectangle((int)targetPos.X - Projectile.width / 2, (int)targetPos.Y - Projectile.height / 2, Projectile.width, Projectile.height);
                        if (target.active && target.Hitbox.Intersects(tRect) && !target.friendly && !target.dontTakeDamage && !target.buffImmune[Mod.Find<ModBuff>("Sap").Type])
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
                        for (int d = 0; d < 8; d++)
                        {
                            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, 90, 0, 0f, 50);
                            dust.noGravity = true;
                            dust.velocity = target.DirectionTo(dust.position) * 3;
                            dust.position = target.Center;
                        }
                    }
                }
            }
            if (tNPC >= 0)
            {
                NPC target = Main.npc[tNPC];
                Projectile.ai[0] = target.Center.X;
                Projectile.ai[1] = target.Center.X;
                targetPos = target.Center;
            }
            Projectile.rotation = 0;
            if (Projectile.timeLeft > 10)
            {
                if (Projectile.timeLeft % 5 == 0)
                {
                    Dust.NewDustDirect(Projectile.Center, 1, 1, 90, 0, 0f, 50).noGravity = true;
                }
                
                Vector2 move = targetPos - Projectile.Center;
                if (move.Length() > Projectile.localAI[0] && Projectile.localAI[0] > 0)
                {
                    move *= Projectile.localAI[0] / move.Length();
                }
                float home = MathHelper.Max((Projectile.timeLeft - 90) / 4, 1f);
                if (home < 15 && !Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, targetPos, 1, 1))
                {
                    home = 15;
                }
                Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                if (Vector2.Distance(targetPos, Projectile.Center) < 25)
                {
                    Projectile.timeLeft = 10;
                    Projectile.position = targetPos - Projectile.Size / 2;
                    Projectile.velocity = Vector2.Zero;
                } 
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
                if (Projectile.frame < 5)
                {
                    Projectile.frame = 6 - (Projectile.timeLeft / 2);
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            Rectangle rect = new Rectangle(0, Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), tex.Width, tex.Height / Main.projFrames[Projectile.type]);
            Color color = Color.White;
            color *= 0.7f;
            sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(rect), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}

