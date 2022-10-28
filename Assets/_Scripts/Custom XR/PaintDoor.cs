using UnityEngine;

public class PaintDoor : MonoBehaviour
{
    public GameObject paintAnim;
    public PortalTeleporter door;

    [Space]
    public bool withParticle = true;
    public int particleCount = 20;
    public int countToReady = 45;
    public ParticleSystem[] particles;
    private bool[] readys;
    public bool ready;
    private int readyTest;

    private void Awake()
    {
        readys = new bool[particles.Length];
    }

    public void ActivateParticle(int num)
    {
        if (paintAnim.activeSelf)
        {
            paintAnim.SetActive(false);
        }

        if (!ready)
        {
            if (!readys[num])
            {
                if (withParticle)
                {
                    particles[num].Emit(particleCount);
                }
                readys[num] = true;
            }

            readyTest = 0;
            for (int i = 0; i < particles.Length; i++)
            {
                if (readys[i] == true)
                {
                    readyTest++;
                }
            }

            if (readyTest >= countToReady)
            {
                ready = true;
                door.canTeleport = true;
            }
        }
    }
}