//Author: Marcus Mellström

using UnityEngine;

public abstract class Event<T> where T : Event<T>
{
    public string eventDescription;

    private bool hasExecuted;

    public delegate void EventListener (T info);
    private static event EventListener listeners;

    public static void RegisterListener (EventListener listener)
    {
        listeners += listener;
    }

    public static void UnregisterListener (EventListener listener)
    {
        listeners -= listener;
    }

    public void ExecuteEvent ()
    {
        if(hasExecuted) {
            throw new System.Exception("Event already executed");
        }
        hasExecuted = true;
        listeners?.Invoke(this as T);
    }
}

public class EnemyDeathEvent : Event<EnemyDeathEvent>
{
    public GameObject gameObject;

    public EnemyDeathEvent(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}

public class DeathEvent : Event<DeathEvent>
{
    public GameObject Actor;

    public DeathEvent(GameObject actor)
    {
        Actor = actor;
    }
}

public class PowerCorePlacedEvent : Event<PowerCorePlacedEvent>
{

}

public class KeycardPickedUpEvent : Event<KeycardPickedUpEvent>
{
    public GameObject gameObject;

    public KeycardPickedUpEvent(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}

public class SpawnTriggerEvent : Event<SpawnTriggerEvent>
{
    public Spawner[] spawners;

    public SpawnTriggerEvent(Spawner[] spawners)
    {
        this.spawners = spawners;
    }
}

public class EnemyAggroEvent : Event<EnemyAggroEvent>
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public EnemyAggroEvent(AudioClip audioClip, AudioSource audioSource)
    {
        this.audioClip = audioClip;
        this.audioSource = audioSource;
    }
}

public class EnemyAttackEvent : Event<EnemyAttackEvent>
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public EnemyAttackEvent(AudioClip audioClip, AudioSource audioSource)
    {
        this.audioClip = audioClip;
        this.audioSource = audioSource;
    }

}

public class PlayerDeathEvent : Event<PlayerDeathEvent>
{
    public Vector3 respawnPoint;

    public PlayerDeathEvent(Vector3 respawnPoint)
    {
        this.respawnPoint = respawnPoint;
    }
}

public class ObjectDestroyedEvent : Event<ObjectDestroyedEvent>
{
    public GameObject gameObject;

    public ObjectDestroyedEvent(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}

public class ParticleSystemDestroyedEvent : Event<ParticleSystemDestroyedEvent>
{
    public GameObject gameObject;

    public ParticleSystemDestroyedEvent(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}