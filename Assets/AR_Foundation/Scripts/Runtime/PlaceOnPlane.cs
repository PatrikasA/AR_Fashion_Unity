using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;


    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : PressInputBase
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
        
        public bool canMove = false;
        public GameObject maneqManagementButtons;
        
        [SerializeField] private TextMeshProUGUI tmpro; //cia reiktu daryt per tutorialmanager,
                                                        //bet man jo neranda unity, tai idk
                                                        //quick fixas 


        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// Dress list
        /// </summary>
        private DressList dressList;

        public void toggleCanMove()
        {
            canMove = !canMove;
        }
        
        public void OnSliderValueChanged(float value)
        {
            if(spawnedObject != null)
            {
                float rotationAngle = value * 360f; 
                spawnedObject.transform.rotation = Quaternion.Euler(0, rotationAngle, 0); 
            }
        }
        
        public void OnScaleSliderValueChanged(float value)
        {
            if (spawnedObject != null)
            {
                float scale = 1 + value;
                spawnedObject.transform.localScale = new Vector3(scale, scale, scale);
            }
        }


        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        bool m_Pressed;
        [SerializeField]
        private Slider m_RotationSlider;
        [SerializeField]
        private Slider m_ScaleSlider;
        public Slider rotationSlider
        {
            get { return m_RotationSlider; }
            set { m_RotationSlider = value; }
        }
        
        public Slider scaleSlider
        {
            get { return m_ScaleSlider; }
            set { m_ScaleSlider = value; }
        }
        protected override void Awake()
        {
            base.Awake();
            m_RaycastManager = GetComponent<ARRaycastManager>();

            // Initialize rotation slider
            if (m_RotationSlider != null)
            {
                m_RotationSlider.onValueChanged.AddListener(OnSliderValueChanged);
            }

            // Initialize scale slider
            if (m_ScaleSlider != null)
            {
                m_ScaleSlider.onValueChanged.AddListener(OnScaleSliderValueChanged);
            }
        }

        private void Start()
        {
            dressList = DressList.Instance;
            m_PlacedPrefab = dressList.GetDressesList()[DataManager.Instance.GetSelectedDressIndex()];
        }

        void Update()
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Do nothing if the touch is over a UI element
            }

            if (Pointer.current == null || m_Pressed == false)
                return;

            var touchPosition = Pointer.current.position.ReadValue();

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    maneqManagementButtons.SetActive(true);
                    tmpro.text="You can drag your character around with your finger. Press lock position when you are done";
                    DataManager.Instance.SetSelectedDressPrefab(MannequinAnimationManager.Instance.gameObject);
                }
                else if (!canMove) //idk kodel atvirksciai
                {
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }

        protected override void OnPress(Vector3 position) => m_Pressed = true;

        protected override void OnPressCancel() => m_Pressed = false;

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
