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
	public class FocusSouls : ModProjectile
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
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 114;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
	        Player player = Main.player[Projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.velocity = Vector2.Zero;
            Projectile.ai[0]++;
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    //projectile.ai[0]++;
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - Projectile.Center;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                    Projectile.netUpdate = true;
                }
            }
            if (Projectile.ai[0] == 25)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
            }
            if (Projectile.ai[0] < 45)
            {
                Projectile.alpha -= 4;
            }
            if (Projectile.ai[0] > 45 && Projectile.ai[0] < 48 && player.channel && !player.noItems && !player.CCed)
            {
                Projectile.ai[0] = 46;
            }
            if (Projectile.ai[0] == 58)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                double rot = Projectile.rotation + (-35f * (Math.PI / 180));
                Vector2 pos1 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);
                rot = Projectile.rotation + (35f * (Math.PI / 180));
                Vector2 pos2 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = Projectile.rotation + (145f * (Math.PI / 180));
                Vector2 pos3 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = Projectile.rotation + (-145f * (Math.PI / 180));
                Vector2 pos4 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);

                Vector2 mousePos = Main.MouseWorld;
                if (Main.myPlayer == Projectile.owner)
                {
                    mousePos = Main.MouseWorld;
                }

                Vector2 dir1 = (mousePos - pos1);
                dir1.Normalize();
                Vector2 dir2 = (mousePos - pos2);
                dir2.Normalize();
                Vector2 dir3 = (mousePos - pos3);
                dir3.Normalize();
                Vector2 dir4 = (mousePos - pos4);
                dir4.Normalize();

                Projectile.NewProjectile(pos1, dir1, Mod.Find<ModProjectile>("FocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(pos2, dir2, Mod.Find<ModProjectile>("FocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(pos3, dir3, Mod.Find<ModProjectile>("FocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(pos4, dir4, Mod.Find<ModProjectile>("FocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 85)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
            }
            if (Projectile.timeLeft <= 10)
            {
                Projectile.alpha += 16;
            }
            if (Projectile.ai[0] < 48)
            {
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.spriteDirection = Projectile.direction;
                Projectile.position = (center - Projectile.velocity) - Projectile.Size / 2f + new Vector2(0, -50 * player.gravDir);
                Projectile.localAI[1] = Projectile.velocity.ToRotation() + 1.57f;
                player.ChangeDir(Projectile.direction);
                player.heldProj = Projectile.whoAmI;
                player.itemTime = 10;
                player.itemAnimation = 10;
                Projectile.timeLeft = 140;
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }
            Projectile.rotation = Projectile.localAI[1];
        }
        public override bool CanHitPvp(Player target)
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
            //color.A = (byte)projectile.alpha;
            sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);
            return false;
        }
    }
}
