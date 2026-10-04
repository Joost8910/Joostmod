using JoostMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools.Hammers
{
    public class NightsWrath : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Night's Wrath");
        }
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.knockBack = 7;
            Item.value = 54000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.hammer = 80;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.tileBoost = 1;
            Item.autoReuse = true;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = damageDone;
            float scale = 0.8f * Item.scale;
            int c = hit.Crit ? 1 : 0;
            Vector2 pos = target.Hitbox.ClosestPointInRect(player.Center);
            if (c > 0)
            {
                SoundEngine.PlaySound(SoundID.Item145.WithPitchOffset(-0.75f).WithVolumeScale(0.75f), pos);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item144.WithPitchOffset(-0.5f).WithVolumeScale(0.5f), pos);
            }
            Projectile P = Projectile.NewProjectileDirect(player.GetSource_OnHit(target), pos, Vector2.Zero, ModContent.ProjectileType<NightEcho>(), damage, 0, player.whoAmI, scale, c);
            P.localNPCImmunity[target.whoAmI] = -1;
        }
        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
        {
            int damage = hurtInfo.Damage;
            float scale = 0.8f * Item.scale;
            Vector2 pos = target.Hitbox.ClosestPointInRect(player.Center);
            SoundEngine.PlaySound(SoundID.Item144.WithPitchOffset(-0.5f).WithVolumeScale(0.5f), pos);
            Projectile.NewProjectile(player.GetSource_OnHit(target), pos, Vector2.Zero, ModContent.ProjectileType<NightEcho>(), damage, 0, player.whoAmI, scale);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MoltenHamaxe)
                .AddIngredient<AquaHammer>()
                .AddIngredient<JungleHammer>()
                .AddIngredient(ItemID.TheBreaker)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}


