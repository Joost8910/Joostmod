using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class BoomerangBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boomerang Bullet");
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation -= 0.25f * Projectile.timeLeft * Projectile.direction;
            Projectile.spriteDirection = Projectile.direction;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.rand.NextBool(10))
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<BoomerangBullet2>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.JungleGrass, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, 0, default, 0.75f);
                }
            }
        }
    }
    public class BoomerangBullet2 : ModProjectile
    {
        public override string Texture => "JoostMod/Projectiles/Ranged/BoomerangBullet";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boomerang Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
        }
        public override void OnKill(int timeLeft)
        {
            int item = Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Items.Ammo.BoomerangBullet>(), 1, false, 0, false, false);
            if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f, 0f, 0f, 0, 0, 0);
            }
        }
    }
    public class EndlessBoomerangBullet : ModProjectile
    {
        public override string Texture => "JoostMod/Projectiles/Ranged/BoomerangBullet";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boomerang Bullet");
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.rotation -= 0.25f * Projectile.timeLeft * Projectile.direction;
            Projectile.spriteDirection = Projectile.direction;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.rand.NextBool(10))
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<EndlessBoomerangBullet2>(), (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.JungleGrass, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, 0, default, 0.75f);
                }
            }
        }
    }
    public class EndlessBoomerangBullet2 : ModProjectile
    {
        public override string Texture => "JoostMod/Projectiles/Ranged/BoomerangBullet";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boomerang Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
        }
    }
}

