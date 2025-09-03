using UnityEngine;

public class FlipSpriteScript : MonoBehaviour
{
    public playerInputScript InputScript;
    private bool facingRight;
    private void flip()
    {
        if (InputScript.playerMovementInput.x > 0f && facingRight || InputScript.playerMovementInput.x < 0f && !facingRight)
        {
            facingRight = !facingRight;

            Vector2 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
            //assigns a variable localscale to our characters local scale, then * -1 to flip it before having equal transform.localscale again
        }
    }
    void Update()
    {
        flip();
    }
}
