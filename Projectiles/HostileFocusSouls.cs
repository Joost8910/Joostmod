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
	public class HostileFocusSouls : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Focus Souls");
        }
		public override void SetDefaults()
		{
			Projectile.width = 104;
			Projectile.height = 104;
			Projectile.aiStyle = -1;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 114;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Vector2 center = host.Center;
            Projectile.velocity.X = Projectile.direction;
            Projectile.velocity.Y = 0;
            Projectile.localAI[0]++;
            if (!host.active || host.type != Mod.Find<ModNPC>("Spectre").Type)
            {
                Projectile.Kill();
            }
            if (Projectile.localAI[0] == 55)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
            }
            if (Projectile.localAI[0] < 70)
            {
                Projectile.alpha -= 3;
            }
            if (Projectile.localAI[0] == 70)
            {
                double rot = Projectile.rotation + (-35f * (Math.PI / 180));
                Vector2 pos1 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);
                rot = Projectile.rotation + (35f * (Math.PI / 180));
                Vector2 pos2 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = Projectile.rotation + (145f * (Math.PI / 180));
                Vector2 pos3 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = Projectile.rotation + (-145f * (Math.PI / 180));
                Vector2 pos4 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);

                Vector2 targetPos = Projectile.Center + new Vector2(Projectile.direction * 1200, 0);

                Vector2 dir1 = (targetPos - pos1);
                dir1.Normalize();
                Vector2 dir2 = (targetPos - pos2);
                dir2.Normalize();
                Vector2 dir3 = (targetPos - pos3);
                dir3.Normalize();
                Vector2 dir4 = (targetPos - pos4);
                dir4.Normalize();

                Projectile.NewProjectile(pos1, dir1, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
                Projectile.NewProjectile(pos2, dir2, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
                Projectile.NewProjectile(pos3, dir3, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
                Projectile.NewProjectile(pos4, dir4, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
            }
            if (Projectile.localAI[0] == 100)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
            }
            if (Projectile.timeLeft <= 10)
            {
                Projectile.alpha += 16;
            }
            if (Projectile.localAI[0] < 70)
            {
                Projectile.direction = host.direction;
                Projectile.spriteDirection = Projectile.direction;
                Projectile.velocity.X = Projectile.direction;
                Projectile.position = (center - Projectile.velocity) - Projectile.Size / 2f + new Vector2(host.direction * 60, 0);
                Projectile.localAI[1] = Projectile.velocity.ToRotation() + 1.57f;
                Projectile.timeLeft = 130;
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }
            Projectile.rotation = Projectile.localAI[1];
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
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
            Color color = Color.White * (1f - (Projectile.alpha / 255f));
            color.A = 150;
            sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);
            return false;
        }
    }
}
