using System.Globalization;
using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class LandTree1Controller : MonoBehaviour
    {
        // These attributes are set by instantiator of this component using config file
        public float GrowthDuration = 1200.0f;
        public float Health = 100.0f;
        public bool Knifeable = false;

        // These attributes should not be touched
        public bool Running = false;
        public GameObject StaticPrefab = null;
        public float _progress = 0.0f;
        public float _passedProgress = 0.0f;

        // Optimisation variables
        private Plantable _plant = null;
        private GrownPlant _grownPlant = null;
        private Vector3 _origScale = Vector3.zero;
        private Vector3 _origStaticScale = Vector3.zero;
        private double _initTimeValue = 0.0;

        private void InitAnimation(GameObject go)
        {
            if (!Running)
            {
#if DEBUG_FLORA
                PrefabIdentifier id = go.GetComponent<PrefabIdentifier>();
                if (id != null)
                    Logger.Log("DEBUG: LandTree1Controller.Update(): gameObject name=[" + go.name + "] id=[" + id.Id + "] position x=[" + go.transform.localPosition.x + "] y=[" + go.transform.localPosition.y + "] z=[" + go.transform.localPosition.z + "] => Initializing");
                else
                    Logger.Log("DEBUG: LandTree1Controller.Update(): gameObject name=[" + go.name + "] position x=[" + go.transform.localPosition.x + "] y=[" + go.transform.localPosition.y + "] z=[" + go.transform.localPosition.z + "] => Initializing");
#endif
                if (StaticPrefab != null)
                {
                    // Configure static renderer
                    StaticPrefab.transform.parent = go.transform;
                    StaticPrefab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    StaticPrefab.transform.localScale = new Vector3(12f, 12f, 12f);
                    StaticPrefab.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                    // Update sky applier
                    SkyApplier skyApplier = go.GetComponent<SkyApplier>();
                    skyApplier.renderers = go.GetComponentsInChildren<Renderer>();
                    skyApplier.anchorSky = Skies.Auto;

                    StaticPrefab.SetActive(true);
                }
                // Hide seed model
                GameObject seed = _grownPlant.gameObject.FindChild("Generic_plant_seed");
                if (seed != null)
                    seed.GetComponent<MeshRenderer>().enabled = false;
                // Store init values
                _initTimeValue = DayNightCycle.main.timePassed;
                foreach (Transform tr in go.transform)
                {
                    bool isStatic = tr.name.StartsWith("Land_tree_01_static", true, CultureInfo.InvariantCulture);
                    if (_origScale == Vector3.zero && !isStatic)
                        _origScale = new Vector3(tr.localScale.x, tr.localScale.y, tr.localScale.z);
                    else if (_origStaticScale == Vector3.zero && isStatic)
                        _origStaticScale = new Vector3(tr.localScale.x, tr.localScale.y, tr.localScale.z);
                }
                // Init trees sizes
                foreach (Transform tr in go.transform)
                {
                    tr.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                }
                Running = true;
            }
        }

        public void Awake()
        {
            Running = false;

            // Init values
            _initTimeValue = DayNightCycle.main.timePassed;
            _progress = 0.0f;

            // Disable knifeable
            LiveMixin liveMixin = GetComponent<LiveMixin>();
            if (liveMixin == null)
            {
                _grownPlant = GetComponent<GrownPlant>();
                if (_grownPlant != null)
                    liveMixin = _grownPlant.gameObject.GetComponent<LiveMixin>();
            }
            if (liveMixin != null)
                liveMixin.data.knifeable = Knifeable;

#if DEBUG_FLORA_ENTRY
            Logger.Log("DEBUG: LandTree1Controller.Awake() for gameObject name=[" + this.gameObject.name + "] controllerEnabled=[" + this.enabled + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif
            this.enabled = true;
        }

        public void Update()
        {
#if (DEBUG_FLORA || DEBUG_FLORA_ENTRY || DEBUG_FLORA_ANIMATION)
            PrefabIdentifier id = GetComponent<PrefabIdentifier>();
#endif
#if DEBUG_FLORA_ENTRY
            if (id != null)
                Logger.Log("DEBUG: Entering LandTree1Controller.Update() for gameObject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
            else
                Logger.Log("DEBUG: Entering LandTree1Controller.Update() for gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif

            if (_plant == null)
                _plant = GetComponent<Plantable>();
            if (_grownPlant == null)
                _grownPlant = GetComponent<GrownPlant>();

            if (_grownPlant == null && _plant != null)
            {
                if (_plant.linkedGrownPlant != null)
                {
#if DEBUG_FLORA
                    Logger.Log("DEBUG: LandTree1Controller.Update() Associating grown plant in gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif
                    _grownPlant = _plant.linkedGrownPlant;
                    _grownPlant.seed = _plant;
                }
            }
            if (_grownPlant != null)
            {
                if (!Running)
                    InitAnimation(_grownPlant.gameObject);
                else
                {
                    // Animation
                    _progress = ((float)(DayNightCycle.main.timePassed - _initTimeValue) / GrowthDuration) + _passedProgress;
#if DEBUG_FLORA_ANIMATION
                    if (id != null)
                        Logger.Log("DEBUG: LandTree1Controller.Update(): PROGRESS gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] progress=[" + _progress + "] pastProgress=[" + _passedProgress + "] originScale x=[" + _origScale.x + "] y=[" + _origScale.y + "] z=[" + _origScale.z + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "]");
                    else
                        Logger.Log("DEBUG: LandTree1Controller.Update(): PROGRESS gameObject name=[" + _grownPlant.gameObject.name + "] progress=[" + _progress + "] pastProgress=[" + _passedProgress + "] originScale x=[" + _origScale.x + "] y=[" + _origScale.y + "] z=[" + _origScale.z + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "]");
#endif
                    if (_grownPlant.gameObject.transform.localPosition.x > 4900.0f && _grownPlant.gameObject.transform.localPosition.x < 5100.0f &&
                        _grownPlant.gameObject.transform.localPosition.z > 4900.0f && _grownPlant.gameObject.transform.localPosition.z < 5100.0f)
                    {
#if DEBUG_FLORA
                        if (id != null)
                            Logger.Log("DEBUG: PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Disabling animation component");
                        else
                            Logger.Log("DEBUG: PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Disabling animation component");
#endif
                        this.enabled = false;
                    }
                    else
                    {
                        if (_progress < 1.0f)
                        {
                            foreach (Transform tr in _grownPlant.gameObject.transform)
                            {
                                if (tr.name.StartsWith("Land_tree_01_static", true, CultureInfo.InvariantCulture))
                                    tr.localScale = new Vector3(_origStaticScale.x * _progress, _origStaticScale.y * _progress, _origStaticScale.z * _progress);
                            }
                        }
                        else
                        {
#if DEBUG_FLORA
                            if (id != null)
                                Logger.Log("DEBUG: LandTree1Controller.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => Set final size");
                            else
                                Logger.Log("DEBUG: LandTree1Controller.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => Set final size");
#endif
                            // Set final size
                            _progress = 1.0f;
                            foreach (Transform tr in _grownPlant.gameObject.transform)
                            {
                                if (!tr.name.StartsWith("Land_tree_01_static", true, CultureInfo.InvariantCulture))
                                    tr.localScale = new Vector3(_origScale.x, _origScale.y, _origScale.z);
                            }
                            // Enable knifeable
                            LiveMixin liveMixin = _grownPlant.gameObject.GetComponent<LiveMixin>();
                            liveMixin.data.knifeable = Knifeable;
                            // Disable static part
                            this.StaticPrefab.SetActive(false);
                            // Disable controller
                            this.enabled = false;
                        }
                    }
                }
            }
            else
            {
#if DEBUG_FLORA
                if (id != null)
                    Logger.Log("DEBUG: LandTree1Controller.Update(): gameObject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "] => No grown plant: Disabling controller");
                else
                    Logger.Log("DEBUG: LandTree1Controller.Update(): gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "] => No grown plant: Disabling controller");
#endif
                this.enabled = false;
            }
        }

    }
}
