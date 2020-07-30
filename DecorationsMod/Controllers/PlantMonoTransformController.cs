using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class PlantMonoTransformController : MonoBehaviour
    {
        // These attributes are set by instantiator of this component using config file
        public float GrowthDuration = 1200.0f;
        public float Health = 100.0f;
        public bool Knifeable = false;

        // These attributes should not be touched
        public bool Running = false;
        public float _progress = 0.0f;
        public float _passedProgress = 0.0f;
        public Vector3 _origScale = Vector3.zero;

        private GrownPlant _grownPlant = null;
        private Plantable _plant = null;
        private double _initTimeValue = 0.0;

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
            Logger.Log("DEBUG: PlantMonoTransformController.Awake() for gameObject name=[" + this.gameObject.name + "] controllerEnabled=[" + this.enabled + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
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
                Logger.Log("DEBUG: A) Entering PlantMonoTransformController.Update() for gameObject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
            else
                Logger.Log("DEBUG: A) Entering PlantMonoTransformController.Update() for gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
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
                    Logger.Log("DEBUG: PlantMonoTransformController.Update() Associating grown plant in gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif
                    _grownPlant = _plant.linkedGrownPlant;
                    _grownPlant.seed = _plant;
                }
            }
            if (_grownPlant != null)
            {
#if DEBUG_FLORA_ENTRY
                if (id == null)
                {
                    id = _grownPlant.gameObject.GetComponent<PrefabIdentifier>();
                    if (id != null)
                        Logger.Log("DEBUG: B) Entering PlantMonoTransformController.Update() for gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "]");
                    else
                        Logger.Log("DEBUG: B) Entering PlantMonoTransformController.Update() for gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "]");
                }
#endif
                if (!Running)
                {
#if DEBUG_FLORA
                    if (id != null)
                        Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Initializing");
                    else
                        Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Initializing");
#endif
                    // Hide seed model and show plant model
                    PrefabsHelper.ShowPlantAndHideSeed(_grownPlant.gameObject.transform);
                    // Store init values
                    _initTimeValue = DayNightCycle.main.timePassed;
                    if (_origScale == Vector3.zero)
                        _origScale = new Vector3(_grownPlant.gameObject.transform.localScale.x, _grownPlant.gameObject.transform.localScale.y, _grownPlant.gameObject.transform.localScale.z);
                    // Init tree size
                    _grownPlant.gameObject.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                    Running = true;
                }
                else
                {
                    // Animation
                    _progress = ((float)(DayNightCycle.main.timePassed - _initTimeValue) / GrowthDuration) + _passedProgress;
#if DEBUG_FLORA_ANIMATION
                    if (id != null)
                        Logger.Log("DEBUG: PlantMonoTransformController.Update(): PROGRESS gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => progress=[" + _progress + "] pastProgress=[" + _passedProgress + "] originScale x=[" + _origScale.x + "] y=[" + _origScale.y + "] z=[" + _origScale.z + "]");
                    else
                        Logger.Log("DEBUG: PlantMonoTransformController.Update(): PROGRESS gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => progress=[" + _progress + "] pastProgress=[" + _passedProgress + "] originScale x=[" + _origScale.x + "] y=[" + _origScale.y + "] z=[" + _origScale.z + "]");
#endif
                    //if (_grownPlant.gameObject.transform.localPosition.x > 4900.0f && _grownPlant.gameObject.transform.localPosition.x < 5100.0f && _grownPlant.gameObject.transform.localPosition.z > 4900.0f && _grownPlant.gameObject.transform.localPosition.z < 5100.0f)
                    if (_grownPlant.gameObject.transform.localPosition.x > 4500.0f && _grownPlant.gameObject.transform.localPosition.z > 4500.0f)
                    {
#if DEBUG_FLORA
                        if (id != null)
                            Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Disabling animation component");
                        else
                            Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Disabling animation component");
#endif
                        this.enabled = false;
                    }
                    else
                    {
                        if (_progress < 1.0f)
                            _grownPlant.gameObject.transform.localScale = new Vector3(_origScale.x * _progress, _origScale.y * _progress, _origScale.z * _progress);
                        else
                        {
#if DEBUG_FLORA
                            if (id != null)
                                Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Set final size");
                            else
                                Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Set final size");
#endif
                            // Set final size
                            _progress = 1.0f;
                            _grownPlant.gameObject.transform.localScale = new Vector3(_origScale.x, _origScale.y, _origScale.z);
                            // Enable knifeable
                            LiveMixin liveMixin = _grownPlant.gameObject.GetComponent<LiveMixin>();
                            liveMixin.data.knifeable = Knifeable;
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
                    Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "] => No grown plant: Replacing seed by plant");
                else
                    Logger.Log("DEBUG: PlantMonoTransformController.Update(): gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "] => No grown plant: Replacing seed by plant");
#endif
                this.enabled = false;
            }
        }
    }
}
