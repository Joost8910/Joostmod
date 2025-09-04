using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod
{
    public class JoostMusic : ModSystem
    {
        public override void SetStaticDefaults()
        {
            MusicID.Sets.SkipsVolumeRemap[MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SAXAppears")] = true;
        }
    }
}
