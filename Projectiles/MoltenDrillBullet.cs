using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class MoltenDrillBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Drill Bullet");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.Normalize();
            projectile.ai[0] = 5;
            target.AddBuff(BuffID.OnFire, 600);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (target.defense / 2) + (damage / 40);
        }
        public override void AI()
        {
            if (projectile.timeLeft >= 300)
            {
                projectile.penetrate = projectile.damage;
                if (projectile.penetrate > 40)
                {
                    projectile.penetrate = 40;
                }
                projectile.knockBack = 0;
                projectile.ai[1] = projectile.velocity.Length();
            }
            projectile.ai[0]--;
            if (projectile.ai[0] < 0)
            {
                projectile.velocity.Normalize();
                projectile.velocity *= projectile.ai[1];
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 2)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 3;
            }
            if (projectile.timeLeft % 20 == 0)
            {
                Main.PlaySound(2, projectile.Center, 22);
            }
            if (projectile.timeLeft % 5 == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
            }
            float x = (int)(projectile.Center.X / 16);
            float y = (int)(projectile.Center.Y / 16);
            if (Main.tile[(int)Math.Round(x), (int)Math.Round(y)].active())
            {
                Main.PlaySound(2, projectile.Center, 23);
                if (Main.tile[(int)Math.Round(x), (int)Math.Round(y)].type == TileID.Grass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].type == TileID.FleshGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].type == TileID.CorruptGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].type == TileID.HallowedGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].type == TileID.JungleGrass || Main.tile[(int)Math.Round(x), (int)Math.Round(y)].type == TileID.MushroomGrass)
                {
                    Main.player[projectile.owner].PickTile((int)Math.Round(x), (int)Math.Round(y), 100);
                }
                Main.player[projectile.owner].PickTile((int)Math.Round(x), (int)Math.Round(y), 100);
                projectile.Kill();
            }
        }
    }
}
