using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class InfernalChakram : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Chakram");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80 && (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 0 || Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 2))
            {
                int d = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, ModContent.ProjectileType<DousedChakram>(), Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner);
                for (int k = 0; k < 200; k++)
                {
                    Projectile g = Main.projectile[k];
                    if ((g.type == ModContent.ProjectileType<Fires>() || g.type == ModContent.ProjectileType<Fires2>()) && g.active && g.owner == Projectile.owner)
                    {
                        g.ai[0] = d;
                    }
                }
                SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
                Projectile.Kill();
            }
            Projectile.rotation = Projectile.timeLeft * 6;
            if (Projectile.timeLeft >= 1799)
            {
                for (int l = 0; l < 200; l++)
                {
                    Projectile f = Main.projectile[l];
                    if ((f.type == ModContent.ProjectileType<Fires>() || f.type == ModContent.ProjectileType<Fires2>()) && f.active && f.owner == Projectile.owner)
                    {
                        f.ai[0] = Projectile.whoAmI;
                    }
                }
            }
            if (Projectile.timeLeft % 5 == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 1f);
                Main.dust[num1].noGravity = true;
            }
            if (Projectile.timeLeft % 30 == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Fires>(), Projectile.damage, Projectile.knockBack / 3, Projectile.owner, Projectile.whoAmI);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Fires2>(), Projectile.damage, Projectile.knockBack / 3, Projectile.owner, Projectile.whoAmI);
            }
            if (Projectile.timeLeft <= 1760)
            {
                Projectile.aiStyle = 3;
                Projectile.tileCollide = false;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            if (Projectile.aiStyle != 3)
            {
                Projectile.velocity *= -1;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
            if (Projectile.aiStyle != 3)
            {
                Projectile.velocity *= -1;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

    }
}

