using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Microsoft.Xna.Framework.Graphics;

namespace JoostMod.Items
{
    public class JoostGlobalItem : GlobalItem
    {
        public int meleeDamage = 0;
        public int thrownDamage = 0;
        public int rangedDamage = 0;
        public int magicDamage = 0;
        public int summonDamage = 0;
        public int maxHealth = 0;
        public int lifeRegen = 0;
        public int fishingPower = 0;
        public Texture2D glowmaskTex = null;
        public Color glowmaskColor = Color.White;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = glowmaskTex;
            if (tex != null)
            {
                drawColor = glowmaskColor;
                spriteBatch.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            }
        }
        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = glowmaskTex;
            if (tex != null)
            {
                float x = (float)(item.width / 2f - tex.Width / 2f);
                float y = (float)(item.height - tex.Height);
                lightColor = glowmaskColor;
                alphaColor = lightColor;
                spriteBatch.Draw(tex, new Vector2(item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
            }
        } 
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            JoostGlobalItem myClone = (JoostGlobalItem)base.Clone(item, itemClone);
            myClone.meleeDamage = meleeDamage;
            myClone.thrownDamage = thrownDamage;
            myClone.rangedDamage = rangedDamage;
            myClone.magicDamage = magicDamage;
            myClone.summonDamage = summonDamage;
            myClone.maxHealth = maxHealth;
            myClone.lifeRegen = lifeRegen;
            myClone.fishingPower = fishingPower;
            return myClone;
        }
        public override bool PreReforge(Item item)
        {
            meleeDamage = 0;
            thrownDamage = 0;
            rangedDamage = 0;
            magicDamage = 0;
            summonDamage = 0;
            maxHealth = 0;
            lifeRegen = 0;
            fishingPower = 0;
            return base.PreReforge(item);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.LivingLoom)
            {
                TooltipLine line = new TooltipLine(Mod, "CraftedAt", "Crafted at a living tree's leaves");
                tooltips.Add(line);
            }
            if (meleeDamage > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "meleePrefix", "+" + meleeDamage + "% melee damage");
                line.IsModifier = true;
                tooltips.Add(line);
            }
            if (thrownDamage > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "thrownPrefix", "+" + thrownDamage + "% thrown damage");
                line.IsModifier = true;
                tooltips.Add(line);
            }
            if (rangedDamage > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "rangedPrefix", "+" + rangedDamage + "% ranged damage");
                line.IsModifier = true;
                tooltips.Add(line);
            }
            if (magicDamage > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "magicPrefix", "+" + magicDamage + "% magic damage");
                line.IsModifier = true;
                tooltips.Add(line);
            }
            if (summonDamage > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "summonPrefix", "+" + summonDamage + "% summon damage");
                line.IsModifier = true;
                tooltips.Add(line);
            }
            if (maxHealth > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "maxLifePrefix", "+" + maxHealth + " max life");
                line.IsModifier = true;
                tooltips.Add(line);
            }
            if (lifeRegen > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "lifeRegenPrefix", "+" + lifeRegen + " life regen");
                line.IsModifier = true;
                tooltips.Add(line);
            }
            if (fishingPower > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "fishPrefix", "+" + fishingPower + " fishing power");
                line.IsModifier = true;
                tooltips.Add(line);
            }
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix > 0)
            {
                player.GetDamage(DamageClass.Melee) += meleeDamage * 0.01f;
                player.GetDamage(DamageClass.Throwing) += thrownDamage * 0.01f;
                player.GetDamage(DamageClass.Ranged) += rangedDamage * 0.01f;
                player.GetDamage(DamageClass.Magic) += magicDamage * 0.01f;
                player.GetDamage(DamageClass.Summon) += summonDamage * 0.01f;
                player.statLifeMax2 += maxHealth;
                player.lifeRegen += lifeRegen;
                player.fishingSkill += fishingPower;
            }
        }
        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(meleeDamage);
            writer.Write(thrownDamage);
            writer.Write(rangedDamage);
            writer.Write(magicDamage);
            writer.Write(summonDamage);
            writer.Write(maxHealth);
            writer.Write(lifeRegen);
            writer.Write(fishingPower);
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            meleeDamage = reader.ReadInt32();
            thrownDamage = reader.ReadInt32();
            rangedDamage = reader.ReadInt32();
            magicDamage = reader.ReadInt32();
            summonDamage = reader.ReadInt32();
            maxHealth = reader.ReadInt32();
            lifeRegen = reader.ReadInt32();
            fishingPower = reader.ReadInt32();
        }
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context.Equals("bossBag"))
            {
                if (arg == ItemID.SkeletronBossBag)
                {
                    player.QuickSpawnItem(Mod.Find<ModItem>("SkellyStaff").Type, 1);
                }
                if (arg == ItemID.WallOfFleshBossBag)
                {
                    player.QuickSpawnItem(ItemID.LifeCrystal, 1 + Main.rand.Next(3));
                }
                if (arg == ItemID.PlanteraBossBag)
                {
                    player.QuickSpawnItem(ItemID.LifeFruit, 2 + Main.rand.Next(3));
                    if (Main.rand.Next(2) == 0)
                    {
                        player.QuickSpawnItem(Mod.Find<ModItem>("RoseWeave").Type, 1);
                    }
                }
                if (arg == ItemID.FishronBossBag)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        player.QuickSpawnItem(Mod.Find<ModItem>("BubbleBottle").Type, 999);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        player.QuickSpawnItem(Mod.Find<ModItem>("DukeFishRod").Type, 1);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        player.QuickSpawnItem(Mod.Find<ModItem>("MegaBubbleShield").Type, 1);
                    }
                }
            }
        }
        public static float LegendaryDamage()
        {
            float damageMult = 1f +                             //1
            (JoostWorld.downedPinkzor ? 0.05f : 0f) +           //1.05
            (JoostWorld.downedRogueTomato ? 0.05f : 0f) +       //1.1
            (JoostWorld.downedWoodGuardian ? 0.05f : 0f) +      //1.15
            (NPC.downedSlimeKing ? 0.1f : 0f) +                 //1.25
            (JoostWorld.downedFloweringCactoid ? 0.05f : 0f) +  //1.3
            (NPC.downedGoblins ? 0.1f : 0f) +                   //1.4
            (NPC.downedBoss1 ? 0.1f : 0f) +                     //1.5
            (JoostWorld.downedICU ? 0.1f : 0f) +                //1.6
            (NPC.downedBoss2 ? 0.1f : 0f) +                     //1.7
            (JoostWorld.downedSporeSpawn ? 0.1f : 0f) +         //1.8
            (DD2Event.DownedInvasionT1 ? 0.1f : 0f) +           //1.9
            (NPC.downedQueenBee ? 0.1f : 0f) +                  //2
            (JoostWorld.downedRoc ? 0.1f : 0f) +                //2.1
            (NPC.downedBoss3 ? 0.2f : 0f) +                     //2.3
            (JoostWorld.downedSkeletonDemoman ? 0.1f : 0f) +    //2.4
            (NPC.downedDeerclops ? 0.2f : 0f) +                 //2.6
            (JoostWorld.downedCactusWorm ? 0.2f : 0f) +         //2.8
            (JoostWorld.downedImpLord ? 0.1f : 0f) +            //2.9
            (Main.hardMode ? 0.35f : 0f) +                      //3.25
            (NPC.downedPirates ? 0.25f : 0f) +                  //3.5
            (JoostWorld.downedStormWyvern ? 0.5f : 0f) +        //4
            (NPC.downedQueenSlime ? 1f : 0f) +                  //5
            (NPC.downedMechBoss1 ? 1f : 0f) +                   //6
            (NPC.downedMechBoss2 ? 1f : 0f) +                   //7
            (NPC.downedMechBoss3 ? 1f : 0f) +                   //8                                          
            (DD2Event.DownedInvasionT2 ? 2f : 0f) +             //10
            (NPC.downedPlantBoss ? 2f : 0f) +                   //12
            (NPC.downedGolemBoss ? 3f : 0f) +                   //15
            (NPC.downedFishron ? 3f : 0f) +                     //18
            (NPC.downedMartians ? 3f : 0f) +                    //21
            (NPC.downedHalloweenTree ? 1.5f : 0f) +             //22.5
            (NPC.downedHalloweenKing ? 1.5f : 0f) +             //24
            (NPC.downedChristmasTree ? 1.5f : 0f) +             //25.5
            (NPC.downedChristmasSantank ? 1.5f : 0f) +          //27
            (NPC.downedChristmasIceQueen ? 1.5f : 0f) +         //28.5
            (NPC.downedEmpressOfLight ? 3.5f : 0f) +            //32
            (DD2Event.DownedInvasionT3 ? 4f : 0f) +             //36
            (NPC.downedAncientCultist ? 1f : 0f) +              //37
            (NPC.downedTowerNebula ? 1f : 0f) +                 //38
            (NPC.downedTowerSolar ? 1f : 0f) +                  //39
            (NPC.downedTowerVortex ? 1f: 0f) +                  //40
            (NPC.downedTowerStardust ? 1f : 0f) +               //41
            (NPC.downedMoonlord ? 9f : 0f) +                    //50
            (JoostWorld.downedJumboCactuar ? 10f : 0f) +        //60
            (JoostWorld.downedSAX ? 10f : 0f) +                 //70
            (JoostWorld.downedGilgamesh ? 10f : 0f);            //80
            return damageMult;
        }
    }
    public class EmptyHeartRod : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<JoostPlayer>().emptyHeart && player.chaosState && item.type == ItemID.RodofDiscord)
            {
                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                if (Main.rand.Next(2) == 0)
                {
                    damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
                }
                player.statLife = 0;
                player.KillMe(damageSource, 1.0, 0, false);
            }
            return base.CanUseItem(item, player);
        }
    }
    public class AncientStoneMoss : GlobalItem
    {
        public override bool? UseItem(Item item, Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (item.type == ItemID.StaffofRegrowth && player.controlUseItem)
            {
                int i = Player.tileTargetX;
                int j = Player.tileTargetY;
                if (Main.tile[i, j] != null && Main.tile[i, j].TileType == Mod.Find<ModTile>("AncientStone").Type)
                {
                    SoundEngine.PlaySound(SoundID.Dig, new Vector2(i * 16, j * 16));
                    Main.tile[i, j].TileType = (ushort)Mod.Find<ModTile>("AncientMossyStone").Type;
                    WorldGen.SquareTileFrame(i, j, true);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
                    }
                }
            }
            return base.UseItem(item, player);
        }
    }
    public class AmmoChance : GlobalItem
    {
        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            if (Main.rand.NextFloat() <= (1f - player.GetModPlayer<JoostModPlayer>().ammoConsume))
            {
                return false;
            }
            return true;
        }
    }
    public class grab : GlobalItem
    {
        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("IceBeamCannon").Type] > 0)
            {
                grabRange = 600;
            }
        }
    }
    public class ThrownChance : GlobalItem
    {
        public override bool ConsumeItem(Item item, Player player)
        {
            if (item.CountsAsClass(DamageClass.Throwing) && Main.rand.NextFloat() <= (1f - player.GetModPlayer<JoostModPlayer>().throwConsume))
            {
                return false;
            }
            return true;
        }
    }
    public class MeleeStrike : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (player.GetModPlayer<JoostModPlayer>().crimsonPommel)
            {
                if (target.life <= 0 && target.type != NPCID.TargetDummy && !target.HasBuff(Mod.Find<ModBuff>("LifeDrink").Type))
                {
                    float lifeStoled = target.lifeMax * 0.04f;
                    if ((int)lifeStoled > 0 && !player.moonLeech)
                    {
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
                    }
                }
                target.AddBuff(Mod.Find<ModBuff>("LifeDrink").Type, 1200, false);
            }
            if (player.GetModPlayer<JoostModPlayer>().corruptPommel)
            {
                if (target.life <= 0 && target.type != NPCID.TargetDummy && !target.HasBuff(Mod.Find<ModBuff>("CorruptSoul").Type))
                {
                    float damag = target.lifeMax * 0.25f;
                    if ((int)damag > 0)
                    {
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, -5, Mod.Find<ModProjectile>("CorruptedSoul").Type, (int)damag, 0, player.whoAmI);
                    }
                }
                target.AddBuff(Mod.Find<ModBuff>("CorruptSoul").Type, 1200, false);
            }
        }
        public override void OnHitPvp(Item item, Player player, Player target, int damage, bool crit)
        {
            if (player.GetModPlayer<JoostModPlayer>().crimsonPommel)
            {
                if (target.statLife <= 0 && !target.HasBuff(Mod.Find<ModBuff>("LifeDrink").Type))
                {
                    float lifeStoled = target.statLifeMax2 * 0.04f;
                    if ((int)lifeStoled > 0 && !player.moonLeech)
                    {
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
                    }
                }
                target.AddBuff(Mod.Find<ModBuff>("LifeDrink").Type, 1200, false);
            }
            if (player.GetModPlayer<JoostModPlayer>().corruptPommel)
            {
                if (target.statLife <= 0 && !target.HasBuff(Mod.Find<ModBuff>("CorruptSoul").Type))
                {
                    float damag = target.statLifeMax2 * 0.25f;
                    if ((int)damag > 0)
                    {
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, -5, Mod.Find<ModProjectile>("CorruptedSoul").Type, (int)damag, 0, player.whoAmI);
                    }
                }
                target.AddBuff(Mod.Find<ModBuff>("CorruptSoul").Type, 1200, false);
            }
        }
    }
    public class Defaults : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SandBlock)
            {
                item.value = 5;
            }
        }
    }
    public class JoostModPlayer : ModPlayer
    {
        public float ammoConsume = 1;
        public float throwConsume = 1;
        public bool crimsonPommel = false;
        public bool corruptPommel = false;
        public override void ResetEffects()
        {
            throwConsume = 1;
            ammoConsume = 1;
            crimsonPommel = false;
            corruptPommel = false;
        }
    }
}