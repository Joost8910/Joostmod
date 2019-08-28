using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Items;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;

namespace JoostMod
{
    public class JoostPlayer : ModPlayer
    {
        private const int saveVersion = 0;
        public bool cactuarMinions = false;
        public bool lunarRod = false;
        public bool powerSpirit = false;
        public bool IceXMinion = false;
        public bool WindMinion = false;
        public bool fishMinion = false;
        public bool EnkiduMinion = false;
        public bool emberMinion = false;
        public bool frostEmberMinion = false;
        public bool Gnome = false;
        public bool Skelly = false;
        public bool HarpyMinion = false;
        public bool icuMinion = false;
        public bool dirtMinion = false;
        public bool XShield = false;
        private int XShieldTimer = 60;
        public bool SpectreOrbs = false;
        private int SpectreOrbTimer = 40;
        public bool SkullSigil = false;
        private int SkullSigilTimer = 180;
        public bool bubbleShield = false;
        private int bubbleShieldTimer = 90;
        public bool megaBubbleShield = false;
        private int megaBubbleShieldTimer = 30;
        public bool hoverBoots = false;
        public bool spaceJump = false;
        private int hoverBootsTimer = 900;
        private int hoverBootsStart = 0;
        public bool GMagic = false;
        private int GMagicTimer = 63;
        public bool HavocPendant = false;
        public bool HarmonyPendant = false;
        public bool gMelee = false;
        public bool gRanged = false;
        public bool gRangedIsActive = false;
        public bool gThrown = false;
        private int gThrownTimer = 1200;
        private int sandStormTimer = 6;
        public bool sandStorm = false;
        public bool swordSapling = false;
        private int swordSaplingTimer = 10;
        public bool hatchetSapling = false;
        private int hatchetSaplingTimer = 18;
        public bool staffSapling = false;
        private int staffSaplingTimer = 37;
        public bool bowSapling = false;
        private int bowSaplingTimer = 27;
        public bool fishingSapling = false;
        public int spelunky = 0;
        public bool spelunkGlow = false;
        private int spelunkerTimer = 0;
        public bool legendOwn = false;
        public bool SaitamaOwn = false;
        public bool isLegend = false;
        public bool isUncleCarius = false;
        public bool isSaitama = false;
        public int GnunderCool = 0;
        public int spinTimer = 0;
        public bool planeMount = false;
        public int enemyIgnoreDefenseDamage = 0;
        public bool cactoidCommendation = false;
        private int cactoidCommendationTimer = 4500;
        public bool sporgan = false;
        public bool rocWings = false;
        private int rot = 0;
        public bool bonesHurt = false;
        public float boneHurtDamage = 1;
        public bool corruptSoul = false;
        public bool lifeRend = false;
        public bool infectedRed = false;
        public bool infectedGreen = false;
        public bool infectedBlue = false;
        public bool infectedYellow = false;
        public bool cactusBoots = false;
        private float cactusBootsTimer = 40;
        public bool fleshShield = false;
        private int fleshShieldTimer = 0;
        public int dashType = 0;
        public int dashDamage = 0;
        private bool[] dashHit = new bool[200];
        private bool dashBounce = false;
        public bool westStone = false;
        public bool eastStone = false;
        public bool highStone = false;
        public bool deepStone = false;
        public override void ResetEffects()
        {
            cactuarMinions = false;
            lunarRod = false;
            powerSpirit = false;
            IceXMinion = false;
            WindMinion = false;
            fishMinion = false;
            EnkiduMinion = false;
            emberMinion = false;
            frostEmberMinion = false;
            Gnome = false;
            Skelly = false;
            HarpyMinion = false;
            icuMinion = false;
            dirtMinion = false;
            XShield = false;
            SpectreOrbs = false;
            SkullSigil = false;
            GMagic = false;
            bubbleShield = false;
            megaBubbleShield = false;
            hoverBoots = false;
            spaceJump = false;
            HavocPendant = false;
            HarmonyPendant = false;
            gMelee = false;
            gThrown = false;
            gRanged = false;
            planeMount = false;
            sandStorm = false;
            swordSapling = false;
            hatchetSapling = false;
            staffSapling = false;
            bowSapling = false;
            fishingSapling = false;
            spelunky = 0;
            spelunkGlow = false;
            enemyIgnoreDefenseDamage = 0;
            cactoidCommendation = false;
            sporgan = false;
            rocWings = false;
            bonesHurt = false;
            corruptSoul = false;
            lifeRend = false;
            infectedRed = false;
            infectedGreen = false;
            infectedBlue = false;
            infectedYellow = false;
            if (!player.HasBuff(mod.BuffType("BoneHurt")))
            {
                boneHurtDamage = 1;
            }
            cactusBoots = false;
            fleshShield = false;
            dashType = 0;
            dashDamage = 0;
        }
        public override void UpdateBadLifeRegen()
        {
            if (bonesHurt)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                boneHurtDamage += 0.0167f;
                player.lifeRegen -= (int)boneHurtDamage * 2;
                if (player.boneArmor)
                {
                    player.lifeRegen -= (int)boneHurtDamage * 2;
                }
            }
            if (corruptSoul)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 10;
            }
            if (infectedRed && !XShield)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 5;
            }
            if (infectedGreen && !XShield)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 5;
            }
            if (infectedBlue && !XShield)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 5;
            }
            if (infectedYellow && !XShield)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 5;
            }
        }
        public override void FrameEffects()
        {
            if (player.HeldItem.type == mod.ItemType("CactusGlove"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("CactusGlove", EquipType.HandsOn);
            }
            if (player.HeldItem.type == mod.ItemType("ChlorophyteGlove"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("ChlorophyteGlove", EquipType.HandsOn);
            }
            if (player.HeldItem.type == mod.ItemType("GnunderGlove"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("GnunderGlove", EquipType.HandsOn);
            }
            if (player.HeldItem.type == mod.ItemType("GooGlove"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("GooGlove", EquipType.HandsOn);
            }
            if (player.HeldItem.type == mod.ItemType("HellstoneShuriken"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("HellstoneShuriken", EquipType.HandsOn);
            }
            if (player.HeldItem.type == mod.ItemType("PumpkinGlove"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("PumpkinGlove", EquipType.HandsOn);
            }
            if (player.HeldItem.type == mod.ItemType("StoneFist"))
            {
                player.handoff = (sbyte)mod.GetEquipSlot("StoneFist", EquipType.HandsOff);
            }
            if (player.HeldItem.type == mod.ItemType("OnePunch"))
            {
                player.handoff = (sbyte)mod.GetEquipSlot("OnePunch", EquipType.HandsOff);
            }
            if (player.HeldItem.type == mod.ItemType("MutantCannon"))
            {
                player.handoff = (sbyte)mod.GetEquipSlot("MutantCannon", EquipType.HandsOff);
            }
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (JoostMod.ArmorAbilityHotKey.JustPressed)
            {
                if (gThrown)
                {
                    if (gThrownTimer <= 0)
                    {
                        player.AddBuff(mod.BuffType("gThrownDodge"), 120);
                        gThrownTimer = 1200;
                    }
                }
                if (gRanged)
                {
                    player.AddBuff(mod.BuffType("gRangedBuff"), 2);
                    if (!gRangedIsActive)
                    {
                        gRangedIsActive = true;
                    }
                    else
                    {
                        gRangedIsActive = false;
                    }
                }
                if (GMagic && !player.HasBuff(BuffID.ManaSickness) && player.ownedProjectileCounts[mod.ProjectileType("BitterEndFriendly")] + player.ownedProjectileCounts[mod.ProjectileType("BitterEndFriendly2")] <= 0)
                {
                    if (player.statMana >= player.statManaMax2)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("BitterEndFriendly"), (int)(2000 * player.magicDamage), 20f, player.whoAmI);
                        player.manaRegenDelay = 180 * (2+player.manaRegenDelayBonus);
                        player.statMana *= 0;
                    }
                }
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (item.melee && gMelee && player.ownedProjectileCounts[mod.ProjectileType("Masamune")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), (int)(500 * player.allDamageMult * (player.allDamage + player.meleeDamage - 1f) * player.meleeDamageMult), 5f, player.whoAmI);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), 0, 0, player.whoAmI);
            }
        }
        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            if (item.melee && gMelee && player.ownedProjectileCounts[mod.ProjectileType("Masamune")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), (int)(500 * player.allDamageMult * (player.allDamage + player.meleeDamage - 1f) * player.meleeDamageMult), 5f, player.whoAmI);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), 0, 0, player.whoAmI);
            }
        }
        public override void OnHitNPCWithProj(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.melee && player.heldProj == projectile.whoAmI)
            {
                if (player.GetModPlayer<JoostModPlayer>(mod).crimsonPommel)
                {
                    if (target.life <= 0 && target.type != NPCID.TargetDummy && !target.HasBuff(mod.BuffType("LifeDrink")))
                    {
                        float lifeStoled = target.lifeMax * 0.04f;
                        if ((int)lifeStoled > 0 && !player.moonLeech)
                        {
                            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
                        }
                    }
                    target.AddBuff(mod.BuffType("LifeDrink"), 1200, false);
                }
                if (player.GetModPlayer<JoostModPlayer>(mod).corruptPommel)
                {
                    if (target.life <= 0 && target.type != NPCID.TargetDummy && !target.HasBuff(mod.BuffType("CorruptSoul")))
                    {
                        float damag = target.lifeMax * 0.25f;
                        if ((int)damag > 0)
                        {
                            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, -5, mod.ProjectileType("CorruptedSoul"), (int)damag, 0, player.whoAmI);
                        }
                    }
                    target.AddBuff(mod.BuffType("CorruptSoul"), 1200, false);
                }
                if (gMelee && player.ownedProjectileCounts[mod.ProjectileType("Masamune")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), (int)(500 * player.meleeDamage), 10f, player.whoAmI);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), 0, 0, player.whoAmI);
                }
            }
            if (sandStorm && projectile.thrown)
            {
                int erg = 80;
                if (Main.rand.Next(2) == 0)
                {
                    erg = 80;
                }
                else
                {
                    erg = -80;
                }

                float xPos = projectile.position.X + erg;
                Vector2 vector2 = new Vector2(xPos, projectile.position.Y + Main.rand.Next(-80, 81));

                float num80 = xPos;
                float speedX = (float)target.position.X - vector2.X;
                float speedY = (float)target.position.Y - vector2.Y;
                float dir = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                dir = 10 / num80;
                speedX *= dir * 150 * player.thrownVelocity;
                speedY *= dir * 150 * player.thrownVelocity;
                if (sandStormTimer <= 0)
                {
                    Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, mod.ProjectileType("Sand"), (int)(22 * player.thrownDamage), 1, projectile.owner);
                    sandStormTimer = 5;
                }

            }

        }
        public override void OnHitPvpWithProj(Projectile projectile, Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.melee && player.heldProj == projectile.whoAmI)
            {
                if (player.GetModPlayer<JoostModPlayer>(mod).crimsonPommel)
                {
                    if (target.statLife <= 0 && !target.HasBuff(mod.BuffType("LifeDrink")))
                    {
                        float lifeStoled = target.statLifeMax2 * 0.04f;
                        if ((int)lifeStoled > 0 && !player.moonLeech)
                        {
                            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
                        }
                    }
                    target.AddBuff(mod.BuffType("LifeDrink"), 1200, false);
                }
                if (player.GetModPlayer<JoostModPlayer>(mod).corruptPommel)
                {
                    if (target.statLife <= 0 && !target.HasBuff(mod.BuffType("CorruptSoul")))
                    {
                        float damag = target.statLifeMax2 * 0.25f;
                        if ((int)damag > 0)
                        {
                            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, -5, mod.ProjectileType("CorruptedSoul"), (int)damag, 0, player.whoAmI);
                        }
                    }
                    target.AddBuff(mod.BuffType("CorruptSoul"), 1200, false);
                }
                if (gMelee && player.ownedProjectileCounts[mod.ProjectileType("Masamune")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), (int)(500 * player.meleeDamage), 10f, player.whoAmI);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), 0, 0, player.whoAmI);
                }
            }
            if (sandStorm && projectile.thrown)
            {
                int erg = 80;
                if (Main.rand.Next(2) == 0)
                {
                    erg = 80;
                }
                else
                {
                    erg = -80;
                }

                float xPos = projectile.position.X + erg;
                Vector2 vector2 = new Vector2(xPos, projectile.position.Y + Main.rand.Next(-80, 81));

                float num80 = xPos;
                float speedX = (float)target.position.X - vector2.X;
                float speedY = (float)target.position.Y - vector2.Y;
                float dir = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                dir = 10 / num80;
                speedX *= dir * 150 * player.thrownVelocity;
                speedY *= dir * 150 * player.thrownVelocity;
                if (sandStormTimer <= 0)
                {
                    Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, mod.ProjectileType("Sand"), (int)(22 * player.thrownDamage), 1, projectile.owner);
                    sandStormTimer = 5;
                }

            }
        }
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk)
            {
                return;
            }
            if (liquidType == 0 && caughtType != 2334 && caughtType != 2335 && caughtType != 2336 && caughtType != 3203 && caughtType != 3204 && caughtType != 3205 && caughtType != 3206 && caughtType != 3207 && caughtType != 3208)
            {
                if (Main.rand.Next(Math.Max(5, 500 / power)) == 0 && worldLayer == 1 && !Main.hardMode && (player.position.X / 16 < 350 || player.position.X / 16 > Main.maxTilesX - 350))
                {
                    caughtType = mod.ItemType("GrenadeFish");
                }
                if (Main.rand.Next(Math.Max(20, 2000 / power)) == 0 && worldLayer == 1 && !Main.hardMode && (player.position.X / 16 < 350 || player.position.X / 16 > Main.maxTilesX - 350))
                {
                    caughtType = mod.ItemType("PufferfishStaff");
                }
                if (Main.rand.Next(Math.Max(25, 2500 / power)) == 0 && player.ZoneCorrupt && NPC.downedBoss2)
                {
                    caughtType = mod.ItemType("ToxicBucket");
                }
                if (Main.rand.Next(Math.Max(25, 2500 / power)) == 0 && player.ZoneCrimson && NPC.downedBoss2)
                {
                    caughtType = mod.ItemType("BloodyBucket");
                }
                if (Main.rand.Next(Math.Max(30, 3000 / power)) == 0 && worldLayer == 1 && !Main.hardMode && (player.position.X / 16 < 350 || player.position.X / 16 > Main.maxTilesX - 350))
                {
                    caughtType = mod.ItemType("BubbleShield");
                }
                if (Main.rand.Next(Math.Max(30, 6000 / power)) == 0 && (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3) && (player.position.X / 16 < 350 || player.position.X / 16 > Main.maxTilesX - 350))
                {
                    caughtType = mod.ItemType("Larpoon");
                }
                if (Main.rand.Next(Math.Max(30, 6000 / power)) == 0 && (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3) && (player.ZoneJungle || player.ZoneSnow))
                {
                    caughtType = mod.ItemType("RoboCod");
                }
                if (Main.rand.Next(Math.Max(60, 12000 / power)) == 0 && NPC.downedGolemBoss && Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16)].wall == WallID.LihzahrdBrickUnsafe)
                {
                    caughtType = mod.ItemType("Sunfish");
                }
            }
            if (lunarRod && Main.rand.Next(4) == 0)
            {
                caughtType = 3456 + Main.rand.Next(4);
            }
            if (Main.rand.Next(Math.Max(100, 50000 / power)) == 0 && liquidType == 0)
            {
                if (player.position.X / 16 < 350 && !westStone)
                {
                    caughtType = mod.ItemType("SeaStoneWest");
                }
                if (player.position.X / 16 > Main.maxTilesX - 350 && !eastStone)
                {
                    caughtType = mod.ItemType("SeaStoneEast");
                }
                if (worldLayer >= 3 && !deepStone)
                {
                    caughtType = mod.ItemType("SeaStoneDeep");
                }
                if (worldLayer <= 0 && !highStone)
                {
                    caughtType = mod.ItemType("SeaStoneHigh");
                }
            }
            if (Main.rand.Next(Math.Max(4, 400 / power)) == 0 && liquidType == 0 && isUncleCarius)
            {
                if (player.position.X / 16 < 350 && !westStone)
                {
                    caughtType = mod.ItemType("SeaStoneWest");
                }
                if (player.position.X / 16 > Main.maxTilesX - 350 && !eastStone)
                {
                    caughtType = mod.ItemType("SeaStoneEast");
                }
                if (worldLayer >= 3 && !deepStone)
                {
                    caughtType = mod.ItemType("SeaStoneDeep");
                }
                if (worldLayer <= 0 && !highStone)
                {
                    caughtType = mod.ItemType("SeaStoneHigh");
                }
            }
        }
        public override void PostUpdateEquips()
        {
            if (player.name == "Grognak" || player.name == "Larkus" || player.name == "Gnunderson" || player.name == "Boook" || player.name == "David" || player.name.Contains("Joost"))
            {
                isLegend = true;
            }
            if (player.name == "Uncle Carius" || player.name.Contains("Joost"))
            {
                isUncleCarius = true;
            }
            if (player.name == "Saitama")
            {
                isSaitama = true;
            }
            if (gRanged)
            {
                if (gRangedIsActive)
                {
                    player.AddBuff(mod.BuffType("gRangedBuff"), 2);
                    player.rangedDamageMult *= 1 + (player.statDefense * 0.005f);
                    player.statDefense = 0;
                }
            }
            if (!gRanged)
            {
                gRangedIsActive = false;
            }
            if (sandStorm)
            {
                sandStormTimer--;
                if (sandStormTimer < 0)
                    sandStormTimer = 0;
            }
            else
            {
                sandStormTimer = 6;
            }
            if (gThrown)
            {
                if (gThrownTimer > 0)
                {
                    player.AddBuff(mod.BuffType("gThrownCooldown"), gThrownTimer);
                }
                gThrownTimer--;
                if (gThrownTimer < 0)
                    gThrownTimer = 0;
            }
            else
            {
                gThrownTimer = 1200;
            }
            if (EnkiduMinion)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("EnkiduMinion")] <= 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("EnkiduMinion"), (int)(600 * player.minionDamage), 10f, player.whoAmI);
                }
            }
            if (XShield)
            {
                XShieldTimer--;
                if (XShieldTimer < 0)
                {
                    int type = mod.ProjectileType("XParasite");
                    int damage = (int)(300 * player.allDamage * player.allDamageMult);

                    float summon = player.minionDamage * player.minionDamageMult;
                    int melee = (int)(300 * (player.allDamage + player.meleeDamage - 1) * player.meleeDamageMult * player.allDamageMult);
                    int ranged = (int)(300 * (player.allDamage + player.rangedDamage - 1) * player.rangedDamageMult * player.allDamageMult);
                    int magic = (int)(300 * (player.allDamage + player.magicDamage - 1) * player.magicDamageMult * player.allDamageMult);
                    int thrown = (int)(300 * (player.allDamage + player.thrownDamage - 1) * player.thrownDamageMult * player.allDamageMult);

                    bool flag = Main.rand.NextBool(4);

                    if (!flag)
                    {
                        IList<int> dmgType = new List<int>();
                        dmgType.Add((int)(summon * 300));
                        dmgType.Add(melee);
                        dmgType.Add(ranged);
                        dmgType.Add(magic);
                        dmgType.Add(thrown);

                        int maxValue = 0;
                        foreach (int value in dmgType)
                        {
                            if (value > maxValue)
                            {
                                maxValue = value;
                            }
                        }
                        if (maxValue == melee)
                        {
                            type = mod.ProjectileType("XParasiteYellow");
                            damage = melee;
                        }
                        if (maxValue == ranged)
                        {
                            type = mod.ProjectileType("XParasiteRed");
                            damage = ranged;
                        }
                        if (maxValue == magic)
                        {
                            type = mod.ProjectileType("XParasite");
                            damage = magic;
                        }
                        if (maxValue == thrown)
                        {
                            type = mod.ProjectileType("XParasiteGreen");
                            damage = thrown;
                        }
                        if (maxValue == summon)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                type = mod.ProjectileType("XParasiteYellow");
                                damage = (int)(melee * summon);
                                break;
                            case 2:
                                type = mod.ProjectileType("XParasiteGreen");
                                damage = (int)(thrown * summon);
                                break;
                            case 3:
                                type = mod.ProjectileType("XParasiteRed");
                                damage = (int)(ranged * summon);
                                break;
                            default:
                                type = mod.ProjectileType("XParasite");
                                damage = (int)(magic * summon);
                                break;
                        }
                    }
                    Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                    
                    Projectile.NewProjectile(player.MountedCenter.X, player.MountedCenter.Y, dir.X, dir.Y, type, damage, 3, player.whoAmI);
                    flag = flag ? flag : Main.rand.NextBool(4);
                    if (flag)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                type = mod.ProjectileType("XParasiteYellow");
                                damage = (int)(melee * summon);
                                break;
                            case 2:
                                type = mod.ProjectileType("XParasiteGreen");
                                damage = (int)(thrown * summon);
                                break;
                            case 3:
                                type = mod.ProjectileType("XParasiteRed");
                                damage = (int)(ranged * summon);
                                break;
                            default:
                                type = mod.ProjectileType("XParasite");
                                damage = (int)(magic * summon);
                                break;
                        }
                    }
                    Projectile.NewProjectile(player.MountedCenter.X, player.MountedCenter.Y, -dir.X, -dir.Y, type, damage, 3, player.whoAmI);
                    flag = flag ? flag : Main.rand.NextBool(4);
                    if (flag)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                type = mod.ProjectileType("XParasiteYellow");
                                damage = (int)(melee * summon);
                                break;
                            case 2:
                                type = mod.ProjectileType("XParasiteGreen");
                                damage = (int)(thrown * summon);
                                break;
                            case 3:
                                type = mod.ProjectileType("XParasiteRed");
                                damage = (int)(ranged * summon);
                                break;
                            default:
                                type = mod.ProjectileType("XParasite");
                                damage = (int)(magic * summon);
                                break;
                        }
                    }
                    dir = dir.RotatedBy(90f * 0.0174f);
                    Projectile.NewProjectile(player.MountedCenter.X, player.MountedCenter.Y, dir.X, dir.Y, type, damage, 3, player.whoAmI);
                    flag = flag ? flag : Main.rand.NextBool(4);
                    if (flag)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                type = mod.ProjectileType("XParasiteYellow");
                                damage = (int)(melee * summon);
                                break;
                            case 2:
                                type = mod.ProjectileType("XParasiteGreen");
                                damage = (int)(thrown * summon);
                                break;
                            case 3:
                                type = mod.ProjectileType("XParasiteRed");
                                damage = (int)(ranged * summon);
                                break;
                            default:
                                type = mod.ProjectileType("XParasite");
                                damage = (int)(magic * summon);
                                break;
                        }
                    }
                    Projectile.NewProjectile(player.MountedCenter.X, player.MountedCenter.Y, -dir.X, -dir.Y, type, damage, 3, player.whoAmI);
                    
                    XShieldTimer += 90;
                }
            }
            else
            {
                XShieldTimer = 90;
            }
            if (cactoidCommendation)
            {
                cactoidCommendationTimer -= 1 + Main.rand.Next(2);
                if (cactoidCommendationTimer <= 0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Cactoid"));
                    }
                    else
                    {
                        NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Cactite"));
                    }
                    cactoidCommendationTimer = 4500;
                }
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].team == player.team && player.team > 0)
                    {
                        Main.player[i].AddBuff(mod.BuffType("CactoidFriend"), 5, true);
                    }
                }
            }
            else
            {
                cactoidCommendationTimer = 4500;
            }
            if (cactusBoots && player.velocity.Y == 0)
            {
                cactusBootsTimer -= Math.Abs(player.velocity.X);
                if (cactusBootsTimer < 0)
                {
                    bool cflag = false;
                    int c = 3;
                    for (c = 3; c < 8 + player.extraAccessorySlots; c++)
                    {
                        if (player.armor[c].type == mod.ItemType("CactusBoots"))
                        {
                            cflag = true;
                            break;
                        }
                    }
                    if (cflag)
                    {
                        Projectile.NewProjectile(player.Center.X, player.position.Y, 0, 7, mod.ProjectileType("BootCactus"), player.GetWeaponDamage(player.armor[c]), player.GetWeaponKnockback(player.armor[c], player.armor[c].knockBack), player.whoAmI);
                    }
                    cactusBootsTimer = 40;
                }
            }
            else
            {
                cactusBootsTimer = 40;
            }
            if (SpectreOrbs)
            {
                SpectreOrbTimer--;
                if (SpectreOrbTimer < 0)
                {
                    bool sflag = false;
                    int s = 3;
                    for (s = 3; s < 8 + player.extraAccessorySlots; s++)
                    {
                        if (player.armor[s].type == mod.ItemType("SpectralOrb"))
                        {
                            sflag = true;
                            break;
                        }
                    }
                    if (sflag)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 2f, mod.ProjectileType("SpectreOrb"), player.GetWeaponDamage(player.armor[s]), player.GetWeaponKnockback(player.armor[s], player.armor[s].knockBack), player.whoAmI);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 2f, mod.ProjectileType("SpectreOrb"), player.GetWeaponDamage(player.armor[s]), player.GetWeaponKnockback(player.armor[s], player.armor[s].knockBack), player.whoAmI, 0f, 180f);
                    }
                    SpectreOrbTimer = 40;
                }
            }
            else
            {
                SpectreOrbTimer = 40;
            }
            if (SkullSigil && player.statLife <= player.statLifeMax2 / 2)
            {
                SkullSigilTimer--;
                if (SkullSigilTimer % 20 == 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("ShadowAura"), 1, 1, player.whoAmI);
                }
                if (SkullSigilTimer < 0)
                {
                    bool skflag = false;
                    int sk = 3;
                    for (sk = 3; sk < 8 + player.extraAccessorySlots; sk++)
                    {
                        if (player.armor[sk].type == mod.ItemType("SigilofSkulls") || player.armor[sk].type == mod.ItemType("ScrollofDeath"))
                        {
                            skflag = true;
                            break;
                        }
                    }
                    if (skflag)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 2f, 2f, mod.ProjectileType("Skull"), player.GetWeaponDamage(player.armor[sk]), player.GetWeaponKnockback(player.armor[sk], player.armor[sk].knockBack), player.whoAmI, 45f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 2f, -2f, mod.ProjectileType("Skull"), player.GetWeaponDamage(player.armor[sk]), player.GetWeaponKnockback(player.armor[sk], player.armor[sk].knockBack), player.whoAmI, 135f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, 2f, mod.ProjectileType("Skull"), player.GetWeaponDamage(player.armor[sk]), player.GetWeaponKnockback(player.armor[sk], player.armor[sk].knockBack), player.whoAmI, 225f);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, -2f, mod.ProjectileType("Skull"), player.GetWeaponDamage(player.armor[sk]), player.GetWeaponKnockback(player.armor[sk], player.armor[sk].knockBack), player.whoAmI, 315f);
                    }
                    SkullSigilTimer = 180;
                }
            }
            else
            {
                SkullSigilTimer = 180;
            }
            if (GnunderCool > 0)
            {
                GnunderCool--;
            }
            if (spelunky > 0)
            {
                spelunkerTimer++;
                if (spelunkerTimer >= 10)
                {
                    spelunkerTimer = 0;
                    int tileX = (int)player.Center.X / 16;
                    int tileY = (int)player.Center.Y / 16;
                    int num2;
                    for (int i = tileX - spelunky; i <= tileX + spelunky; i = num2 + 1)
                    {
                        for (int j = tileY - spelunky; j <= tileY + spelunky; j = num2 + 1)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                Vector2 vector = new Vector2((float)(tileX - i), (float)(tileY - j));
                                if (vector.Length() < (float)spelunky && i > 0 && i < Main.maxTilesX - 1 && j > 0 && j < Main.maxTilesY - 1 && Main.tile[i, j] != null && Main.tile[i, j].active())
                                {
                                    bool flag = false;
                                    if (Main.tile[i, j].type == 185 && Main.tile[i, j].frameY == 18)
                                    {
                                        if (Main.tile[i, j].frameX >= 576 && Main.tile[i, j].frameX <= 882)
                                        {
                                            flag = true;
                                        }
                                    }
                                    else if (Main.tile[i, j].type == 186 && Main.tile[i, j].frameX >= 864 && Main.tile[i, j].frameX <= 1170)
                                    {
                                        flag = true;
                                    }
                                    if (flag || Main.tileSpelunker[(int)Main.tile[i, j].type] || (Main.tileAlch[(int)Main.tile[i, j].type] && Main.tile[i, j].type != 82))
                                    {
                                        int dust = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
                                        Main.dust[dust].fadeIn = 0.75f;
                                        Dust dust2 = Main.dust[dust];
                                        dust2.velocity *= 0.1f;
                                        if (spelunkGlow)
                                        {
                                            int dust3 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 213, 0f, 0f, 150, default(Color), 0.3f);
                                            Main.dust[dust3].fadeIn = 0.75f;
                                            Dust dust4 = Main.dust[dust3];
                                            dust4.velocity *= 0.1f;
                                        }
                                    }
                                }
                            }
                            num2 = j;
                        }
                        num2 = i;
                    }
                }
            }
            if (swordSapling)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(player, false))
                    {
                        if (Vector2.Distance(npc.Center, player.Center) < 70 && (player.Center.X - npc.Center.X) * player.direction > 0 && Collision.CanHitLine(player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = player.DirectionTo(npc.Center);
                        }
                    }
                }
                swordSaplingTimer--;
                if (swordSaplingTimer < 0 && target)
                {
                    bool flag = false;
                    int s = 3;
                    for (s = 3; s < 8 + player.extraAccessorySlots; s++)
                    {
                        if (player.armor[s].type == mod.ItemType("SwordSapling"))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag && player.ownedProjectileCounts[mod.ProjectileType("SaplingSword")] < 1)
                    {
                        Main.PlaySound(2, player.Center, 1);
                        Projectile.NewProjectile(player.Center.X + 16*player.direction, player.Center.Y, shoot.X * 3, shoot.Y * 3, mod.ProjectileType("SaplingSword"), player.GetWeaponDamage(player.armor[s]), player.GetWeaponKnockback(player.armor[s], player.armor[s].knockBack), player.whoAmI);
                    }
                    swordSaplingTimer = 9;
                }
            }
            else
            {
                swordSaplingTimer = 9;
            }
            if (hatchetSapling)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(player, false))
                    {
                        if (Vector2.Distance(npc.Center, player.Center) < 300 && (player.Center.X - npc.Center.X) * player.direction > 0 && Collision.CanHitLine(player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = player.DirectionTo(npc.Center);
                        }
                    }
                }
                hatchetSaplingTimer--;
                if (hatchetSaplingTimer < 0 && target)
                {
                    bool flag = false;
                    int s = 3;
                    for (s = 3; s < 8 + player.extraAccessorySlots; s++)
                    {
                        if (player.armor[s].type == mod.ItemType("HatchetSapling"))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag && player.ownedProjectileCounts[mod.ProjectileType("CopperHatchet")] + player.ownedProjectileCounts[mod.ProjectileType("CopperHatchet2")] < 3)
                    {
                        Main.PlaySound(2, player.Center, 19);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, shoot.X * 12 * player.thrownVelocity, shoot.Y * 12 * player.thrownVelocity, mod.ProjectileType("CopperHatchet"), player.GetWeaponDamage(player.armor[s]), player.GetWeaponKnockback(player.armor[s], player.armor[s].knockBack), player.whoAmI);
                    }
                    hatchetSaplingTimer = 15;
                }
            }
            else
            {
                hatchetSaplingTimer = 15;
            }
            if (bowSapling)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(player, false))
                    {
                        if (Vector2.Distance(npc.Center, player.Center) < 800 && (player.Center.X - npc.Center.X) * player.direction > 0 && Collision.CanHitLine(player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = player.DirectionTo(npc.Center);
                        }
                    }
                }
                bowSaplingTimer--;
                if (bowSaplingTimer < 0 && target)
                {
                    bool flag = false;
                    int s = 3;
                    for (s = 3; s < 8 + player.extraAccessorySlots; s++)
                    {
                        if (player.armor[s].type == mod.ItemType("BowSapling"))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Main.PlaySound(2, player.Center, 5);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, shoot.X * 9.6f, shoot.Y * 9.6f, 1, player.GetWeaponDamage(player.armor[s]), player.GetWeaponKnockback(player.armor[s], player.armor[s].knockBack), player.whoAmI);
                    }
                    bowSaplingTimer = 27;
                }
            }
            else
            {
                bowSaplingTimer = 27;
            }
            if (staffSapling)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(player, false))
                    {
                        if (Vector2.Distance(npc.Center, player.Center) < 650 && (player.Center.X - npc.Center.X) * player.direction > 0 && Collision.CanHitLine(player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = player.DirectionTo(npc.Center);
                        }
                    }
                }
                staffSaplingTimer--;
                if (staffSaplingTimer < 0 && target)
                {
                    bool flag = false;
                    int s = 3;
                    for (s = 3; s < 8 + player.extraAccessorySlots; s++)
                    {
                        if (player.armor[s].type == mod.ItemType("StaffSapling"))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, shoot.X * 6.5f, shoot.Y * 6.5f, 121, player.GetWeaponDamage(player.armor[s]), player.GetWeaponKnockback(player.armor[s], player.armor[s].knockBack), player.whoAmI);
                    }
                    staffSaplingTimer = 37;
                }
            }
            else
            {
                staffSaplingTimer = 37;
            }
            if (fishingSapling && player.HeldItem.fishingPole > 0 && player.ownedProjectileCounts[mod.ProjectileType("SaplingFishHook")] < 1 && !player.CCed && !player.noItems && !player.pulley && !player.dead)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, -player.direction * 2, 0, mod.ProjectileType("SaplingFishHook"), 10, 0, player.whoAmI);
                Main.PlaySound(SoundID.Item1, player.Center);
            }
            if (bubbleShield)
            {
                bubbleShieldTimer--;
                if (bubbleShieldTimer < 0)
                {
                    bool bflag = false;
                    int b = 3;
                    for (b = 3; b < 8 + player.extraAccessorySlots; b++)
                    {
                        if (player.armor[b].type == mod.ItemType("BubbleShield"))
                        {
                            bflag = true;
                            break;
                        }
                    }
                    if (bflag)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 1f, 0f, mod.ProjectileType("BubbleShield"), player.GetWeaponDamage(player.armor[b]), player.GetWeaponKnockback(player.armor[b], player.armor[b].knockBack), player.whoAmI);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -1f, 0f, mod.ProjectileType("BubbleShield"), player.GetWeaponDamage(player.armor[b]), player.GetWeaponKnockback(player.armor[b], player.armor[b].knockBack), player.whoAmI);
                    }
                    bubbleShieldTimer = 90;
                }
            }
            else
            {
                bubbleShieldTimer = 90;
            }
            if (megaBubbleShield)
            {
                megaBubbleShieldTimer--;
                if (megaBubbleShieldTimer < 0)
                {
                    bool mbflag = false;
                    int mb = 3;
                    for (mb = 3; mb < 8 + player.extraAccessorySlots; mb++)
                    {
                        if (player.armor[mb].type == mod.ItemType("MegaBubbleShield"))
                        {
                            mbflag = true;
                            break;
                        }
                    }
                    if (mbflag)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 1f, 0f, mod.ProjectileType("MegaBubbleShield"), player.GetWeaponDamage(player.armor[mb]), player.GetWeaponKnockback(player.armor[mb], player.armor[mb].knockBack), player.whoAmI);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -1f, 0f, mod.ProjectileType("MegaBubbleShield"), player.GetWeaponDamage(player.armor[mb]), player.GetWeaponKnockback(player.armor[mb], player.armor[mb].knockBack), player.whoAmI);
                    }
                    megaBubbleShieldTimer = 30;
                }
            }
            else
            {
                megaBubbleShieldTimer = 30;
            }
            if (hoverBoots && player.velocity.Y != 0 && hoverBootsTimer > 0 && (player.controlLeft || player.controlRight) && !player.controlDown)
            {
                player.fallStart = (int)(player.position.Y / 16f);
                hoverBootsTimer--;
                player.slowFall = false;
                float num = player.gravity;
                if (player.gravDir == 1f && player.velocity.Y > -num)
                {
                    player.velocity.Y = -(num + 1E-06f);
                    if (hoverBootsStart <= 0)
                    {
                        hoverBootsStart = 1;
                        player.velocity.Y -= num;
                    }
                    player.slippy2 = true;
                    player.maxRunSpeed *= 1.75f;
                }
                else if (player.gravDir == -1f && player.velocity.Y < num)
                {
                    player.velocity.Y = num + 1E-06f;
                    if (hoverBootsStart <= 0)
                    {
                        hoverBootsStart = 1;
                        player.velocity.Y += num;
                    }
                    player.slippy2 = true;
                    player.maxRunSpeed *= 1.75f;
                }
            }
            if (hoverBoots && player.velocity.Y == 0 && hoverBootsTimer < 900)
            {
                hoverBootsTimer += 10;
                hoverBootsStart = 0;
            }
            if (player.velocity.Y != 0 && spaceJump)
            {
                if (player.controlJump)
                {
                    player.fallStart = (int)(player.position.Y / 16f);
                    float num = player.gravity;
                    if (player.gravDir == 1f && player.velocity.Y > num * 15)
                    {
                        player.velocity.Y = -12f;
                        spinTimer = 45;
                        Main.PlaySound(16, (int)player.position.X, (int)player.position.Y);
                        for (int i = 0; i < 7; i++)
                        {
                            Dust.NewDust(player.position, player.width, player.height, 16, Main.rand.Next(-5, 5), Main.rand.Next(-5, 0), 0, default(Color), Main.rand.Next(2, 5) * 0.2f);
                        }
                    }
                    else if (player.gravDir == -1f && player.velocity.Y < -num * 15)
                    {
                        player.velocity.Y = 12f;
                        spinTimer = 45;
                        Main.PlaySound(16, (int)player.position.X, (int)player.position.Y);
                        for (int i = 0; i < 7; i++)
                        {
                            Dust.NewDust(player.position, player.width, player.height, 16, Main.rand.Next(-5, 5), Main.rand.Next(0, 5), 0, default(Color), Main.rand.Next(2, 5) * 0.2f);
                        }
                    }
                }
            }
            if (spinTimer > 0)
            {
                spinTimer--;
                player.fullRotation = (float)(spinTimer * 24 * 0.0174f * -player.direction * player.gravDir);
                player.fullRotationOrigin = player.Center - player.position;
                if (spinTimer % 15 == 0)
                {
                    Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, 7);
                }
                if (player.velocity.Y == 0 || player.mount.Active || player.itemAnimation != 0)
                {
                    player.fullRotation = 0f;
                    spinTimer = 0;
                }
            }
            else if (rocWings && player.ownedProjectileCounts[mod.ProjectileType("SwingyHook")] + player.ownedProjectileCounts[mod.ProjectileType("MobHook")] + player.ownedProjectileCounts[mod.ProjectileType("EnchantedSwingyHook")] + player.ownedProjectileCounts[mod.ProjectileType("CactusHook")] <= 0 && player.velocity.Y != 0 && !player.mount.Active && player.controlUp && !player.controlJump && !player.pulley && player.itemAnimation == 0)
            {
                int rotSpeed = 5;
                if (player.wet)
                {
                    if (player.honeyWet)
                    {
                        player.maxFallSpeed = 10.5f;
                    }
                    else
                    {
                        player.maxFallSpeed = 24.5f;
                    }
                }
                else
                {
                    player.maxFallSpeed = 35f;
                }
                if (player.direction == 1)
                {
                    if (player.controlRight)
                    {
                        if (rot < 90)
                        {
                            rot += rotSpeed;
                        }
                        else
                        {
                            player.direction = -1;
                        }
                    }
                    if (player.controlLeft)
                    {
                        if (rot > -90)
                        {
                            rot -= rotSpeed;
                        }
                        else
                        {
                            player.direction = -1;
                        }
                    }
                }
                else
                {
                    if (player.controlLeft)
                    {
                        if (rot < 90)
                        {
                            rot += rotSpeed;
                        }
                        else
                        {
                            player.direction = 1;
                        }
                    }
                    if (player.controlRight)
                    {
                        if (rot > -90)
                        {
                            rot -= rotSpeed;
                        }
                        else
                        {
                            player.direction = 1;
                        }
                    }
                }
                player.fullRotation = (rot * 0.0174f * player.gravDir * player.direction);
                player.fullRotationOrigin = player.Center - player.position;
                float speed = player.velocity.Length();
                player.velocity.X = (float)Math.Cos(player.fullRotation) * speed * player.direction;
                if (speed > 3)
                {
                    player.velocity.Y = ((float)Math.Sin(player.fullRotation) * speed * player.direction);
                    if (player.velocity.Y * player.gravDir >= 0 && player.velocity.Y * player.gravDir < player.gravity)
                    {
                        player.velocity.Y = player.gravity * player.gravDir;
                    }
                }
                else if (rot < 90)
                {
                    if (player.direction == 1)
                    {
                        if (!player.controlLeft)
                        {
                            rot += rotSpeed;
                        }
                        else
                        {
                            player.direction = -1;
                        }
                    }
                    else
                    {
                        if (!player.controlRight)
                        {
                            rot += rotSpeed;
                        }
                        else
                        {
                            player.direction = 1;
                        }
                    }
                }
                player.slowFall = false;
                player.controlLeft = false;
                player.controlRight = false;
            }
            else if (rot != 0)
            {
                rot = 0;
                player.fullRotation = 0f;
            }
            if (planeMount && player.velocity.Y != 0 && player.itemAnimation == 0)
            {
                player.fullRotation = (float)(Math.Atan2(player.velocity.Y, player.velocity.X)) + (player.velocity.X > 0 || (player.velocity.X == 0 && player.direction == 1) ? 0 : (float)Math.PI);
                player.fullRotationOrigin = player.Center - player.position + new Vector2(0, player.height / 2);
            }
            if (planeMount && player.velocity.Y == 0)
            {
                player.fullRotation = 0f;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("CactusHook")] > 0)
            {
                player.buffImmune[BuffID.Suffocation] = true;
            }
            if (fleshShield)
            {
                fleshShieldTimer++;
                if (fleshShieldTimer > 2 + (int)(((float)player.statLife / (float)player.statLifeMax2) * 200f))
                {
                    Projectile.NewProjectile(player.Center.X + player.direction * 20, player.Center.Y, player.direction * 10, 0, mod.ProjectileType("Leech"), (int)(20 * player.meleeDamage), 1, player.whoAmI);
                    fleshShieldTimer = 0;
                }
            }
            else
            {
                fleshShieldTimer = 0;
            }
            if (player.dash <= 0)
            {
                Dash();
            }
        }
        public override void UpdateDead()
        {
            if (corruptSoul)
            {
                float damag = player.statLifeMax2 * 0.25f;
                float num3 = 0f;
                int ploya = player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[player.whoAmI].hostile && !Main.player[i].hostile) && Main.player[player.whoAmI].team != Main.player[i].team) && Math.Abs(Main.player[i].Center.X - player.Center.X + Math.Abs(Main.player[i].Center.Y - player.Center.Y)) < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num3)
                    {
                        num3 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        ploya = i;
                    }
                }
                if ((int)damag > 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, -5, mod.ProjectileType("CorruptedSoul"), (int)damag, 0, ploya);
                }
                corruptSoul = false;
            }
            if (lifeRend)
            {
                float lifeStoled = player.statLifeMax2 * 0.05f;
                float num3 = 0f;
                int ploya = player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[player.whoAmI].hostile && !Main.player[i].hostile) && Main.player[player.whoAmI].team != Main.player[i].team) && Math.Abs(Main.player[i].Center.X - player.Center.X + Math.Abs(Main.player[i].Center.Y - player.Center.Y)) < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num3)
                    {
                        num3 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        ploya = i;
                    }
                }
                if ((int)lifeStoled > 0 && !player.moonLeech)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, (float)ploya, lifeStoled);
                }
                lifeRend = false;
            }
            if (infectedBlue)
            {
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("IceXParasite"));
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("IceXParasite"));
                infectedBlue = false;
            }
            if (infectedGreen)
            {
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("GreenXParasite"));
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("GreenXParasite"));
                infectedGreen = false;
            }
            if (infectedRed)
            {
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("RedXParasite"));
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("RedXParasite"));
                infectedRed = false;
            }
            if (infectedYellow)
            {
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("XParasite"));
                NPC.NewNPC((int)player.MountedCenter.X, (int)player.MountedCenter.Y, mod.NPCType("XParasite"));
                infectedYellow = false;
            }
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
                    ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (enemyIgnoreDefenseDamage > 0)
            {
                damage = enemyIgnoreDefenseDamage;
                customDamage = true;
            }
            enemyIgnoreDefenseDamage = 0;
            if (sporgan)
            {
                bool sflag = false;
                int s = 3;
                for (s = 3; s < 8 + player.extraAccessorySlots; s++)
                {
                    if (player.armor[s].type == mod.ItemType("Sporgan"))
                    {
                        sflag = true;
                        break;
                    }
                }
                if (sflag)
                {
                    int num = 10;
                    double deltaAngle = (float)(Math.PI * 2) / num;
                    for (int i = 0; i < num; i++)
                    {
                        double offsetAngle = (float)(Math.PI / 2) + deltaAngle * i;
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 3 * (float)Math.Sin(offsetAngle), 3 * (float)Math.Cos(offsetAngle), 228, player.GetWeaponDamage(player.armor[s]), 0, player.whoAmI);
                    }
                }
            }
            if (player.HasBuff(mod.BuffType("gThrownDodge")))
            {
                player.AddBuff(mod.BuffType("gThrownBuff"), 200);
                player.longInvince = true;
                player.ShadowDodge();
                for (int j = 0; j < 80; j++)
                {
                    int num = Dust.NewDust(player.position, player.width, player.height, 31, 0f, 0f, 0, Color.Black, 1f);
                    Dust dust = Main.dust[num];
                    dust.position.X = dust.position.X + (float)Main.rand.Next(-20, 21);
                    dust.position.Y = dust.position.Y + (float)Main.rand.Next(-20, 21);
                    dust.velocity *= 0.4f;
                    dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    if (Main.rand.Next(2) == 0)
                    {
                        dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                        dust.noGravity = true;
                    }
                }
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int j = 0; j < 22; j++)
                    {
                        if (player.buffTime[j] > 0 && player.buffType[j] == mod.BuffType("gThrownDodge"))
                        {
                            player.DelBuff(j);
                        }
                    }
                }
                return false;
            }
            if (XShield && XShieldTimer > 0)
            {
                XShieldTimer -= damage;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public void Dash()
        {
            if (dashType == 1 && player.dashDelay < 0 && player.whoAmI == Main.myPlayer)
            {
                Rectangle rectangle = new Rectangle((int)((double)player.position.X + (double)player.velocity.X * 0.5 - 4.0), (int)((double)player.position.Y + (double)player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly)
                    {
                        NPC npc = Main.npc[i];
                        Rectangle rect = npc.getRect();
                        if (rectangle.Intersects(rect) && (npc.noTileCollide || player.CanHit(npc)))
                        {
                            bool crit = false;
                            if (Main.rand.Next(100) < player.meleeCrit)
                            {
                                crit = true;
                            }
                            int dir = player.direction;
                            if (player.velocity.X < 0f)
                            {
                                dir = -1;
                            }
                            if (player.velocity.X > 0f)
                            {
                                dir = 1;
                            }
                            if (!dashHit[i])
                            {
                                player.ApplyDamageToNPC(npc, dashDamage, 0, dir, crit);
                                dashHit[i] = true;
                            }
                            if (npc.knockBackResist > 0 && (player.velocity.X > 12 || player.velocity.X < -12 || player.velocity.X < -Math.Max(player.accRunSpeed, player.maxRunSpeed) || player.velocity.X > Math.Max(player.accRunSpeed, player.maxRunSpeed)))
                            {
                                float push = player.Center.X + 12;
                                if (dir < 0)
                                {
                                    push = (player.Center.X - 12) - npc.width;
                                }
                                Vector2 pos = npc.position;
                                pos.X = push + player.velocity.X;
                                if (!dashBounce)
                                {
                                    if (Collision.SolidCollision(pos, npc.width, npc.height))
                                    {
                                        player.velocity.X = -dir * 9;
                                        player.velocity.Y = -4f;
                                        player.ApplyDamageToNPC(npc, dashDamage, 0, dir, crit);
                                        dashBounce = true; 
                                    }
                                    else
                                    {
                                        npc.position.X = push;
                                        npc.velocity.X = player.velocity.X;
                                    }
                                }
                            }
                            else if (!dashBounce)
                            {
                                player.velocity.X = -dir * 9;
                                player.velocity.Y = -4f;
                                dashBounce = true;
                            }
                            if (player.immuneTime < 4)
                            {
                                player.immune = true;
                                player.immuneNoBlink = true;
                                player.immuneTime = 4;
                            }
                        }
                    }
                }
            }
            if (player.dashDelay < 0)
            {
                float num7 = 12f;
                float num8 = 0.992f;
                float num9 = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                float num10 = 0.96f;
                int delay = 20;
                if (dashType == 1)
                {
                    for (int l = 0; l < 0; l++)
                    {
                        int num13;
                        if (player.velocity.Y == 0f)
                        {
                            num13 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, 31, 0f, 0f, 100, default(Color), 1.4f);
                        }
                        else
                        {
                            num13 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)(player.height / 2) - 8f), player.width, 16, 31, 0f, 0f, 100, default(Color), 1.4f);
                        }
                        Main.dust[num13].velocity *= 0.1f;
                        Main.dust[num13].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                        Main.dust[num13].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    }
                    delay = 25;
                }
                if (dashType > 0)
                {
                    player.vortexStealthActive = false;
                    if (player.velocity.X > num7 || player.velocity.X < -num7)
                    {
                        player.velocity.X = player.velocity.X * num8;
                        return;
                    }
                    if (player.velocity.X > num9 || player.velocity.X < -num9)
                    {
                        player.velocity.X = player.velocity.X * num10;
                        return;
                    }
                    player.dashDelay = delay;
                    if (player.velocity.X < 0f)
                    {
                        player.velocity.X = -num9;
                        return;
                    }
                    if (player.velocity.X > 0f)
                    {
                        player.velocity.X = num9;
                        return;
                    }
                }
            }
            else if (dashType > 0 && !player.mount.Active)
            {
                if (dashType == 1)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        dashHit[i] = false;
                    }
                    dashBounce = false;
                    float speed = 15.5f;
                    int dir = 0;
                    bool dashing = false;
                    if (player.dashTime > 0)
                    {
                        player.dashTime--;
                    }
                    if (player.dashTime < 0)
                    {
                        player.dashTime++;
                    }
                    if (player.controlRight && player.releaseRight)
                    {
                        if (player.dashTime > 0)
                        {
                            dir = 1;
                            dashing = true;
                            player.dashTime = 0;
                        }
                        else
                        {
                            player.dashTime = 15;
                        }
                    }
                    else if (player.controlLeft && player.releaseLeft)
                    {
                        if (player.dashTime < 0)
                        {
                            dir = -1;
                            dashing = true;
                            player.dashTime = 0;
                        }
                        else
                        {
                            player.dashTime = -15;
                        }
                    }
                    if (dashing)
                    {
                        player.velocity.X = speed * dir;
                        Point point3 = (player.Center + new Vector2((float)(dir * player.width / 2 + 2), player.gravDir * -(float)player.height / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point4 = (player.Center + new Vector2((float)(dir * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point3.X, point3.Y) || WorldGen.SolidOrSlopedTile(point4.X, point4.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        player.dashDelay = -1;
                        for (int num21 = 0; num21 < 0; num21++)
                        {
                            int num22 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f, 0f, 100, default(Color), 2f);
                            Dust dust3 = Main.dust[num22];
                            dust3.position.X = dust3.position.X + (float)Main.rand.Next(-5, 6);
                            Dust dust4 = Main.dust[num22];
                            dust4.position.Y = dust4.position.Y + (float)Main.rand.Next(-5, 6);
                            Main.dust[num22].velocity *= 0.2f;
                            Main.dust[num22].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                            Main.dust[num22].shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
                        }
                        return;
                    }
                }
            }

        }
    }
}