using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class GrognakHammer2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
            Main.projFrames[Projectile.type] = 12;
        }
        public override void SetDefaults()
        {
            Projectile.width = 82;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.ownerHitCheck = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 pCenter = player.MountedCenter;
            Projectile.scale = player.inventory[player.selectedItem].scale;
            Projectile.width = (int)(82 * Projectile.scale);
            Projectile.height = (int)(30 * Projectile.scale);
            Projectile.localNPCHitCooldown = (int)(24f / player.GetAttackSpeed(DamageClass.Melee));

            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 mousePos = Main.MouseWorld;
                Vector2 diff = mousePos - pCenter;
                diff.Normalize();
                float home = 12f;
                Projectile.velocity = ((home - 1f) * Projectile.velocity + diff) / home;
                Projectile.velocity.Normalize();
                Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.netUpdate = true;
            }
            Projectile.position = pCenter - Projectile.Size / 2;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.timeLeft = 2;
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_sky_dragons_fury_swing_1").WithPitchOffset(-0.2f), Projectile.Center); // 230
            }

            float speed = player.GetAttackSpeed(DamageClass.Melee);
            Projectile.ai[0] += speed;
            if (Projectile.ai[0] >= 4 && Projectile.ai[1] == 1)
            {
                Projectile.ai[1] = 0;
                SoundEngine.PlaySound(SoundID.Item8.WithVolumeScale(0.5f), Projectile.Center);
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity * 6 * speed, ModContent.ProjectileType<GrognakBeam2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            if (Projectile.ai[0] >= 24)
            {
                if (player.controlUseTile)
                {
                    Projectile.Kill();
                }
                else if (player.controlUseItem)
                {
                    Projectile.ai[0] = 0;
                    Projectile.ai[1] = 1;
                }
                else
                {
                    Projectile.Kill();
                }
            }

            if (Projectile.ai[0] < 12)
            {
                player.heldProj = Projectile.whoAmI;
            }
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.direction = Projectile.ai[0] >= 5 && Projectile.ai[0] < 16 ? Projectile.spriteDirection : -Projectile.spriteDirection;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));

            Projectile.frame = (int)(Projectile.ai[0] / 2);

            Player.CompositeArmStretchAmount frontStretch = Player.CompositeArmStretchAmount.Full;
            Player.CompositeArmStretchAmount backStretch = Player.CompositeArmStretchAmount.Full;
            int d = 24;
            switch (Projectile.frame)
            {
                case 1:
                case 3:
                    frontStretch = Player.CompositeArmStretchAmount.ThreeQuarters;
                    backStretch = Player.CompositeArmStretchAmount.Quarter;
                    d = 12;
                    break;
                case 2:
                    frontStretch = Player.CompositeArmStretchAmount.None;
                    backStretch = Player.CompositeArmStretchAmount.None;
                    d = 0;
                    break;
                case 8:
                case 9:
                    backStretch = Player.CompositeArmStretchAmount.None;
                    break;
                case 7:
                case 10:
                    backStretch = Player.CompositeArmStretchAmount.Quarter;
                    break;
                case 6:
                case 11:
                    backStretch = Player.CompositeArmStretchAmount.ThreeQuarters;
                    break;
            }
            float armRot = player.itemRotation - (float)Math.PI / 2 * player.direction;
            Vector2 frontPos = player.GetFrontHandPosition(Player.CompositeArmStretchAmount.None, armRot);
            Vector2 backPos = player.GetBackHandPosition(Player.CompositeArmStretchAmount.None, armRot);

            Vector2 tPos = pCenter + Projectile.velocity * d * player.direction * Projectile.spriteDirection;

            float fArmRot = (tPos - frontPos).ToRotation() - (float)Math.PI / 2;
            float bArmRot = (tPos - backPos).ToRotation() - (float)Math.PI / 2;

            player.SetCompositeArmFront(true, frontStretch, fArmRot);
            player.SetCompositeArmBack(true, backStretch, bArmRot);

        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.frame >= 3 && Projectile.frame <= 5)
            {
                Player p = Main.player[Projectile.owner];
                Vector2 pCenter = p.MountedCenter;
                Vector2 unit = Projectile.velocity;
                float point = 0f;
                Vector2 start = pCenter + Projectile.velocity * 53;
                Vector2 end = start + unit * 18;
                if (Collision.CheckAABBvLineCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), start, end, 18, ref point))
                {
                    crit = true;
                    SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_ground_miss_1").WithVolumeScale(0.3f), Projectile.Center); // 211
                    for (int i = 0; i < 12; i++)
                    {
                        Dust.NewDustDirect(pCenter + Projectile.velocity * 62, 1, 1, DustID.NorthPole, 0, 0, 150, new Color(0, 255, 0), 1f).noGravity = true;
                    }
                }
            }
            if (Projectile.ai[0] > 10)
            {
                damage = (int)(damage * 0.8f);
                knockback *= 0.8f;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.frame >= 3 && Projectile.frame <= 5)
            {
                Player p = Main.player[Projectile.owner];
                Vector2 pCenter = p.MountedCenter;
                Vector2 unit = Projectile.velocity;
                float point = 0f;
                Vector2 start = pCenter + Projectile.velocity * 54;
                Vector2 end = start + unit * 18;
                if (Collision.CheckAABBvLineCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), start, end, 18, ref point))
                {
                    crit = true;
                    SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_ground_miss_1").WithVolumeScale(0.3f), Projectile.Center); // 211
                    for (int i = 0; i < 12; i++)
                    {
                        Dust.NewDustDirect(pCenter + Projectile.velocity * 62, 1, 1, DustID.NorthPole, 0, 0, 150, new Color(0, 255, 0), 1f).noGravity = true;
                    }
                }
            }
            if (Projectile.ai[0] > 10)
            {
                damage = (int)(damage * 0.8f);
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player p = Main.player[Projectile.owner];
            Vector2 unit = Projectile.velocity;
            float point = 0f;
            Vector2 pCenter = p.MountedCenter;
            Vector2 start = pCenter + Projectile.velocity * (-79 + Projectile.frame * 19);
            Vector2 end = start + unit * Projectile.width;
            if (Projectile.ai[0] >= 10)
            {
                start = pCenter + Projectile.velocity * -3;
                if (Projectile.ai[0] >= 16)
                {
                    start -= Projectile.velocity * (16 * (Projectile.frame - 7));
                    end = pCenter;
                }
                else
                {
                    end -= Projectile.velocity * ((Projectile.frame - 4) * (19 + 16));
                }
            }
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, Projectile.height, ref point))
            {
                return true;
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipVertically;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16));
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rectangle = new Rectangle(0, Projectile.frame * (texture.Height / Main.projFrames[Projectile.type]), texture.Width, texture.Height / Main.projFrames[Projectile.type]);
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / Main.projFrames[Projectile.type] / 2f);
            Main.EntitySpriteDraw(texture, new Vector2(Projectile.position.X - Main.screenPosition.X + Projectile.width / 2 - texture.Width / 2f + origin.X, Projectile.position.Y - Main.screenPosition.Y + 25 - texture.Height / Main.projFrames[Projectile.type] + origin.Y * 1.5f), new Rectangle?(rectangle), color, Projectile.rotation, origin, Projectile.scale, effects, 0);

            /*
            Player p = Main.player[Projectile.owner];
            Vector2 pCenter = p.MountedCenter;
            Texture2D tTex = TextureAssets.Projectile[ModContent.ProjectileType<GrogWaveFlipped2>()].Value;
            Rectangle? tRect = new Rectangle?(new Rectangle(0, 0, tTex.Width, tTex.Height));
            Vector2 tOrigin = new Vector2(tTex.Width / 2, tTex.Height / 2);
            float tRot = Projectile.velocity.ToRotation();
            float tScale = 0.25f;

            int d = 24;
            switch (Projectile.frame)
            {
                case 1:
                case 3:
                    d = 12;
                    break;
                case 2:
                    d = 0;
                    break;
            }
            Vector2 tPos = pCenter + Projectile.velocity * d * p.direction * Projectile.spriteDirection;
            Main.EntitySpriteDraw(tTex, tPos - Main.screenPosition, tRect, Color.Yellow, tRot, tOrigin, tScale, effects, 0);

            Vector2 unit = Projectile.velocity;
            Vector2 start = pCenter + Projectile.velocity * (-79 + Projectile.frame * 19);
            Vector2 end = start + unit * Projectile.width;
            if (Projectile.ai[0] >= 10)
            {
                start = pCenter + Projectile.velocity * -3;
                if (Projectile.ai[0] >= 16)
                {
                    start -= Projectile.velocity * (16 * (Projectile.frame - 7));
                    end = pCenter;
                }
                else
                {
                    end -= Projectile.velocity * ((Projectile.frame - 4) * (19 + 16));
                }
            }
             
            float num = Projectile.height * 0.5f;
            Vector2 sPos = start - Main.screenPosition;
            Vector2 ePos = end - Main.screenPosition;
            Vector2 off1 = (tRot + (float)Math.PI / 2).ToRotationVector2() * num;
            Vector2 off2 = (tRot - (float)Math.PI / 2).ToRotationVector2() * num;

            Main.EntitySpriteDraw(tTex, sPos, tRect, Color.Red, tRot, tOrigin, tScale, effects, 0);
            Main.EntitySpriteDraw(tTex, sPos + off1, tRect, Color.HotPink, tRot, tOrigin, tScale, effects, 0);
            Main.EntitySpriteDraw(tTex, sPos + off2, tRect, Color.HotPink, tRot, tOrigin, tScale, effects, 0);
            Main.EntitySpriteDraw(tTex, ePos, tRect, Color.Blue, tRot, tOrigin, tScale, effects, 0);
            Main.EntitySpriteDraw(tTex, ePos + off1, tRect, Color.Aqua, tRot, tOrigin, tScale, effects, 0);
            Main.EntitySpriteDraw(tTex, ePos + off2, tRect, Color.Aqua, tRot, tOrigin, tScale, effects, 0);
            */
            return false;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (Projectile.frame == 2)
                overPlayers.Add(index);
        }
    }
}