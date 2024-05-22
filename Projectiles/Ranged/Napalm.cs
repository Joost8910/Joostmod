using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class Napalm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Napalm");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Shuriken;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 0.5f;
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.FinalDamage *= 0.5f;
        }
        public override void AI()
        {
            Projectile.rotation -= 0.1f * Projectile.timeLeft * Math.Sign(Projectile.velocity.X);
        }
        public override void OnKill(int timeLeft)
        {
            var souce = Projectile.GetSource_Death();
            int type = ModContent.ProjectileType<NapalmFire>();
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 7f, 0f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 0f, 7f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, -7f, 0f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 0f, -7f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 5f, 5f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, 5f, -5f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, -5f, 5f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(souce, Projectile.Center.X, Projectile.Center.Y, -5f, -5f, type, Projectile.damage, Projectile.knockBack, Projectile.owner);

            SoundEngine.PlaySound(SoundID.Item34, Projectile.position);
        }
    }
    public class NapalmFire : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_85";
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 193;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 4;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Inflate(30, 30);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override bool PreDraw(ref Color lightColor) //Copied from DrawProj_Flamethrower
        {
            bool flag = Projectile.ai[0] == 1f;
            float num = 60f;
            float num2 = 12f;
            float fromMax = num + num2;
            Texture2D value = TextureAssets.Projectile[Projectile.type].Value;
            Color arg_38_0 = Color.Transparent;
            Color color = new Color(255, 80, 20, 200);
            Color color2 = new Color(255, 255, 20, 70);
            Color color3 = Color.Lerp(new Color(255, 80, 20, 100), color2, 0.25f);
            Color color4 = new Color(80, 80, 80, 100);
            float num3 = 0.35f;
            float num4 = 0.7f;
            float num5 = 0.85f;
            float num6 = (Projectile.localAI[0] > num - 10f) ? 0.175f : 0.2f;
            if (flag)
            {
                color = new Color(95, 120, 255, 200);
                color2 = new Color(50, 180, 255, 70);
                color3 = new Color(95, 160, 255, 100);
                color4 = new Color(33, 125, 202, 100);
            }
            int verticalFrames = 7;
            float num7 = Utils.Remap(Projectile.localAI[0], num, fromMax, 1f, 0f, true);
            float num8 = Math.Min(Projectile.localAI[0], 20f);
            float num9 = Utils.Remap(Projectile.localAI[0], 0f, fromMax, 0f, 1f, true);
            float num10 = Utils.Remap(num9, 0.2f, 0.5f, 0.25f, 1f, true);
            Rectangle rectangle = (!flag) ? value.Frame(1, verticalFrames, 0, 3, 0, 0) : value.Frame(1, verticalFrames, 0, (int)Utils.Remap(num9, 0.5f, 1f, 3f, 5f, true), 0, 0);
            if (num9 >= 1f)
            {
                return false;
            }
            for (int i = 0; i < 2; i++)
            {
                for (float num11 = 1f; num11 >= 0f; num11 -= num6)
                {
                    Color arg_301_0 = (num9 < 0.1f) ? Color.Lerp(Color.Transparent, color, Utils.GetLerpValue(0f, 0.1f, num9, true)) : ((num9 < 0.2f) ? Color.Lerp(color, color2, Utils.GetLerpValue(0.1f, 0.2f, num9, true)) : ((num9 < num3) ? color2 : ((num9 < num4) ? Color.Lerp(color2, color3, Utils.GetLerpValue(num3, num4, num9, true)) : ((num9 < num5) ? Color.Lerp(color3, color4, Utils.GetLerpValue(num4, num5, num9, true)) : ((num9 >= 1f) ? Color.Transparent : Color.Lerp(color4, Color.Transparent, Utils.GetLerpValue(num5, 1f, num9, true)))))));
                    float num12 = (1f - num11) * Utils.Remap(num9, 0f, 0.2f, 0f, 1f, true);
                    Vector2 vector = Projectile.Center - Main.screenPosition + Projectile.velocity * (0f - num8) * num11;
                    Color color5 = arg_301_0 * num12;
                    Color color6 = color5;
                    if (!flag)
                    {
                        color6.G = (byte)(color6.G / 2);
                        color6.B = (byte)(color6.B / 2);
                        color6.A = (byte)Math.Min((float)color5.A + 80f * num12, 255f);
                        Utils.Remap(Projectile.localAI[0], 20f, fromMax, 0f, 1f, true);
                    }
                    float num13 = 1f / num6 * (num11 + 1f);
                    float num14 = Projectile.rotation + num11 * 1.57079637f + Main.GlobalTimeWrappedHourly * num13 * 2f;
                    float num15 = Projectile.rotation - num11 * 1.57079637f - Main.GlobalTimeWrappedHourly * num13 * 2f;
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            if (!flag)
                            {
                                Main.EntitySpriteDraw(value, vector + Projectile.velocity * (0f - num8) * num6 * 0.2f, new Rectangle?(rectangle), color5 * num7 * 0.25f, num14 + 1.57079637f, rectangle.Size() / 2f, num10 * 0.75f, 0, 0f);
                                Main.EntitySpriteDraw(value, vector, new Rectangle?(rectangle), color5 * num7, num15 + 1.57079637f, rectangle.Size() / 2f, num10 * 0.75f, 0, 0f);
                            }
                        }
                    }
                    else
                    {
                        Main.EntitySpriteDraw(value, vector + Projectile.velocity * (0f - num8) * num6 * 0.5f, new Rectangle?(rectangle), color6 * num7 * 0.25f, num14 + 0.7853982f, rectangle.Size() / 2f, num10, 0, 0f);
                        Main.EntitySpriteDraw(value, vector, new Rectangle?(rectangle), color6 * num7, num15, rectangle.Size() / 2f, num10, 0, 0f);
                    }
                }
            }
            return base.PreDraw(ref lightColor);
        }
    }
}

