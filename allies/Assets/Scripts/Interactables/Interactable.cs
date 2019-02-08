using UnityEngine;

public class Interactable : FMNObject
{
  [HideInInspector]
  public PolygonCollider2D polygonCollider2D;
  [HideInInspector]
  public Rigidbody2D rb;

  [Header("Action")]
  public bool actionActivated = false;

  [Header("Sounds")]
  public AudioClip audioClip_activate;
  public AudioClip audioClip_deactivate;

  public override void Init()
  {
    gameManager = GameManager.globalGameManager;
    InitAudioSource();
    InitRigidBody();
    InitPolygonCollider();
    MoveToParentTransform();
    initialized = true;
  }

  protected override void InitAudioSource()
  {
    audioSource = gameObject.AddComponent<AudioSource>();
    audioSource.outputAudioMixerGroup = gameManager.soundManager.mixer_interactables;
  }

  public override void MoveToParentTransform()
  {
    transform.SetParent(gameManager.interactablesManager.transform);
  }

  public virtual void Activate()
  {
    actionActivated = true;
    SoundManager.PlayOneShot(audioClip_activate, audioSource);
  }

  public virtual void Deactivate()
  {
    actionActivated = false;
    SoundManager.PlayOneShot(audioClip_activate, audioSource);
  }

  private void InitRigidBody()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();

    if (!rb)
      rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;

    rb.isKinematic = true;
    rb.useFullKinematicContacts = true;
    rb.gravityScale = GameManager.globalGravityScale;
  }

  private void InitPolygonCollider()
  {
    polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();

    if (!polygonCollider2D)
      polygonCollider2D = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
  }
}