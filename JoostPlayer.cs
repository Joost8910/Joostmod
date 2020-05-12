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
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace JoostMod
{
    public class JoostPlayer : ModPlayer
    {
        private const int saveVersion = 0;
        public bool cactuarMinions = false;
        public bool lunarRod = false;
        public bool stormy = false;
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
        public int hoverBootsTimer = 900;
        private int hoverBootsStart = 0;
        private float hoverWing = 0;
        private int hoverRocket = 0;
        private bool hoverJump = false;
        public bool hovering = false;
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
        public bool shieldSapling = false;
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
        private int cactoidCommendationTimer = 7200;
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
        public bool dirtArmor = false;
        public bool havelShield = false;
        public bool havelBlocking = false;
        public bool havelArmor = false;
        public bool havelArmorActive = false;
        private int havelArmorTimer = -1;
        public bool fireArmor = false;
        public bool fireArmorIsActive = false;
        public bool blazeAnklet = false;
        public bool airArmor = false;
        public bool airArmorIsActive = false;
        private int airArmorDodgeTimer = -1;
        public bool airMedallion = false;
        public bool zoraArmor = false;
        public bool waterBubble = false;
        public bool hideBubble = false;
        public bool emptyHeart = false;
        public float runAccelerationMult = 1;
        public float accRunSpeedMult = 1;
        public int dashType = 0;
        public int dashDamage = 0;
        private bool[] dashHit = new bool[200];
        private bool dashBounce = false;
        public bool westStone = false;
        public bool eastStone = false;
        public bool highStone = false;
        public bool deepStone = false;
        public Vector2 sandSharkVel = Vector2.Zero;
        public override void ResetEffects()
        {
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
            XShield = false;
            SpectreOrbs = false;
            SkullSigil = false;
            GMagic = false;
            bubbleShield = false;
            megaBubbleShield = false;
            hoverBoots = false;
            hovering = false;
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
            shieldSapling = false;
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
            if (player.mount.Type != mod.MountType("SandShark"))
            {
                sandSharkVel = Vector2.Zero;
            }
            cactusBoots = false;
            dirtArmor = false;
            fleshShield = false;
            havelShield = false;
            havelBlocking = false;
            havelArmor = false;
            fireArmor = false;
            blazeAnklet = false;
            airArmor = false;
            airArmorIsActive = false;
            airMedallion = false;
            zoraArmor = false;
            waterBubble = false;
            hideBubble = false;
            emptyHeart = false;
            accRunSpeedMult = 1;
            runAccelerationMult = 1;
            dashType = 0;
            dashDamage = 0;
        }
        public override void SetupStartInventory(IList<Item> items, bool mediumCoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("StormyCollar"));
            items.Add(item);
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
            if (fireArmorIsActive)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 12;
            }
        }
        public override void UpdateLifeRegen()
        {
            if (airArmorIsActive)
            {
                player.lifeRegenTime += 10;
                player.lifeRegen += 3;
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
            if (player.HeldItem.type == mod.ItemType("SandGlove"))
            {
                player.handon = (sbyte)mod.GetEquipSlot("SandGlove", EquipType.HandsOn);
            }
            if (player.HeldItem.type == mod.ItemType("MutantCannon"))
            {
                player.handoff = (sbyte)mod.GetEquipSlot("MutantCannon", EquipType.HandsOff);
            }

            if (havelBlocking)
            {
                player.shield = (sbyte)mod.GetEquipSlot("HavelsGreatshield", EquipType.Shield);
            }
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (player.shield == (sbyte)mod.GetEquipSlot("HavelsGreatshield", EquipType.Shield) && !havelBlocking)
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
                if (havelArmorActive)
                {
                    if (layers[i] == PlayerLayer.Legs)
                    {
                        layers.Insert(i + 1, stoneLegs);
                        stoneLegs.visible = true;
                    }
                    if (layers[i] == PlayerLayer.Body)
                    {
                        layers.Insert(i + 1, stoneBody);
                        stoneBody.visible = true;
                    }
                    if (layers[i] == PlayerLayer.Head)
                    {
                        layers.Insert(i + 1, stoneHead);
                        stoneHead.visible = true;
                    }
                    if (layers[i] == PlayerLayer.Arms)
                    {
                        layers.Insert(i + 1, stoneArms);
                        stoneArms.visible = true;
                    }
                }
            }
        }
        public static readonly PlayerLayer shieldDownLayer = new PlayerLayer("JoostMod", "shieldDownLayer", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            Mod mod = JoostMod.instance;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.shield == (sbyte)mod.GetEquipSlot("HavelsGreatshield", EquipType.Shield))
            {
                Texture2D tex = mod.GetTexture("Items/HavelsGreatshield_ShieldDown");
                Rectangle frame = new Rectangle(drawPlayer.bodyFrame.X-1, drawPlayer.bodyFrame.Y, drawPlayer.bodyFrame.Width+6, drawPlayer.bodyFrame.Height);
                float rot = drawPlayer.bodyRotation;
                Vector2 drawPos = drawPlayer.bodyPosition;
                Vector2 origin = drawInfo.bodyOrigin;
                Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
                SpriteEffects effects = SpriteEffects.None;
                if (drawPlayer.direction == -1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                if (drawPlayer.gravDir == -1f)
                {
                    effects |= SpriteEffects.FlipVertically;
                }
                DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
                data.shader = drawInfo.shieldShader;
                Main.playerDrawData.Add(data);
            }
        });
        public static readonly PlayerLayer stoneHead = new PlayerLayer("JoostMod", "stoneHead", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            Mod mod = JoostMod.instance;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.GetModPlayer<JoostPlayer>().havelArmorActive)
            {
                Texture2D tex = mod.GetTexture("Buffs/StoneFlesh_Head");
                Rectangle frame = drawPlayer.bodyFrame;
                float rot = drawPlayer.headRotation;
                Vector2 drawPos = drawPlayer.bodyPosition;
                Vector2 origin = drawInfo.headOrigin;
                Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
                SpriteEffects effects = SpriteEffects.None;
                if (drawPlayer.direction == -1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                if (drawPlayer.gravDir == -1f)
                {
                    effects |= SpriteEffects.FlipVertically;
                }
                DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
                data.shader = drawInfo.headArmorShader;
                Main.playerDrawData.Add(data);
            }
        });
        public static readonly PlayerLayer stoneArms = new PlayerLayer("JoostMod", "stoneArms", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            Mod mod = JoostMod.instance;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.GetModPlayer<JoostPlayer>().havelArmorActive)
            {
                Texture2D tex = mod.GetTexture("Buffs/StoneFlesh_Arms");
                Rectangle frame = drawPlayer.bodyFrame;
                float rot = drawPlayer.bodyRotation;
                Vector2 drawPos = drawPlayer.bodyPosition;
                Vector2 origin = drawInfo.bodyOrigin;
                Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
                SpriteEffects effects = SpriteEffects.None;
                if (drawPlayer.direction == -1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                if (drawPlayer.gravDir == -1f)
                {
                    effects |= SpriteEffects.FlipVertically;
                }
                DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
                data.shader = drawInfo.bodyArmorShader;
                Main.playerDrawData.Add(data);
            }
        });
        public static readonly PlayerLayer stoneBody = new PlayerLayer("JoostMod", "stoneBody", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            Mod mod = JoostMod.instance;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.GetModPlayer<JoostPlayer>().havelArmorActive)
            {
                Texture2D tex = mod.GetTexture("Buffs/StoneFlesh_Body");
                Rectangle frame = drawPlayer.bodyFrame;
                float rot = drawPlayer.bodyRotation;
                Vector2 drawPos = drawPlayer.bodyPosition;
                Vector2 origin = drawInfo.bodyOrigin;
                Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
                SpriteEffects effects = SpriteEffects.None;
                if (drawPlayer.direction == -1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                if (drawPlayer.gravDir == -1f)
                {
                    effects |= SpriteEffects.FlipVertically;
                }
                DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
                data.shader = drawInfo.bodyArmorShader;
                Main.playerDrawData.Add(data);
            }
        });
        public static readonly PlayerLayer stoneLegs = new PlayerLayer("JoostMod", "stoneLegs", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
        {
            Mod mod = JoostMod.instance;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.GetModPlayer<JoostPlayer>().havelArmorActive)
            {
                Texture2D tex = mod.GetTexture("Buffs/StoneFlesh_Legs");
                Rectangle frame = drawPlayer.legFrame;
                float rot = drawPlayer.legRotation;
                Vector2 drawPos = drawPlayer.legPosition;
                Vector2 origin = drawInfo.legOrigin;
                Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
                SpriteEffects effects = SpriteEffects.None;
                if (drawPlayer.direction == -1)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                if (drawPlayer.gravDir == -1f)
                {
                    effects |= SpriteEffects.FlipVertically;
                }
                DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
                data.shader = drawInfo.legArmorShader;
                Main.playerDrawData.Add(data);
            }
        });
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
                if (havelArmor && havelArmorTimer < 0)
                {
                    if (!havelArmorActive)
                    {
                        havelArmorTimer = 25;
                        Main.PlaySound(42, (int)player.Center.X, (int)player.Center.Y, 230, 1, -0.3f);
                        for (int d = 0; d < 40; d++)
                        {
                            int dust = Dust.NewDust(player.position - new Vector2(10, 10), player.width+20, player.height+20, 1, player.velocity.X * 0.8f, player.velocity.Y * 0.8f, 0, default(Color), 1.5f);
                            Vector2 vel = player.MountedCenter - Main.dust[dust].position;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel + player.velocity * 0.8f;
                            Main.dust[dust].noGravity = true;
                        }
                    }
                    else
                    {
                        havelArmorTimer = 20;
                        Main.PlaySound(21, (int)player.Center.X, (int)player.Center.Y, 1, 1, -0.3f);
                    }
                }
                if (fireArmor)
                {
                    player.AddBuff(mod.BuffType("fireArmorBuff"), 2);
                    if (!fireArmorIsActive)
                    {
                        Main.PlaySound(42, player.Center, 198);
                        for (int i = 0; i < 30; i++)
                            Dust.NewDustDirect(player.position, player.width, player.height, 6, player.velocity.X, player.velocity.Y, 0, default, Main.rand.NextFloat() + 3).noGravity = true;
                        fireArmorIsActive = true;
                    }
                    else
                    {
                        Main.PlaySound(2, player.Center, 13);
                        for (int i = 0; i < 30; i++)
                            Dust.NewDust(player.position, player.width, player.height, 31, 0, -1.5f, 0, new Color(0.2f, 0.1f, 0.15f), Main.rand.NextFloat() + 1);
                        fireArmorIsActive = false;
                    }
                }
                if (airArmor)
                {
                    float count = 0;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile proj = Main.projectile[i];
                        if (proj.owner == player.whoAmI && proj.active && proj.minionSlots > 0)
                        {
                            proj.Kill();
                            count += proj.minionSlots;
                            for (int d = 0; d < 10; d++)
                                Dust.NewDust(proj.position, proj.width, proj.height, 31, 0, 0, 0, Color.White, Main.rand.NextFloat() + 1);
                        }
                    }
                    int duration = (int)(count * 2 * 60);

                    if (duration > 0)
                    {
                        if (airArmorDodgeTimer <= 0 && player.immuneTime < 40)
                        {
                            airArmorDodgeTimer = 165;
                            player.immune = true;
                            player.immuneTime = 45;
                        }
                        player.AddBuff(mod.BuffType("AirArmorBuff"), duration);
                        Main.PlaySound(42, player.Center, 203);
                        for (int i = 0; i < 30; i++)
                            Dust.NewDust(player.position, player.width, player.height, 31, -4 * player.direction, 0f, 0, Color.White, Main.rand.NextFloat() + 1);
                    }
                }
                if (zoraArmor && player.ownedProjectileCounts[mod.ProjectileType("ZoraSpin")] < 1)
                {
                    int damage = (int)(40 * player.allDamageMult * (player.allDamage + player.magicDamage - 1f) * player.magicDamageMult);
                    int wet = player.wet ? 1 : 0;
                    float speed = 6;
                    Vector2 vel = new Vector2(0, -player.gravity);
                    if (player.controlRight)
                    {
                        vel.X += speed;
                    }
                    if (player.controlLeft)
                    {
                        vel.X -= speed;
                    }
                    if (player.controlUp || player.controlJump)
                    {
                        vel.Y -= player.gravDir * speed;
                    }
                    if (player.controlDown)
                    {
                        vel.Y += player.gravDir * speed;
                    }
                    if (vel.X != 0 && vel.Y != 0)
                    {
                        vel *= 0.707f;
                    }
                    if (player.wet)
                    {
                        vel *= 2;
                        if (player.immuneTime < 24)
                        {
                            player.immune = true;
                            player.immuneTime = 24;
                        }
                    }
                    Projectile.NewProjectile(player.Center, vel, mod.ProjectileType("ZoraSpin"), damage, 5f, player.whoAmI, wet);
                    Main.PlaySound(42, player.Center, 201);
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
                if (gMelee && player.ownedProjectileCounts[mod.ProjectileType("Masamune")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), (int)(500 * (player.allDamage + player.meleeDamage - 1) * player.meleeDamageMult * player.allDamageMult), 10f, player.whoAmI);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), 0, 0, player.whoAmI);
                }
            }
            if (projectile.ranged)
            {
                if (fireArmorIsActive)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }
            }
            if (projectile.minion)
            {
                if (airMedallion && projectile.type != mod.ProjectileType("AirBlast") && Main.rand.NextBool(10))
                {
                    Main.PlaySound(2, target.Center, 18);
                    Projectile.NewProjectile(target.Center.X, target.position.Y + target.height, 0, -10f, mod.ProjectileType("AirBlast"), (int)(25 * (player.allDamage + player.minionDamage - 1) * player.minionDamageMult * player.allDamageMult), 0, player.whoAmI);
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
                    Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, mod.ProjectileType("Sand"), (int)(20 * (player.allDamage + player.thrownDamage - 1) * player.thrownDamageMult * player.allDamageMult), 1, projectile.owner);
                    sandStormTimer = 5;
                }

            }

        }
        public override void OnHitPvpWithProj(Projectile projectile, Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.melee && player.heldProj == projectile.whoAmI)
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
                if (gMelee && player.ownedProjectileCounts[mod.ProjectileType("Masamune")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), (int)(500 * player.meleeDamage), 10f, player.whoAmI);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("Masamune"), 0, 0, player.whoAmI);
                }
            }
            if (projectile.ranged)
            {
                if (fireArmorIsActive)
                {
                    target.AddBuff(BuffID.OnFire, 600);
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
                    Projectile.NewProjectile(vector2.X, vector2.Y, speedX, speedY, mod.ProjectileType("Sand"), (int)(20 * (player.allDamage + player.thrownDamage - 1) * player.thrownDamageMult * player.allDamageMult), 1, projectile.owner);
                    sandStormTimer = 5;
                }

            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (waterBubble && proj.magic && target.wet)
            {
                damage = (int)(damage * 1.15f);
                knockback *= 1.15f;
            }
        }
        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (waterBubble && proj.magic && target.wet)
            {
                damage = (int)(damage * 1.15f);
            }
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (waterBubble && item.magic && target.wet)
            {
                damage = (int)(damage * 1.15f);
                knockback *= 1.15f;
            }
        }
        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if (waterBubble && item.magic && target.wet)
            {
                damage = (int)(damage * 1.15f);
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
                if (Main.rand.Next(Math.Max(30 * (Main.hardMode ? 2 : 1), 3000 / power)) == 0 && worldLayer == 1 && (player.position.X / 16 < 350 || player.position.X / 16 > Main.maxTilesX - 350))
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
            if (liquidType == 0 && Main.rand.Next(Math.Max(20, 1000 / power)) == 0 && Main.hardMode && cactoidCommendation && player.ZoneDesert)
            {
                caughtType = mod.ItemType("CactoidCompact");
            }

            if (lunarRod && Main.rand.Next(4) == 0)
            {
                caughtType = 3456 + Main.rand.Next(4);
            }
            if (JoostWorld.downedGilgamesh)
            {
                if (Main.rand.NextBool(Math.Max(100, 200000 / power)))
                {
                    if (player.ZoneCorrupt || player.ZoneCrimson)
                    {
                        caughtType = mod.ItemType("EvilStone");
                    }
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && player.ZoneDungeon)
                {
                    caughtType = mod.ItemType("SkullStone");
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && player.ZoneJungle)
                {
                    caughtType = mod.ItemType("JungleStone");
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && player.ZoneUnderworldHeight)
                {
                    caughtType = mod.ItemType("InfernoStone");
                }
                else if (Main.rand.NextBool(Math.Max(100, 200000 / power)) && liquidType == 0)
                {
                    if (player.position.X / 16 < 350)
                    {
                        caughtType = mod.ItemType("SeaStoneWest");
                    }
                    if (player.position.X / 16 > Main.maxTilesX - 350)
                    {
                        caughtType = mod.ItemType("SeaStoneEast");
                    }
                    if (worldLayer >= 3)
                    {
                        caughtType = mod.ItemType("SeaStoneDeep");
                    }
                    if (worldLayer <= 0)
                    {
                        caughtType = mod.ItemType("SeaStoneHigh");
                    }
                }
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
        public override void PreUpdate()
        {
            if (waterBubble)
            {
                player.wet = true;
                player.wetCount = 10;
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
            if (waterBubble)
            {
                player.wet = true;
                player.wetCount = 10;
                if (!hideBubble && player.ownedProjectileCounts[mod.ProjectileType("PersonalBubble")] < 1)
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("PersonalBubble"), 0, 0, player.whoAmI);
                }
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
            else
            {
                gRangedIsActive = false;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("ZoraSpin")] > 0)
            {
                player.noItems = true;
                if (player.wet)
                    player.maxFallSpeed += 10;
            }
            if (fireArmor)
            {
                if (fireArmorIsActive)
                {
                    player.AddBuff(mod.BuffType("fireArmorBuff"), 2);
                    player.rangedDamageMult *= 1.4f;
                    player.moveSpeed *= 1.4f;
                    player.maxRunSpeed *= 1.4f;
                    if (player.mount._type == mod.MountType("FierySoles"))
                        accRunSpeedMult *= 1.4f;
                    else
                        player.accRunSpeed *= 1.4f;
                    player.onFire = true;
                    player.ClearBuff(BuffID.Chilled);
                    player.ClearBuff(BuffID.Frozen);
                    Dust.NewDust(player.position, player.width, player.width, 6, 0, 0, 0, default, Main.rand.NextFloat() + 1);
                }
            }
            else
            {
                fireArmorIsActive = false;
            }
            if (havelArmor)
            {
                if (havelArmorTimer > 0)
                {
                    havelArmorTimer--;
                    player.velocity *= 0.8f;
                }
                else if (havelArmorTimer == 0)
                {
                    havelArmorTimer = -1;
                    if (!havelArmorActive)
                    {
                        havelArmorActive = true;
                        Main.PlaySound(42, (int)player.Center.X, (int)player.Center.Y, 211, 1, -0.2f);
                    }
                    else
                    {
                        havelArmorActive = false;
                        Main.PlaySound(42, (int)player.Center.X, (int)player.Center.Y, 207, 1, -0.2f);
                        for (int d = 0; d < 30; d++)
                        {
                            int dust = Dust.NewDust(player.position, player.width, player.height, 1, 0, 0, 0, default(Color), 1.5f);
                            Vector2 vel = Main.dust[dust].position - player.MountedCenter;
                            vel.Normalize();
                            Main.dust[dust].velocity = vel;
                        }
                    }
                }
                if (havelArmorActive)
                {
                    player.AddBuff(mod.BuffType("HavelBuff"), 2);
                    player.accRunSpeed = 0;
                    if (player.mount._type == mod.MountType("EarthMount"))
                    {
                        player.accRunSpeed = 4;
                        player.maxRunSpeed = 4;
                    }
                    player.wingTime--;
                    player.maxFallSpeed += 10;
                    if (player.velocity.Y != 0)
                    {
                        player.velocity.Y += 0.3f * player.gravDir;
                    }
                    player.jumpSpeedBoost = -1;
                    player.jumpBoost = false;
                    if (Math.Abs(player.velocity.X) < player.maxRunSpeed * 1.2f)
                    {
                        if (player.velocity.X > player.maxRunSpeed)
                        {
                            player.velocity.X = player.maxRunSpeed;
                        }
                        if (player.velocity.X < -player.maxRunSpeed)
                        {
                            player.velocity.X = -player.maxRunSpeed;
                        }
                    }
                    else
                    {
                        player.velocity.X *= 0.9f;
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
                    int damage = (int)(1500 * player.allDamageMult * (player.allDamage + player.minionDamage - 1f) * player.minionDamageMult);
                    float knockback = 10f + player.minionKB;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("EnkiduMinion"), damage, knockback, player.whoAmI);
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
                player.npcTypeNoAggro[mod.NPCType("Cactoid")] = true;
                player.npcTypeNoAggro[mod.NPCType("Cactite")] = true;
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
                    cactoidCommendationTimer = 7200;
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
                cactoidCommendationTimer = 7200;
            }
            if (cactusBoots && player.velocity.Y == 0)
            {
                cactusBootsTimer -= Math.Abs(player.velocity.X);
                if (cactusBootsTimer < 0)
                {
                    int damage = (int)(18 * player.allDamageMult * (player.allDamage + player.minionDamage - 1f) * player.minionDamageMult);
                    float knockback = 0f;
                    Projectile.NewProjectile(player.Center.X, player.position.Y, 0, 7, mod.ProjectileType("BootCactus"), damage, knockback, player.whoAmI);
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
                    int damage = (int)(100 * player.allDamageMult * (player.allDamage + player.minionDamage - 1f) * player.minionDamageMult);
                    float knockback = 6.4f + player.minionKB;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 2f, mod.ProjectileType("SpectreOrb"), damage, knockback, player.whoAmI);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 2f, mod.ProjectileType("SpectreOrb"), damage, knockback, player.whoAmI, 0f, 180f);
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
                    int damage = (int)(125 * player.allDamageMult * (player.allDamage + player.minionDamage - 1f) * player.minionDamageMult);
                    float knockback = 5.5f + player.minionKB;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 2f, 2f, mod.ProjectileType("Skull"), damage, knockback, player.whoAmI, 45f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 2f, -2f, mod.ProjectileType("Skull"), damage, knockback, player.whoAmI, 135f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, 2f, mod.ProjectileType("Skull"), damage, knockback, player.whoAmI, 225f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, -2f, mod.ProjectileType("Skull"), damage, knockback, player.whoAmI, 315f);
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
            if (shieldSapling)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("ShieldSapling")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("ShieldSapling"), 1, 2 + player.minionKB, player.whoAmI);
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
                    if (player.ownedProjectileCounts[mod.ProjectileType("SaplingSword")] < 1)
                    {
                        Main.PlaySound(2, player.Center, 1);
                        Projectile.NewProjectile(player.Center.X + 16*player.direction, player.Center.Y, shoot.X * 3, shoot.Y * 3, mod.ProjectileType("SaplingSword"), (int)(12 * player.allDamageMult * (player.allDamage + player.meleeDamage - 1f) * player.meleeDamageMult), 4f * (player.kbGlove ? 2 : 1) * (player.kbBuff ? 1.5f : 1), player.whoAmI);
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
                    if (player.ownedProjectileCounts[mod.ProjectileType("CopperHatchet")] + player.ownedProjectileCounts[mod.ProjectileType("CopperHatchet2")] < 3)
                    {
                        Main.PlaySound(2, player.Center, 19);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, shoot.X * 12 * player.thrownVelocity, shoot.Y * 12 * player.thrownVelocity, mod.ProjectileType("CopperHatchet"), (int)(7 * player.allDamageMult * (player.allDamage + player.thrownDamage - 1f) * player.thrownDamageMult), 3f, player.whoAmI);
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
                    int damage = (int)(7 * player.allDamageMult * (player.allDamage + player.rangedDamage - 1f) * player.rangedDamageMult);
                    float knockback = 2;
                    int proj = ProjectileID.WoodenArrowFriendly;
                    float speed = 9.6f;
                    bool flag = false;
                    for (int i = 0; i < 58; i++)
                    {
                        Item item = player.inventory[i];
                        if (item.ammo == AmmoID.Arrow && item.stack > 0)
                        {
                            flag = true;
                            damage += item.damage;
                            knockback += item.knockBack;
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
                    if (flag)
                    {
                        Main.PlaySound(2, player.Center, 5);
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, shoot.X * speed, shoot.Y * speed, proj, damage, knockback, player.whoAmI);
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
                    int damage = (int)(15 * player.allDamageMult * (player.allDamage + player.magicDamage - 1f) * player.magicDamageMult);
                    float knockback = 3.5f;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, shoot.X * 6.5f, shoot.Y * 6.5f, 121, damage, knockback, player.whoAmI);
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
                    int damage = (int)(10 * player.allDamageMult * (player.allDamage + player.minionDamage - 1f) * player.minionDamageMult);
                    float knockback = 8f + player.minionKB;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 1f, 0f, mod.ProjectileType("BubbleShield"), damage, knockback, player.whoAmI);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -1f, 0f, mod.ProjectileType("BubbleShield"), damage,knockback, player.whoAmI);
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
                    int damage = (int)(90 * player.allDamageMult * (player.allDamage + player.minionDamage - 1f) * player.minionDamageMult);
                    float knockback = 18.5f + player.minionKB;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 1f, 0f, mod.ProjectileType("MegaBubbleShield"), damage, knockback, player.whoAmI);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, -1f, 0f, mod.ProjectileType("MegaBubbleShield"), damage, knockback, player.whoAmI);
                    megaBubbleShieldTimer = 30;
                }
            }
            else
            {
                megaBubbleShieldTimer = 30;
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
            bool noHooks = player.ownedProjectileCounts[mod.ProjectileType("SwingyHook")] + player.ownedProjectileCounts[mod.ProjectileType("MobHook")] + player.ownedProjectileCounts[mod.ProjectileType("EnchantedSwingyHook")] + player.ownedProjectileCounts[mod.ProjectileType("CactusHook")] <= 0;
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
            else if (rocWings && noHooks && player.velocity.Y != 0 && !player.mount.Active && player.controlUp && !player.controlJump && !player.pulley && player.itemAnimation == 0)
            {
                int rotSpeed = 6;
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
                if (speed > player.maxFallSpeed / 3 || rot > 0)
                {
                    player.velocity.Y = ((float)Math.Sin(player.fullRotation) * speed * player.direction);
                    if (player.velocity.Y * player.gravDir >= 0 && player.velocity.Y * player.gravDir < player.gravity)
                    {
                        player.velocity.Y = player.gravity * player.gravDir;
                    }
                }
                else
                {
                    player.maxFallSpeed /= 10;
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
            else
            {
                int hX = (int)(player.position.X / 16f);
                int hX2 = (int)((player.position.X + player.width - 0.5f) / 16f);
                int hY = (int)((player.position.Y + player.height + 1) / 16f);
                bool tile = SolidGround(hX, hX2, hY, hY);
                if (hoverBoots && hoverBootsTimer > 0 && player.jump <= 0 && !player.pulley)
                {
                    if ((player.controlLeft || player.controlRight) && !player.controlDown)
                    {
                        int height = player.height;
                        if (player.onTrack)
                        {
                            height = player.height - 10;
                        }
                        Vector2 arg_69_0 = Collision.TileCollision(player.position, player.velocity, player.width, height, false, false, (int)player.gravDir);
                        float num = player.velocity.Length();
                        Vector2 value = Vector2.Normalize(player.velocity);
                        if (arg_69_0.Y == 0f)
                        {
                            value.Y = 0f;
                        }
                        if (player.position.Y != (int)player.position.Y)
                        {
                            player.position.Y = (int)player.position.Y;
                        }
                        float num2 = num;
                        if (num2 > 16f)
                        {
                            num2 = 16f;
                        }
                        Vector2 velocity = value * num2;
                        if (player.gravDir == -1f)
                        {
                            if ((player.velocity.Y <= player.gravity) && !player.controlUp)
                            {
                                Collision.StepUp(ref player.position, ref velocity, player.width, player.height, ref player.stepSpeed, ref player.gfxOffY, (int)player.gravDir, true, 0);
                            }
                        }
                        else if (player.velocity.Y >= player.gravity && !player.controlDown && !player.mount.Cart)
                        {
                            Collision.StepUp(ref player.position, ref velocity, player.width, player.height, ref player.stepSpeed, ref player.gfxOffY, (int)player.gravDir, true, 0);
                        }
                    }
                    if (!tile)
                    {
                        bool flight = (player.wingTime > 0 || player.rocketTime > 0) && player.controlJump;
                        if ((player.controlLeft || player.controlRight) && !player.controlDown)
                        {
                            if (!flight)
                            {
                                hoverWing = player.wingTime;
                                hoverRocket = player.rocketTime;
                                player.fallStart = (int)(player.position.Y / 16f);
                                hoverBootsTimer--;
                                player.slowFall = false;
                                player.autoJump = false;
                                hoverJump = player.controlJump;
                                player.controlJump = false;
                                player.releaseJump = false;
                                player.wingTime = 0;
                                player.rocketTime = 0;
                                player.velocity.Y = 0;
                                hovering = true;
                                /*
                                if (hoverBootsStart == 0)
                                {
                                    hoverBootsStart++;
                                    player.velocity.Y = (player.gravity + 1E-06f) * -player.gravDir;
                                }
                                */
                                player.gravity = 0;
                                int num7 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + 2f), player.width + 8, 4, 16, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, Color.Gold * 1.2f, 1.5f);
                                Main.dust[num7].velocity.X = Main.dust[num7].velocity.X * 0.2f;
                                Main.dust[num7].velocity.Y = Main.dust[num7].velocity.Y * 0.2f;
                                Main.dust[num7].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                            }
                            player.canRocket = true;
                            player.rocketRelease = true;
                        }
                        else if (player.velocity.Y == 0)
                        {
                            player.velocity.Y += (1E-06f) * player.gravDir;
                        }
                        Player.jumpHeight = 0;
                        if (!flight)
                        {
                            Player.jumpSpeed = 0;
                        }
                    }
                }
                if (hoverBoots && tile)
                {
                    hoverBootsTimer = 450;
                    hoverBootsStart = 0;
                }
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
                if (fleshShieldTimer > 4 + (int)(((float)player.statLife / (float)player.statLifeMax2) * 296f))
                {
                    Projectile.NewProjectile(player.Center.X + player.direction * 20, player.Center.Y, player.direction * 10, 0, mod.ProjectileType("Leech"), (int)(20 * (player.allDamage + player.meleeDamage - 1) * player.meleeDamageMult * player.allDamageMult), 1, player.whoAmI);
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
            if (dirtArmor)
            {
                int dirt = 0;
                for (int i = 0; i < 58; i++)
                {
                    if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
                    {
                        dirt += player.inventory[i].stack;
                    }
                }
                player.statDefense += dirt / 666;
            }
            if (havelShield && (player.ownedProjectileCounts[mod.ProjectileType("HavelShield")] > 0 || (player.controlUseTile && player.itemAnimation == 0 && !ItemLoader.AltFunctionUse(player.HeldItem, player))))
            {
                player.noItems = true;
                havelBlocking = true;
                if (player.ownedProjectileCounts[mod.ProjectileType("HavelShield")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("HavelShield"), (int)(50 * player.allDamageMult * (player.allDamage + player.meleeDamage - 1f) * player.meleeDamageMult), 8f * (player.kbGlove ? 2 : 1) * (player.kbBuff ? 1.5f : 1), player.whoAmI);
                }
                if (Math.Abs(player.velocity.X) < player.maxRunSpeed * 1.2f)
                {
                    if (player.velocity.X > player.maxRunSpeed)
                    {
                        player.velocity.X = player.maxRunSpeed;
                    }
                    if (player.velocity.X < -player.maxRunSpeed)
                    {
                        player.velocity.X = -player.maxRunSpeed;
                    }
                }
                else
                {
                    player.velocity.X *= 0.95f;
                }
            }
            if (blazeAnklet)
            {
                int num = (int)(player.Center.X / 16f);
                int num2 = (int)((player.position.Y + player.height) / 16f);
                if (player.gravDir == -1f)
                {
                    num2 = (int)(player.position.Y - 0.1f) / 16;
                }
                int type = -1;
                if (Main.tile[num, num2].nactive() && Main.tileSolid[Main.tile[num, num2].type])
                {
                    type = Main.tile[num, num2].type;
                }
                else if (Main.tile[num - 1, num2].nactive() && Main.tileSolid[Main.tile[num - 1, num2].type])
                {
                    type = Main.tile[num - 1, num2].type;
                }
                else if (Main.tile[num + 1, num2].nactive() && Main.tileSolid[Main.tile[num + 1, num2].type])
                {
                    type = Main.tile[num + 1, num2].type;
                }
                if (type != -1 && TileID.Sets.TouchDamageHot[type] != 0)
                {
                    player.maxRunSpeed *= 4f;
                    player.runAcceleration *= 2f;
                    player.runSlowdown *= 2f;
                    if (player.mount._type == mod.MountType("FierySoles"))
                    {
                        runAccelerationMult *= 2f;
                        accRunSpeedMult *= 2.5f;
                    }
                    Dust.NewDustDirect(new Vector2(player.position.X, num2 * 16 - 10 * player.gravDir), player.width, 10, 6).noGravity = true;
                }
                player.rangedCrit += (int)Math.Round(Math.Abs(player.velocity.X / 2));
                if (fireArmorIsActive && player.velocity.Y == 0 && Math.Abs(player.velocity.X) > 5)
                {
                    for (int i = 0; i <= Math.Round(Math.Abs(player.velocity.X) / 16f); i++)
                    {
                        Projectile.NewProjectile((num + i * player.direction) * 16, num2 * 16, 0, 0, mod.ProjectileType("Flame2"), (int)(25 * (player.allDamage + player.rangedDamage - 1) * player.rangedDamageMult * player.allDamageMult), 0, player.whoAmI);
                    }
                }
            }
            if (airArmorIsActive)
            {
                player.maxRunSpeed *= 1.5f;
                if (player.mount._type != mod.MountType("FierySoles"))
                    accRunSpeedMult *= 1.5f;
                player.runAcceleration *= 2f;
                player.runSlowdown *= 3f;
                player.jumpSpeedBoost += 5f;
                player.noFallDmg = true;
                if (player.controlRight && player.velocity.X < player.maxRunSpeed)
                {
                    player.velocity.X += 0.3f;
                }
                if (player.controlLeft && player.velocity.X > -player.maxRunSpeed)
                {
                    player.velocity.X -= 0.3f;
                }
                if (player.gravDir > 0)
                {
                    if (player.controlJump && player.velocity.Y > -player.maxRunSpeed)
                    {
                        player.velocity.Y -= 0.25f;
                    }
                    if (player.controlDown && player.velocity.Y < player.maxRunSpeed)
                    {
                        player.velocity.Y += 0.25f;
                    }
                }
                else
                {
                    if (player.controlJump && player.velocity.Y < player.maxRunSpeed)
                    {
                        player.velocity.Y += 0.25f;
                    }
                    if (player.controlDown && player.velocity.Y > -player.maxRunSpeed)
                    {
                        player.velocity.Y -= 0.25f;
                    }
                }
                Dust.NewDust(player.position, player.width, player.height, 31, -4 * player.direction, 0f, 0, Color.White, 1f);
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
                    Main.PlaySound(2, player.Center, 7);
                }
                if (Main.rand.NextBool(2))
                    Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y + (player.height / 2 * player.gravDir)), 31, Vector2.Zero, 0, Color.White, 1).noGravity = true;
            }
            if (emptyHeart)
            {
                player.allDamage += 0.2f;
                if (player.statLife > 1)
                {
                    player.statLife = 1;
                }
                player.statLifeMax2 = 1;
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            player.runAcceleration *= runAccelerationMult;
            player.accRunSpeed *= accRunSpeedMult;
        }
        public override void PreUpdateMovement()
        {
            if (waterBubble)
            {
                player.wet = true;
                player.wetCount = 10;
            }
        }
        public override void PostUpdate()
        {
            if (hoverBoots)
            {
                if (hovering)
                {
                    player.wingTime = hoverWing;
                    player.rocketTime = hoverRocket;
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
                    if (Main.tile[i, j].active() && !Main.tile[i, j].inActive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type]))
                    {
                        return true;
                    }
                }
            }
            return false;
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
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (shieldSapling)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.type == mod.ProjectileType("ShieldSapling") && proj.getRect().Intersects(projectile.getRect()) && proj.hostile && proj.damage <= 15 && proj.active)
                    {
                        //Main.NewText(proj.damage, Color.DarkGreen);
                        proj.Kill();
                        Main.PlaySound(3, projectile.Center, 4);
                        return false;
                    }
                }
            }
            return base.CanBeHitByProjectile(proj);
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
                int sdamage = (int)(12 * (player.allDamage + player.minionDamage - 1) * player.minionDamageMult * player.allDamageMult);
                float knockback = 0.1f + player.minionKB;
                int num = 10;
                double deltaAngle = (float)(Math.PI * 2) / num;
                for (int i = 0; i < num; i++)
                {
                    double offsetAngle = (float)(Math.PI / 2) + deltaAngle * i;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 3 * (float)Math.Sin(offsetAngle), 3 * (float)Math.Cos(offsetAngle), 228, sdamage, knockback, player.whoAmI);
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
            if (dirtArmor)
            {
                int dirt = 0;
                for (int i = 0; i < 58; i++)
                {
                    if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
                    {
                        dirt += player.inventory[i].stack;
                    }
                }
                int amount = Math.Min(((dirt / 666) / 2), damage);
                for (int i = 0; i < 58 && amount > 0; i++)
                {
                    if (player.inventory[i].stack > 0 && player.inventory[i].type == ItemID.DirtBlock)
                    {
                        if (player.inventory[i].stack >= amount)
                        {
                            player.inventory[i].stack -= amount;
                            amount = 0;
                        }
                        else
                        {
                            amount -= player.inventory[i].stack;
                            player.inventory[i].stack = 0;
                        }
                        if (player.inventory[i].stack <= 0)
                        {
                            player.inventory[i].SetDefaults(0, false);
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
                Main.PlaySound(21, (int)player.Center.X, (int)player.Center.Y, 1, 1, -0.5f);
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(player.position, player.width, player.height, 1);
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (havelArmorActive && npc.knockBackResist > 0)
            {
                float KB = 3;
                int dir = player.direction;
                if (npc.Center.X > player.Center.X)
                {
                    dir = 1;
                }
                else
                {
                    dir = -1;
                }
                if (player.velocity.X * dir > 0)
                {
                    KB += Math.Abs(player.velocity.X);
                }
                npc.velocity.X = dir * KB;
                npc.velocity.Y--;
                if (Main.netMode != 0)
                {
                    ModPacket packet = mod.GetPacket();
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
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("HavelShield")] > 0)
            {
                float x = player.MountedCenter.X - 9 + (player.direction * 11);
                float y = player.position.Y + (player.height / 2) - 23;
                Rectangle rect = new Rectangle((int)x, (int)y, 18, 46);
                if (rect.Intersects(npc.getRect()))
                {
                    damage = (int)(damage * 0.2f);
                    Main.PlaySound(21, (int)npc.Center.X, (int)npc.Center.Y, 1, 1, -0.4f);
                }
            }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("HavelShield")] > 0)
            {
                float x = player.MountedCenter.X - 9 + (player.direction * 11);
                float y = player.position.Y + (player.height / 2) - 23;
                Rectangle rect = new Rectangle((int)x, (int)y, 18, 46);
                if (proj.Colliding(proj.getRect(), rect))
                {
                    damage = (int)(damage * 0.2f);
                    proj.penetrate--;
                    Main.PlaySound(21, (int)proj.Center.X, (int)proj.Center.Y, 1, 1, -0.4f);
                }
            }
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
                                        if (Main.netMode != 0)
                                        {
                                            ModPacket packet = mod.GetPacket();
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