using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.NPCs
{
    public class CactusWormBody : CactusWorm
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm");
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.aiStyle = -1;
            NPC.damage = 10;    
            NPC.defense = 5;         
            NPC.knockBackResist = 0f;
            NPC.width = 12;
            NPC.height = 12;       
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.dontCountMe = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SucculentCactus").Type, 1);
            }
		}
    }
}