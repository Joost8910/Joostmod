using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Town
{
    [AutoloadHead]
    public class CactusPerson : ModNPC
    {
        public override string Texture
		{
			get
			{
				return "JoostMod/NPCs/Town/CactusPerson";
			}
		}

		public override string[] AltTextures
		{
			get
			{
				return new string[] { "JoostMod/NPCs/Town/CactusPerson_Alt" };
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Cactus Person";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Person");
            Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 5;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 750;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 35;
			NPCID.Sets.AttackAverageChance[npc.type] = 10;
			NPCID.Sets.HatOffsetY[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 14;
			npc.height = 42;
			npc.aiStyle = 7;
			npc.damage = 18;
			npc.defense = 35;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 1f;
            animationType = NPCID.GoblinTinkerer;
        }

		public override void HitEffect(int hitDirection, double damage)
		{
			int num = npc.life > 0 ? 4 : 25;
			for (int k = 0; k < num; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.t_Cactus);
			} 
		}
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return JoostWorld.downedCactusWorm;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == mod.NPCType("Cactite") || target.type == mod.NPCType("Cactoid") || target.type == mod.NPCType("Cactuar") || target.type == mod.NPCType("HallowedCactuar"))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void PostAI()
        {
            int xl = (int)((npc.position.X - 2) / 16f);
            int xr = (int)((npc.position.X + npc.width + 2) / 16f);
            int xc = (int)((npc.Center.X) / 16f);
            int y = (int)((npc.position.Y + npc.height + 2) / 16f);
            if (npc.velocity.X < -4)
            {
                npc.direction = -1;
            }
            if (npc.velocity.X > 4)
            {
                npc.direction = 1;
            }
            if (npc.velocity.Y == 0)
            {
                if (Main.tile[xr, y].type == TileID.ConveyorBeltRight)
                {
                    npc.direction = -1;
                }
                if (Main.tile[xl, y].type == TileID.ConveyorBeltLeft)
                {
                    npc.direction = 1;
                }
                int type = Main.tile[xc, y].type;
                if (Main.time % (60 * (60 / (int)((1 + Math.Abs(npc.velocity.X)) * 30))) == 0 && npc.velocity.X != 0)
                {
                    if (type == TileID.Sand || type == TileID.Pearlsand || type == TileID.Sandstone || type == TileID.HardenedSand || type == TileID.HallowHardenedSand || type == TileID.HallowSandstone || type == TileID.SandstoneBrick || type == TileID.SandStoneSlab || type == TileID.CactusBlock)
                    {
                        Projectile.NewProjectile(npc.direction * 8 + xc * 16, npc.position.Y, 0, 7, mod.ProjectileType("BootCactus"), npc.damage, 1, Main.myPlayer, 0, 1);
                    }
                }
                if (npc.oldVelocity.X < 0 && npc.velocity.X > 0)
                {
                    npc.direction = 1;
                }
                if (npc.oldVelocity.X > 0 && npc.velocity.X < 0)
                {
                    npc.direction = -1;
                }
                if (Math.Abs(npc.oldVelocity.X) > 4 && npc.velocity.X == 0)
                {
                    npc.velocity.Y = -6;
                    npc.velocity.X = npc.oldVelocity.X;
                }
            }
            else
            {
                if (npc.velocity.X == 0)
                {
                    npc.velocity.X = npc.direction;
                }
                npc.velocity.X += npc.direction * 0.095f;
            }
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
		{
			int score = 0;
			for (int x = left - Main.zoneX / 2 / 16 - 1 - Lighting.offScreenTiles; x <= right + Main.zoneX / 2 / 16 + 1 + Lighting.offScreenTiles; x++)
			{
				for (int y = top - Main.zoneY / 2 / 16 - 1 - Lighting.offScreenTiles; y <= bottom + Main.zoneY / 2 / 16 + 1 + Lighting.offScreenTiles; y++)
				{
					int type = Main.tile[x, y].type;
					if (type == TileID.Sand || type == TileID.Pearlsand || type == TileID.Sandstone || type == TileID.HardenedSand || type == TileID.HallowHardenedSand || type == TileID.HallowSandstone || type == TileID.SandstoneBrick || type == TileID.SandStoneSlab)
                    {
						score++;
					}
                    if (type == TileID.CactusBlock)
                    {
                        score += 5;
                    }
				}
			}
			return score > 800;
		}

		public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(14))
			{
				case 1:
					return "Areq";
				case 2:
					return "Arroja";
				case 3:
					return "Bartschalla";
                case 4:
                    return "Chiapa";
                case 5:
                    return "Erio";
                case 6:
                    return "Frailea";
                case 7:
                    return "Islaya";
                case 8:
                    return "Lobeira";
                case 9:
                    return "Lobivia";
                case 10:
                    return "Toumeya";
                case 11:
                    return "Kakitar";
                default:
					return "Joost";
			}
		}
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (projectile.Name.Contains("Needle") || projectile.Name.Contains("Cact"))
            {
                return false;
            }
            return base.CanBeHitByProjectile(projectile);
        }
        public override string GetChat()
        {
            if (Main.rand.Next(6) == 0 && (Main.bloodMoon || Main.eclipse))
            {
                if (npc.position.Y / 16 > Main.worldSurface)
                {
                    return "Those eclipse monsters can't get us while we're down here, right?";
                }
                return "I want to go hide underground until this is over!";
            }
            if (Main.eclipse)
            {
                switch (Main.rand.Next(3))
                {
                    case 1:
                        return "This is even worse than a lunar eclipse!";
                    case 2:
                        return "I'm scared! I don't want to become a vampire!";
                    default:
                        return "*hooHAAhooHAAAhooHAAhoo*";
                }
            }
            if (Main.bloodMoon)
            {
                if (Main.raining && Main.rand.Next(4) == 0 && npc.position.Y/16 <= Main.worldSurface)
                {
                    return "AACK It's raining blood!";
                }
                switch (Main.rand.Next(3))
                {
                    case 1:
                        return "Why does a lunar eclipse equate to monsters everywhere!?";
                    case 2:
                        return "Please make sure the doors are properly barricaded.";
                    default:
                        return "Those flesh monster are abosulutely horrific!";
                }
            }
            if (Main.LocalPlayer.ZoneSandstorm)
            {
                if (Main.rand.Next(6) == 0 && npc.position.Y / 16 <= Main.worldSurface)
                {
                    return "It's best to stay inside during this weather lest you be buffeted and blown away";
                }
            }
            else if (Main.LocalPlayer.ZoneSnow)
            {
                if (Main.rand.Next(5) == 0)
                {
                    return "It's pricking cold here! Why am I here!?";
                }
            }
            else if (Main.raining && Main.rand.Next(6) == 0 && npc.position.Y / 16 <= Main.worldSurface)
            {
                return "It's nice to get some moisture here now and then";
            }
            int truffle = NPC.FindFirstNPC(NPCID.Truffle);
            if (truffle >= 0 && Main.rand.Next(6) == 0)
            {
                return "I can sympathize with " + Main.npc[truffle].GivenName + "; it's not easy being a tasty sentient plant surrounded by mammals.";
            }
            if (Main.rand.Next(6) == 0)
            {
                if (npc.homeless)
                {
                    return "Do you happen to have a vacancy in the desert?";
                }
                else
                {
                    return "It's so nice of you to have built a house for me";
                }
            }
            switch (Main.rand.Next(5))
            {
                case 1:
                    return "Cacti come in many forms, Saguaro, Barrel, Cactuar, Cactoid, Person, Worm...";
                case 2:
                    return "Hows the weather for you? If you don't like it, try out one of my weather stars! Effects saguaranteed!";
                case 3:
                    return "Fish love the taste of cactus, that's why we live in the desert where there is no fish.";
                case 4:
                    return "Have you ever thought that we are just pets kept in the Terrarium of some unspeakably massive creature beyond mortal comprehension?";
                default:
                    return "*Sigh*... Bapanada";
            }
        }


        public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(ItemID.SandBlock);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Cactus);
			nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.PinkPricklyPear);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("CactusJuice"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("SucculentCactus"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("CactusBait"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("ClearStar"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("RainStar"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("SandstormStar"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("SlimeStar"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("SucculentThrow")); 
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("EnhancedCactusHelmet"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("EnhancedCactusBreastplate"));
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("EnhancedCactusLeggings"));
            nextSlot++;
            if (JoostWorld.downedJumboCactuar)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("JoostJuice"));
                nextSlot++;
            }
        }

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Cactus, 15 + Main.rand.Next(21));
        }

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 0;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 40;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = mod.ProjectileType("StickyCactus");
			attackDelay = 20;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 14f;
            gravityCorrection = 16f;
        }
	}
}