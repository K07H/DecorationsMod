﻿using UnityEngine;

namespace DecorationsMod.Controllers
{
    public class PlantGenericController : MonoBehaviour
    {
        // These attributes are set by instantiator of this component using config file
        public float GrowthDuration = 1200.0f;
        public float Health = 100.0f;
        public bool Knifeable = false;

        // These attributes should not be touched
        public bool RestoreColliders = false;
        public bool EnableColliders = false;
        public bool RestoreRadius = false;
        public bool RestoreBoxColliders = false;
        public bool Running = false;
        public float _progress = 0.0f;
        public float _passedProgress = 0.0f;

        private Plantable _plant = null;
        private GrownPlant _grownPlant = null;
        private Vector3 _origScale = Vector3.zero;
        private double _initTimeValue = 0.0;
        private BoxCollider _tmpCollider = null;

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
            Logger.Debug("PlantGenericController.Awake() for gameObject name=[" + this.gameObject.name + "] controllerEnabled=[" + this.enabled + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
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
                Logger.Debug("Entering PlantGenericController.Update() for gameObject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
            else
                Logger.Debug("Entering PlantGenericController.Update() for gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
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
                    Logger.Debug("PlantGenericController.Update() Associating grown plant in gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "]");
#endif
                    _grownPlant = _plant.linkedGrownPlant;
                    _grownPlant.seed = _plant;
                }
            }
            if (_grownPlant != null)
            {
                if (!Running)
                {
#if DEBUG_FLORA
                    if (id != null)
                        Logger.Debug("PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => Initializing");
                    else
                        Logger.Debug("PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => Initializing");
#endif
                    // Hide seed model and show plant model
                    PrefabsHelper.ShowPlantAndHideSeed(_grownPlant.gameObject.transform);
                    // Store init values
                    _initTimeValue = DayNightCycle.main.timePassed;
                    foreach (Transform tr in _grownPlant.gameObject.transform)
                    {
                        _origScale = new Vector3(tr.localScale.x, tr.localScale.y, tr.localScale.z);
                        break;
                    }
                    // If we need to add a temporary collider, do it before scaling
                    if (EnableColliders)
                    {
                        var coveTreeModel = _grownPlant.gameObject.FindChild("lost_river_cove_tree_01");
                        if (coveTreeModel != null)
                        {
                            _tmpCollider = coveTreeModel.AddComponent<BoxCollider>();
                            if (_tmpCollider != null)
                            {
                                _tmpCollider.size = new Vector3(7.0f, 20.0f, 7.0f);
                                _tmpCollider.center = new Vector3(_tmpCollider.center.x, _tmpCollider.center.y + 10.0f, _tmpCollider.center.z);
                            }
                        }
                    }
                    // Init tree/plant size
                    foreach (Transform tr in _grownPlant.gameObject.transform)
                        if (tr.name != "Generic_plant_seed")
                            tr.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

                    // Init colliders size
                    if (RestoreColliders)
                    {
                        Collider[] colliders = _grownPlant.gameObject.GetComponentsInChildren<Collider>();
                        foreach (Collider collider in colliders)
                        {
                            collider.transform.localScale *= 1000.0f;
                            collider.enabled = true;
                        }
                    }
                    if (RestoreRadius)
                    {
                        SphereCollider[] colliders = _grownPlant.gameObject.GetComponentsInChildren<SphereCollider>();
                        foreach (SphereCollider collider in colliders)
                        {
                            collider.radius *= 1000.0f;
                            collider.enabled = true;
                        }
                    }
                    if (RestoreBoxColliders)
                    {
                        BoxCollider[] colliders = _grownPlant.gameObject.GetComponentsInChildren<BoxCollider>();
                        foreach (BoxCollider collider in colliders)
                        {
                            collider.size *= 1000.0f;
                            collider.enabled = true;
                        }
                    }

                    Running = true;
                }
                else
                {
                    // Animation
                    _progress = ((float)(DayNightCycle.main.timePassed - _initTimeValue) / GrowthDuration) + _passedProgress;
#if DEBUG_FLORA_ANIMATION
                    if (id != null)
                        Logger.Debug("PlantGenericController.Update(): PROGRESS gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => progress=[" + _progress + "] pastProgress=[" + _passedProgress + "] originScale x=[" + _origScale.x + "] y=[" + _origScale.y + "] z=[" + _origScale.z + "]");
                    else
                        Logger.Debug("PlantGenericController.Update(): PROGRESS gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => progress=[" + _progress + "] pastProgress=[" + _passedProgress + "] originScale x=[" + _origScale.x + "] y=[" + _origScale.y + "] z=[" + _origScale.z + "]");
#endif
                    if (_grownPlant.gameObject.transform.localPosition.x > 4500.0f && _grownPlant.gameObject.transform.localPosition.z > 4500.0f)
                    {
#if DEBUG_FLORA
                        if (id != null)
                            Logger.Debug("PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Disabling animation component");
                        else
                            Logger.Debug("PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.gameObject.transform.localPosition.x + "] y=[" + _grownPlant.gameObject.transform.localPosition.y + "] z=[" + _grownPlant.gameObject.transform.localPosition.z + "] => Disabling animation component");
#endif
                        this.enabled = false;
                    }
                    else
                    {
                        if (_progress < 1.0f)
                        {
                            foreach (Transform tr in _grownPlant.gameObject.transform)
                                if (tr.name != "Generic_plant_seed")
                                    tr.localScale = new Vector3(_origScale.x * _progress, _origScale.y * _progress, _origScale.z * _progress);
                        }
                        else
                        {
#if DEBUG_FLORA
                            if (id != null)
                                Logger.Debug("PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] id=[" + id.Id + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => Set final size");
                            else
                                Logger.Debug("PlantGenericController.Update(): gameObject name=[" + _grownPlant.gameObject.name + "] position x=[" + _grownPlant.transform.localPosition.x + "] y=[" + _grownPlant.transform.localPosition.y + "] z=[" + _grownPlant.transform.localPosition.z + "] => Set final size");
#endif
                            // Set final size
                            _progress = 1.0f;
                            foreach (Transform tr in _grownPlant.gameObject.transform)
                                if (tr.name != "Generic_plant_seed")
                                    tr.localScale = new Vector3(_origScale.x, _origScale.y, _origScale.z);

                            // Set final colliders
                            if (EnableColliders)
                            {
                                // Disable temporary collider
                                if (_tmpCollider != null)
                                {
                                    _tmpCollider.enabled = false;
                                    GameObject.DestroyImmediate(_tmpCollider);
                                }
                                // Enable origin colliders
                                Collider[] colliders = _grownPlant.gameObject.GetComponentsInChildren<Collider>();
                                foreach (Collider collider in colliders)
                                    collider.enabled = true;
                            }

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
                    Logger.Debug("PlantGenericController.Update(): gameObject name=[" + this.gameObject.name + "] id=[" + id.Id + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "] => No grown plant: Disabling controller");
                else
                    Logger.Debug("PlantGenericController.Update(): gameObject name=[" + this.gameObject.name + "] position x=[" + this.gameObject.transform.localPosition.x + "] y=[" + this.gameObject.transform.localPosition.y + "] z=[" + this.gameObject.transform.localPosition.z + "] => No grown plant: Disabling controller");
#endif
                this.enabled = false;
            }
        }
    }
}
