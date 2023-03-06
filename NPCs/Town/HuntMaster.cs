using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Town
{
	[AutoloadHead]
	public class HuntMaster : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "JoostMod/NPCs/Town/HuntMaster";
			}
		}


		public override bool IsLoadingEnabled(Mod mod)
		{
			name = "Hunt Master";
			return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hunt Master");
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 5;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.DangerDetectRange[NPC.type] = 600;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 25;
			NPCID.Sets.AttackAverageChance[NPC.type] = 2;
			NPCID.Sets.HatOffsetY[NPC.type] = 6;
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 42;
			NPC.aiStyle = 7;
			NPC.damage = 17;
			NPC.defense = 25;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.7f;
			AnimationType = NPCID.GoblinTinkerer;
		}

        public override bool CanGoToStatue(bool toKingStatue)
        {
            toKingStatue = true;
            return true;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return JoostWorld.downedPinkzor;
        }
        public override void PostAI()
        {
            int x = (int)((NPC.position.X - 2) / 16f);
            int x2 = (int)((NPC.position.X + NPC.width + 2) / 16f);
            int y = (int)((NPC.position.Y + NPC.height + 2) / 16f);
            if (NPC.velocity.X < -4)
            {
                NPC.direction = -1;
            }
            if (NPC.velocity.X > 4)
            {
                NPC.direction = 1;
            }
            if (NPC.velocity.Y == 0)
            {
                if (Main.tile[x2, y].TileType == TileID.ConveyorBeltRight)
                {
                    NPC.direction = -1;
                }
                if (Main.tile[x, y].TileType == TileID.ConveyorBeltLeft)
                {
                    NPC.direction = 1;
                }
                if (NPC.oldVelocity.X < 0 && NPC.velocity.X > 0)
                {
                    NPC.direction = 1;
                }
                if (NPC.oldVelocity.X > 0 && NPC.velocity.X < 0)
                {
                    NPC.direction = -1;
                }
                if (Math.Abs(NPC.oldVelocity.X) > 4 && NPC.velocity.X == 0)
                {
                    NPC.velocity.Y = -6;
                    NPC.velocity.X = NPC.oldVelocity.X;
                }
            }
            else if (NPC.velocity.X == 0)
            {
                NPC.velocity.X = NPC.direction;
            }
        }

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
		{
			switch (WorldGen.genRand.Next(8))
			{
				case 1:
					return "Dantro";
				case 2:
					return "Tomaj";
				case 3:
					return "Montblanc";
				case 4:
					return "Saph";
				case 5:
					return "Josh";
				case 6:
					return "Dave";
                case 7:
                    return "Bodega";
				default:
					return "Greg";
			}
		}


		public override string GetChat()
        {
            if (Main.LocalPlayer.ZoneSnow && Main.rand.Next(8) == 0)
			{
				return "Yeah, I know I'm not wearin' a shirt. The cold never bothered me anyways.";
			}
            if (Main.eclipse)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        return "These are some pretty unique monsters out tonig- er, today!";
                    case 2:
                        return "All sorts of loot is bound to be found from these monsters!";
                    case 3:
                        return "Today seems like it would make a good horror movie.";
                    case 4:
                        return "What's with the moon causing monsters to show up?";
                    default:
                        return "All these creeps runnin' amok sure makes one stressful.";
                }
            }
            if (Main.bloodMoon)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        return "Never gunna run out of things to kill tonight!";
                    case 2:
                        return "Where do all these zombies come from anyways?";
                    case 3:
                        return "Somethin' 'bout this eclipse makes the monsters go nuts.";
                    case 4:
                        return "How come zombies can open doors tonight but not other nights?";
                    default:
                        return "All these creeps runnin' amok sure makes one stressful.";
                }
            }
            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl > 0 && Main.rand.NextBool(8))
            {
                return "I overheard " + Main.npc[partyGirl].GivenName + " call me a 'total DILF' the other day. What's a dilf? Is she makin' fun of me?";
            }
            if ((Main.expertMode || Main.LocalPlayer.difficulty == 2) && Main.hardMode && Main.rand.NextBool(8))
            {
                return "Expert mode, Hardmode, Hardcore; it's all so confusin'!";
            }
            switch (Main.rand.Next(7))
			{
				case 1:
					return "Be on the lookout fer Tim while yer underground. He tends to target people who dress in gem robes.";
				case 2:
					return "Ya ever had deep fried slime? That stuff's great!";
				case 3:
					return "Every now an' then, I get the feelin' somethin's tryin' to hunt me.";
				case 4:
					return "Where do all these slimes come from? How the hell do they even reproduce? Whaddya mean they come from the sky!?";
				case 5:
					return "Hunting monsters is a surprisingly great source of revenue, where do they get all this money from anyways?";
				case 6:
                    return "Where do demon eyes come from? I've seen demons, their eyes ain't that big!";
                default:
                    return "I could really go for some chicken right now.";
            }
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Quest";
		}
		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Player player = Main.player[Main.myPlayer];
			if (firstButton)
			{
				Main.npcChatCornerItem = 0;
				SoundEngine.PlaySound(SoundID.MenuTick);
				int pinkzor = player.FindItem(Mod.Find<ModItem>("Pinkzor").Type);
				if (pinkzor != -1)
				{
					player.inventory[pinkzor].stack--;
					if (player.inventory[pinkzor].stack <= 0)
					{
						player.inventory[pinkzor] = new Item();
					}
					Main.npcChatText = "'Ello there! My name's " + Main.npc[NPC.FindFirstNPC(NPC.type)].GivenName + ", I'm the Hunt Master. I seek out unique monsters and take 'em down before they can take us down. Thanks for helpin' me outta that slime; it got the jump on me. How about ya do some work for me? I'll let ya know if there's a creep needin' huntin' and I'll pay if ya bring back proof of it's death. Here's somethin' for helpin' out with that big pink slime";
					SoundEngine.PlaySound(SoundID.Chat);
					player.QuickSpawnItem(Mod.Find<ModItem>("GooGlove").Type);
				}
				else
				{
                    Main.npcChatText = "";
                    JoostMod.instance.ShowHuntUI();
                    /*
                   foreach(HuntInfo hunt in JoostMod.instance.hunts)
                   {
                        int item = player.FindItem((int)hunt.item);
                        if (item != -1)
                        {
                            player.inventory[item].stack--;
                            if (player.inventory[item].stack <= 0)
                            {
                                player.inventory[item] = new Item();
                            }
                            Main.npcChatText = hunt.complete;
                            Main.PlaySound(24, -1, -1, 1, 1f, 0f);
                            hunt.reward(player);
                            break;
                        }
                        else if (hunt.available())
                        {
                            Main.npcChatText = hunt.accept;
                            if (JoostWorld.activeQuest != (int)hunt.NPC)
                            {
                                Main.NewText(Lang.GetNPCNameValue((int)hunt.NPC), 225, 25, 25);
                                Main.NewText("The Hunt Begins!", 225, 25, 25);
                                JoostWorld.activeQuest = (int)hunt.NPC;
                                if (Main.netMode == 1)
                                {
                                    ModPacket packet = mod.GetPacket();
                                    packet.Write((byte)JoostModMessageType.ActiveQuest);
                                    packet.Write(JoostWorld.activeQuest);
                                    packet.Send();
                                }
                            }
                            break;
                        }
                        else
                        {
                            Main.npcChatText = "Sorry! I don't have any hunts for ya right now, why don't ya try lookin' for somethin' yerself?";
                        }
                   }*/
				}
			}
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 17;
			knockback = 1.5f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 15;
			randExtraCooldown = 5;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = 54;
			attackDelay = 20;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
            gravityCorrection = 32f;
		}
	}
}