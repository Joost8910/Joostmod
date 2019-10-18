using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.IO;

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
        public override bool InstancePerEntity
        {
            get
            {
                return true;
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
        public override bool NewPreReforge(Item item)
        {
            meleeDamage = 0;
            thrownDamage = 0;
            rangedDamage = 0;
            magicDamage = 0;
            summonDamage = 0;
            maxHealth = 0;
            lifeRegen = 0;
            fishingPower = 0;
            return base.NewPreReforge(item);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.LivingLoom)
            {
                TooltipLine line = new TooltipLine(mod, "CraftedAt", "Crafted at a living tree's leaves");
                tooltips.Add(line);
            }
            if (meleeDamage > 0)
            {
                TooltipLine line = new TooltipLine(mod, "meleePrefix", "+" + meleeDamage + "% melee damage");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (thrownDamage > 0)
            {
                TooltipLine line = new TooltipLine(mod, "thrownPrefix", "+" + thrownDamage + "% thrown damage");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (rangedDamage > 0)
            {
                TooltipLine line = new TooltipLine(mod, "rangedPrefix", "+" + rangedDamage + "% ranged damage");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (magicDamage > 0)
            {
                TooltipLine line = new TooltipLine(mod, "magicPrefix", "+" + magicDamage + "% magic damage");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (summonDamage > 0)
            {
                TooltipLine line = new TooltipLine(mod, "summonPrefix", "+" + summonDamage + "% summon damage");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (maxHealth > 0)
            {
                TooltipLine line = new TooltipLine(mod, "maxLifePrefix", "+" + maxHealth + " max life");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (lifeRegen > 0)
            {
                TooltipLine line = new TooltipLine(mod, "lifeRegenPrefix", "+" + lifeRegen + " life regen");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (fishingPower > 0)
            {
                TooltipLine line = new TooltipLine(mod, "fishPrefix", "+" + fishingPower + " fishing power");
                line.isModifier = true;
                tooltips.Add(line);
            }
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix > 0)
            {
                player.meleeDamage += meleeDamage * 0.01f;
                player.thrownDamage += thrownDamage * 0.01f;
                player.rangedDamage += rangedDamage * 0.01f;
                player.magicDamage += magicDamage * 0.01f;
                player.minionDamage += summonDamage * 0.01f;
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
                    player.QuickSpawnItem(mod.ItemType("SkellyStaff"), 1);
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
                        player.QuickSpawnItem(mod.ItemType("RoseWeave"), 1);
                    }
                }
                if (arg == ItemID.FishronBossBag)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("BubbleBottle"), 999);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("DukeFishRod"), 1);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("MegaBubbleShield"), 1);
                    }
                }
            }
        }
        public static float LegendaryDamage()
        {
            float damageMult = 1f + //1
            (NPC.downedSlimeKing ? 0.25f : 0f) + //1.25
            (NPC.downedBoss1 ? 0.25f : 0f) + //1.5
            (NPC.downedBoss2 ? 0.25f : 0f) + //1.75
            (NPC.downedQueenBee ? 0.25f : 0f) + //2
            (NPC.downedBoss3 ? 0.25f : 0f) + // 2.25
            (JoostWorld.downedCactusWorm ? 0.25f : 0f) + //2.5
            (Main.hardMode ? 0.5f : 0f) + //3
            (NPC.downedMechBoss1 ? 1f : 0f) + //4
            (NPC.downedMechBoss2 ? 1f : 0f) + //5
            (NPC.downedMechBoss3 ? 1f : 0f) + //6
            (NPC.downedPlantBoss ? 1f : 0f) + //7
            (NPC.downedGolemBoss ? 1f : 0f) + //8
            (NPC.downedFishron ? 1f : 0f) + //9
            (NPC.downedAncientCultist ? 1f : 0f) + //10
            (NPC.downedTowerNebula ? 1f : 0f) + //11
            (NPC.downedTowerSolar ? 1f : 0f) + //12
            (NPC.downedTowerVortex ? 1f : 0f) + //13
            (NPC.downedTowerStardust ? 1f : 0f) + //14
            (NPC.downedMoonlord ? 6f : 0f) + //20
            (JoostWorld.downedJumboCactuar ? 15f : 0f) + //35
            (JoostWorld.downedSAX ? 15f : 0f) + //50
            (JoostWorld.downedGilgamesh ? 15f : 0f); //65
            return damageMult;
        }
    }
    public class Ammohalf : GlobalItem
    {
        public override bool ConsumeAmmo(Item item, Player player)
        {
            if (player.GetModPlayer<JoostModPlayer>().ammo50 && Main.rand.NextFloat() < .5f)
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
            if (player.ownedProjectileCounts[mod.ProjectileType("IceBeamCannon")] > 0)
            {
                grabRange = 600;
            }
        }
    }
    public class Ammonone : GlobalItem
    {
        public override bool ConsumeAmmo(Item item, Player player)
        {
            if (player.GetModPlayer<JoostModPlayer>().ammoNone)
            {
                return false;
            }
            return true;
        }
    }
    public class Thrownone : GlobalItem
    {
        public override bool ConsumeItem(Item item, Player player)
        {
            if (player.GetModPlayer<JoostModPlayer>().throwNone && item.thrown)
            {
                return false;
            }
            return true;
        }
    }
    public class MeleeStrike : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.GetModPlayer<JoostModPlayer>().crimsonPommel)
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
            if (player.GetModPlayer<JoostModPlayer>().corruptPommel)
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
        }
        public override void OnHitPvp(Item item, Player player, Player target, int damage, bool crit)
        {
            if (player.GetModPlayer<JoostModPlayer>().crimsonPommel)
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
            if (player.GetModPlayer<JoostModPlayer>().corruptPommel)
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
        public bool ammo50 = false;
        public bool ammoNone = false;
        public bool throwNone = false;
        public bool crimsonPommel = false;
        public bool corruptPommel = false;
        public override void ResetEffects()
        {
            ammo50 = false;
            ammoNone = false;
            throwNone = false;
            crimsonPommel = false;
            corruptPommel = false;
        }
    }
}