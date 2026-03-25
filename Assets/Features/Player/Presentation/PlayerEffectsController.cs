using Feature.Player.Domain;
using Shared.Data;
using System;

namespace Feature.Player.Presentation
{
    public class PlayerEffectsController : IDisposable
    {
        private readonly IAudioPlayer _audio;
        private readonly ILifeEvents _lifeEvents;

        public PlayerEffectsController(IAudioPlayer audio, ILifeEvents lifeEvents)
        {
            _audio = audio;
            _lifeEvents = lifeEvents;
        
            _lifeEvents.ReceivedDamage += OnReceivedDamage;
        }

        private void OnReceivedDamage(AttackInfo info)
        {
            _audio.PlayOneShot("Hit");
        }

        public void Dispose()
        {
            _lifeEvents.ReceivedDamage -= OnReceivedDamage;
        }
    }
}