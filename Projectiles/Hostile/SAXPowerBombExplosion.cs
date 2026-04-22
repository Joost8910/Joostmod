using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using JoostMod.NPCs.Bosses.SAX;

namespace JoostMod.Projectiles.Hostile
{
    public class SAXPowerBombExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Power Bomb");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1000;
            Projectile.height = 500;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 110;
            Projectile.hostile = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 1;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 55)
            {
                Projectile.scale = Projectile.timeLeft * 0.036f;
                Projectile.position.X = Projectile.Center.X - (float)(1000 * Projectile.scale / 2f);
                Projectile.position.Y = Projectile.Center.Y - (float)(500 * Projectile.scale / 2f);
                Projectile.width = (int)Math.Round(1000 * Projectile.scale);
                Projectile.height = (int)Math.Round(500 * Projectile.scale);
            }
            else
            {
                int size = 110 - Projectile.timeLeft;
                Projectile.scale = size * 0.036f;
                Projectile.position.X = Projectile.Center.X - (float)(1000 * Projectile.scale / 2f);
                Projectile.position.Y = Projectile.Center.Y - (float)(500 * Projectile.scale / 2f);
                Projectile.width = (int)Math.Round(1000 * Projectile.scale);
                Projectile.height = (int)Math.Round(500 * Projectile.scale);
                Lighting.AddLight(Projectile.Center, 10f, 10f, 10f);
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            //player.GetModPlayer<JoostPlayer>().enemyIgnoreDefenseDamage = 10;
            modifiers.ScalingArmorPenetration += 1f;
            if (Projectile.timeLeft < 55)
            {
                modifiers.SetMaxDamage((int)(2.5 * JoostFunctions.GameDamageMult()));
            }
            else
            {
                modifiers.SetMaxDamage((int)(5 * JoostFunctions.GameDamageMult()));
            }
            modifiers.ModifyHurtInfo += (ref Player.HurtInfo hurtInfo) =>
            {
                hurtInfo.Dodgeable = false;
            };
            //modifiers.Cancel();
            //target.Hurt(modifiers.inf);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ScalingArmorPenetration += 1f;
            if (Projectile.timeLeft < 55)
            {
                modifiers.SetMaxDamage((int)(2.5 * JoostFunctions.GameDamageMult()));

            }
            else
            {
                modifiers.SetMaxDamage((int)(5 * JoostFunctions.GameDamageMult()));
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == ModContent.NPCType<SAX>() || target.type == ModContent.NPCType<XParasite>())
            {
                return false;
            }
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
            {
                return true;
            }
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
            {
                return base.CanHitPlayer(target);
            }
            return false;
        }
        public bool Collides(Vector2 ellipsePos, Vector2 ellipseDim, Vector2 boxPos, Vector2 boxDim)
        {
            Vector2 ellipseCenter = ellipsePos + 0.5f * ellipseDim;
            float x = 0f; //ellipse center
            float y = 0f; //ellipse center
            if (boxPos.X > ellipseCenter.X)
            {
                x = boxPos.X - ellipseCenter.X; //left corner
            }
            else if (boxPos.X + boxDim.X < ellipseCenter.X)
            {
                x = boxPos.X + boxDim.X - ellipseCenter.X; //right corner
            }
            if (boxPos.Y > ellipseCenter.Y)
            {
                y = boxPos.Y - ellipseCenter.Y; //top corner
            }
            else if (boxPos.Y + boxDim.Y < ellipseCenter.Y)
            {
                y = boxPos.Y + boxDim.Y - ellipseCenter.Y; //bottom corner
            }
            float a = ellipseDim.X / 2f;
            float b = ellipseDim.Y / 2f;
            return x * x / (a * a) + y * y / (b * b) < 1; //point collision detection
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.timeLeft < 55)
            {
                Texture2D tex2 = (Texture2D)ModContent.Request<Texture2D>($"{Texture}2");
                Main.EntitySpriteDraw(tex2, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex2.Width, tex2.Height)), Color.Black, Projectile.rotation, new Vector2(tex2.Width / 2, tex2.Height / 2), Projectile.scale, SpriteEffects.None, 0);
            }
            else
            {
                Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
                Color color = Color.Black;
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale * 2, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale * 8, SpriteEffects.None, 0);
            }
            return false;
        }
        //public override void OnKill(int timeLeft)
        //{
        //    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<SAXPowerBombExplosion2>(), (int)(Projectile.damage * 0.25f), Projectile.knockBack);
        //}
    }
}