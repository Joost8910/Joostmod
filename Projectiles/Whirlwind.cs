using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Whirlwind : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Whirlwind");
			Main.projFrames[Projectile.type] = 6;
		}
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
		    Projectile.alpha = 80;
            Projectile.extraUpdates = 1;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 8;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            DrawHeldProjInFrontOfHeldItemAndArms = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                Projectile.ai[0]++;
                player.AddBuff(Mod.Find<ModBuff>("Whirlwind").Type, 2);
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 vector13 = Main.MouseWorld - vector;
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
            }
            else
            {
                Projectile.Kill();
            }
            if (Projectile.ai[0] % 16 <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item18, Projectile.position);
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 6;
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.velocity.X = Projectile.direction;
            Projectile.position.X -= Projectile.velocity.X;
            Projectile.position.Y -= 11;
            Projectile.rotation = 0f;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction * (Projectile.ai[0] % 32 <= 16 ? 1 : -1));
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            return false;
        }
  
    }
}