using JoostMod.Items;
using JoostMod.Items.Accessories;
using JoostMod.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Grappling;
using JoostMod.Projectiles.Magic;
using JoostMod.Projectiles.Accessory;
using JoostMod.Projectiles.Ranged;
using JoostMod.Projectiles.Thrown;
using JoostMod.Projectiles.Melee;
using JoostMod.Projectiles.Summon;

namespace JoostMod
{
    public class JoostPlayer : ModPlayer
    {
        private const int saveVersion = 0;

        public bool bonesHurt = false;
        public float boneHurtDamage = 1;
        public bool corruptSoul = false;
        public bool lifeRend = false;
        public bool infectedRed = false;
        public bool infectedGreen = false;
        public bool infectedBlue = false;
        public bool infectedYellow = false;
        public bool sap = false;

        public bool stormy = false;

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
        public bool stormWyvernMinion = false;

        public bool planeMount = false;
        public Vector2 sandSharkVel = Vector2.Zero;
        
        public Item XShieldItem = null;
        private int XShieldTimer = 60;
        public Item SpectreOrbsItem = null;
        private int SpectreOrbTimer = 40;
        public Item SkullSigilItem = null;
        private int SkullSigilTimer = 180;
        public Item bubbleShieldItem = null;
        private int bubbleShieldTimer = 90;
        public Item megaBubbleShieldItem = null;
        private int megaBubbleShieldTimer = 30;
        public bool hoverBoots = false;
        public bool spaceJump = false;
        public int hoverBootsTimer = 900;
        private int hoverBootsStart = 0;
        private float hoverWing = 0;
        private int hoverRocket = 0;
        private int hoverMount = 0;
        private bool hoverCanJump = false;
        private bool hoverDoJump = false;
        public bool hovering = false;
        public bool HavocPendant = false;
        public bool HarmonyPendant = false;
        public Item swordSaplingItem = null;
        private int swordSaplingTimer = 10;
        public Item hatchetSaplingItem = null;
        private int hatchetSaplingTimer = 18;
        public Item staffSaplingItem = null;
        private int staffSaplingTimer = 37;
        public Item bowSaplingItem = null;
        private int bowSaplingTimer = 27;
        public Item fishingSaplingItem = null;
        public Item shieldSaplingItem = null;
        public int spelunky = 0;
        public bool spelunkGlow = false;
        private int spelunkerTimer = 0;
        public bool glowContacts = false;
        public int glowEyeType = 0;
        public bool glowEyeNoGlow = false;
        public int glowEyeDye = 0;

        public Item cactoidCommendationItem = null;
        private int cactoidCommendationTimer = 7200;
        public Item sporganItem = null;
        public bool rocWings = false;
        private int rot = 0;
        public Item cactusBootsItem = null;
        private float cactusBootsTimer = 40;
        public Item fleshShieldItem = null;
        private int fleshShieldTimer = 0;
        public Item havelShieldItem = null;
        public bool havelBlocking = false;
        public bool blazeAnklet = false;
        public bool airMedallion = false;
        public Item waterBubbleItem = null;
        public bool hideBubble = false;
        public bool emptyHeart = false;
        
        public bool dirtArmor = false;
        public bool slimeArmor = false;
        public bool slimeActive = false;
        public int slimedNPC = -1;
        private Vector2 slimedNPCOffset = Vector2.Zero;
        public bool slimeClimbWall = false;
        public bool slimeClimbCeiling = false;
        private int slimeTimer = -1;
        public bool pinkSlimeArmor = false;
        public bool pinkSlimeActive = false;
        private int pinkSlimeTimer = -1;
        public bool havelArmor = false;
        public bool havelArmorActive = false;
        private int havelArmorTimer = -1;
        public bool fireArmor = false;
        public bool fireArmorIsActive = false;
        public bool airArmor = false;
        public bool airArmorIsActive = false;
        private int airArmorDodgeTimer = -1;
        public bool zoraArmor = false;
        private int sandStormTimer = 6;
        public bool sandStorm = false;
        public bool GMagic = false;
        private int GMagicTimer = 63;
        public bool gMelee = false;
        public bool gRanged = false;
        public bool gRangedIsActive = false;
        public bool gThrown = false;
        private int gThrownTimer = 1200;

        public bool westStone = false;
        public bool eastStone = false;
        public bool highStone = false;
        public bool deepStone = false;

        public bool legendOwn = false;
        public bool SaitamaOwn = false;
        public bool isLegend = false;
        public bool isUncleCarius = false;
        public bool isSaitama = false;
        public int LegendCool = 0;
        public int spinTimer = 0;

        public bool noHooks = false;
        public float runAccelerationMult = 1;
        public float accRunSpeedMult = 1;
        public int dashType = 0;
        public int dashDamage = 0;
        private bool[] dashHit = new bool[200];
        private bool dashBounce = false;
        public int enemyIgnoreDefenseDamage = 0;
        private Vector2 oldVelocity = Vector2.Zero;

        public bool drawOverArmor = false;

        public override void ResetEffects()
        {
            bonesHurt = false;
            corruptSoul = false;
            lifeRend = false;
            infectedRed = false;
            infectedGreen = false;
            infectedBlue = false;
            infectedYellow = false;
            sap = false;
            if (!Player.HasBuff(ModContent.BuffType<BoneHurt>()))
            {
                boneHurtDamage = 1;
            }

            stormy = false;

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
            stormWyvernMinion = false;

            hoverBoots = false;
            hovering = false;
            spaceJump = false;
            HavocPendant = false;
            HarmonyPendant = false;
            sandStorm = false;
            spelunky = 0;
            spelunkGlow = false;
            glowContacts = false;
            glowEyeType = 0;
            glowEyeNoGlow = false;
            glowEyeDye = 0;

            rocWings = false;
            hideBubble = false;
            emptyHeart = false;
            cactusBootsItem = null;
            havelBlocking = false;
            blazeAnklet = false;
            airMedallion = false;

            planeMount = false;
            if (Player.mount.Type != Mod.Find<ModMount>("SandShark").Type)
            {
                sandSharkVel = Vector2.Zero;
            }

            dirtArmor = false;
            slimeArmor = false;
            slimeActive = false;
            pinkSlimeArmor = false;
            pinkSlimeActive = false;
            havelArmor = false;
            fireArmor = false;
            airArmor = false;
            airArmorIsActive = false;
            zoraArmor = false;
            GMagic = false;
            gMelee = false;
            gThrown = false;
            gRanged = false;

            waterBubbleItem = null;
            XShieldItem = null;
            cactoidCommendationItem = null;
            SpectreOrbsItem = null;
            SkullSigilItem = null;
            swordSaplingItem = null;
            hatchetSaplingItem = null;
            staffSaplingItem = null;
            bowSaplingItem = null;
            fishingSaplingItem = null;
            shieldSaplingItem = null;
            bubbleShieldItem = null;
            megaBubbleShieldItem = null;
            fleshShieldItem = null;
            havelShieldItem = null;
            sporganItem = null;

            noHooks = Player.ownedProjectileCounts[ModContent.ProjectileType<SwingyHook>()] + Player.ownedProjectileCounts[ModContent.ProjectileType<MobHook>()] + Player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedSwingyHook>()] + Player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedMobHook>()] + Player.ownedProjectileCounts[ModContent.ProjectileType<CactusHook>()] <= 0 && Player.grappling[0] == -1;
            accRunSpeedMult = 1;
            runAccelerationMult = 1;
            dashType = 0;
            dashDamage = 0;
            enemyIgnoreDefenseDamage = 0;

            drawOverArmor = havelArmorActive || pinkSlimeActive || slimeActive;
        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)/* tModPorter Suggestion: Return an Item array to add to the players starting items. Use ModifyStartingInventory for modifying them if needed */
        {
            return new[] { new Item(Mod.Find<ModItem>("StormyCollar").Type) };
        }
        public override void UpdateBadLifeRegen()
        {
            if (bonesHurt)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                boneHurtDamage += 0.0167f;
                Player.lifeRegen -= (int)boneHurtDamage * 2;
                if (Player.boneArmor)
                {
                    Player.lifeRegen -= (int)boneHurtDamage * 2;
                }
            }
            if (corruptSoul)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 10;
            }
            if (infectedRed && XShieldItem == null)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 8;
            }
            if (infectedGreen && XShieldItem == null)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 8;
            }
            if (infectedBlue && XShieldItem == null)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 8;
            }
            if (infectedYellow && XShieldItem == null)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 8;
            }
            if (fireArmorIsActive)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 12;
            }
            if (sap)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 20;
            }
        }
        public override void UpdateLifeRegen()
        {
            if (airArmorIsActive)
            {
                Player.lifeRegenTime += 10;
                Player.lifeRegen += 3;
            }
        }
        public override void FrameEffects()
        {
            if (Player.HeldItem.type == Mod.Find<ModItem>("CactusGlove").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "CactusGlove", EquipType.HandsOn);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("ChlorophyteGlove").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "ChlorophyteGlove", EquipType.HandsOn);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("GnunderGlove").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "GnunderGlove", EquipType.HandsOn);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("GooGlove").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "GooGlove", EquipType.HandsOn);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("HellstoneShuriken").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "HellstoneShuriken", EquipType.HandsOn);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("PumpkinGlove").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "PumpkinGlove", EquipType.HandsOn);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("StoneFist").Type)
            {
                Player.handoff = (sbyte)EquipLoader.GetEquipSlot(Mod, "StoneFist", EquipType.HandsOff);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("OnePunch").Type)
            {
                Player.handoff = (sbyte)EquipLoader.GetEquipSlot(Mod, "OnePunch", EquipType.HandsOff);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("SandGlove").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "SandGlove", EquipType.HandsOn);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("MutantCannon").Type)
            {
                Player.handoff = (sbyte)EquipLoader.GetEquipSlot(Mod, "MutantCannon", EquipType.HandsOff);
            }
            if (Player.HeldItem.type == Mod.Find<ModItem>("GrabGlove").Type)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "GrabGlove", EquipType.HandsOn);
                Player.handoff = (sbyte)EquipLoader.GetEquipSlot(Mod, "GrabGlove", EquipType.HandsOff);
            }

            if (havelBlocking)
            {
                Player.shield = (sbyte)EquipLoader.GetEquipSlot(Mod, "HavelsGreatshield", EquipType.Shield);
            }


            if (glowContacts)
            {
                for (int e = 3; e < 8 + Player.extraAccessorySlots; e++)
                {
                    if (Player.armor[e].type == ModContent.ItemType<GlowingContacts>())
                    {
                        switch (e)
                        {
                            case 4:
                                glowEyeType = 1;
                                break;
                            case 5:
                                glowEyeType = 2;
                                break;
                            case 6:
                                glowEyeType = 3;
                                break;
                            case 7:
                                glowEyeType = 4;
                                break;
                            case 8:
                            case 9:
                                glowEyeType = 5;
                                break;
                            default:
                                glowEyeType = 0;
                                break;
                        }
                        glowEyeDye = Player.dye[e].dye;
                        break;
                    }
                }
                for (int v = 13; v < 18 + Player.extraAccessorySlots; v++)
                {
                    if (Player.armor[v].type == ModContent.ItemType<GlowingContacts>())
                    {
                        switch (v)
                        {
                            case 14:
                                glowEyeType = 1;
                                break;
                            case 15:
                                glowEyeType = 2;
                                break;
                            case 16:
                                glowEyeType = 3;
                                break;
                            case 17:
                                glowEyeType = 4;
                                break;
                            case 18:
                            case 19:
                                glowEyeType = 5;
                                break;
                            default:
                                glowEyeType = 0;
                                break;
                        }
                        if (Player.hideVisibleAccessory[v - 10])
                        {
                            glowEyeNoGlow = true;
                        }
                        glowEyeDye = Player.dye[v - 10].dye;
                        break;
                    }
                }
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (slimeArmor && Player.itemAnimation == 0)
            {
                if (slimeClimbWall)
                {
                    if (Player.velocity.Y != 0)
                    {
                        if (Player.position.Y % 48 > 32)
                        {
                            Player.bodyFrame.Y = Player.bodyFrame.Height * 2;
                        }
                        else if (Player.position.Y % 48 > 16)
                        {
                            Player.bodyFrame.Y = Player.bodyFrame.Height * 3;
                        }
                        else
                        {
                            Player.bodyFrame.Y = Player.bodyFrame.Height * 4;
                        }
                    }
                    else
                    {
                        Player.bodyFrame.Y = Player.bodyFrame.Height * 3;
                    }
                }
                if (slimeClimbCeiling)
                {
                    if (Player.velocity.X != 0)
                    {
                        if (Player.position.X % 32 > 16)
                        {
                            Player.bodyFrame.Y = Player.bodyFrame.Height * 5;
                        }
                        else
                        {
                            Player.bodyFrame.Y = Player.bodyFrame.Height * 2;
                        }
                    }
                    else
                    {
                        Player.bodyFrame.Y = Player.bodyFrame.Height * 5;
                    }
                }
            }
        }
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            if (Player.shield == (sbyte)EquipLoader.GetEquipSlot(Mod, "HavelsGreatshield", EquipType.Shield) && !havelBlocking)
            {
                PlayerDrawLayers.Shield.Hide();
            }
        }
        /*
        public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (glowEyeType == 0)
                {
                    if (layers[i] == PlayerLayer.FaceAcc)
                    {
                        layers.Insert(i + 1, glowEye);
                        glowEye.visible = true;
                    }
                }
                else
                {
                    if (layers[i] == PlayerLayer.Head)
                    {
                        layers.Insert(i + 1, glowEye);
                        glowEye.visible = true;
                 
                    }
                }
                if (Player.face == (sbyte)EquipLoader.GetEquipSlot(Mod, "GlowingContacts", EquipType.Face))
                {
                    if (layers[i] == PlayerLayer.FaceAcc)
                    {
                        layers[i].visible = false;
                    }
                }
            }
        }
        */
        /* Old ModifyDrawLayers
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (Player.shield == (sbyte)EquipLoader.GetEquipSlot(Mod, "HavelsGreatshield", EquipType.Shield) && !havelBlocking)
                {
                    if (layers[i] == PlayerLayer.ShieldAcc)
                    {
                        layers[i].visible = false;
                    }
                    if (layers[i] == PlayerLayer.Wings)
                    {
                        layers.Insert(i + 1, shieldDownLayer);
                        shieldDownLayer.visible = true;
                    }
                }
                if (glowContacts)
                {
                    if (glowEyeType == 0)
                    {
                        if (layers[i] == PlayerLayer.Face)
                        {
                            layers.Insert(i + 1, glowEye);
                            glowEye.visible = true;
                        }
                    }
                    else
                    {
                        if (layers[i] == PlayerLayer.Head)
                        {
                            layers.Insert(i + 1, glowEye);
                            glowEye.visible = true;
                        }
                    }
                    if (Player.face == (sbyte)EquipLoader.GetEquipSlot(Mod, "GlowingContacts", EquipType.Face))
                    {
                        if (layers[i] == PlayerLayer.FaceAcc)
                        {
                            layers[i].visible = false;
                        }
                    }
                }
                if (havelArmorActive || pinkSlimeActive || slimeActive)
                {
                    if (layers[i] == PlayerLayer.Legs)
                    {
                        layers.Insert(i + 1, overLegs);
                        overLegs.visible = true;
                    }
                    if (layers[i] == PlayerLayer.Body)
                    {
                        layers.Insert(i + 1, overBody);
                        overBody.visible = true;
                    }
                    if (layers[i] == PlayerLayer.Head)
                    {
                        layers.Insert(i + 1, overHead);
                        overHead.visible = true;
                    }
                    if (layers[i] == PlayerLayer.Arms)
                    {
                        layers.Insert(i + 1, overArms);
                        overArms.visible = true;
                    }
                }
                if (PlayerLayer.HeldItem.visible && Player.HeldItem.type != ItemID.None && !Player.HeldItem.noUseGraphic && (Player.itemAnimation > 0 || Player.HeldItem.holdStyle == 1) && Player.HeldItem.GetGlobalItem<JoostGlobalItem>().glowmaskTex != null)
                {
                    if (layers[i] == PlayerLayer.HeldItem)
                    {
                        layers.Insert(i + 1, itemGlowmask);
                        itemGlowmask.visible = true;
                    }
                }
            }
        }
        */
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (JoostMod.ArmorAbilityHotKey.JustPressed)
            {
                if (gThrown)
                {
                    if (gThrownTimer <= 0)
                    {
                        Player.AddBuff(ModContent.BuffType<gThrownDodge>(), 120);
                        gThrownTimer = 1200;
                    }
                }
                if (gRanged)
                {
                    Player.AddBuff(ModContent.BuffType<gRangedBuff>(), 2);
                    if (!gRangedIsActive)
                    {
                        gRangedIsActive = true;
                    }
                    else
                    {
                        gRangedIsActive = false;
                    }
                }
                if (GMagic && !Player.HasBuff(BuffID.ManaSickness) && Player.ownedProjectileCounts[ModContent.ProjectileType<BitterEndFriendly>()] + Player.ownedProjectileCounts[ModContent.ProjectileType<BitterEndFriendly2>()] <= 0)
                {
                    if (Player.statMana >= Player.statManaMax2)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis("SetBonus_GenjiMagic"), Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<BitterEndFriendly>(), (int)Player.GetDamage(DamageClass.Magic).ApplyTo(2000), 20f, Player.whoAmI);
                        Player.manaRegenDelay = 180 * (2+Player.manaRegenDelayBonus);
                        Player.statMana *= 0;
                    }
                }
                if (havelArmor && havelArmorTimer < 0)
                {
                    if (!havelArmorActive)
                    {
                        havelArmorTimer = 25;
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_sky_dragons_fury_swing_1").WithPitchOffset(-0.3f), Player.Center); //230
                        for (int d = 0; d < 40; d++)
                        {
                            int dust = Dust.NewDust(Player.position - new Vector2(10, 10), Player.width+20, Player.height+20, 1, Player.velocity.X * 0.8f, Player.velocity.Y * 0.8f, 0, default(Color), 1.5f);
                            Vector2 vel = Player.MountedCenter - Main.dust[dust].position;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel + Player.velocity * 0.8f;
                            Main.dust[dust].noGravity = true;
                        }
                    }
                }
                if (fireArmor)
                {
                    Player.AddBuff(ModContent.BuffType<fireArmorBuff>(), 2);
                    if (!fireArmorIsActive)
                    {
                        SoundEngine.PlaySound(new ("Terraria/Sounds/Custom/dd2_betsy_fireball_shot_1"), Player.Center); //198
                        for (int i = 0; i < 30; i++)
                            Dust.NewDustDirect(Player.position, Player.width, Player.height, 6, Player.velocity.X, Player.velocity.Y, 0, default, Main.rand.NextFloat() + 3).noGravity = true;
                        fireArmorIsActive = true;
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item13, Player.Center);
                        for (int i = 0; i < 30; i++)
                            Dust.NewDust(Player.position, Player.width, Player.height, 31, 0, -1.5f, 0, new Color(0.2f, 0.1f, 0.15f), Main.rand.NextFloat() + 1);
                        fireArmorIsActive = false;
                    }
                }
                if (airArmor)
                {
                    float count = 0;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile proj = Main.projectile[i];
                        if (proj.owner == Player.whoAmI && proj.active && proj.minionSlots > 0)
                        {
                            proj.Kill();
                            count += proj.minionSlots;
                            for (int d = 0; d < 10; d++)
                                Dust.NewDust(proj.position, proj.width, proj.height, DustID.Smoke, 0, 0, 0, Color.White, Main.rand.NextFloat() + 1);
                        }
                    }
                    int duration = (int)(count * 4 * 60);

                    if (duration > 0)
                    {
                        if (airArmorDodgeTimer <= 0 && Player.immuneTime < 60)
                        {
                            airArmorDodgeTimer = 165;
                            Player.immune = true;
                            Player.immuneTime = 60;
                        }
                        Player.AddBuff(ModContent.BuffType<AirArmorBuff>(), duration);
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_book_staff_cast_2"), Player.Center); //203
                        for (int i = 0; i < 30; i++)
                            Dust.NewDust(Player.position, Player.width, Player.height, 31, -4 * Player.direction, 0f, 0, Color.White, Main.rand.NextFloat() + 1);
                    }
                }
                if (zoraArmor && Player.ownedProjectileCounts[ModContent.ProjectileType<ZoraSpin>()] < 1)
                {
                    int damage = (int)Player.GetDamage(DamageClass.Magic).ApplyTo(40f);
                    int wet = Player.wet ? 1 : 0;
                    float speed = 6;
                    Vector2 vel = new Vector2(0, -Player.gravity);
                    if (Player.controlRight)
                    {
                        vel.X += speed;
                    }
                    if (Player.controlLeft)
                    {
                        vel.X -= speed;
                    }
                    if (Player.controlUp || Player.controlJump)
                    {
                        vel.Y -= Player.gravDir * speed;
                    }
                    if (Player.controlDown)
                    {
                        vel.Y += Player.gravDir * speed;
                    }
                    if (vel.X != 0 && vel.Y != 0)
                    {
                        vel *= 0.707f;
                    }
                    if (Player.wet)
                    {
                        vel *= 2;
                        if (Player.immuneTime < 24)
                        {
                            Player.immune = true;
                            Player.immuneTime = 24;
                        }
                    }
                    Projectile.NewProjectile(Player.GetSource_FromThis("SetBonus_Zora"), Player.Center, vel, ModContent.ProjectileType<ZoraSpin>(), damage, 5f, Player.whoAmI, wet);
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_book_staff_cast_0"), Player.Center); //201
                }
                if (pinkSlimeArmor && pinkSlimeTimer < 0)
                {
                    if (!pinkSlimeActive)
                    {
                        pinkSlimeTimer = 15;
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_sky_dragons_fury_swing_1").WithPitchOffset(-0.1f), Player.Center); //230
                        for (int d = 0; d < 40; d++)
                        {
                            int dust = Dust.NewDust(Player.position - new Vector2(10, 10), Player.width + 20, Player.height + 20, DustID.PinkSlime, Player.velocity.X, Player.velocity.Y, 0, default(Color), 1f);
                            Vector2 vel = Player.MountedCenter - Main.dust[dust].position;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel + Player.velocity;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cBody, Player);
                        }
                    }
                    else
                    {
                        pinkSlimeTimer = 15;
                        SoundEngine.PlaySound(SoundID.NPCHit1.WithPitchOffset(-0.3f), Player.Center);
                    }
                }
                if (slimeArmor && slimeTimer < 0)
                {
                    if (!slimeActive)
                    {
                        slimeTimer = 15;
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_sky_dragons_fury_swing_1").WithPitchOffset(-0.1f), Player.Center); //230
                        for (int d = 0; d < 40; d++)
                        {
                            int dust = Dust.NewDust(Player.position - new Vector2(10, 10), Player.width + 20, Player.height + 20, 4, Player.velocity.X, Player.velocity.Y, 100, Color.Blue, 1f);
                            Vector2 vel = Player.MountedCenter - Main.dust[dust].position;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel + Player.velocity;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cBody, Player);
                        }
                    }
                    else
                    {
                        slimeTimer = 15;
                        SoundEngine.PlaySound(SoundID.NPCHit1.WithPitchOffset(-0.3f), Player.Center);
                    }
                }
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (item.CountsAsClass(DamageClass.Melee) && gMelee && Player.ownedProjectileCounts[ModContent.ProjectileType<Masamune>()] < 1)
            {
                Projectile.NewProjectile(Player.GetSource_OnHit(target), Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), (int)Player.GetDamage(DamageClass.Melee).ApplyTo(500), 5f, Player.whoAmI);
                //Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), 0, 0, Player.whoAmI);
            }
        }
        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            if (item.CountsAsClass(DamageClass.Melee) && gMelee && Player.ownedProjectileCounts[ModContent.ProjectileType<Masamune>()] < 1)
            {
                Projectile.NewProjectile(Player.GetSource_OnHit(target), Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), (int)Player.GetDamage(DamageClass.Melee).ApplyTo(500), 5f, Player.whoAmI);
                //Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), 0, 0, Player.whoAmI);
            }
        }
        public override void OnHitNPCWithProj(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            var source = projectile.GetSource_OnHit(target);
            if (projectile.CountsAsClass(DamageClass.Melee) && player.heldProj == projectile.whoAmI)
            {
                if (player.GetModPlayer<JoostModPlayer>().crimsonPommel)
                {
                    if (target.life <= 0 && target.type != NPCID.TargetDummy && !target.HasBuff(ModContent.BuffType<LifeDrink>()))
                    {
                        float lifeStoled = target.lifeMax * 0.04f;
                        if ((int)lifeStoled > 0 && !player.moonLeech)
                        {
                            Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
                        }
                    }
                    target.AddBuff(ModContent.BuffType<LifeDrink>(), 1200, false);
                }
                if (player.GetModPlayer<JoostModPlayer>().corruptPommel)
                {
                    if (target.life <= 0 && target.type != NPCID.TargetDummy && !target.HasBuff(ModContent.BuffType<CorruptSoul>()))
                    {
                        float damag = target.lifeMax * 0.25f;
                        if ((int)damag > 0)
                        {
                            Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0, -5, ModContent.ProjectileType<CorruptedSoul>(), (int)damag, 0, player.whoAmI);
                        }
                    }
                    target.AddBuff(ModContent.BuffType<CorruptSoul>(), 1200, false);
                }
                if (gMelee && player.ownedProjectileCounts[ModContent.ProjectileType<Masamune>()] < 1)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), (int)Player.GetDamage(DamageClass.Melee).ApplyTo(500), 5f, Player.whoAmI);
                    //Projectile.NewProjectile(source, Player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), 0, 0, player.whoAmI);
                }
            }
            if (projectile.CountsAsClass(DamageClass.Ranged))
            {
                if (fireArmorIsActive)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }
            }
            if (projectile.minion)
            {
                if (airMedallion && projectile.type != ModContent.ProjectileType<AirBlast>() && Main.rand.NextBool(10))
                {
                    SoundEngine.PlaySound(SoundID.Item18, target.Center);
                    Projectile.NewProjectile(source, target.Center.X, target.position.Y + target.height, 0, -10f, ModContent.ProjectileType<AirBlast>(), (int)player.GetDamage(DamageClass.Summon).ApplyTo(25), 0, player.whoAmI);
                }
            }
            if (sandStorm && projectile.CountsAsClass(DamageClass.Throwing))
            {
                int erg = 80;
                if (Main.rand.NextBool(2))
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
                Vector2 velocity = new(target.position.X - vector2.X, target.position.Y - vector2.Y);
                float dir = (float)Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
                dir = 10 / num80;
                velocity.X *= dir * 150 * player.ThrownVelocity;
                velocity.Y *= dir * 150 * player.ThrownVelocity;
                if (sandStormTimer <= 0)
                {
                    Projectile.NewProjectile(source, vector2.X, vector2.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Sand>(), (int)player.GetDamage(DamageClass.Throwing).ApplyTo(20), 1, projectile.owner);
                    sandStormTimer = 5;
                }

            }

        }
        public override void OnHitPvpWithProj(Projectile projectile, Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            var source = projectile.GetSource_OnHit(target);
            if (projectile.CountsAsClass(DamageClass.Melee) && player.heldProj == projectile.whoAmI)
            {
                if (player.GetModPlayer<JoostModPlayer>().crimsonPommel)
                {
                    if (target.statLife <= 0 && !target.HasBuff(ModContent.BuffType<LifeDrink>()))
                    {
                        float lifeStoled = target.statLifeMax2 * 0.04f;
                        if ((int)lifeStoled > 0 && !player.moonLeech)
                        {
                            Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
                        }
                    }
                    target.AddBuff(ModContent.BuffType<LifeDrink>(), 1200, false);
                }
                if (player.GetModPlayer<JoostModPlayer>().corruptPommel)
                {
                    if (target.statLife <= 0 && !target.HasBuff(ModContent.BuffType<CorruptSoul>()))
                    {
                        float damag = target.statLifeMax2 * 0.25f;
                        if ((int)damag > 0)
                        {
                            Projectile.NewProjectile(source, target.Center.X, target.Center.Y, 0, -5, ModContent.ProjectileType<CorruptedSoul>(), (int)damag, 0, player.whoAmI);
                        }
                    }
                    target.AddBuff(ModContent.BuffType<CorruptSoul>(), 1200, false);
                }
                if (gMelee && player.ownedProjectileCounts[ModContent.ProjectileType<Masamune>()] < 1)
                {
                    Projectile.NewProjectile(source, Player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), (int)player.GetDamage(DamageClass.Melee).ApplyTo(500), 5f, player.whoAmI);
                    //Projectile.NewProjectile(source, Player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Masamune>(), 0, 0, player.whoAmI);
                }
            }
            if (projectile.CountsAsClass(DamageClass.Ranged))
            {
                if (fireArmorIsActive)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }
            }
            if (sandStorm && projectile.CountsAsClass(DamageClass.Throwing))
            {
                int erg = 80;
                if (Main.rand.NextBool(2))
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
                Vector2 velocity = new(target.position.X - vector2.X, target.position.Y - vector2.Y);
                float dir = (float)Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
                dir = 10 / num80;
                velocity.X *= dir * 150 * player.ThrownVelocity;
                velocity.Y *= dir * 150 * player.ThrownVelocity;
                if (sandStormTimer <= 0)
                {
                    Projectile.NewProjectile(source, vector2.X, vector2.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Sand>(), (int)player.GetDamage(DamageClass.Throwing).ApplyTo(20), 1, projectile.owner);
                    sandStormTimer = 5;
                }

            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (waterBubbleItem != null && proj.CountsAsClass(DamageClass.Magic) && target.wet)
            {
                damage = (int)(damage * 1.15f);
                knockback *= 1.15f;
            }
        }
        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (waterBubbleItem != null && proj.CountsAsClass(DamageClass.Magic) && target.wet)
            {
                damage = (int)(damage * 1.15f);
            }
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (waterBubbleItem != null && item.CountsAsClass(DamageClass.Magic) && target.wet)
            {
                damage = (int)(damage * 1.15f);
                knockback *= 1.15f;
            }
        }
        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if (waterBubbleItem != null && item.CountsAsClass(DamageClass.Magic) && target.wet)
            {
                damage = (int)(damage * 1.15f);
            }
        }
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            bool inWater = !attempt.inLava && !attempt.inHoney;
            int power = attempt.fishingLevel;
            int worldLayer = attempt.heightLevel;
            if (inWater && !attempt.crate)
            {
                if (Main.rand.NextBool(Math.Max(5, 500 / power)) && worldLayer == 1 && !Main.hardMode && (Player.position.X / 16 < 350 || Player.position.X / 16 > Main.maxTilesX - 350))
                {
                    itemDrop = Mod.Find<ModItem>("GrenadeFish").Type;
                }
                if (Main.rand.NextBool(Math.Max(20, 2000 / power)) && worldLayer == 1 && !Main.hardMode && (Player.position.X / 16 < 350 || Player.position.X / 16 > Main.maxTilesX - 350))
                {
                    itemDrop = Mod.Find<ModItem>("PufferfishStaff").Type;
                }
                if (Main.rand.NextBool(Math.Max(25, 2500 / power)) && Player.ZoneCorrupt && NPC.downedBoss2)
                {
                    itemDrop = Mod.Find<ModItem>("ToxicBucket").Type;
                }
                if (Main.rand.NextBool(Math.Max(25, 2500 / power)) && Player.ZoneCrimson && NPC.downedBoss2)
                {
                    itemDrop = Mod.Find<ModItem>("BloodyBucket").Type;
                }
                if (Main.rand.NextBool(Math.Max(30 * (Main.hardMode ? 2 : 1), 3000 / power)) && worldLayer == 1 && (Player.position.X / 16 < 350 || Player.position.X / 16 > Main.maxTilesX - 350))
                {
                    itemDrop = Mod.Find<ModItem>("BubbleShield").Type;
                }
                if (Main.rand.NextBool(Math.Max(30, 6000 / power)) && (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3) && (Player.position.X / 16 < 350 || Player.position.X / 16 > Main.maxTilesX - 350))
                {
                    itemDrop = Mod.Find<ModItem>("Larpoon").Type;
                }
                if (Main.rand.NextBool(Math.Max(30, 6000 / power)) && (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3) && (Player.ZoneJungle || Player.ZoneSnow))
                {
                    itemDrop = Mod.Find<ModItem>("RoboCod").Type;
                }
                if (Main.rand.NextBool(Math.Max(60, 12000 / power)) && NPC.downedGolemBoss && Main.tile[(int)(Player.Center.X / 16), (int)(Player.Center.Y / 16)].WallType == WallID.LihzahrdBrickUnsafe)
                {
                    itemDrop = Mod.Find<ModItem>("Sunfish").Type;
                }
            }
            if (inWater && Main.rand.NextBool(Math.Max(20, 1000 / power)) && Main.hardMode && cactoidCommendationItem != null && Player.ZoneDesert)
            {
                itemDrop = Mod.Find<ModItem>("CactoidCompact").Type;
            }

            if (lunarRod && Main.rand.NextBool(4))
            {
                itemDrop = 3456 + Main.rand.Next(4);
            }
            if (JoostWorld.downedGilgamesh)
            {
                if (Main.rand.NextBool(Math.Max(100, 200000 / power)))
                {
                    if (Player.ZoneCorrupt || Player.ZoneCrimson)
                    {
                        itemDrop = Mod.Find<ModItem>("EvilStone").Type;
                    }
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && Player.ZoneDungeon)
                {
                    itemDrop = Mod.Find<ModItem>("SkullStone").Type;
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && Player.ZoneJungle)
                {
                    itemDrop = Mod.Find<ModItem>("JungleStone").Type;
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && Player.ZoneUnderworldHeight)
                {
                    itemDrop = Mod.Find<ModItem>("InfernoStone").Type;
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && inWater)
                {
                    if (Player.position.X / 16 < 350)
                    {
                        itemDrop = Mod.Find<ModItem>("SeaStoneWest").Type;
                    }
                    if (Player.position.X / 16 > Main.maxTilesX - 350)
                    {
                        itemDrop = Mod.Find<ModItem>("SeaStoneEast").Type;
                    }
                    if (worldLayer >= 3)
                    {
                        itemDrop = Mod.Find<ModItem>("SeaStoneDeep").Type;
                    }
                    if (worldLayer <= 0)
                    {
                        itemDrop = Mod.Find<ModItem>("SeaStoneHigh").Type;
                    }
                }
            }
            if (Main.rand.NextBool(Math.Max(100, 50000 / power)) && inWater)
            {
                if (Player.position.X / 16 < 350 && !westStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneWest").Type;
                }
                if (Player.position.X / 16 > Main.maxTilesX - 350 && !eastStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneEast").Type;
                }
                if (worldLayer >= 3 && !deepStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneDeep").Type;
                }
                if (worldLayer <= 0 && !highStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneHigh").Type;
                }
            }
            if (Main.rand.NextBool(Math.Max(4, 400 / power)) && inWater && isUncleCarius)
            {
                if (Player.position.X / 16 < 350 && !westStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneWest").Type;
                }
                if (Player.position.X / 16 > Main.maxTilesX - 350 && !eastStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneEast").Type;
                }
                if (worldLayer >= 3 && !deepStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneDeep").Type;
                }
                if (worldLayer <= 0 && !highStone)
                {
                    itemDrop = Mod.Find<ModItem>("SeaStoneHigh").Type;
                }
            }
        }
        public override void PreUpdate()
        {
            if (waterBubbleItem != null)
            {
                Player.wet = true;
                Player.wetCount = 10;
            }
            oldVelocity = Player.velocity;
        }
        public override void PostUpdateEquips()
        {
            if (Player.name == "Grognak" || Player.name == "Larkus" || Player.name == "Gnunderson" || Player.name == "Boook" || Player.name == "David" || Player.name.Contains("Joost"))
            {
                isLegend = true;
            }
            if (Player.name == "Uncle Carius" || Player.name.Contains("Joost"))
            {
                isUncleCarius = true;
            }
            if (Player.name == "Saitama")
            {
                isSaitama = true;
            }
            if (waterBubbleItem != null)
            {
                Player.wet = true;
                Player.wetCount = 10;
                if (!hideBubble && Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Accessory.PersonalBubble>()] < 1)
                {
                    var source = Player.GetSource_Accessory(waterBubbleItem);
                    Projectile.NewProjectile(source, Player.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Accessory.PersonalBubble>(), 0, 0, Player.whoAmI);
                }
            }
            if (gRanged)
            {
                if (gRangedIsActive)
                {
                    Player.AddBuff(ModContent.BuffType<gRangedBuff>(), 2);
                    Player.GetDamage(DamageClass.Ranged) *= 1 + (Player.statDefense * 0.005f);
                    Player.statDefense = 0;
                }
            }
            else
            {
                gRangedIsActive = false;
            }
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<ZoraSpin>()] > 0)
            {
                Player.noItems = true;
                if (Player.wet)
                    Player.maxFallSpeed += 10;
            }
            if (fireArmor)
            {
                if (fireArmorIsActive)
                {
                    Player.AddBuff(ModContent.BuffType<fireArmorBuff>(), 2);
                    Player.GetDamage(DamageClass.Ranged) *= 1.4f;
                    Player.moveSpeed *= 1.4f;
                    Player.maxRunSpeed *= 1.4f;
                    if (Player.mount._type == Mod.Find<ModMount>("FierySoles").Type)
                        accRunSpeedMult *= 1.4f;
                    else
                        Player.accRunSpeed *= 1.4f;
                    Player.onFire = true;
                    Player.ClearBuff(BuffID.Chilled);
                    Player.ClearBuff(BuffID.Frozen);
                    Dust.NewDust(Player.position, Player.width, Player.width, 6, 0, 0, 0, default, Main.rand.NextFloat() + 1);
                }
            }
            else
            {
                fireArmorIsActive = false;
            }
            if (havelArmor)
            {
                if (Player.HasBuff(ModContent.BuffType<HavelBuff>()))
                {
                    havelArmorActive = true;
                }
                if (havelArmorTimer > 0)
                {
                    havelArmorTimer--;
                    Player.velocity *= 0.8f;
                }
                else if (havelArmorTimer == 0)
                {
                    havelArmorTimer = -1;
                    if (!havelArmorActive)
                    {
                        havelArmorActive = true;
                        Player.AddBuff(ModContent.BuffType<HavelBuff>(), 1800);
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_ground_miss_1").WithPitchOffset(-0.2f), Player.Center); //211

                    }
                    else
                    {
                        havelArmorActive = false;
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_ground_impact_0").WithPitchOffset(-0.2f), Player.Center); //207

                        for (int d = 0; d < 30; d++)
                        {
                            int dust = Dust.NewDust(Player.position, Player.width, Player.height, 1, 0, 0, 0, default(Color), 1.5f);
                            Vector2 vel = Main.dust[dust].position - Player.MountedCenter;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel;
                        }
                    }
                }
                if (havelArmorActive)
                {
                    Player.accRunSpeed = 0;
                    if (Player.mount._type == Mod.Find<ModMount>("EarthMount").Type)
                    {
                        Player.accRunSpeed = 4;
                        Player.maxRunSpeed = 4;
                    }
                    Player.wingTime--;
                    Player.maxFallSpeed += 10;
                    if (Player.velocity.Y != 0)
                    {
                        Player.velocity.Y += 0.3f * Player.gravDir;
                    }
                    Player.jumpSpeedBoost = -1;
                    Player.jumpBoost = false;
                    if (Math.Abs(Player.velocity.X) < Player.maxRunSpeed * 1.2f)
                    {
                        if (Player.velocity.X > Player.maxRunSpeed)
                        {
                            Player.velocity.X = Player.maxRunSpeed;
                        }
                        if (Player.velocity.X < -Player.maxRunSpeed)
                        {
                            Player.velocity.X = -Player.maxRunSpeed;
                        }
                    }
                    else
                    {
                        Player.velocity.X *= 0.9f;
                    }
                    if (!Player.HasBuff(ModContent.BuffType<HavelBuff>()) && havelArmorTimer < 0)
                    {
                        havelArmorTimer = 20;
                        SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.3f), Player.Center);
                    }
                }
            }
            else
            {
                havelArmorActive = false;
                havelArmorTimer = -1;
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
                    Player.AddBuff(ModContent.BuffType<gThrownCooldown>(), gThrownTimer);
                }
                gThrownTimer--;
                if (gThrownTimer < 0)
                    gThrownTimer = 0;
            }
            else
            {
                gThrownTimer = 1200;
            }

            if (pinkSlimeArmor)
            {
                if (Player.HasBuff(ModContent.BuffType<PinkSlimeBuff>()))
                {
                    pinkSlimeActive = true;
                }
                if (pinkSlimeTimer > 0)
                {
                    pinkSlimeTimer--;
                }
                else if (pinkSlimeTimer == 0)
                {
                    pinkSlimeTimer = -1;
                    if (!pinkSlimeActive)
                    {
                        pinkSlimeActive = true;
                        Player.AddBuff(ModContent.BuffType<PinkSlimeBuff>(), 10);
                        SoundEngine.PlaySound(SoundID.DD2_OgreSpit.WithPitchOffset(0.2f), Player.Center); //155, given theres no variations we can actually just use the soundid path for this one. yaaay
                    }
                    else
                    {
                        pinkSlimeActive = false;
                        Player.fullRotation = 0;
                        Player.DelBuff(Player.FindBuffIndex(ModContent.BuffType<PinkSlimeBuff>()));
                        SoundEngine.PlaySound(SoundID.NPCDeath1.WithPitchOffset(-0.2f), Player.Center);
                        for (int d = 0; d < 30; d++)
                        {
                            int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.PinkSlime, 0, 0, 0, default(Color), 1.5f);
                            Vector2 vel = Main.dust[dust].position - Player.MountedCenter;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel;
                            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cBody, Player);
                        }
                    }
                }
                if (pinkSlimeActive)
                {
                    Player.mount.Dismount(Player);
                    Player.controlMount = false;
                    Player.maxFallSpeed += 10;
                    Player.noItems = true;
                    Player.autoJump = true;
                    Player.fallStart = (int)Player.position.Y / 16;
                    Player.noKnockback = true;
                    if (Player.velocity.Y != 0)
                        Player.runSlowdown = 0;
                }
            }
            else
            {
                pinkSlimeActive = false;
                pinkSlimeTimer = -1;
            }

            if (slimeArmor)
            {
                if (Player.HasBuff(ModContent.BuffType<SlimeBuff>()))
                {
                    slimeActive = true;
                }
                if (slimeTimer > 0)
                {
                    slimeTimer--;
                }
                else if (slimeTimer == 0)
                {
                    slimeTimer = -1;
                    if (!slimeActive)
                    {
                        slimeActive = true;
                        Player.AddBuff(ModContent.BuffType<SlimeBuff>(), 10);
                        SoundEngine.PlaySound(SoundID.DD2_OgreSpit.WithPitchOffset(0.2f), Player.Center);
                    }
                    else
                    {
                        slimeActive = false;
                        Player.fullRotation = 0;
                        Player.DelBuff(Player.FindBuffIndex(ModContent.BuffType<SlimeBuff>()));
                        SoundEngine.PlaySound(SoundID.NPCDeath1.WithPitchOffset(-0.2f), Player.Center);
                        for (int d = 0; d < 30; d++)
                        {
                            int dust = Dust.NewDust(Player.position, Player.width, Player.height, 4, 0, 0, 100, Color.Blue, 1.5f);
                            Vector2 vel = Main.dust[dust].position - Player.MountedCenter;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel;
                            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cBody, Player);
                        }
                    }
                }
                if (slimeActive)
                {
                    Player.mount.Dismount(Player);
                    Player.controlMount = false;
                    Player.noItems = true;
                    Player.noKnockback = true;
                    Player.drippingSlime = true;
                    if (Player.velocity.Y == 0)
                    {
                        Player.runSlowdown = 1f;
                        Player.moveSpeed *= 0.5f;
                    }
                    if (slimedNPC >= 0)
                    {
                        NPC sn = Main.npc[slimedNPC];
                        if (sn.active)
                        {
                            Player.position = sn.Center - (Player.Size / 2) + slimedNPCOffset;
                            Player.velocity = sn.velocity;
                            Player.controlUp = false;
                            Player.controlDown = false;
                            Player.controlLeft = false;
                            Player.controlRight = false;
                            if (Player.controlJump && Player.releaseJump)
                            {
                                Player.velocity += Player.DirectionFrom(sn.Center) * 6;
                                Player.immune = true;
                                Player.immuneTime = 20;
                                slimedNPC = -1;
                                slimedNPCOffset = Vector2.Zero;
                            }
                        }
                        else
                        {
                            slimedNPC = -1;
                            slimedNPCOffset = Vector2.Zero;
                        }
                    }
                }
                else
                {
                    slimedNPC = -1;
                    slimedNPCOffset = Vector2.Zero;
                }
            }
            else
            {
                slimeArmor = false;
                slimeTimer = -1;
                slimedNPC = -1;
                slimedNPCOffset = Vector2.Zero;
            }

            if (EnkiduMinion)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.EnkiduMinion>()] <= 0)
                {
                    int damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(1500);
                    float knockback = Player.GetKnockback(DamageClass.Summon).ApplyTo(10f);
                    Projectile.NewProjectile(Player.GetSource_FromThis("SetBonus_GenjiSummon"), Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.Minions.EnkiduMinion>(), damage, knockback, Player.whoAmI);
                }
            }
            if (XShieldItem != null)
            {
                XShieldTimer--;
                if (XShieldTimer < 0)
                {
                    int type = ModContent.ProjectileType<XParasiteIce>();
                    int damage = (int)Player.GetDamage(DamageClass.Generic).ApplyTo(300);

                    int summon = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(300);
                    int melee = (int)Player.GetDamage(DamageClass.Melee).ApplyTo(300);
                    int ranged = (int)Player.GetDamage(DamageClass.Ranged).ApplyTo(300);
                    int magic = (int)Player.GetDamage(DamageClass.Magic).ApplyTo(300);
                    int thrown = (int)Player.GetDamage(DamageClass.Throwing).ApplyTo(300);
                    bool flag = Main.rand.NextBool(4);

                    if (!flag)
                    {
                        IList<int> dmgType = new List<int>();
                        dmgType.Add(summon);
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
                            type = ModContent.ProjectileType<XParasiteYellow>();
                            damage = melee;
                        }
                        if (maxValue == ranged)
                        {
                            type = ModContent.ProjectileType<XParasiteRed>();
                            damage = ranged;
                        }
                        if (maxValue == magic)
                        {
                            type = ModContent.ProjectileType<XParasiteIce>();
                            damage = magic;
                        }
                        if (maxValue == thrown)
                        {
                            type = ModContent.ProjectileType<XParasiteGreen>();
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
                                type = ModContent.ProjectileType<XParasiteYellow>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(melee);
                                break;
                            case 2:
                                type = ModContent.ProjectileType<XParasiteGreen>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(thrown);
                                break;
                            case 3:
                                type = ModContent.ProjectileType<XParasiteRed>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(ranged);
                                break;
                            default:
                                type = ModContent.ProjectileType<XParasiteIce>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(magic);
                                break;
                        }
                    }
                    Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                    var source = Player.GetSource_Accessory(XShieldItem);
                    
                    Projectile.NewProjectile(source, Player.MountedCenter.X, Player.MountedCenter.Y, dir.X, dir.Y, type, damage, 3, Player.whoAmI);
                    flag = flag ? flag : Main.rand.NextBool(4);
                    if (flag)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                type = ModContent.ProjectileType<XParasiteYellow>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(melee);
                                break;
                            case 2:
                                type = ModContent.ProjectileType<XParasiteGreen>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(thrown);
                                break;
                            case 3:
                                type = ModContent.ProjectileType<XParasiteRed>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(ranged);
                                break;
                            default:
                                type = ModContent.ProjectileType<XParasiteIce>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(magic);
                                break;
                        }
                    }
                    Projectile.NewProjectile(source, Player.MountedCenter.X, Player.MountedCenter.Y, -dir.X, -dir.Y, type, damage, 3, Player.whoAmI);
                    flag = flag ? flag : Main.rand.NextBool(4);
                    if (flag)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                type = ModContent.ProjectileType<XParasiteYellow>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(melee);
                                break;
                            case 2:
                                type = ModContent.ProjectileType<XParasiteGreen>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(thrown);
                                break;
                            case 3:
                                type = ModContent.ProjectileType<XParasiteRed>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(ranged);
                                break;
                            default:
                                type = ModContent.ProjectileType<XParasiteIce>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(magic);
                                break;
                        }
                    }
                    dir = dir.RotatedBy(90f * 0.0174f);
                    Projectile.NewProjectile(source, Player.MountedCenter.X, Player.MountedCenter.Y, dir.X, dir.Y, type, damage, 3, Player.whoAmI);
                    flag = flag ? flag : Main.rand.NextBool(4);
                    if (flag)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 1:
                                type = ModContent.ProjectileType<XParasiteYellow>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(melee);
                                break;
                            case 2:
                                type = ModContent.ProjectileType<XParasiteGreen>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(thrown);
                                break;
                            case 3:
                                type = ModContent.ProjectileType<XParasiteRed>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(ranged);
                                break;
                            default:
                                type = ModContent.ProjectileType<XParasiteIce>();
                                damage = (int)Player.GetDamage(DamageClass.Summon).ApplyTo(magic);
                                break;
                        }
                    }
                    Projectile.NewProjectile(source, Player.MountedCenter.X, Player.MountedCenter.Y, -dir.X, -dir.Y, type, damage, 3, Player.whoAmI);
                    
                    XShieldTimer += 90;
                }
            }
            else
            {
                XShieldTimer = 90;
            }
            if (cactoidCommendationItem != null)
            {
                Player.npcTypeNoAggro[Mod.Find<ModNPC>("Cactoid").Type] = true;
                Player.npcTypeNoAggro[Mod.Find<ModNPC>("Cactite").Type] = true;
                cactoidCommendationTimer -= 1 + Main.rand.Next(2);
                if (cactoidCommendationTimer <= 0)
                {
                    var source = Player.GetSource_Accessory(cactoidCommendationItem);
                    if (Main.rand.NextBool(3))
                    {
                        NPC.NewNPC(source, (int)Player.Center.X, (int)Player.Center.Y, Mod.Find<ModNPC>("Cactoid").Type);
                    }
                    else
                    {
                        NPC.NewNPC(source, (int)Player.Center.X, (int)Player.Center.Y, Mod.Find<ModNPC>("Cactite").Type);
                    }
                    cactoidCommendationTimer = 7200;
                }
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].team == Player.team && Player.team > 0)
                    {
                        Main.player[i].AddBuff(ModContent.BuffType<CactoidFriend>(), 5, true);
                    }
                }
            }
            else
            {
                cactoidCommendationTimer = 7200;
            }
            if (cactusBootsItem != null && Player.velocity.Y == 0)
            {
                cactusBootsTimer -= Math.Abs(Player.velocity.X);
                if (cactusBootsTimer < 0)
                {
                    var source = Player.GetSource_Accessory(cactusBootsItem);
                    //int damage = (int)(18 * Player.GetDamage(DamageClass.Generic) * (Player.GetDamage(DamageClass.Generic) + Player.GetDamage(DamageClass.Summon) - 1f) * Player.GetDamage(DamageClass.Summon));
                    int damage = Player.GetWeaponDamage(cactusBootsItem);
                    float knockback = 0f;
                    Projectile.NewProjectile(source, Player.Center.X, Player.position.Y, 0, 7, ModContent.ProjectileType<BootCactus>(), damage, knockback, Player.whoAmI);
                    cactusBootsTimer = 40;
                }
            }
            else
            {
                cactusBootsTimer = 40;
            }
            if (SpectreOrbsItem != null)
            {
                SpectreOrbTimer--;
                if (SpectreOrbTimer < 0)
                {
                    var source = Player.GetSource_Accessory(SpectreOrbsItem);
                    //int damage = (int)(100 * Player.GetDamage(DamageClass.Generic) * (Player.GetDamage(DamageClass.Generic) + Player.GetDamage(DamageClass.Summon) - 1f) * Player.GetDamage(DamageClass.Summon));
                    //float knockback = 6.4f + Player.Getknockback(DamageClass.Summon).Base;
                    int damage = Player.GetWeaponDamage(SpectreOrbsItem);
                    float knockback = Player.GetWeaponKnockback(SpectreOrbsItem);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<SpectreOrb>(), damage, knockback, Player.whoAmI);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<SpectreOrb>(), damage, knockback, Player.whoAmI, 0f, 180f);
                    SpectreOrbTimer = 40;
                }
            }
            else
            {
                SpectreOrbTimer = 40;
            }
            if (SkullSigilItem != null && Player.statLife <= Player.statLifeMax2 / 2)
            {
                var source = Player.GetSource_Accessory(SkullSigilItem);
                SkullSigilTimer--;
                if (SkullSigilTimer % 20 == 0)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<ShadowAura>(), 1, 1, Player.whoAmI);
                }
                if (SkullSigilTimer < 0)
                {
                    //int damage = (int)(125 * Player.GetDamage(DamageClass.Generic) * (Player.GetDamage(DamageClass.Generic) + Player.GetDamage(DamageClass.Summon) - 1f) * Player.GetDamage(DamageClass.Summon));
                    //float knockback = 5.5f + Player.Getknockback(DamageClass.Summon).Base;
                    int damage = Player.GetWeaponDamage(SkullSigilItem);
                    float knockback = Player.GetWeaponKnockback(SkullSigilItem);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 2f, 2f, ModContent.ProjectileType<Skull>(), damage, knockback, Player.whoAmI, 45f);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 2f, -2f, ModContent.ProjectileType<Skull>(), damage, knockback, Player.whoAmI, 135f);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, -2f, 2f, ModContent.ProjectileType<Skull>(), damage, knockback, Player.whoAmI, 225f);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, -2f, -2f, ModContent.ProjectileType<Skull>(), damage, knockback, Player.whoAmI, 315f);
                    SkullSigilTimer = 180;
                }
            }
            else
            {
                SkullSigilTimer = 180;
            }
            if (LegendCool > 0)
            {
                LegendCool--;
            }
            if (LegendCool == 0)
            {
                SoundEngine.PlaySound(SoundID.MaxMana, Player.Center);
                Dust.NewDustPerfect(Player.Center, 178, new Vector2(-Player.direction, -Player.gravDir * 3), 0, Color.LimeGreen, 2);
                LegendCool--;
            }
            if (spelunky > 0)
            {
                spelunkerTimer++;
                if (spelunkerTimer >= 10)
                {
                    spelunkerTimer = 0;
                    int tileX = (int)Player.Center.X / 16;
                    int tileY = (int)Player.Center.Y / 16;
                    int num2;
                    for (int i = tileX - spelunky; i <= tileX + spelunky; i = num2 + 1)
                    {
                        for (int j = tileY - spelunky; j <= tileY + spelunky; j = num2 + 1)
                        {
                            if (Main.rand.NextBool(4))
                            {
                                Vector2 vector = new Vector2((float)(tileX - i), (float)(tileY - j));
                                if (vector.Length() < (float)spelunky && i > 0 && i < Main.maxTilesX - 1 && j > 0 && j < Main.maxTilesY - 1 && Main.tile[i, j] != null && Main.tile[i, j].HasTile)
                                {
                                    bool flag = false;
                                    if (Main.tile[i, j].TileType == 185 && Main.tile[i, j].TileFrameY == 18)
                                    {
                                        if (Main.tile[i, j].TileFrameX >= 576 && Main.tile[i, j].TileFrameX <= 882)
                                        {
                                            flag = true;
                                        }
                                    }
                                    else if (Main.tile[i, j].TileType == 186 && Main.tile[i, j].TileFrameX >= 864 && Main.tile[i, j].TileFrameX <= 1170)
                                    {
                                        flag = true;
                                    }
                                    if (flag || Main.tileSpelunker[(int)Main.tile[i, j].TileType] || (Main.tileAlch[(int)Main.tile[i, j].TileType] && Main.tile[i, j].TileType != 82))
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
            if (shieldSaplingItem != null)
            {
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<ShieldSapling>()] < 1)
                {
                    var source = Player.GetSource_Accessory(shieldSaplingItem);
                    float knockback = Player.GetKnockback(DamageClass.Summon).ApplyTo(2f);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0, 0, ModContent.ProjectileType<ShieldSapling>(), 1, knockback, Player.whoAmI);
                }
            }
            if (swordSaplingItem != null)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(Player, false))
                    {
                        if (Vector2.Distance(npc.Center, Player.Center) < 70 && (Player.Center.X - npc.Center.X) * Player.direction > 0 && Collision.CanHitLine(Player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = Player.DirectionTo(npc.Center);
                        }
                    }
                }
                swordSaplingTimer--;
                if (swordSaplingTimer < 0 && target)
                {
                    if (Player.ownedProjectileCounts[ModContent.ProjectileType<SaplingSword>()] < 1)
                    {
                        var source = Player.GetSource_Accessory(swordSaplingItem);
                        int damage = Player.GetWeaponDamage(swordSaplingItem);
                        float knockback = Player.GetWeaponKnockback(swordSaplingItem);
                        SoundEngine.PlaySound(SoundID.Item1, Player.Center);
                        Projectile.NewProjectile(source, Player.Center.X + 16*Player.direction, Player.Center.Y, shoot.X * 3, shoot.Y * 3, ModContent.ProjectileType<SaplingSword>(), damage, knockback, Player.whoAmI, 9);
                    }
                    swordSaplingTimer = (int)(9f / Player.GetAttackSpeed(DamageClass.Melee));
                }
            }
            else
            {
                swordSaplingTimer = 9;
            }
            if (hatchetSaplingItem != null)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(Player, false))
                    {
                        if (Vector2.Distance(npc.Center, Player.Center) < 300 && (Player.Center.X - npc.Center.X) * Player.direction > 0 && Collision.CanHitLine(Player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = Player.DirectionTo(npc.Center);
                        }
                    }
                }
                hatchetSaplingTimer--;
                if (hatchetSaplingTimer < 0 && target)
                {
                    if (Player.ownedProjectileCounts[ModContent.ProjectileType<CopperHatchet>()] + Player.ownedProjectileCounts[ModContent.ProjectileType<CopperHatchet2>()] < 3)
                    {
                        var source = Player.GetSource_Accessory(hatchetSaplingItem);
                        int damage = Player.GetWeaponDamage(hatchetSaplingItem);
                        float knockback = Player.GetWeaponKnockback(hatchetSaplingItem);
                        SoundEngine.PlaySound(SoundID.Item19, Player.Center);
                        Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, shoot.X * 12 * Player.ThrownVelocity, shoot.Y * 12 * Player.ThrownVelocity, ModContent.ProjectileType<CopperHatchet>(), damage, knockback, Player.whoAmI);
                    }
                    hatchetSaplingTimer = 15;
                }
            }
            else
            {
                hatchetSaplingTimer = 15;
            }
            if (bowSaplingItem != null)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(Player, false))
                    {
                        if (Vector2.Distance(npc.Center, Player.Center) < 800 && (Player.Center.X - npc.Center.X) * Player.direction > 0 && Collision.CanHitLine(Player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = Player.DirectionTo(npc.Center);
                        }
                    }
                }
                bowSaplingTimer--;
                if (bowSaplingTimer < 0 && target)
                {
                    //int damage = (int)(7 * Player.GetDamage(DamageClass.Generic) * (Player.GetDamage(DamageClass.Generic) + Player.GetDamage(DamageClass.Ranged) - 1f) * Player.GetDamage(DamageClass.Ranged));
                    //float knockback = 2;
                    var source = Player.GetSource_Accessory(bowSaplingItem);
                    //int damage = Player.GetWeaponDamage(bowSaplingItem);
                    //float knockback = Player.GetWeaponKnockback(bowSaplingItem);
                    bool flag = Player.PickAmmo(bowSaplingItem, out int proj, out float speed, out int damage, out float knockback, out int arrowID);
                    /*
                    int proj = ProjectileID.WoodenArrowFriendly;
                    float speed = 9.6f;
                    bool flag = false;
                    for (int i = 0; i < 58; i++)
                    {
                        Item item = Player.inventory[i];
                        if (item.ammo == AmmoID.Arrow && item.stack > 0)
                        {
                            flag = true;
                            damage += item.damage;
                            knockback += item.knockback;
                            proj = item.shoot;
                            speed += item.shootSpeed;
                            if (item.consumable)
                            {
                                item.stack--;
                                if (item.stack <= 0)
                                {
                                    item.active = false;
                                    item.TurnToAir();
                                }
                            }
                            break;
                        }
                    }
                    */
                    if (flag)
                    {
                        SoundEngine.PlaySound(SoundID.Item5, Player.Center);
                        Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, shoot.X * speed, shoot.Y * speed, proj, damage, knockback, Player.whoAmI);
                    }
                    bowSaplingTimer = 27;
                }
            }
            else
            {
                bowSaplingTimer = 27;
            }
            if (staffSaplingItem != null)
            {
                bool target = false;
                Vector2 shoot = new Vector2(0, 0);
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(Player, false))
                    {
                        if (Vector2.Distance(npc.Center, Player.Center) < 650 && (Player.Center.X - npc.Center.X) * Player.direction > 0 && Collision.CanHitLine(Player.Center, 1, 1, npc.position, npc.width, npc.height))
                        {
                            target = true;
                            shoot = Player.DirectionTo(npc.Center);
                        }
                    }
                }
                staffSaplingTimer--;
                if (staffSaplingTimer < 0 && target)
                {
                    var source = Player.GetSource_Accessory(staffSaplingItem);
                    int damage = Player.GetWeaponDamage(staffSaplingItem);
                    float knockback = Player.GetWeaponKnockback(staffSaplingItem);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, shoot.X * 6.5f, shoot.Y * 6.5f, ProjectileID.AmethystBolt, damage, knockback, Player.whoAmI);
                    staffSaplingTimer = 37;
                }
            }
            else
            {
                staffSaplingTimer = 37;
            }
            if (fishingSaplingItem != null && Player.HeldItem.fishingPole > 0 && Player.ownedProjectileCounts[ModContent.ProjectileType<SaplingFishHook>()] < 1 && !Player.CCed && !Player.noItems && !Player.pulley && !Player.dead)
            {
                var source = Player.GetSource_Accessory(fishingSaplingItem);
                Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, -Player.direction * 2, 0, ModContent.ProjectileType<SaplingFishHook>(), 10, 0, Player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item1, Player.Center);
            }
            if (bubbleShieldItem != null)
            {
                bubbleShieldTimer--;
                if (bubbleShieldTimer < 0)
                {
                    var source = Player.GetSource_Accessory(bubbleShieldItem);
                    int damage = Player.GetWeaponDamage(bubbleShieldItem);
                    float knockback = Player.GetWeaponKnockback(bubbleShieldItem);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 1f, 0f, ModContent.ProjectileType<Projectiles.Accessory.BubbleShield>(), damage, knockback, Player.whoAmI);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, -1f, 0f, ModContent.ProjectileType<Projectiles.Accessory.BubbleShield>(), damage,knockback, Player.whoAmI);
                    bubbleShieldTimer = 90;
                }
            }
            else
            {
                bubbleShieldTimer = 90;
            }
            if (megaBubbleShieldItem != null)
            {
                megaBubbleShieldTimer--;
                if (megaBubbleShieldTimer < 0)
                {
                    var source = Player.GetSource_Accessory(megaBubbleShieldItem);
                    int damage = Player.GetWeaponDamage(megaBubbleShieldItem);
                    float knockback = Player.GetWeaponKnockback(megaBubbleShieldItem);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 1f, 0f, ModContent.ProjectileType<Projectiles.Accessory.MegaBubbleShield>(), damage, knockback, Player.whoAmI);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, -1f, 0f, ModContent.ProjectileType<Projectiles.Accessory.MegaBubbleShield>(), damage, knockback, Player.whoAmI);
                    megaBubbleShieldTimer = 30;
                }
            }
            else
            {
                megaBubbleShieldTimer = 30;
            }
            if (Player.velocity.Y != 0 && spaceJump)
            {
                if (Player.controlJump)
                {
                    Player.fallStart = (int)(Player.position.Y / 16f);
                    float num = Player.gravity;
                    if (Player.gravDir == 1f && Player.velocity.Y > num * 15)
                    {
                        Player.velocity.Y = -12f;
                        spinTimer = 45;
                        SoundEngine.PlaySound(SoundID.DoubleJump, Player.position);
                        for (int i = 0; i < 7; i++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, 16, Main.rand.Next(-5, 5), Main.rand.Next(-5, 0), 0, default(Color), Main.rand.Next(2, 5) * 0.2f);
                        }
                    }
                    else if (Player.gravDir == -1f && Player.velocity.Y < -num * 15)
                    {
                        Player.velocity.Y = 12f;
                        spinTimer = 45;
                        SoundEngine.PlaySound(SoundID.DoubleJump, Player.position);
                        for (int i = 0; i < 7; i++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, 16, Main.rand.Next(-5, 5), Main.rand.Next(0, 5), 0, default(Color), Main.rand.Next(2, 5) * 0.2f);
                        }
                    }
                }
            }
            if (spinTimer > 0)
            {
                spinTimer--;
                Player.fullRotation = (float)(spinTimer * 24 * 0.0174f * -Player.direction * Player.gravDir);
                Player.fullRotationOrigin = Player.Center - Player.position;
                if (spinTimer % 15 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item7, Player.Center);
                }
                if (Player.velocity.Y == 0 || Player.mount.Active || Player.itemAnimation != 0)
                {
                    Player.fullRotation = 0f;
                    spinTimer = 0;
                }
            }
            else if (rocWings && noHooks && Player.velocity.Y != 0 && !Player.mount.Active && Player.controlUp && !Player.controlJump && !Player.pulley && Player.itemAnimation == 0)
            {
                int rotSpeed = 6;
                if (Player.wet)
                {
                    if (Player.honeyWet)
                    {
                        Player.maxFallSpeed = 10.5f;
                    }
                    else
                    {
                        Player.maxFallSpeed = 24.5f;
                    }
                }
                else
                {
                    Player.maxFallSpeed = 35f;
                }
                if (Player.direction == 1)
                {
                    if (Player.controlRight)
                    {
                        if (rot < 90)
                        {
                            rot += rotSpeed;
                        }
                        else
                        {
                            Player.direction = -1;
                        }
                    }
                    if (Player.controlLeft)
                    {
                        if (rot > -90)
                        {
                            rot -= rotSpeed;
                        }
                        else
                        {
                            Player.direction = -1;
                        }
                    }
                }
                else
                {
                    if (Player.controlLeft)
                    {
                        if (rot < 90)
                        {
                            rot += rotSpeed;
                        }
                        else
                        {
                            Player.direction = 1;
                        }
                    }
                    if (Player.controlRight)
                    {
                        if (rot > -90)
                        {
                            rot -= rotSpeed;
                        }
                        else
                        {
                            Player.direction = 1;
                        }
                    }
                }
                Player.fullRotation = (rot * 0.0174f * Player.gravDir * Player.direction);
                Player.fullRotationOrigin = Player.Center - Player.position;
                float speed = Player.velocity.Length();
                Player.velocity.X = (float)Math.Cos(Player.fullRotation) * speed * Player.direction;
                if (speed > Player.maxFallSpeed / 3 || rot > 0)
                {
                    Player.velocity.Y = ((float)Math.Sin(Player.fullRotation) * speed * Player.direction);
                    if (Player.velocity.Y * Player.gravDir >= 0 && Player.velocity.Y * Player.gravDir < Player.gravity)
                    {
                        Player.velocity.Y = Player.gravity * Player.gravDir;
                    }
                }
                else
                {
                    Player.maxFallSpeed /= 10;
                }
                Player.slowFall = false;
                Player.controlLeft = false;
                Player.controlRight = false;
            }
            else if (rot != 0)
            {
                rot = 0;
                Player.fullRotation = 0f;
            }
            else
            {
                int hX = (int)(Player.position.X / 16f);
                int hX2 = (int)((Player.position.X + Player.width - 0.5f) / 16f);
                int hY = (int)((Player.position.Y + Player.height + 1) / 16f);
                bool tile = SolidGround(hX, hX2, hY, hY);
                if (Player.mount.Cart && Minecart.OnTrack(Player.position, Player.width, Player.height))
                {
                    tile = true;
                }
                if (Player.waterWalk || Player.waterWalk2)
                {
                    Vector2 vel = Player.velocity + new Vector2(0, Player.gravDir);
                    Vector2 vel2 = Collision.WaterCollision(Player.position, vel, Player.width, Player.height, false, false, Player.waterWalk);
                    if (vel != vel2)
                    {
                        tile = true;
                    }
                }
                if (tile)
                {
                    hoverCanJump = true;
                }
                if (hoverBoots && hoverBootsTimer > 0 && !Player.pulley && !slimeClimbWall && !slimeClimbCeiling && Player.mount.Type != MountID.CuteFishron && Player.mount.Type != MountID.UFO)
                {
                    if (Player.mount.Cart && !Minecart.OnTrack(Player.position, Player.width, Player.height))
                    {
                        if (Player.controlLeft)
                            Player.releaseLeft = false;
                        if (Player.controlRight)
                            Player.releaseRight = false;
                    }
                    hoverDoJump = Player.controlJump;
                    if ((Player.controlLeft || Player.controlRight) && !Player.controlDown && !Player.controlJump)
                    {
                        int height = Player.height;
                        if (Player.onTrack)
                        {
                            height = Player.height - 10;
                        }
                        Vector2 arg_69_0 = Collision.TileCollision(Player.position, Player.velocity, Player.width, height, false, false, (int)Player.gravDir);
                        float num = Player.velocity.Length();
                        Vector2 value = Vector2.Normalize(Player.velocity);
                        if (arg_69_0.Y == 0f)
                        {
                            value.Y = 0f;
                        }
                        if (Player.position.Y != (int)Player.position.Y)
                        {
                            Player.position.Y = (int)Player.position.Y;
                        }
                        float num2 = num;
                        if (num2 > 16f)
                        {
                            num2 = 16f;
                        }
                        Vector2 velocity = value * num2;
                        if (Player.gravDir == -1f)
                        {
                            if ((Player.velocity.Y <= Player.gravity) && !Player.controlUp)
                            {
                                Collision.StepUp(ref Player.position, ref velocity, Player.width, Player.height, ref Player.stepSpeed, ref Player.gfxOffY, (int)Player.gravDir, true, 0);
                            }
                        }
                        else if (Player.velocity.Y >= Player.gravity && !Player.controlDown && !Player.mount.Cart)
                        {
                            Collision.StepUp(ref Player.position, ref velocity, Player.width, Player.height, ref Player.stepSpeed, ref Player.gfxOffY, (int)Player.gravDir, true, 0);
                        }
                    }
                    if (!tile)
                    {
                        bool flight = (Player.wingTime > 0 || Player.rocketTime > 0 || Player.mount._flyTime > 0) && Player.controlJump;
                        if ((Player.controlLeft || Player.controlRight) && !Player.controlDown && !Player.controlJump)
                        {
                            if (!flight)
                            {
                                hoverMount = Player.mount._flyTime;
                                hoverWing = Player.wingTime;
                                hoverRocket = Player.rocketTime;
                                Player.fallStart = (int)(Player.position.Y / 16f);
                                hoverBootsTimer--;
                                Player.slowFall = false;
                                Player.autoJump = false;
                                //hoverJump = player.controlJump; //what was player even doing here?
                                //player.controlJump = false;
                                //player.releaseJump = false;
                                Player.mount._flyTime = 0;
                                Player.wingTime = 0;
                                Player.rocketTime = 0;
                                Player.velocity.Y = 0;
                                hovering = true;
                                /*
                                if (hoverBootsStart == 0)
                                {
                                    hoverBootsStart++;
                                    player.velocity.Y = (player.gravity + 1E-06f) * -player.gravDir;
                                }
                                */
                                Player.gravity = 0;
                                int d = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (Player.gravDir > 0 ? (float)Player.height + 2f : -2f)), Player.width + 8, 4, 246, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, Color.Gold, 1.5f);
                                Main.dust[d].velocity.X = Main.dust[d].velocity.X * 0.2f;
                                Main.dust[d].velocity.Y = Main.dust[d].velocity.Y * 0.2f;
                                Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                                if (hoverBootsTimer < 60)
                                {
                                    Dust.NewDustDirect(new Vector2(Player.position.X - 4f, Player.position.Y + (Player.gravDir > 0 ? (float)Player.height + 2f : -2f)), Player.width + 8, 4, 31, -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, Color.White, 1f).velocity *= 0.2f;
                                }
                            }
                            Player.canRocket = true;
                            Player.rocketRelease = true;
                        }
                        else if (Player.velocity.Y == 0 && !(hoverCanJump && hoverDoJump))
                        {
                            Player.velocity.Y += (1E-06f) * Player.gravDir;
                        }
                        /*
                        if (!hoverCanJump)
                        {
                            Player.jumpHeight = 0;
                            if (!flight)
                            {
                                Player.jumpSpeed = 0;
                            }
                        }
                        */
                    }
                }
                if (hoverBoots && tile)
                {
                    hoverBootsTimer = 450;
                    hoverBootsStart = 0;
                }
            }
            if (planeMount && Player.velocity.Y != 0 && Player.itemAnimation == 0)
            {
                Player.fullRotation = (float)(Math.Atan2(Player.velocity.Y, Player.velocity.X)) + (Player.velocity.X > 0 || (Player.velocity.X == 0 && Player.direction == 1) ? 0 : (float)Math.PI);
                Player.fullRotationOrigin = Player.Center - Player.position + new Vector2(0, Player.height / 2);
            }
            if (planeMount && Player.velocity.Y == 0)
            {
                Player.fullRotation = 0f;
            }
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<CactusHook>()] > 0)
            {
                Player.buffImmune[BuffID.Suffocation] = true;
            }
            if (fleshShieldItem != null)
            {
                fleshShieldTimer++;
                if (fleshShieldTimer > 10 + (int)(((float)Player.statLife / (float)Player.statLifeMax2) * 350f))
                {
                    var source = Player.GetSource_Accessory(fleshShieldItem);
                    int damage = Player.GetWeaponDamage(fleshShieldItem) / 2;
                    Projectile.NewProjectile(source, Player.Center.X + Player.direction * 20, Player.Center.Y, Player.direction * 10, 0, ModContent.ProjectileType<Leech>(), damage, 1, Player.whoAmI);
                    fleshShieldTimer = 0;
                }
            }
            else
            {
                fleshShieldTimer = 0;
            }
            if (Player.dash <= 0)
            {
                Dash();
            }
            if (dirtArmor)
            {
                int dirt = 0;
                for (int i = 0; i < 58; i++)
                {
                    if (Player.inventory[i].type == ItemID.DirtBlock && Player.inventory[i].stack > 0)
                    {
                        dirt += Player.inventory[i].stack;
                    }
                }
                Player.statDefense += dirt / 666;
            }
            if (havelShieldItem != null && (Player.ownedProjectileCounts[ModContent.ProjectileType<HavelShield>()] > 0 || (Player.controlUseTile && Player.itemAnimation == 0 && !ItemLoader.AltFunctionUse(Player.HeldItem, Player))))
            {
                Player.noItems = true;
                havelBlocking = true;
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<HavelShield>()] < 1)
                {
                    var source = Player.GetSource_Accessory(bubbleShieldItem);
                    int damage = Player.GetWeaponDamage(bubbleShieldItem);
                    float knockback = Player.GetWeaponKnockback(bubbleShieldItem);
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0, 0, ModContent.ProjectileType<HavelShield>(), damage, knockback, Player.whoAmI);
                }
                if (Math.Abs(Player.velocity.X) < Player.maxRunSpeed * 1.2f)
                {
                    if (Player.velocity.X > Player.maxRunSpeed)
                    {
                        Player.velocity.X = Player.maxRunSpeed;
                    }
                    if (Player.velocity.X < -Player.maxRunSpeed)
                    {
                        Player.velocity.X = -Player.maxRunSpeed;
                    }
                }
                else
                {
                    Player.velocity.X *= 0.93f;
                }
            }
            if (blazeAnklet)
            {
                int num = (int)(Player.Center.X / 16f);
                int num2 = (int)((Player.position.Y + Player.height) / 16f);
                if (Player.gravDir == -1f)
                {
                    num2 = (int)(Player.position.Y - 0.1f) / 16;
                }
                int type = -1;
                if (Main.tile[num, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num, num2].TileType])
                {
                    type = Main.tile[num, num2].TileType;
                }
                else if (Main.tile[num - 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num - 1, num2].TileType])
                {
                    type = Main.tile[num - 1, num2].TileType;
                }
                else if (Main.tile[num + 1, num2].HasUnactuatedTile && Main.tileSolid[Main.tile[num + 1, num2].TileType])
                {
                    type = Main.tile[num + 1, num2].TileType;
                }
                if (type != -1 && TileID.Sets.TouchDamageHot[type] != 0)
                {
                    Player.maxRunSpeed *= 4f;
                    Player.runAcceleration *= 2f;
                    Player.runSlowdown *= 2f;
                    if (Player.mount._type == Mod.Find<ModMount>("FierySoles").Type)
                    {
                        runAccelerationMult *= 2f;
                        accRunSpeedMult *= 2.5f;
                    }
                    Dust.NewDustDirect(new Vector2(Player.position.X, num2 * 16 - 10 * Player.gravDir), Player.width, 10, DustID.Torch).noGravity = true;
                }
                Player.GetCritChance(DamageClass.Ranged) += (int)Math.Round(Math.Abs(Player.velocity.X / 2));
                if (fireArmorIsActive && Player.velocity.Y == 0 && Math.Abs(Player.velocity.X) > 5)
                {
                    for (int i = 0; i <= Math.Round(Math.Abs(Player.velocity.X) / 16f); i++)
                    {
                        var source = Player.GetSource_FromThis("SetBonus_FireArmor");
                        int damage = (int)Player.GetDamage(DamageClass.Ranged).ApplyTo(25);
                        Projectile.NewProjectile(source, (num + i * Player.direction) * 16, num2 * 16, 0, 0, ModContent.ProjectileType<Flame2>(), damage, 0, Player.whoAmI);
                    }
                }
            }
            if (airArmorIsActive)
            {
                Player.maxRunSpeed *= 1.5f;
                if (Player.mount._type != Mod.Find<ModMount>("FierySoles").Type)
                    accRunSpeedMult *= 1.5f;
                Player.runAcceleration *= 2f;
                Player.runSlowdown *= 3f;
                Player.jumpSpeedBoost += 5f;
                Player.noFallDmg = true;
                Dust.NewDust(Player.position, Player.width, Player.height, 31, -4 * Player.direction, 0f, 0, Color.White, 1f);
            }
            if (airArmorDodgeTimer > 0)
            {
                airArmorDodgeTimer--;
            }
            else if (airArmor)
            {
                if (airArmorDodgeTimer == 0)
                {
                    airArmorDodgeTimer--;
                    SoundEngine.PlaySound(SoundID.Item7, Player.Center);
                }
                if (Main.rand.NextBool(2))
                    Dust.NewDustPerfect(new Vector2(Player.Center.X, Player.Center.Y + (Player.height / 2 * Player.gravDir)), 31, Vector2.Zero, 0, Color.White, 1).noGravity = true;
            }
            if (emptyHeart)
            {
                Player.GetDamage(DamageClass.Generic) += 0.2f;
                if (Player.statLife > 1)
                {
                    Player.statLife = 1;
                }
                Player.statLifeMax2 = 1;
            }
        }
        /*
        public override void UpdateVanityAccessories()
        {
            if (!glowContacts)
            {
                for (int e = 3; e < 8 + Player.extraAccessorySlots; e++)
                {
                    if (Player.armor[e].type == ModContent.ItemType<GlowingContacts>())
                    {
                        glowContacts = true;
                        switch (e)
                        {
                            case 4:
                                glowEyeType = 1;
                                break;
                            case 5:
                                glowEyeType = 2;
                                break;
                            case 6:
                                glowEyeType = 3;
                                break;
                            case 7:
                                glowEyeType = 4;
                                break;
                            case 8:
                                glowEyeType = 5;
                                break;
                            default:
                                glowEyeType = 0;
                                break;
                        }
                        break;
                    }
                }
                for (int v = 13; v < 18 + Player.extraAccessorySlots; v++)
                {
                    if (Player.armor[v].type == ModContent.ItemType<GlowingContacts>())
                    {
                        glowContacts = true;
                        switch (v)
                        {
                            case 14:
                                glowEyeType = 1;
                                break;
                            case 15:
                                glowEyeType = 2;
                                break;
                            case 16:
                                glowEyeType = 3;
                                break;
                            case 17:
                                glowEyeType = 4;
                                break;
                            case 18:
                                glowEyeType = 5;
                                break;
                            default:
                                glowEyeType = 0;
                                break;
                        }
                        if (Player.hideVisibleAccessory[v - 10])
                        {
                            glowEyeNoGlow = true;
                        }
                        break;
                    }
                }
            }
        }
        */
        public override void PostUpdateRunSpeeds()
        {
            Player.runAcceleration *= runAccelerationMult;
            Player.accRunSpeed *= accRunSpeedMult;
            if (hovering)
            {
                Player.onWrongGround = false;
            }
            if (hoverBoots)
            {
                if (hoverCanJump && hoverDoJump)
                    Player.controlJump = true;

                if (Player.jump > 0 && !Player.controlJump)
                {
                    hoverCanJump = false;
                }
            }
            if (slimeArmor && !Player.mount.Active && noHooks && slimedNPC == -1)
            {
                slimeClimbWall = false;
                slimeClimbCeiling = false;
                Player.sliding = false;
                Player.spikedBoots = 0;

                float speed = 2f;

                bool ceiling = false;
                bool wall = false;
                float posX = Player.position.X;
                float ceilXL = Player.position.X - 2;
                float ceilXC = Player.Center.X;
                float ceilXR = Player.position.X + Player.width + 2;
                if (Player.slideDir == 1)
                {
                    posX += (float)Player.width;
                }
                posX += (float)Player.slideDir;
                float posY = Player.position.Y;
                float ceilY = Player.position.Y - 1f;
                if (Player.gravDir < 0f)
                {
                    posY = Player.position.Y + Player.height;
                    ceilY = Player.position.Y + (float)Player.height + 1f;
                }
                posX /= 16f;
                posY /= 16f;
                ceilY /= 16f;
                ceilXL /= 16f;
                ceilXC /= 16f;
                ceilXR /= 16f;
                if (WorldGen.SolidOrSlopedTile((int)posX, (int)posY) || WorldGen.SolidOrSlopedTile((int)posX, (int)posY + 1) || WorldGen.SolidOrSlopedTile((int)posX, (int)posY + 2))
                {
                    wall = true;
                }
                if (WorldGen.SolidOrSlopedTile((int)ceilXC, (int)ceilY) || (WorldGen.SolidOrSlopedTile((int)ceilXL, (int)ceilY) && !WorldGen.SolidOrSlopedTile((int)ceilXL, (int)(ceilY+Player.gravDir))) || (WorldGen.SolidOrSlopedTile((int)ceilXR, (int)ceilY) && !WorldGen.SolidTile((int)ceilXR, (int)(ceilY+Player.gravDir))))
                {
                    ceiling = true;
                }
                if (wall)
                {
                    float grav = Player.gravity;
                    if (Player.slowFall)
                    {
                        if (Player.controlUp)
                        {
                            grav = Player.gravity / 10f * Player.gravDir;
                        }
                        else
                        {
                            grav = Player.gravity / 3f * Player.gravDir;
                        }
                    }
                    Player.fallStart = (int)(Player.position.Y / 16f);
                    if (Player.controlDown)
                    {
                        Player.velocity.Y = speed * Player.gravDir;
                        if (!WorldGen.SolidTile((int)posX, (int)(posY + (Player.velocity.Y / 16f))) && !Collision.SolidCollision(new Vector2(Player.position.X, (int)(posY * 16) + Player.gravDir), Player.width, Player.height))
                        {
                            Player.velocity.Y = Player.velocity.Y = (-grav + 1E-05f) * Player.gravDir;
                            Player.position.Y = (int)(posY * 16f) + Player.gravDir;
                        }
                        /*
                        int d = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)((player.width / 2 - 4) * player.slideDir), player.position.Y + (float)(player.height / 2) + (float)(player.height / 2 - 4) * player.gravDir), 8, 8, 4, 0f, 0f, 100, Color.Blue, 1f);
                        if (player.slideDir < 0)
                        {
                            Dust d2 = Main.dust[d];
                            d2.position.X = d2.position.X - 10f;
                        }
                        if (player.gravDir < 0f)
                        {
                            Dust d2 = Main.dust[d];
                            d2.position.Y = d2.position.Y - 12f;
                        }
                        Main.dust[d].velocity *= 0.1f;
                        Main.dust[d].scale *= 1.2f;
                        Main.dust[d].noGravity = true;
                        Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(player.cBody, player);
                        */
                    }
                    else if (Player.controlUp)
                    {
                        Player.velocity.Y = -speed * Player.gravDir;
                    }
                    else
                    {
                        Player.velocity.Y = (-grav + 1E-05f) * Player.gravDir;
                    }
                    slimeClimbWall = true;
                    Player.sliding = true;
                }
                else if (ceiling && !(Player.controlDown && !Player.controlLeft && !Player.controlRight) && !Player.justJumped)
                {
                    float grav = Player.gravity;
                    if (Player.slowFall)
                    {
                        if (Player.controlUp)
                        {
                            grav = Player.gravity / 10f * Player.gravDir;
                        }
                        else
                        {
                            grav = Player.gravity / 3f * Player.gravDir;
                        }
                    }
                    if (Player.velocity.Y >= 0)
                    {
                        if (Player.controlLeft)
                        {
                            if (Player.velocity.X > -speed)
                            {
                                Player.velocity.X -= Player.runAcceleration;
                            }
                            else
                            {
                                Player.velocity.X = -speed;
                            }
                        }
                        else if (Player.controlRight)
                        {
                            if (Player.velocity.X < speed)
                            {
                                Player.velocity.X += Player.runAcceleration;
                            }
                            else
                            {
                                Player.velocity.X = speed;
                            }
                        }
                        else
                        {
                            Player.velocity.X = 0;
                        }
                    }
                    else
                    {
                        Player.velocity.X = 0;
                    }

                    Player.velocity.Y = (-grav + 1E-05f) * Player.gravDir;
                    if (Player.controlUp)
                    {
                        Player.velocity.Y = -speed * 2 * Player.gravDir;
                    }
                    if (Player.controlJump && Player.releaseJump)
                    {
                        Player.velocity.Y = speed * Player.gravDir;
                        Player.jump = 0;
                        Player.controlJump = false;
                    }
                    Player.fallStart = (int)(Player.position.Y / 16f);
                    slimeClimbCeiling = true;
                    Player.sliding = true;
                }
            }
            else
            {
                slimeClimbCeiling = false;
                slimeClimbWall = false;
            }
        }
        public override void PreUpdateMovement()
        {
            if (waterBubbleItem != null)
            {
                Player.wet = true;
                Player.wetCount = 10;
            }
        }
        public override void PostUpdate()
        {
            if (hoverBoots)
            {
                if (hovering)
                {
                    Player.wingTime = hoverWing;
                    Player.rocketTime = hoverRocket;
                    Player.mount._flyTime = hoverMount;
                }
                if (Player.jump <= 0 && Player.controlJump)
                {
                    hoverCanJump = false;
                }
            }
            if (airArmorIsActive)
            {
                if (Player.controlRight && Player.velocity.X < Player.maxRunSpeed)
                {
                    Player.velocity.X += 0.3f;
                }
                if (Player.controlLeft && Player.velocity.X > -Player.maxRunSpeed)
                {
                    Player.velocity.X -= 0.3f;
                }
                if (Player.gravDir > 0)
                {
                    if (Player.controlJump && Player.velocity.Y > -Player.maxRunSpeed)
                    {
                        Player.velocity.Y -= 0.25f;
                    }
                    if (Player.controlDown && Player.velocity.Y < Player.maxRunSpeed)
                    {
                        Player.velocity.Y += 0.25f;
                    }
                }
                else
                {
                    if (Player.controlJump && Player.velocity.Y < Player.maxRunSpeed)
                    {
                        Player.velocity.Y += 0.25f;
                    }
                    if (Player.controlDown && Player.velocity.Y > -Player.maxRunSpeed)
                    {
                        Player.velocity.Y -= 0.25f;
                    }
                }
            }
            if (pinkSlimeActive)
            {
                if (Player.velocity.Y != 0)
                {
                    Player.fullRotation = Utils.AngleLerp((float)(Math.Atan2(oldVelocity.Y, oldVelocity.X) + Math.PI / 2 * (oldVelocity.X == 0 ? Player.direction : Math.Sign(oldVelocity.X))) + (oldVelocity.X > 0 || (oldVelocity.X == 0 && Player.direction == 1) ? 0 : (float)Math.PI), (float)(Math.Atan2(Player.velocity.Y, Player.velocity.X) + Math.PI / 2 * (Player.velocity.X == 0 ? Player.direction : Math.Sign(Player.velocity.X))) + (Player.velocity.X > 0 || (Player.velocity.X == 0 && Player.direction == 1) ? 0 : (float)Math.PI), 0.5f);
                    Player.fullRotationOrigin = Player.Center - Player.position;
                }
                else
                {
                    Player.fullRotation = 0;
                }
                if (oldVelocity.Length() > 1)
                {
                    Vector2 cVel = Collision.TileCollision(Player.position, Player.velocity, Player.width, Player.height, Player.controlDown, false, (int)Player.gravDir);


                    if (Math.Abs(cVel.X) < 0.1f && Math.Abs(oldVelocity.X) > 1)
                    {
                        Player.velocity.X = -oldVelocity.X;
                        if ((Player.controlLeft || Player.controlRight))
                        {
                            if (Math.Abs(Player.velocity.X) < Player.jumpSpeed + Player.jumpSpeedBoost)
                                Player.velocity.X = (Player.jumpSpeed + Player.jumpSpeedBoost) * Math.Sign(Player.velocity.X);
                            if (Math.Abs(Player.velocity.X) < 195)
                                Player.velocity.X += (0.3f + Player.runAcceleration) * Math.Sign(Player.velocity.X); 
                        }
                        SoundEngine.PlaySound(SoundID.NPCHit1.WithVolumeScale(0.1f).WithPitchOffset(-0.5f), Player.Center);
                    }
                    if (Math.Abs(cVel.Y) < 0.1f && Math.Abs(oldVelocity.Y) > 1)
                    {
                        Player.velocity.Y = -oldVelocity.Y;
                        if (Player.controlJump)
                        {
                            if (Math.Abs(Player.velocity.Y) < Player.jumpSpeed + Player.jumpSpeedBoost)
                                Player.velocity.Y = (Player.jumpSpeed + Player.jumpSpeedBoost) * Math.Sign(Player.velocity.Y);
                            if (Math.Abs(Player.velocity.Y) < 195)
                                Player.velocity.Y +=  (1 + Player.runAcceleration) * Math.Sign(Player.velocity.Y);
                            if ((Player.controlLeft || Player.controlRight) && Math.Abs(Player.velocity.X) < 195)
                            {
                                Player.velocity.X += (0.3f + Player.runAcceleration) * Math.Sign(Player.velocity.X);
                            }
                        }
                        SoundEngine.PlaySound(SoundID.NPCHit1.WithVolumeScale(0.1f).WithPitchOffset(-0.5f), Player.Center);
                    }
                }
            }
        }
        private static bool SolidGround(int startX, int endX, int startY, int endY)
        {
            if (startX < 0)
            {
                return true;
            }
            if (endX >= Main.maxTilesX)
            {
                return true;
            }
            if (startY < 0)
            {
                return true;
            }
            if (endY >= Main.maxTilesY)
            {
                return true;
            }
            for (int i = startX; i < endX + 1; i++)
            {
                for (int j = startY; j < endY + 1; j++)
                {
                    if (Main.tile[i, j] == null)
                    {
                        return false;
                    }
                    if (Main.tile[i, j].HasTile && !Main.tile[i, j].IsActuated && (Main.tileSolid[(int)Main.tile[i, j].TileType] || Main.tileSolidTop[(int)Main.tile[i, j].TileType]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override void UpdateDead()
        {
            var source = Player.GetSource_Death();
            if (corruptSoul)
            {
                float damag = Player.statLifeMax2 * 0.25f;
                float num3 = 0f;
                int ploya = Player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[Player.whoAmI].hostile && !Main.player[i].hostile) && Main.player[Player.whoAmI].team != Main.player[i].team) && Math.Abs(Main.player[i].Center.X - Player.Center.X + Math.Abs(Main.player[i].Center.Y - Player.Center.Y)) < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num3)
                    {
                        num3 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        ploya = i;
                    }
                }
                if ((int)damag > 0)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0, -5, ModContent.ProjectileType<CorruptedSoul>(), (int)damag, 0, ploya);
                }
                corruptSoul = false;
            }
            if (lifeRend)
            {
                float lifeStoled = Player.statLifeMax2 * 0.05f;
                float num3 = 0f;
                int ploya = Player.whoAmI;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[Player.whoAmI].hostile && !Main.player[i].hostile) && Main.player[Player.whoAmI].team != Main.player[i].team) && Math.Abs(Main.player[i].Center.X - Player.Center.X + Math.Abs(Main.player[i].Center.Y - Player.Center.Y)) < 1200f && (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife) > num3)
                    {
                        num3 = (float)(Main.player[i].statLifeMax2 - Main.player[i].statLife);
                        ploya = i;
                    }
                }
                if ((int)lifeStoled > 0 && !Player.moonLeech)
                {
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 0f, 0f, 305, 0, 0f, Player.whoAmI, (float)ploya, lifeStoled);
                }
                lifeRend = false;
            }
            if (infectedBlue)
            {
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("IceXParasite").Type);
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("IceXParasite").Type);
                infectedBlue = false;
            }
            if (infectedGreen)
            {
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("GreenXParasite").Type);
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("GreenXParasite").Type);
                infectedGreen = false;
            }
            if (infectedRed)
            {
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("RedXParasite").Type);
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("RedXParasite").Type);
                infectedRed = false;
            }
            if (infectedYellow)
            {
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("XParasite").Type);
                NPC.NewNPC(source, (int)Player.MountedCenter.X, (int)Player.MountedCenter.Y, Mod.Find<ModNPC>("XParasite").Type);
                infectedYellow = false;
            }
        }
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (npc.whoAmI == slimedNPC)
            {
                return false;
            }
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (shieldSaplingItem != null)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.type == ModContent.ProjectileType<ShieldSapling>() && proj.getRect().Intersects(projectile.getRect()) && proj.hostile && proj.damage <= 15 && proj.active)
                    {
                        //Main.NewText(proj.damage, Color.DarkGreen);
                        proj.Kill();
                        SoundEngine.PlaySound(SoundID.NPCHit4, projectile.Center);
                        return false;
                    }
                }
            }
            return base.CanBeHitByProjectile(proj);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (enemyIgnoreDefenseDamage > 0)
            {
                damage = enemyIgnoreDefenseDamage;
                customDamage = true;
            }
            enemyIgnoreDefenseDamage = 0;
            if (sporganItem != null)
            {
                var source = Player.GetSource_Accessory(sporganItem);
                int sdamage = Player.GetWeaponDamage(sporganItem);
                float knockback = Player.GetWeaponKnockback(sporganItem);
                int num = 10;
                double deltaAngle = (float)(Math.PI * 2) / num;
                for (int i = 0; i < num; i++)
                {
                    double offsetAngle = (float)(Math.PI / 2) + deltaAngle * i;
                    Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, 3 * (float)Math.Sin(offsetAngle), 3 * (float)Math.Cos(offsetAngle), ProjectileID.SporeCloud, sdamage, knockback, Player.whoAmI);
                }
            }
            if (Player.HasBuff(ModContent.BuffType<gThrownDodge>()))
            {
                Player.AddBuff(ModContent.BuffType<gThrownBuff>(), 200);
                Player.longInvince = true;
                Player.ShadowDodge();
                for (int j = 0; j < 80; j++)
                {
                    int num = Dust.NewDust(Player.position, Player.width, Player.height, 31, 0f, 0f, 0, Color.Black, 1f);
                    Dust dust = Main.dust[num];
                    dust.position.X = dust.position.X + (float)Main.rand.Next(-20, 21);
                    dust.position.Y = dust.position.Y + (float)Main.rand.Next(-20, 21);
                    dust.velocity *= 0.4f;
                    dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    if (Main.rand.NextBool(2))
                    {
                        dust.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                        dust.noGravity = true;
                    }
                }
                if (Player.whoAmI == Main.myPlayer)
                {
                    for (int j = 0; j < 22; j++)
                    {
                        if (Player.buffTime[j] > 0 && Player.buffType[j] == ModContent.BuffType<gThrownDodge>())
                        {
                            Player.DelBuff(j);
                        }
                    }
                }
                return false;
            }
            if (XShieldItem != null && XShieldTimer > 0)
            {
                XShieldTimer -= damage;
            }
            if (dirtArmor)
            {
                int dirt = 0;
                for (int i = 0; i < 58; i++)
                {
                    if (Player.inventory[i].type == ItemID.DirtBlock && Player.inventory[i].stack > 0)
                    {
                        dirt += Player.inventory[i].stack;
                    }
                }
                int amount = Math.Min(((dirt / 666) / 2), damage);
                for (int i = 0; i < 58 && amount > 0; i++)
                {
                    if (Player.inventory[i].stack > 0 && Player.inventory[i].type == ItemID.DirtBlock)
                    {
                        if (Player.inventory[i].stack >= amount)
                        {
                            Player.inventory[i].stack -= amount;
                            amount = 0;
                        }
                        else
                        {
                            amount -= Player.inventory[i].stack;
                            Player.inventory[i].stack = 0;
                        }
                        if (Player.inventory[i].stack <= 0)
                        {
                            Player.inventory[i].SetDefaults(0, false);
                        }
                        if (amount <= 0)
                        {
                            break;
                        }
                    }
                }
            }
            if (havelArmorActive)
            {
                damage = (int)(damage * 0.6f);
                playSound = false;
                genGore = false;
                SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.5f), Player.Center);
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, 1);
                }
            }
            if (pinkSlimeActive)
            {
                Player.velocity.X = Math.Max(Math.Abs(Player.velocity.X), 5f) * hitDirection;
                if ((Player.controlLeft || Player.controlRight))
                {
                    if (Math.Abs(Player.velocity.X) < Player.jumpSpeed + Player.jumpSpeedBoost)
                        Player.velocity.X = (Player.jumpSpeed + Player.jumpSpeedBoost) * Math.Sign(Player.velocity.X);
                    if (Math.Abs(Player.velocity.X) < 196)
                        Player.velocity.X += Player.runAcceleration + Math.Sign(Player.velocity.X);
                }
                damage = (int)(damage * 0.5f);
                playSound = false;
                genGore = false;
                SoundEngine.PlaySound(SoundID.NPCHit1.WithVolumeScale(0.7f).WithPitchOffset(-0.4f), Player.Center);
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, 243);
                }
            }
            if (slimeActive)
            {
                damage = (int)(damage * 0.8f);
                playSound = false;
                genGore = false;
                SoundEngine.PlaySound(SoundID.NPCHit1.WithVolumeScale(0.7f).WithPitchOffset(-0.4f), Player.Center);
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, 4, 0, 0, 100, Color.Blue);
                }
            }
            return true;
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (damageSource.SourceOtherIndex == 8)
            {
                if (bonesHurt)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " had their bones dissolved");
                }
                if (corruptSoul)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " had their soul corrupted");
                }
                if (infectedBlue || infectedGreen || infectedRed || infectedYellow)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was infected by X");
                }
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (havelArmorActive && npc.knockBackResist > 0)
            {
                float KB = 3;
                int dir = Player.direction;
                if (npc.Center.X > Player.Center.X)
                {
                    dir = 1;
                }
                else
                {
                    dir = -1;
                }
                if (Player.velocity.X * dir > 0)
                {
                    KB += Math.Abs(Player.velocity.X);
                }
                npc.velocity.X = dir * KB;
                npc.velocity.Y--;
                if (Main.netMode != 0)
                {
                    ModPacket packet = Mod.GetPacket();
                    packet.Write((byte)JoostModMessageType.NPCpos);
                    packet.Write(npc.whoAmI);
                    packet.WriteVector2(npc.position);
                    packet.WriteVector2(npc.velocity);
                    ModPacket netMessage = packet;
                    netMessage.Send();
                }
            }
            if (fireArmorIsActive)
            {
                npc.AddBuff(BuffID.OnFire, 600);
            }
            if (slimeActive)
            {
                slimedNPC = npc.whoAmI;
                slimedNPCOffset = Player.Center - npc.Center;
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (havelShieldItem != null && Player.ownedProjectileCounts[ModContent.ProjectileType<HavelShield>()] > 0)
            {
                float x = Player.MountedCenter.X - 9 + (Player.direction * 11);
                float y = Player.position.Y + (Player.height / 2) - 23;
                Rectangle rect = new Rectangle((int)x, (int)y, 18, 46);
                if (rect.Intersects(npc.getRect()))
                {
                    damage -= (int)Player.GetWeaponDamage(havelShieldItem);
                    SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.4f), npc.Center);
                }
            }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (havelShieldItem != null && Player.ownedProjectileCounts[ModContent.ProjectileType<HavelShield>()] > 0)
            {
                float x = Player.MountedCenter.X - 9 + (Player.direction * 11);
                float y = Player.position.Y + (Player.height / 2) - 23;
                Rectangle rect = new Rectangle((int)x, (int)y, 18, 46);
                if (proj.Colliding(proj.getRect(), rect))
                {
                    damage -= (int)Player.GetWeaponDamage(havelShieldItem);
                    proj.penetrate--;
                    SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.4f), proj.Center);
                }
            }
        }
        public void Dash()
        {
            if (dashType == 1 && Player.dashDelay < 0 && Player.whoAmI == Main.myPlayer) //Shield of Flesh
            {
                Rectangle rectangle = new Rectangle((int)((double)Player.position.X + (double)Player.velocity.X * 0.5 - 4.0), (int)((double)Player.position.Y + (double)Player.velocity.Y * 0.5 - 4.0), Player.width + 8, Player.height + 8);
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly)
                    {
                        NPC npc = Main.npc[i];
                        Rectangle rect = npc.getRect();
                        if (rectangle.Intersects(rect) && (npc.noTileCollide || Player.CanHit(npc)))
                        {
                            bool crit = false;
                            if (Main.rand.Next(100) < Player.GetCritChance(DamageClass.Generic))
                            {
                                crit = true;
                            }
                            int dir = Player.direction;
                            if (Player.velocity.X < 0f)
                            {
                                dir = -1;
                            }
                            if (Player.velocity.X > 0f)
                            {
                                dir = 1;
                            }
                            if (!dashHit[i])
                            {
                                Player.ApplyDamageToNPC(npc, dashDamage, 0, dir, crit);
                                dashHit[i] = true;
                            }
                            if (npc.knockBackResist > 0 && (Player.velocity.X >= 12 || Player.velocity.X <= -12 || Player.velocity.X <= -Math.Max(Player.accRunSpeed, Player.maxRunSpeed) || Player.velocity.X >= Math.Max(Player.accRunSpeed, Player.maxRunSpeed)))
                            {
                                float push = Player.Center.X + 12;
                                if (dir < 0)
                                {
                                    push = (Player.Center.X - 12) - npc.width;
                                }
                                Vector2 pos = npc.position;
                                pos.X = push + Player.velocity.X;
                                if (!dashBounce)
                                {
                                    if (Collision.SolidCollision(pos, npc.width, npc.height))
                                    {
                                        Player.velocity.X = -dir * 9;
                                        Player.velocity.Y = -4f;
                                        Player.ApplyDamageToNPC(npc, dashDamage, 0, dir, crit);
                                        dashBounce = true; 
                                    }
                                    else
                                    {
                                        npc.position.X = push;
                                        npc.velocity.X = Player.velocity.X;
                                        if (Main.netMode != NetmodeID.SinglePlayer)
                                        {
                                            ModPacket packet = Mod.GetPacket();
                                            packet.Write((byte)JoostModMessageType.NPCpos);
                                            packet.Write(npc.whoAmI);
                                            packet.WriteVector2(npc.position);
                                            packet.WriteVector2(npc.velocity);
                                            ModPacket netMessage = packet;
                                            netMessage.Send();
                                        }
                                    }
                                }
                            }
                            else if (!dashBounce)
                            {
                                Player.velocity.X = -dir * 9;
                                Player.velocity.Y = -4f;
                                dashBounce = true;
                            }
                            if (Player.immuneTime < 4)
                            {
                                Player.immune = true;
                                Player.immuneNoBlink = true;
                                Player.immuneTime = 4;
                            }
                        }
                    }
                }
            }
            if (Player.dashDelay < 0)
            {
                float num7 = 12f;
                float num8 = 0.992f;
                float num9 = Math.Max(Player.accRunSpeed, Player.maxRunSpeed);
                float num10 = 0.96f;
                int delay = 20;
                if (dashType == 1) //Shield of Flesh
                {
                    for (int l = 0; l < 0; l++)
                    {
                        int num13;
                        if (Player.velocity.Y == 0f)
                        {
                            num13 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)Player.height - 4f), Player.width, 8, 31, 0f, 0f, 100, default(Color), 1.4f);
                        }
                        else
                        {
                            num13 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + (float)(Player.height / 2) - 8f), Player.width, 16, 31, 0f, 0f, 100, default(Color), 1.4f);
                        }
                        Main.dust[num13].velocity *= 0.1f;
                        Main.dust[num13].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                        Main.dust[num13].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
                    }
                    delay = 25;
                }
                if (dashType > 0)
                {
                    Player.vortexStealthActive = false;
                    if (dashType == 1) //Dash Held Extension
                    {
                        if (Player.velocity.Y == 0 && ((Player.velocity.X > 0 && Player.controlRight) || (Player.velocity.X < 0 && Player.controlLeft)))
                        {
                            float eSpeed = Math.Max(num9, 6) + 0.5f;
                            if (Player.velocity.X < 0f && Player.velocity.X > -eSpeed)
                            {
                                Player.velocity.X = -eSpeed;
                            }
                            if (Player.velocity.X > 0f && Player.velocity.X < eSpeed)
                            {
                                Player.velocity.X = eSpeed;
                            }
                            return;
                        }
                    }
                    if (Player.velocity.X > num7 || Player.velocity.X < -num7)
                    {
                        Player.velocity.X = Player.velocity.X * num8;
                        return;
                    }
                    if (Player.velocity.X > num9 || Player.velocity.X < -num9)
                    {
                        Player.velocity.X = Player.velocity.X * num10;
                        return;
                    }
                    Player.dashDelay = delay;
                    if (Player.velocity.X < 0f)
                    {
                        Player.velocity.X = -num9;
                        return;
                    }
                    if (Player.velocity.X > 0f)
                    {
                        Player.velocity.X = num9;
                        return;
                    }
                }
            }
            else if (dashType > 0 && !Player.mount.Active && Player.dashDelay == 0)
            {
                if (dashType == 1) //Shield of Flesh
                {
                    for (int i = 0; i < 200; i++)
                    {
                        dashHit[i] = false;
                    }
                    dashBounce = false;
                    float speed = 15.5f;
                    int dir = 0;
                    bool dashing = false;
                    if (Player.dashTime > 0)
                    {
                        Player.dashTime--;
                    }
                    if (Player.dashTime < 0)
                    {
                        Player.dashTime++;
                    }
                    if (Player.controlRight && Player.releaseRight)
                    {
                        if (Player.dashTime > 0)
                        {
                            dir = 1;
                            dashing = true;
                            Player.dashTime = 0;
                        }
                        else
                        {
                            Player.dashTime = 15;
                        }
                    }
                    else if (Player.controlLeft && Player.releaseLeft)
                    {
                        if (Player.dashTime < 0)
                        {
                            dir = -1;
                            dashing = true;
                            Player.dashTime = 0;
                        }
                        else
                        {
                            Player.dashTime = -15;
                        }
                    }
                    if (dashing)
                    {
                        Player.velocity.X = speed * dir;
                        Point point3 = (Player.Center + new Vector2((float)(dir * Player.width / 2 + 2), Player.gravDir * -(float)Player.height / 2f + Player.gravDir * 2f)).ToTileCoordinates();
                        Point point4 = (Player.Center + new Vector2((float)(dir * Player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point3.X, point3.Y) || WorldGen.SolidOrSlopedTile(point4.X, point4.Y))
                        {
                            Player.velocity.X = Player.velocity.X / 2f;
                        }
                        Player.dashDelay = -1;
                        for (int num21 = 0; num21 < 0; num21++)
                        {
                            int num22 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 31, 0f, 0f, 100, default(Color), 2f);
                            Dust dust3 = Main.dust[num22];
                            dust3.position.X = dust3.position.X + (float)Main.rand.Next(-5, 6);
                            Dust dust4 = Main.dust[num22];
                            dust4.position.Y = dust4.position.Y + (float)Main.rand.Next(-5, 6);
                            Main.dust[num22].velocity *= 0.2f;
                            Main.dust[num22].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                            Main.dust[num22].shader = GameShaders.Armor.GetSecondaryShader(Player.cShield, Player);
                        }
                        return;
                    }
                }
            }

        }
    }
}