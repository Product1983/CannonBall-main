using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
namespace BigRookGames.Build
{
    [RequireComponent(typeof(Animator))]
    public class BasicWoodWallController : MonoBehaviour
    {
        Animator anim;
     
        [SerializeField] private int m_Health = 0;
        [SerializeField] private float cur_Health = 0f;
        public GameObject wallExplosionPrefab;
        public Transform wallExplosionPosition;
        public bool alive=false;
        public AudioClip spawnClip, damageClip, deathClip;
        public AudioSource damageAudioSource;
       [SerializeField] ParticleSystem explosion;
        // --- Variable to determine when to play damage animations and audio ---
        // --- Stage 0 - health: Full Health
        // --- Stage 1 - health: 71-99
        // --- Stage 2 - health: 21-70
        // --- Stage 3 - health: 1-20
        // --- Stage 4 - health: 0;
        public int panelStage = 0;

        private void Awake()
        {
       anim = GetComponent<Animator>();
            anim.enabled = false;
            //damageAudioSource = GetComponent<AudioSource>();
            if (!alive) gameObject.SetActive(true);
        }
        void Start()
        {
           // damageAudioSource.clip = spawnClip;
         // damageAudioSource.Play();
        }
        private void FixedUpdate()
        {
            // --- If alive and the health has crossed below the next threshold, update panel ---
            if (!alive && CheckStageHealthThreshold())//
            {
                anim.SetInteger("Health", m_Health);
                if (m_Health > 0)
                {
                    damageAudioSource = GetComponent<AudioSource>();
                    alive = false;
                    damageAudioSource.clip = deathClip;
                     damageAudioSource.Play();
                    Instantiate(wallExplosionPrefab, wallExplosionPosition);
                }
              //else
              // {
                // damageAudioSource.clip = spawnClip;
                // damageAudioSource.Play();
               // }
            }
            else if (cur_Health <= 280f)
                {
                    m_Health++;
                    cur_Health += Mathf.CeilToInt(1* Time.fixedDeltaTime*0.1f);
                    if (cur_Health == 200f)
                    {
                        Debug.Log("Else");
                        cur_Health = 200f;
                        m_Health = 200;
                        alive = true;
                      //  anim = GetComponent<Animator>();
                        anim.enabled = true;
                        
                       // damageAudioSource.clip = spawnClip;
                        //damageAudioSource.Play();
                    explosion.gameObject.SetActive(true);
                    explosion.Play();
                  // wallExplosionPosition.GetComponent<GameObject>().SetActive(true);
                    }
                }
                else { return; }
                // --- For Example Scene, Take Out In Real Project (set health to 100 to reset wall) ---
            }
        private bool CheckStageHealthThreshold()
      {
           switch(panelStage)
          {
              case 0:
                  if (m_Health < 1000)
                 {
                       panelStage++;
                      return true;
                  }
                    break;
                case 1:
                   if (m_Health < 701)
                    {
                        panelStage++;
                      return true;
                   }
                    break;
                case 2:
                  if (m_Health < 210)
                    {
                      panelStage++;
                 return true;
                  }
                  break;
              case 3:
                  if (m_Health <= 0)
                  {
                       panelStage++;
                    return true;
                  }
                   break;
          }
         return false;
        }

        /// <summary>
        /// Sets the Health integer parameter on the animator to play each animation.
        /// This is called from the UI buttons in the example scene.
        /// </summary>
        // <param name="damageStage"></param>
        public void PlayDamageAnimation(int damageStage)
        {
            switch (damageStage)
            {
              case 0:
                   m_Health = 100;
                   break;
              case 1:
                   m_Health = 99;
                   break;
               case 2:
                   m_Health = 70;
                   break;
               case 3:
                   m_Health = 20;
                 break;
              case 4:
                    m_Health = 0;
                   break;
           }
            anim.SetInteger("Health", m_Health);
        }
    }
}