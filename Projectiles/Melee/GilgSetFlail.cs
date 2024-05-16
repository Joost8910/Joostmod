using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class GilgSetFlail : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flail");
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 480;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            /*
            if (Projectile.localAI[0] < 180)
            {
                Projectile.localAI[0] += 3f;
            }
            double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = Projectile.localAI[0]; //Distance away from the player
            if (Projectile.timeLeft < 60)
            {
                dist = Projectile.timeLeft * 3f;
            }

            //Position the player based on where the player is, the Sin/Cos of the angle times the 
            //distance for the desired distance away from the player minus the projectile's width   
            //and height divided by two so the center of the projectile is at the right place.     
            Projectile.position.X = Main.player[Projectile.owner].Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            Projectile.ai[1] += 6f;
            Projectile.rotation = Projectile.ai[1] * 0.0174f;
            */

            if (Projectile.localAI[0] < 180)
            {
                Projectile.localAI[0] += 1.5f;
            }
            double deg = Projectile.ai[1];
            float dist = Projectile.localAI[0]; //Distance away from the player
            if (Projectile.timeLeft < 120)
            {
                dist = Projectile.timeLeft * 1.5f;
            }
            Player player = Main.player[Projectile.owner];
            Vector2 mountedCenter = player.MountedCenter;
            Vector2 vector2 = new Vector2(Projectile.direction, player.gravDir).RotatedBy((double)(Math.PI * (deg / 6f) * Projectile.direction * player.gravDir));
            vector2.Y *= 0.8f;
            if (vector2.Y * player.gravDir > 0f)
            {
                vector2.Y *= 0.5f;
            }
            Projectile.Center = mountedCenter + vector2 * dist * Projectile.scale;
            Projectile.ai[1] += 0.25f;
            Vector2 dir = mountedCenter - Projectile.Center;
            Projectile.rotation = dir.ToRotation() - 1.57f;

            var stretch = Player.CompositeArmStretchAmount.Full;
            float rot = Projectile.DirectionFrom(mountedCenter).ToRotation();
            if (Math.Sign((Projectile.Center.X - mountedCenter.X) * player.direction) < 0)
            {
                rot *= -1;
                stretch = Player.CompositeArmStretchAmount.Quarter;
            }
            if (Projectile.Center.X < mountedCenter.X)
            {
                rot += 3.14159274f;
            }
            rot = MathHelper.WrapAngle(rot);

            float armRot = rot - (float)Math.PI / 2 * player.direction;
            player.SetCompositeArmBack(true, stretch, armRot);
        }

        // Now this is where the chain magic happens. You don't have to try to figure this whole thing out.
        // Just make sure that you edit the first line (which starts with 'Texture2D texture') correctly.
        public override bool PreDraw(ref Color lightColor)
        {
            // So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
            // Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/Flail_Chain");

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 dir = mountedCenter - position;
            float rotation = dir.ToRotation() - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(dir.X) && float.IsNaN(dir.Y))
                flag = false;
            while (flag)
            {
                if ((double)dir.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = dir;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    dir = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
                }
            }

            return true;
        }
    }
}
