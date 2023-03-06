using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class FireBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lord's Flame");
			Main.projFrames[Projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			Projectile.width = 76;
			Projectile.height = 76;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
			Projectile.timeLeft = 450;
			Projectile.tileCollide = true;
            AIType = ProjectileID.Bullet;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 40;
            height = 40;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 900, true);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 900, true);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.timeLeft);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.timeLeft = reader.ReadInt32();
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 3;
            }
            if (Main.netMode == 1)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    Player enemyPlayer = Main.player[proj.owner];
                    if (enemyPlayer != player && enemyPlayer.hostile && player.hostile && (enemyPlayer.team == 0 || player.team == 0 || enemyPlayer.team != player.team))
                    {
                        if (enemyPlayer.heldProj == i && proj.Hitbox.Intersects(Projectile.Hitbox))
                        {
                            Projectile.velocity *= -1.15f;
                            Projectile.owner = enemyPlayer.whoAmI;
                            Projectile.timeLeft = 450;
                            NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                            SoundEngine.PlaySound(SoundID.NPCHit3, Projectile.Center);
                        }
                    }
                }
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player enemyPlayer = Main.player[i];
                    if (enemyPlayer != player && enemyPlayer.hostile && player.hostile && (enemyPlayer.team == 0 || player.team == 0 || enemyPlayer.team != player.team))
                    {
                        Rectangle itemRect = new Rectangle((int)enemyPlayer.itemLocation.X, (int)enemyPlayer.itemLocation.Y, 32, 32);
                        Item item = enemyPlayer.HeldItem;
                        if (!Main.dedServ)
                        {
                            itemRect = new Rectangle((int)enemyPlayer.itemLocation.X, (int)enemyPlayer.itemLocation.Y, TextureAssets.Item[item.type].Value.Width, TextureAssets.Item[item.type].Value.Height);
                        }
                        if (!item.noMelee && itemRect.Intersects(Projectile.Hitbox))
                        {
                            Projectile.velocity *= -1.15f;
                            Projectile.owner = enemyPlayer.whoAmI;
                            Projectile.timeLeft = 450;
                            NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                            SoundEngine.PlaySound(SoundID.NPCHit3, Projectile.Center);
                        }
                    }
                }
            }
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, -Projectile.velocity.X / 5, -Projectile.velocity.Y / 5, 100, default(Color), 2f + (Main.rand.Next(20) * 0.1f)).noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            //Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("FireballExplosion"), projectile.damage * 3, projectile.knockback, projectile.owner);
            Projectile.NewProjectile(Projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, Projectile.damage / 6, Projectile.knockBack / 4, Projectile.owner);
        }
    }
}

