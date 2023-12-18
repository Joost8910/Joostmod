using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Accessory
{
    public class PersonalBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Personal Bubble");
        }
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 4;
            Projectile.alpha = 50;
            Projectile.extraUpdates = 1;
            DrawHeldProjInFrontOfHeldItemAndArms = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        /*
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.Center.X < target.Center.X)
            {
                hitDirection = 1;
            }
            else
            {
                hitDirection = -1;
            }
        }
        */
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.active && !player.dead && player.GetModPlayer<JoostPlayer>().waterBubbleItem != null && !player.GetModPlayer<JoostPlayer>().hideBubble)
            {
                Projectile.position.X = player.MountedCenter.X - Projectile.width / 2;
                Projectile.position.Y = player.MountedCenter.Y - Projectile.height / 2;
                Projectile.velocity.Y = 0;
                Projectile.rotation = 0;
                player.heldProj = Projectile.whoAmI;
                Projectile.timeLeft = 2;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.netMode != 0)
                    {
                        Player o = Main.player[i];
                        if (o != player && Projectile.Hitbox.Intersects(o.Hitbox))
                        {
                            o.wet = true;
                            if (o.wetCount != 0)
                            {
                                o.wetCount++;
                            }
                        }
                    }
                    if (i < 200)
                    {
                        NPC target = Main.npc[i];
                        if (target.active && !target.friendly && Projectile.Hitbox.Intersects(target.Hitbox))
                        {
                            int dir = 0;
                            if (target.Center.X > Projectile.Center.X)
                            {
                                dir = 1;
                            }
                            if (target.Center.X < Projectile.Center.X)
                            {
                                dir = -1;
                            }
                            int dirY = 0;
                            if (target.Center.Y > Projectile.Center.Y)
                            {
                                dirY = 1;
                            }
                            if (target.Center.Y < Projectile.Center.Y)
                            {
                                dirY = -1;
                            }
                            target.wet = true;
                            if (target.wetCount != 0)
                            {
                                target.wetCount++;
                            }
                            if (target.knockBackResist > 0)
                            {
                                target.velocity.X += dir;
                                target.velocity.Y += dirY;
                            }
                        }
                    }
                }
            }
        }

    }
}

