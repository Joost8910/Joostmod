using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System;
using Terraria.GameContent.Drawing;

namespace JoostMod.Projectiles.Melee
{
    public class TrueNightsFuryBeam : TrueFlailBeam
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            dustId = DustID.CursedTorch;
            lightColor = new Vector3(0.5f, 0.8f, 0.25f);

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
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueNightsEdge, new ParticleOrchestraSettings
            {
                PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox)
            }, default(int?));
        }
    }
}

