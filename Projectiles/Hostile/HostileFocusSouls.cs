using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class HostileFocusSouls : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Focus Souls");
        }
        public override void SetDefaults()
        {
            Projectile.width = 104;
            Projectile.height = 104;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 114;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            var source = Projectile.GetSource_FromAI();
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Vector2 center = host.Center;
            Projectile.velocity.X = Projectile.direction;
            Projectile.velocity.Y = 0;
            Projectile.localAI[0]++;
            if (!host.active || host.type != Mod.Find<ModNPC>("Spectre").Type)
            {
                Projectile.Kill();
            }
            if (Projectile.localAI[0] == 55)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_book_staff_cast_1"), Projectile.Center); // 202
            }
            if (Projectile.localAI[0] < 70)
            {
                Projectile.alpha -= 3;
            }
            if (Projectile.localAI[0] == 70)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_wither_beast_aura_pulse_1"), Projectile.Center); // 164

                double rot = Projectile.rotation + -35f * (Math.PI / 180);
                Vector2 pos1 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);
                rot = Projectile.rotation + 35f * (Math.PI / 180);
                Vector2 pos2 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = Projectile.rotation + 145f * (Math.PI / 180);
                Vector2 pos3 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 26, (float)Math.Sin(rot) * 26);
                rot = Projectile.rotation + -145f * (Math.PI / 180);
                Vector2 pos4 = Projectile.Center + new Vector2((float)Math.Cos(rot) * 24, (float)Math.Sin(rot) * 24);

                Vector2 targetPos = Projectile.Center + new Vector2(Projectile.direction * 1200, 0);

                Vector2 dir1 = targetPos - pos1;
                dir1.Normalize();
                Vector2 dir2 = targetPos - pos2;
                dir2.Normalize();
                Vector2 dir3 = targetPos - pos3;
                dir3.Normalize();
                Vector2 dir4 = targetPos - pos4;
                dir4.Normalize();

                Projectile.NewProjectile(source, pos1, dir1, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
                Projectile.NewProjectile(source, pos2, dir2, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
                Projectile.NewProjectile(source, pos3, dir3, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
                Projectile.NewProjectile(source, pos4, dir4, Mod.Find<ModProjectile>("HostileFocusSoulBeam").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, host.whoAmI);
            }
            if (Projectile.localAI[0] == 100)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_phantom_phoenix_shot_0"), Projectile.Center); // 217
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_sonic_boom_blade_slash_2"), Projectile.Center); // 222
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_betsy_wind_attack_1"), Projectile.Center); // 44
            }
            if (Projectile.timeLeft <= 10)
            {
                Projectile.alpha += 16;
            }
            if (Projectile.localAI[0] < 70)
            {
                Projectile.direction = host.direction;
                Projectile.spriteDirection = Projectile.direction;
                Projectile.velocity.X = Projectile.direction;
                Projectile.position = center - Projectile.velocity - Projectile.Size / 2f + new Vector2(host.direction * 60, 0);
                Projectile.localAI[1] = Projectile.velocity.ToRotation() + 1.57f;
                Projectile.timeLeft = 130;
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }
            Projectile.rotation = Projectile.localAI[1];
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Color.White * (1f - Projectile.alpha / 255f);
            color.A = 150;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
    }
}
