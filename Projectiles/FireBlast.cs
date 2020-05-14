using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class FireBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lord's Flame");
			Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.width = 76;
			projectile.height = 76;
			projectile.aiStyle = 1;
			projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
			projectile.timeLeft = 450;
			projectile.tileCollide = true;
            aiType = ProjectileID.Bullet;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
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
            writer.Write(projectile.timeLeft);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            projectile.timeLeft = reader.ReadInt32();
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 3;
            }
            if (Main.netMode == 1)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    Player enemyPlayer = Main.player[proj.owner];
                    if (enemyPlayer != player && enemyPlayer.hostile && player.hostile && (enemyPlayer.team == 0 || player.team == 0 || enemyPlayer.team != player.team))
                    {
                        if (enemyPlayer.heldProj == i && proj.Hitbox.Intersects(projectile.Hitbox))
                        {
                            projectile.velocity *= -1.15f;
                            projectile.owner = enemyPlayer.whoAmI;
                            projectile.timeLeft = 450;
                            NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                            Main.PlaySound(SoundID.NPCHit3, projectile.Center);
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
                            itemRect = new Rectangle((int)enemyPlayer.itemLocation.X, (int)enemyPlayer.itemLocation.Y, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height);
                        }
                        if (!item.noMelee && itemRect.Intersects(projectile.Hitbox))
                        {
                            projectile.velocity *= -1.15f;
                            projectile.owner = enemyPlayer.whoAmI;
                            projectile.timeLeft = 450;
                            NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                            Main.PlaySound(SoundID.NPCHit3, projectile.Center);
                        }
                    }
                }
            }
            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, -projectile.velocity.X / 5, -projectile.velocity.Y / 5, 100, default(Color), 2f + (Main.rand.Next(20) * 0.1f)).noGravity = true;
        }
        public override void Kill(int timeLeft)
        {
            //Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("FireballExplosion"), projectile.damage * 3, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, projectile.damage / 6, projectile.knockBack / 4, projectile.owner);
        }
    }
}

