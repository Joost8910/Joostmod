using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Sounds.Item
{
	public class MissileShoot : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * .5f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = 0f;
			Main.PlaySoundInstance(soundInstance);
			return soundInstance;
		}
	}
}
