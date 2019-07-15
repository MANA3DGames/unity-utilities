// MANA 3D Games - Robust Double Click Script
// 
// How to use:
// ------------------------------------------------------------------------
// // 1. Create instance of DoubleClick.
// private DoubleClick cDoubleClick;
// 
// // 2. Call OnClickDown() on Mouse Button is Down.
// if ( Input.GetMouseButtonDown( 0 ) )
//      cDoubleClick.OnClickDown( Time.time );
//
// // 3. Call OnClickUp() on Mouse Button is up 
// //    (And don't forget to pass the action that you want to tigger as a second parameter).
// if ( Input.GetMouseButtonUp( 0 ) )
//      cDoubleClick.OnClickUp( Time.time, OnDoubleClick );
//
// ------------------------------------------------------------------------

namespace MANA3DGames
{
    public class DoubleClick
    {
        #region [Global Variables]

        private float gMaxClickDuration;        // Single click elapsed time.
        private float gMaxSecondClickDelay;     // The delay between first click end time and second click start time.

        private float gFirstClickStartTime;     // First click start time.
        private float gFirstClickEndTime;       // First click end time.
        private float gSecondClickStartTime;    // Second click start time.

        private bool gIsValidSoFar;             // A boolean value to keep track of the validity of a double click.

        #endregion

        #region [Constructor]

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MANA3DGames.DoubleClick"/> class.
        /// </summary>
        /// <param name="maxClickDuration">Max click duration.</param>
        /// <param name="maxSecondClickDelay">Max delay between clicks.</param>
        public DoubleClick( float maxClickDuration = 0.5f, float maxSecondClickDelay = 0.5f )
        {
            gMaxClickDuration = maxClickDuration;
            gMaxSecondClickDelay = maxSecondClickDelay;

            gFirstClickStartTime = 0.0f;
            gFirstClickEndTime = 0.0f;
            gSecondClickStartTime = 0.0f;

            gIsValidSoFar = false;
        }

        #endregion

        #region [Events]

        /// <summary>
        /// Call this function once the mouse button is down.
        /// </summary>
        /// <param name="time">Current given time.</param>
        public void OnClickDown( float time )
        {
            // Check if we have a valid single click?
            if ( gIsValidSoFar )
            {
                // Save the current time as the start time of the second click.
                gSecondClickStartTime = time;

                // Check if the delay between the two click is vaild?
                gIsValidSoFar = gSecondClickStartTime - gFirstClickEndTime <= gMaxSecondClickDelay;

                // If the delay is not vaild then start a new first click.
                if ( !gIsValidSoFar )
                    gFirstClickStartTime = time;
            }
            else
                // Start a new first click by saving the current time.
                gFirstClickStartTime = time;
        }

        /// <summary>
        /// Call this function once the mouse button is up.
        /// </summary>
        /// <param name="time">Current given time.</param>
        /// <param name="OnDoubleClick">Action to be triggered once the double click happend</param>
        public void OnClickUp( float time, System.Action OnDoubleClick )
        {
            // Check if we have a valid single click?
            if ( gIsValidSoFar )
            {
                if ( time - gSecondClickStartTime <= gMaxClickDuration )
                    OnDoubleClick?.Invoke();

                gIsValidSoFar = false;
            }
            else
            {
                // Save current time as first click end time.
                gFirstClickEndTime = time;

                // Check if this is going to be a vaild first click.
                gIsValidSoFar = gFirstClickEndTime - gFirstClickStartTime <= gMaxClickDuration;
            }
        }

        #endregion

        #region [Setters]

        /// <summary>
        /// Sets the max duration of <see langword="async"/> single click.
        /// </summary>
        /// <param name="val">Given duration in seconds.</param>
        public void SetMaxClickDuration( float val )
        {
            gMaxClickDuration = val;
        }

        /// <summary>
        /// Sets the max delay between the two clicks.
        /// </summary>
        /// <param name="val">Given delay in seconds.</param>
        public void SetMaxSecondClickDelay( float val )
        {
            gMaxSecondClickDelay = val;
        }

        #endregion
    }
}