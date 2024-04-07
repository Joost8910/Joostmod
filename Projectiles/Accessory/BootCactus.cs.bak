using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Accessory
{
    public class BootCactus : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus");
            Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 61;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 300;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.hide = true;
            Projectile.aiStyle = 0;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 18;
            height = 56;
            fallThrough = false;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] += Projectile.ai[0] < 1 ? 1 : 0;
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type != Mod.Find<ModNPC>("Cactus Person").Type && Projectile.ai[0] >= 24 && !target.friendly && (target.damage > 0 || Projectile.ai[1] <= 0))
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override void AI()
        {
            Projectile.velocity.X = 0;
            Projectile.rotation = 0;
            if (Projectile.ai[0] == 6)
            {
                Projectile.frame = 1;
            }
            if (Projectile.ai[0] == 12)
            {
                Projectile.frame = 2;
            }
            if (Projectile.ai[0] == 18)
            {
                Projectile.frame = 3;
            }
            if (Projectile.ai[0] == 24)
            {
                Projectile.frame = 4;
            }
            if (Projectile.timeLeft < 24)
            {
                Projectile.ai[0]--;
            }
            else if (Projectile.ai[0] > 0 && Projectile.ai[0] < 24)
            {
                Projectile.ai[0]++;
            }
        }
    }
}