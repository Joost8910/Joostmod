using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.NPCs
{
    public class CactusWormTail : CactusWorm
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm");
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.aiStyle = -1; 
            NPC.damage = 5;
            NPC.defense = 10;         
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
        public override void Init()
        {
            base.Init();
            tail = true;
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