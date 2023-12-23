using JoostMod.Projectiles.Accessory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class IceXMinion : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Core-X");
            Main.projFrames[Projectile.type] = 17;
            Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 54;
			Projectile.height = 52;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 20f;
			shoot = ModContent.ProjectileType<IceBeamMinionStart>();
			shootSpeed = 16f;
			shootCool = 204f;
            predict = true;
		}

        public override void SelectFrame(Vector2 targetPos)
        {
            Projectile.spriteDirection = -1;
            Vector2 dir = Projectile.DirectionTo(targetPos);
            Projectile.rotation = dir.ToRotation();
            Projectile.frame = (int)(Projectile.ai[1] / 12) % 17;
            if (Projectile.frame >= 17 || Projectile.frame < 0)
            {
                Projectile.frame = 0;
            }
            
            //Main.NewText(projectile.ai[1], Color.ForestGreen);
            //Main.NewText(projectile.frame, Color.AliceBlue);

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 36)
            {
                Projectile.frameCounter = 0;
            }
        }
        public override void ShootEffects(ref Vector2 shootvel)
        {
            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceBeam").WithVolumeScale(0.2f), Projectile.Center);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16));
            Texture2D tex = TextureAssets.Projectile[ModContent.ProjectileType<XParasiteIce>()].Value;
            Rectangle rect = new Rectangle(0, (int)(Projectile.frameCounter / 6) * 34, (tex.Width), (tex.Height / 6));
            Vector2 vect = new Vector2((float)tex.Width / 2, (float)tex.Height / 12);
            float rotation = 0;
            Main.EntitySpriteDraw(tex, new Vector2(Projectile.position.X - Main.screenPosition.X + (float)(Projectile.width / 2) - (float)(tex.Width / 1) / 2f + vect.X - 2f, Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2) - (float)(tex.Height / 12) + 1f + vect.Y), new Rectangle?(rect), color, rotation, vect, Projectile.scale * 0.9f, effects, 0);

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
			Player player = Main.player[Projectile.owner];
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.dead)
			{
				modPlayer.IceXMinion = false;
			}
			if (modPlayer.IceXMinion)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}


