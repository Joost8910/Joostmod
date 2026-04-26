using JoostMod.NPCs.Bosses.SAX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SAXBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Beam");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            //projectile.light = 0.75f;
            Projectile.coldDamage = true;
            AIType = ProjectileID.Bullet;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            Color color = Color.White;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            int chance = Main.masterMode ? 4 : Main.expertMode ? 3 : 2; //chance out of 4 to freeze
            if (!target.HasBuff(BuffID.Frozen) && !target.buffImmune[BuffID.Frozen] && Main.rand.Next(4) < chance)
            {
                target.AddBuff(BuffID.Frozen, 30, true);
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceFreeze"), target.Center);
           
        }
            target.AddBuff(BuffID.Frostburn2, 600, true);
            target.AddBuff(BuffID.Chilled, 300, true);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= JoostFunctions.GameDamageMult() * 2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (JoostFunctions.MetroidModActive())
            {
                if (ModContent.TryFind("MetroidMod", "InstantFreeze", out ModBuff InstantFreeze) && !target.buffImmune[InstantFreeze.Type])
                {
                    target.AddBuff(InstantFreeze.Type, 150);
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceFreeze"), target.Center);
                }
            }
            target.AddBuff(BuffID.Frostburn2, 600, true);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!(target.type == ModContent.NPCType<SAX>() || target.type == ModContent.NPCType<XParasite>()))
            {
                return true;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            if (Projectile.ai[0] != 0)
            {
                if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = Projectile.position.X;
                }
                if (Projectile.localAI[1] == 0)
                {
                    Projectile.localAI[1] = Projectile.position.Y;
                }
                float freq = 0.15f;
                float mag = 30f;
                int time = 1200 - Projectile.timeLeft;
                Vector2 pos = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
                Vector2 dir = Projectile.velocity;
                dir.Normalize();
                Vector2 axis = dir.RotatedBy(90 * Projectile.ai[0] * 0.0174f);
                Vector2 wave = axis * (float)Math.Sin(time * freq) * mag;
                Projectile.position = pos + wave;
                Projectile.localAI[0] = Projectile.position.X - wave.X + Projectile.velocity.X;
                Projectile.localAI[1] = Projectile.position.Y - wave.Y + Projectile.velocity.Y;
            }
        }
    }
}

