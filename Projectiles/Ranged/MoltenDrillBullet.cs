using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class MoltenDrillBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Drill Bullet");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.velocity.Normalize();
            Projectile.ai[0] = 5;
            target.AddBuff(BuffID.OnFire, 600);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = target.defense / 2 + damage / 40;
        }
        public override void AI()
        {
            if (Projectile.timeLeft >= 300)
            {
                Projectile.penetrate = Projectile.damage;
                if (Projectile.penetrate > 40)
                {
                    Projectile.penetrate = 40;
                }
                Projectile.knockBack = 0;
                Projectile.ai[1] = Projectile.velocity.Length();
            }
            Projectile.ai[0]--;
            if (Projectile.ai[0] < 0)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= Projectile.ai[1];
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 3;
            }
            if (Projectile.timeLeft % 20 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item22, Projectile.Center);
            }
            if (Projectile.timeLeft % 5 == 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            }
            float x = (int)(Projectile.Center.X / 16);
            float y = (int)(Projectile.Center.Y / 16);
            if (Main.tile[(int)Math.Round(x), (int)Math.Round(y)].HasTile)
            {
                SoundEngine.PlaySound(SoundID.Item23, Projectile.Center);
                if (Main.tile[(int)Math.Round(x), (int)Math.Round(y)].TileType == TileID.Grass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].TileType == TileID.FleshGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].TileType == TileID.CorruptGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].TileType == TileID.HallowedGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].TileType == TileID.JungleGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].TileType == TileID.MushroomGrass)
                {
                    Main.player[Projectile.owner].PickTile((int)Math.Round(x), (int)Math.Round(y), 100);
                }
                Main.player[Projectile.owner].PickTile((int)Math.Round(x), (int)Math.Round(y), 100);
                Projectile.Kill();
            }
        }
    }
}
