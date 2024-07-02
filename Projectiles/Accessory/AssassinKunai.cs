using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Accessory
{
    public class AssassinKunai : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 90;
            Projectile.extraUpdates = 1;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetCrit();
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= 2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            switch (Main.player[Projectile.owner].meleeEnchant)
            {
                case 1: //Venom
                    target.AddBuff(70, 180 * Main.rand.Next(5, 10), false);
                    break;
                case 2: //Cursed Flames
                    target.AddBuff(39, 180 * Main.rand.Next(5, 10), false);
                    break;
                case 3: //Fire
                    target.AddBuff(24, 180 * Main.rand.Next(5, 10), false);
                    break;
                case 4: //Gold
                    target.AddBuff(72, 1200, false);
                    break;
                case 5: //Ichor
                    target.AddBuff(69, 180 * Main.rand.Next(10, 20), false);
                    break;
                case 6: //Nanites
                    target.AddBuff(31, 180 * Main.rand.Next(2, 5), false);
                    break;
                case 7: //Party
                    if (Projectile.ai[1] == 0)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, target.velocity.X, target.velocity.Y, 289, 0, 0f, Projectile.owner, 0f, 0f, 0f);
                    break;
                case 8: //Poison
                    target.AddBuff(20, 180 * Main.rand.Next(5, 10), false);
                    break;
                default:
                    break;
            }
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 1;
                Projectile.timeLeft = 30;
                SoundEngine.PlaySound(SoundID.DD2_WyvernDiveDown.WithPitchOffset(1f));
                SoundEngine.PlaySound(SoundID.NPCHit4.WithPitchOffset(0.2f));
            }
            Projectile.damage /= 2;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            switch (Main.player[Projectile.owner].meleeEnchant)
            {
                case 1: //Venom
                    target.AddBuff(70, 300 * Main.rand.Next(5, 10), false);
                    break;
                case 2: //Cursed Flames
                    target.AddBuff(39, 300 * Main.rand.Next(3, 7), false);
                    break;
                case 3: //Fire
                    target.AddBuff(24, 300 * Main.rand.Next(3, 7), false);
                    break;
                case 4: //Gold
                    target.AddBuff(72, 1800, false);
                    break;
                case 5: //Ichor
                    target.AddBuff(69, 300 * Main.rand.Next(10, 20), false);
                    break;
                case 6: //Nanites
                    target.AddBuff(31, 300 * Main.rand.Next(1, 4), false);
                    break;
                case 7: //Party
                    if (Projectile.ai[1] == 0)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, target.velocity.X, target.velocity.Y, 289, 0, 0f, Projectile.owner, 0f, 0f, 0f);
                    break;
                case 8: //Poison
                    target.AddBuff(20, 300 * Main.rand.Next(5, 10), false);
                    break;
                default:
                    break;
            }
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 1;
                Projectile.timeLeft = 45;
                SoundEngine.PlaySound(SoundID.DD2_WyvernDiveDown.WithPitchOffset(1f));
                SoundEngine.PlaySound(SoundID.NPCHit4.WithPitchOffset(0.2f));
            }
            Projectile.damage /= 2;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            Vector2 start = Projectile.oldPosition + Projectile.Size / 2;
            Vector2 end = Projectile.Center;
            if (Projectile.ai[1] > 0)
            {
                end += new Vector2(60).RotatedBy(Projectile.rotation - MathHelper.PiOver4);
            }
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 10, ref point))
            {
                return true;
            }

            return base.Colliding(projHitbox, targetHitbox);
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();

            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }
            Player player = Main.player[Projectile.owner];
            if (player.meleeEnchant == 1 && Main.rand.NextBool(3))
            {
                int num3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 171, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].fadeIn = 1.5f;
                Main.dust[num3].velocity *= 0.25f;
            }
            if (player.meleeEnchant == 1)
            {
                if (Main.rand.NextBool(3))
                {
                    int num4 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 171, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].fadeIn = 1.5f;
                    Main.dust[num4].velocity *= 0.25f;
                    return;
                }
            }
            else if (player.meleeEnchant == 2)
            {
                if (Main.rand.NextBool(2))
                {
                    int num5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75, Projectile.velocity.X * 0.2f + (float)(Projectile.direction * 3), Projectile.velocity.Y * 0.2f, 100, default(Color), 2.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity *= 0.7f;
                    Dust expr_3A3_cp_0_cp_0 = Main.dust[num5];
                    expr_3A3_cp_0_cp_0.velocity.Y = expr_3A3_cp_0_cp_0.velocity.Y - 0.5f;
                    return;
                }
            }
            else if (player.meleeEnchant == 3)
            {
                if (Main.rand.NextBool(2))
                {
                    int num6 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X * 0.2f + (float)(Projectile.direction * 3), Projectile.velocity.Y * 0.2f, 100, default(Color), 2.5f);
                    Main.dust[num6].noGravity = true;
                    Main.dust[num6].velocity *= 0.7f;
                    Dust expr_44D_cp_0_cp_0 = Main.dust[num6];
                    expr_44D_cp_0_cp_0.velocity.Y = expr_44D_cp_0_cp_0.velocity.Y - 0.5f;
                    return;
                }
            }
            else if (player.meleeEnchant == 4)
            {
                if (Main.rand.NextBool(2))
                {
                    int num7 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, Projectile.velocity.X * 0.2f + (float)(Projectile.direction * 3), Projectile.velocity.Y * 0.2f, 100, default(Color), 1.1f);
                    Main.dust[num7].noGravity = true;
                    Dust expr_4DE_cp_0_cp_0 = Main.dust[num7];
                    expr_4DE_cp_0_cp_0.velocity.X = expr_4DE_cp_0_cp_0.velocity.X / 2f;
                    Dust expr_4F9_cp_0_cp_0 = Main.dust[num7];
                    expr_4F9_cp_0_cp_0.velocity.Y = expr_4F9_cp_0_cp_0.velocity.Y / 2f;
                    return;
                }
            }
            else if (player.meleeEnchant == 5)
            {
                if (Main.rand.NextBool(2))
                {
                    int num8 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 169, 0f, 0f, 100, default(Color), 1f);
                    Dust expr_55A_cp_0_cp_0 = Main.dust[num8];
                    expr_55A_cp_0_cp_0.velocity.X = expr_55A_cp_0_cp_0.velocity.X + (float)Projectile.direction;
                    Dust expr_577_cp_0_cp_0 = Main.dust[num8];
                    expr_577_cp_0_cp_0.velocity.Y = expr_577_cp_0_cp_0.velocity.Y + 0.2f;
                    Main.dust[num8].noGravity = true;
                    return;
                }
            }
            else if (player.meleeEnchant == 6)
            {
                if (Main.rand.NextBool(2))
                {
                    int num9 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135, 0f, 0f, 100, default(Color), 1f);
                    Dust expr_5E6_cp_0_cp_0 = Main.dust[num9];
                    expr_5E6_cp_0_cp_0.velocity.X = expr_5E6_cp_0_cp_0.velocity.X + (float)Projectile.direction;
                    Dust expr_603_cp_0_cp_0 = Main.dust[num9];
                    expr_603_cp_0_cp_0.velocity.Y = expr_603_cp_0_cp_0.velocity.Y + 0.2f;
                    Main.dust[num9].noGravity = true;
                    return;
                }
            }
            else if (player.meleeEnchant == 7)
            {
                Vector2 vector = Projectile.velocity;
                if (vector.Length() > 4f)
                {
                    vector *= 4f / vector.Length();
                }
                if (Main.rand.NextBool(20))
                {
                    int num10 = Main.rand.Next(139, 143);
                    int num11 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num10, vector.X, vector.Y, 0, default(Color), 1.2f);
                    Dust expr_6B5_cp_0_cp_0 = Main.dust[num11];
                    expr_6B5_cp_0_cp_0.velocity.X = expr_6B5_cp_0_cp_0.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
                    Dust expr_6E6_cp_0_cp_0 = Main.dust[num11];
                    expr_6E6_cp_0_cp_0.velocity.Y = expr_6E6_cp_0_cp_0.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
                    Dust expr_717_cp_0_cp_0 = Main.dust[num11];
                    expr_717_cp_0_cp_0.velocity.X = expr_717_cp_0_cp_0.velocity.X + (float)Main.rand.Next(-50, 51) * 0.05f;
                    Dust expr_742_cp_0_cp_0 = Main.dust[num11];
                    expr_742_cp_0_cp_0.velocity.Y = expr_742_cp_0_cp_0.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.05f;
                    Main.dust[num11].scale *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
                }
                if (Main.rand.NextBool(40))
                {
                    int num12 = Main.rand.Next(276, 283);
                    int num13 = Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.position, vector, num12, 1f);
                    Gore expr_7D9_cp_0_cp_0 = Main.gore[num13];
                    expr_7D9_cp_0_cp_0.velocity.X = expr_7D9_cp_0_cp_0.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
                    Gore expr_80A_cp_0_cp_0 = Main.gore[num13];
                    expr_80A_cp_0_cp_0.velocity.Y = expr_80A_cp_0_cp_0.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
                    Main.gore[num13].scale *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
                    Gore expr_86A_cp_0_cp_0 = Main.gore[num13];
                    expr_86A_cp_0_cp_0.velocity.X = expr_86A_cp_0_cp_0.velocity.X + (float)Main.rand.Next(-50, 51) * 0.05f;
                    Gore expr_895_cp_0_cp_0 = Main.gore[num13];
                    expr_895_cp_0_cp_0.velocity.Y = expr_895_cp_0_cp_0.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.05f;
                    return;
                }
            }
            else if (player.meleeEnchant == 8 && Main.rand.NextBool(4))
            {
                int num14 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num14].noGravity = true;
                Main.dust[num14].fadeIn = 1.5f;
                Main.dust[num14].velocity *= 0.25f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (Projectile.ai[1] == 0)
            {
                float rot = Projectile.rotation + MathHelper.PiOver4 * Projectile.spriteDirection;
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] + Projectile.Size / 2 - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
                    Main.EntitySpriteDraw(tex, drawPos, rect, color, rot, drawOrigin, Projectile.scale, effects, 0);
                }
            }
            else
            {
                float num = Projectile.rotation + 2.355f;
                Vector2 vector = Projectile.Center + new Vector2(0f, Projectile.gfxOffY);
                Rectangle r = Projectile.getRect();
                float num3 = r.Size().Length() / Projectile.Hitbox.Size().Length();
                float num4 = Utils.Remap((float)Projectile.timeLeft, (float)0, (float)30, 0f, 1f, true);
                float num5 = Utils.Remap(num4, 0f, 0.3f, 0f, 1f, true) * Utils.Remap(num4, 0.3f, 1f, 1f, 0f, true);
                num5 = 1f - (1f - num5) * (1f - num5);
                Vector2 vector3 = Projectile.Center + new Vector2(0f, Projectile.gfxOffY);
                Texture2D value = TextureAssets.Extra[98].Value;
                Vector2 origin = value.Size() / 2f;
                Color color = new Color(246, 216, 235, 15);
                float num6 = num - 0.7853982f * (float)Projectile.spriteDirection;
                Main.EntitySpriteDraw(value, Vector2.Lerp(vector3, vector, 0.5f) - Main.screenPosition, default(Rectangle?), color * num5, num6, origin, new Vector2(num5 * num3, num3) * Projectile.scale * num3, effects, 0f);
                Main.EntitySpriteDraw(value, Vector2.Lerp(vector3, vector, 1f) - Main.screenPosition, default(Rectangle?), color * num5, num6, origin, new Vector2(num5 * num3, num3 * 1.5f) * Projectile.scale * num3, effects, 0f);
                Main.EntitySpriteDraw(value, Vector2.Lerp(vector3, vector, num4 * 1.5f - 0.5f) - Main.screenPosition + new Vector2(0f, 2f), default(Rectangle?), color * num5, num6, origin, new Vector2(num5 * num3 * 1f * num5, num3 * 4f * num5) * Projectile.scale * num3, effects, 0f);
                /*for (float num7 = 0.4f; num7 <= 1f; num7 += 0.1f)
                {
                    Vector2 vector4 = Vector2.Lerp(vector2, vector3, num7 + 0.2f);
                    Main.EntitySpriteDraw(value, vector4 - Main.screenPosition + new Vector2(0f, 2f), default(Rectangle?), color * num5 * 0.75f * num7, num6, origin, new Vector2(num5 * num3 * 1f * num5, num3 * 2f * num5) * Projectile.scale * num3, effects, 0f);
                }*/
            }
            return false;
        }
    }
}

