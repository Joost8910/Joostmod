using System;
using System.Collections.Generic;
using JoostMod.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Summon
{
    public class TailWhip : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tail Whip");
            ProjectileID.Sets.IsAWhip[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();
            Projectile.WhipSettings.Segments = 18;
            //Projectile.WhipSettings.RangeMultiplier = 1.4f;
        }
        public override bool PreAI()
        {
            Projectile.scale = Main.player[Projectile.owner].HeldItem.scale;
            Projectile.WhipSettings.RangeMultiplier = Projectile.scale;
            return base.PreAI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 300);
            target.AddBuff(ModContent.BuffType<TailWhipDebuff>(), 300);
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            Projectile.damage = (int)(Projectile.damage * 0.65f);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)/* tModPorter Note: Removed. Use OnHitPlayer and check info.PvP */
        {
            target.AddBuff(BuffID.Venom, 300);
        }
        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.Maroon);
                Vector2 scale = new Vector2(Projectile.scale, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                pos += diff;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            //Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                // These two values are set to suit this projectile's sprite, but won't necessarily work for your own.
                // You can change them if they don't!
                Rectangle frame = new Rectangle(0, 0, 14, 20); // The size of the Handle (measured in pixels)
                Vector2 origin = new Vector2(7, 6); // Offset for where the player's hand will start measured from the top left of the image.
                float scale = Projectile.scale;

                // These statements determine what part of the spritesheet to draw for the current segment.
                // They can also be changed to suit your sprite.
                if (i == list.Count - 2)
                {
                    // This is the head of the whip. You need to measure the sprite to figure out these values.
                    frame.Y = 62; // Distance from the top of the sprite to the start of the frame.

                    // For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Projectile.ai[0] / timeToFlyOut;
                    scale *= MathHelper.Lerp(0.5f, 1.333f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                else if (i == list.Count - 3)
                {
                    frame.Y = 42;
                    frame.Height = 18;
                }
                else if (i > 0)
                {
                    frame.Y = 22;
                    frame.Height = 18;
                }

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }

    }
    public class TailWhip2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.timeLeft = 32;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.scale = player.HeldItem.scale;
            Projectile.direction = -player.direction;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = (32 - Projectile.timeLeft) * (float)(Math.PI / 180) * 11.25f * Projectile.direction;
            player.heldProj = Projectile.whoAmI;

            var stretch = Player.CompositeArmStretchAmount.Full;
            float rot = Projectile.rotation - MathHelper.PiOver2;
            if (player.direction < 0)
            {
                rot += MathHelper.Pi;
            }
            rot = MathHelper.WrapAngle(rot);
            if (rot * player.direction <= -MathHelper.PiOver2)
            {
                stretch = Player.CompositeArmStretchAmount.None;
            }

            float armRot = rot - (float)Math.PI / 2 * player.direction;
            player.SetCompositeArmFront(true, stretch, armRot);
            Projectile.width = (int)(60 * Projectile.scale);
            Projectile.height = (int)(60 * Projectile.scale);
            Projectile.position = player.GetFrontHandPosition(stretch, armRot) - Projectile.Size / 2;

            Rectangle hitbox = Projectile.Hitbox;
            ModifyDamageHitbox(ref hitbox);

            float mul = Main.GameModeInfo.EnemyDamageMultiplier;
            if (Main.GameModeInfo.IsJourneyMode)
            {
                CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
                if (power.GetIsUnlocked())
                {
                    mul = power.StrengthMultiplierToGiveNPCs;
                }
            }
            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if (ProjCheck(proj) && (proj.hostile && proj.damage <= Projectile.damage || 
                    player.whoAmI != proj.owner && player.hostile && Main.player[proj.owner].hostile && (player.team == 0 || player.team != Main.player[proj.owner].team) && CombinedHooks.CanHitPvpWithProj(proj, player) && proj.damage * mul <= Projectile.damage))
                {
                    if (proj.Hitbox.Intersects(hitbox))
                    {
                        SoundEngine.PlaySound(SoundID.Item150.WithPitchOffset(-0.1f));
                        for (int i = 0; i < 3; i++)
                        {
                            Dust.NewDustDirect(proj.position, proj.width, proj.height, 31, 0f, 0f, 0, default(Color), 1f).velocity *= 0.3f;
                        }

                        proj.hostile = false;
                        proj.friendly = true;

                        Vector2 vector = proj.Center - player.Center;
                        vector.Normalize();
                        vector *= proj.oldVelocity.Length();
                        /*
                        proj.velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        proj.velocity.Normalize();
                        proj.velocity *= vector.Length();
                        proj.velocity += vector * 20f;
                        proj.velocity.Normalize();
                        proj.velocity *= vector.Length();
                        */
                        proj.velocity = vector;
                        proj.damage = (int)(proj.damage * mul * 2);
                        if (proj.aiStyle == 82 || proj.aiStyle == 83)
                        {
                            proj.ai[0] = -1;
                        }
                        break;
                    }
                }
            }
        }
        public static bool ProjCheck(Projectile proj)
        {
            return proj.velocity.Length() > 0 && (proj.aiStyle == 1 || proj.aiStyle == 2 || proj.aiStyle == 8 || proj.aiStyle == 14 || proj.aiStyle == 16 || proj.aiStyle == 21 || proj.aiStyle == 23 || proj.aiStyle == 24 || proj.aiStyle == 28 || proj.aiStyle == 29 || proj.aiStyle == 131 || proj.aiStyle == 45 || proj.aiStyle == 78 || proj.aiStyle == 82 || (proj.aiStyle == 83 && proj.ai[0] < 30));
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (Projectile.timeLeft > 24)
            {
                hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height / 2);
            }
            else if (Projectile.timeLeft > 16)
            {
                if (Projectile.spriteDirection < 0)
                {
                    hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width / 2, Projectile.height);
                }
                else
                {
                    hitbox = new Rectangle((int)Projectile.Center.X, (int)Projectile.position.Y, Projectile.width / 2, Projectile.height);
                }
            }
            else if (Projectile.timeLeft > 8)
            {
                hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.Center.Y, Projectile.width, Projectile.height / 2);
            }
            else
            {
                if (Projectile.spriteDirection > 0)
                {
                    hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width / 2, Projectile.height);
                }
                else
                {
                    hitbox = new Rectangle((int)Projectile.Center.X, (int)Projectile.position.Y, Projectile.width / 2, Projectile.height);
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 300);
            target.AddBuff(ModContent.BuffType<TailWhipDebuff>(), 300);
            Projectile.damage = (int)(Projectile.damage * 0.65f);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)/* tModPorter Note: Removed. Use OnHitPlayer and check info.PvP */
        {
            target.AddBuff(BuffID.Venom, 300);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = Math.Sign(target.Center.X - Projectile.Center.X);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
    }
}
