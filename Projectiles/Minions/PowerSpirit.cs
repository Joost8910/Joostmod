using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class PowerSpirit : Shooter
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit of Power");
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 18000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			inertia = 15f;
			chaseAccel = 35f;
			chaseDist = 30f;
			spacingMult = 0.75f;
			shoot = Mod.Find<ModProjectile>("PowerSpiritBlast").Type;
            shootSpeed = 0;
			shootCool = 60f;
            rapidRate = 2;
            noCollide = true;
        }
        public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.dead)
			{
				modPlayer.powerSpirit = false;
			}
			if (modPlayer.powerSpirit)
			{
				Projectile.timeLeft = 2;
			}
		}
        public override void SelectFrame(Vector2 dir)
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 160)
			{
				Projectile.frameCounter = 0;
            }
            Projectile.frame = (Projectile.frameCounter / 5) % 4;
            Projectile.rotation = 0;
            if (Projectile.minionSlots > 7)
            {
                Projectile.minionSlots = 7;
            }
            rapidAmount = (int)Projectile.minionSlots;
            Lighting.AddLight(Projectile.Center, new Vector3(0.35f, 1f, 0.6f) * (Projectile.minionSlots * 0.2f) * (1 + ((Projectile.localAI[0] - 60) / 100f)));
        }
        public override void ShootEffects()
        {
            Projectile.frameCounter++;
            damageMult = 1f + (Projectile.minionSlots * 0.05f);
            SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)Projectile.localAI[0]);
            writer.Write((short)Projectile.localAI[1]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadInt16();
            Projectile.localAI[1] = reader.ReadInt16();
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.HeldItem.type == Mod.Find<ModItem>("LarkusTome").Type && !player.noItems && !player.CCed && !player.dead)
            {
                if (player.controlUseTile && player.itemTime > 0 && player.itemTime < 4 && modPlayer.LegendCool <= 0)
                {
                    player.itemTime = 2;
                    player.itemAnimation = 2;
                    if (Projectile.localAI[0] <= 300f)
                    {
                        Projectile.localAI[0]++;
                    }

                    if (Projectile.localAI[0] > 60)
                    {
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Vector2 dir = Main.MouseWorld - Projectile.Center;
                            dir.Normalize();
                            if (dir.HasNaNs())
                            {
                                dir = Vector2.UnitX * (float)player.direction;
                            }
                            float speed = Projectile.Distance(Main.MouseWorld) >= chaseAccel ? chaseAccel : Projectile.Distance(Main.MouseWorld);
                            dir *= speed;
                            if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                            {
                                Projectile.netUpdate = true;
                            }
                            Projectile.velocity = dir;
                        }
                        if (Projectile.localAI[0] % 20 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item8.WithVolumeScale(1.2f).WithPitchOffset((Projectile.localAI[0] / 100f) - 1.5f), Projectile.Center);
                        }
                        if (Projectile.localAI[0] % (int)(12 - (Projectile.localAI[0] / 30)) == 0)
                        {
                            Projectile.frameCounter++;
                        }
                        CheckActive();
                        SelectFrame(Vector2.Zero);
                        return false;
                    }
                    else
                    {
                        return base.PreAI();
                    }
                }
                else if (Projectile.localAI[0] > 60)
                {
                    modPlayer.LegendCool = (int)Projectile.localAI[0];
                    Projectile.velocity = Vector2.Zero;
                    float scale = 1 + ((Projectile.localAI[0] - 60) / 100f);
                    if (rapidAmount <= 1)
                    {
                        if (Projectile.localAI[1] == 0f)
                        {
                            if (Main.myPlayer == Projectile.owner)
                            {
                                ShootEffects();
                                int proj = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, 0, shoot, (int)(Projectile.damage * damageMult * scale), Projectile.knockBack, Main.myPlayer, scale, 14 + 7 * scale);
                                Main.projectile[proj].netUpdate = true;
                                Projectile.netUpdate = true;
                                SoundEngine.PlaySound(SoundID.Trackable.WithVolumeScale(scale * 0.3f).WithPitchOffset(0.2f), Projectile.Center);
                            }
                            Projectile.localAI[0] = 0;
                            Projectile.ai[1] = 0f;
                            return base.PreAI();
                        }
                    }
                    else
                    {
                        if (Projectile.localAI[1] <= 0)
                        {
                            if (Projectile.localAI[1] % (rapidRate * 2) == 0 && Main.myPlayer == Projectile.owner)
                            {
                                ShootEffects();
                                int proj = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, 0, shoot, (int)(Projectile.damage * damageMult * scale), Projectile.knockBack, Main.myPlayer, scale, 14 + 7 * scale);
                                Main.projectile[proj].netUpdate = true;
                                Projectile.netUpdate = true;
                                SoundEngine.PlaySound(SoundID.Trackable.WithVolumeScale(scale * 0.3f).WithPitchOffset(0.2f), Projectile.Center);
                            }
                            Projectile.localAI[1]--;
                            if (Projectile.localAI[1] <= -rapidAmount * rapidRate * 2)
                            {
                                Projectile.localAI[0] = 0;
                                Projectile.localAI[1] = 0;
                                Projectile.ai[1] = 0f;
                                return base.PreAI();
                            }
                        }
                    }
                    Projectile.frameCounter++;
                    CheckActive();
                    SelectFrame(Vector2.Zero);
                    return false;
                }
            }
            Projectile.localAI[0] = 0;
            Projectile.localAI[1] = 0;
            return base.PreAI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            if (Projectile.localAI[0] > 60)
            {
                float bright = 1 + (Projectile.localAI[0] / 300f);
                color = color * bright;
            }

            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rectangle = new Rectangle(0, Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), tex.Width, (tex.Height / Main.projFrames[Projectile.type]));

            Texture2D tex2 = Mod.GetTexture("Projectiles/Minions/PowerSpirit_T1");
            Rectangle rectangle2 = new Rectangle(0, 0, tex2.Width, (tex2.Height / 8));

            Texture2D tex3 = Mod.GetTexture("Projectiles/Minions/PowerSpirit_T2");
            Rectangle rectangle3 = new Rectangle(0, 0, tex3.Width, (tex3.Height / 16));
            
            sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, tex.Height / 2 - Projectile.height / 2), new Rectangle?(rectangle), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);
            for (int i = 1; i < Projectile.minionSlots; i++)
            {
                if (i <= 2)
                {
                    rectangle2.Y = (((Projectile.frameCounter / 5) + i * 4) % 8) * rectangle2.Height;
                    sb.Draw(tex2, Projectile.Center - Main.screenPosition + new Vector2(0f, tex2.Height / 2 - Projectile.height / 2), new Rectangle?(rectangle2), color, Projectile.rotation, new Vector2(tex2.Width / 2, tex2.Height / 2), Projectile.scale, effects, 0f);
                }
                else
                {
                    int m = (Projectile.minionSlots == 5) ? 8 : ((Projectile.minionSlots == 6) ? 5 : 4);
                    rectangle3.Y = (((Projectile.frameCounter / 2) + i * m) % 16) * rectangle3.Height;
                    sb.Draw(tex3, Projectile.Center - Main.screenPosition + new Vector2(0f, tex3.Height / 2 - Projectile.height + 1), new Rectangle?(rectangle3), color, Projectile.rotation, new Vector2(tex3.Width / 2, tex3.Height / 2), Projectile.scale, effects, 0f);
                }
            }


            return false;
        }
    }
}


