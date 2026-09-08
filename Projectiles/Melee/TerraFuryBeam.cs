using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class TerraFuryBeam : TrueFlailBeam
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Fury");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            dustId = DustID.TerraBlade;
            lightColor = new Vector3(0.17f, 0.85f, 0f);
            maxDist = 210;
            startDist = 14;
            orbitalSpeedMult = 1.1f;

            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.timeLeft = activeTime;
            Projectile.extraUpdates = 3;
            Projectile.tileCollide = false;
        }
        public override void HitEffects(Entity target)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TerraBlade, new ParticleOrchestraSettings
            {
                PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox)
            }, default(int?));
        }
    }
}

