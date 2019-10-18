using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace JoostMod.Projectiles.Minions
{
	public class IceXMinion : HoverShooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Core-X");
            Main.projFrames[projectile.type] = 17;
            Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 54;
			projectile.height = 52;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			inertia = 20f;
			shoot = mod.ProjectileType("IceBeamMinionStart");
			shootSpeed = 16f;
			shootCool = 204f;
            predict = true;
		}

        public override void SelectFrame(Vector2 targetPos)
        {
            projectile.spriteDirection = -1;
            Vector2 dir = projectile.DirectionTo(targetPos);
            projectile.rotation = dir.ToRotation();
            projectile.frame = (int)(projectile.ai[1] / 12) % 17;
            if (projectile.frame >= 17 || projectile.frame < 0)
            {
                projectile.frame = 0;
            }
            
            //Main.NewText(projectile.ai[1], Color.ForestGreen);
            //Main.NewText(projectile.frame, Color.AliceBlue);

            projectile.frameCounter++;
            if (projectile.frameCounter >= 36)
            {
                projectile.frameCounter = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
            Texture2D tex = mod.GetTexture("Projectiles/XParasite");
            Rectangle rect = new Rectangle(0, (int)(projectile.frameCounter / 6) * 34, (tex.Width), (tex.Height / 6));
            Vector2 vect = new Vector2((float)tex.Width / 2, (float)tex.Height / 12);
            float rotation = 0;
            spriteBatch.Draw(tex, new Vector2(projectile.position.X - Main.screenPosition.X + (float)(projectile.width / 2) - (float)(tex.Width / 1) / 2f + vect.X - 2f, projectile.position.Y - Main.screenPosition.Y + (float)(projectile.height / 2) - (float)(tex.Height / 12) + 1f + vect.Y), new Rectangle?(rect), color, rotation, vect, projectile.scale * 0.9f, effects, 0f);

            /*
            int xFrameCount = 1;
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle rectangle = new Rectangle(0, projectile.frame, (texture.Width / xFrameCount), (texture.Height / Main.projFrames[projectile.type]));
            Vector2 vector = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.projFrames[projectile.type]) / 2f));

            spriteBatch.Draw(texture, new Vector2(projectile.position.X - Main.screenPosition.X + (float)(projectile.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, projectile.position.Y - Main.screenPosition.Y + (float)projectile.height - (float)(texture.Height / Main.projFrames[projectile.type]) + vector.Y), new Rectangle?(rectangle), color, projectile.rotation, vector, projectile.scale, effects, 0f);
            */
            
            return true;
        }
        public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.IceXMinion = false;
			}
			if (modPlayer.IceXMinion)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}


