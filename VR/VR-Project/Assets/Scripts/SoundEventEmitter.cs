using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Maze/SoundEventEmitter")]
public class SoundEventEmitter : ScriptableObject
{
    public event Action<SoundEvent> OnSoundEvent;

    public void EmitSoundEvent(SoundEvent soundEvent) { OnSoundEvent?.Invoke(soundEvent); }
}