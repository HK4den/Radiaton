public class money : MonoBehaviour
{
    void onTriggerEnter (collider other)
    {
        if (other.tag == "player")
        {debug.Log("you got a coin!"); 
        Destroy(this.gameObject);
        }
      }
}