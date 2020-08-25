using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DecorationsMod.Fixers
{
    public class BuilderFixer
    {
		//private static GameObject ghostModel;
		private static readonly FieldInfo _ghostModel = typeof(Builder).GetField("ghostModel", BindingFlags.NonPublic | BindingFlags.Static);
		//private static LayerMask placeLayerMask;
		private static readonly FieldInfo _placeLayerMask = typeof(Builder).GetField("placeLayerMask", BindingFlags.NonPublic | BindingFlags.Static);
		//private static float placeMaxDistance;
		private static readonly FieldInfo _placeMaxDistance = typeof(Builder).GetField("placeMaxDistance", BindingFlags.NonPublic | BindingFlags.Static);
		/*
		//private static Renderer[] renderers;
		private static readonly FieldInfo _renderers = typeof(Builder).GetField("renderers", BindingFlags.NonPublic | BindingFlags.Static);
		//private static Material ghostStructureMaterial;
		private static readonly FieldInfo _ghostStructureMaterial = typeof(Builder).GetField("ghostStructureMaterial", BindingFlags.NonPublic | BindingFlags.Static);
		//private static GameObject prefab;
		private static readonly FieldInfo _prefab = typeof(Builder).GetField("prefab", BindingFlags.NonPublic | BindingFlags.Static);
		//private static TechType constructableTechType;
		private static readonly FieldInfo _constructableTechType = typeof(Builder).GetField("constructableTechType", BindingFlags.NonPublic | BindingFlags.Static);
		//private static float placeMinDistance;
		private static readonly FieldInfo _placeMinDistance = typeof(Builder).GetField("placeMinDistance", BindingFlags.NonPublic | BindingFlags.Static);
		//private static float placeDefaultDistance;
		private static readonly FieldInfo _placeDefaultDistance = typeof(Builder).GetField("placeDefaultDistance", BindingFlags.NonPublic | BindingFlags.Static);
		//private static List<SurfaceType> allowedSurfaceTypes;
		private static readonly FieldInfo _allowedSurfaceTypes = typeof(Builder).GetField("allowedSurfaceTypes", BindingFlags.NonPublic | BindingFlags.Static);
		//private static bool forceUpright;
		private static readonly FieldInfo _forceUpright = typeof(Builder).GetField("forceUpright", BindingFlags.NonPublic | BindingFlags.Static);
		//private static bool allowedInSub;
		private static readonly FieldInfo _allowedInSub = typeof(Builder).GetField("allowedInSub", BindingFlags.NonPublic | BindingFlags.Static);
		//private static bool allowedInBase;
		private static readonly FieldInfo _allowedInBase = typeof(Builder).GetField("allowedInBase", BindingFlags.NonPublic | BindingFlags.Static);
		//private static bool allowedOutside;
		private static readonly FieldInfo _allowedOutside = typeof(Builder).GetField("allowedOutside", BindingFlags.NonPublic | BindingFlags.Static);
		//private static bool allowedOnConstructables;
		private static readonly FieldInfo _allowedOnConstructables = typeof(Builder).GetField("allowedOnConstructables", BindingFlags.NonPublic | BindingFlags.Static);
		//private static bool rotationEnabled;
		private static readonly FieldInfo _rotationEnabled = typeof(Builder).GetField("rotationEnabled", BindingFlags.NonPublic | BindingFlags.Static);
		//private static Vector3 ghostModelPosition;
		private static readonly FieldInfo _ghostModelPosition = typeof(Builder).GetField("ghostModelPosition", BindingFlags.NonPublic | BindingFlags.Static);
		//private static Quaternion ghostModelRotation;
		private static readonly FieldInfo _ghostModelRotation = typeof(Builder).GetField("ghostModelRotation", BindingFlags.NonPublic | BindingFlags.Static);
		//private static Vector3 ghostModelScale;
		private static readonly FieldInfo _ghostModelScale = typeof(Builder).GetField("ghostModelScale", BindingFlags.NonPublic | BindingFlags.Static);
		//private static void InitBounds(GameObject gameObject)
		private static readonly MethodInfo _InitBounds = typeof(Builder).GetMethod("InitBounds", BindingFlags.NonPublic | BindingFlags.Static);
		*/
		//private static void SetupRenderers(GameObject gameObject, bool interior)
		//private static readonly MethodInfo _SetupRenderers = typeof(Builder).GetMethod("SetupRenderers", BindingFlags.NonPublic | BindingFlags.Static);
		//private static void CreatePowerPreview(TechType constructableTechType, GameObject ghostModel)
		//private static readonly MethodInfo _CreatePowerPreview = typeof(Builder).GetMethod("CreatePowerPreview", BindingFlags.NonPublic | BindingFlags.Static);

		private static readonly Vector3 NorthLadder = new Vector3(0.0f, 3.26f, -4.618f);
		private static readonly Vector3 SouthLadder = new Vector3(0.0f, 3.26f, 4.62f);
		private static readonly Vector3 EastLadder = new Vector3(-4.618f, 3.26f, 0.0f);
		private static readonly Vector3 WestLadder = new Vector3(4.618f, 3.26f, 0.0f);

		private static readonly Dictionary<int, KeyValuePair<Vector3, float>> LadderPositions = new Dictionary<int, KeyValuePair<Vector3, float>>()
		{
			{ 0, new KeyValuePair<Vector3, float>(NorthLadder, 0.0f) },
			{ 1, new KeyValuePair<Vector3, float>(EastLadder, 90.0f) },
			{ 2, new KeyValuePair<Vector3, float>(SouthLadder, 180.0f) },
			{ 3, new KeyValuePair<Vector3, float>(WestLadder, 270.0f) }
		};

		private static readonly Vector3 NorthLadderAlt = new Vector3(0.0f, -0.26f, -5.38f);
		private static readonly Vector3 SouthLadderAlt = new Vector3(0.0f, -0.26f, 5.38f);
		private static readonly Vector3 EastLadderAlt = new Vector3(-5.38f, -0.26f, 0.0f);
		private static readonly Vector3 WestLadderAlt = new Vector3(5.38f, -0.26f, 0.0f);

		private static readonly Dictionary<int, KeyValuePair<Vector3, float>> LadderPositionsAlt = new Dictionary<int, KeyValuePair<Vector3, float>>()
		{
			{ 0, new KeyValuePair<Vector3, float>(NorthLadderAlt, 180.0f) },
			{ 1, new KeyValuePair<Vector3, float>(EastLadderAlt, 270.0f) },
			{ 2, new KeyValuePair<Vector3, float>(SouthLadderAlt, 0.0f) },
			{ 3, new KeyValuePair<Vector3, float>(WestLadderAlt, 90.0f) }
		};

		private static int CurrentDirection = 0;
		private static bool CurrentInverted = false;

		private static void Rotate()
		{
			bool scrollUp = Input.GetAxis("Mouse ScrollWheel") > 0.0f;
			bool scrollDown = Input.GetAxis("Mouse ScrollWheel") < 0.0f;
			if (scrollDown || scrollUp)
			{
				if (scrollDown)
				{
					CurrentDirection--;
					if (CurrentDirection < 0)
						CurrentDirection = 3;
				}
				else
				{
					CurrentDirection++;
					if (CurrentDirection > 3)
						CurrentDirection = 0;
				}
			}
		}

		public static readonly Dictionary<Vector3, KeyValuePair<int, bool>> TempLadderDirections = new Dictionary<Vector3, KeyValuePair<int, bool>>();

		private static void PositionLadder(ref Vector3 position, ref Quaternion rotation, Transform foundation, int initialDirection = 0, bool initialInverted = false)
        {
			rotation = foundation.rotation;
			Vector3 newAngles = rotation.eulerAngles;
			int direction = CurrentDirection;
			for (int i = 0; i < initialDirection; i++)
            {
				direction++;
				if (direction > 3)
					direction = 0;
			}
			CurrentInverted = Input.GetKey(KeyCode.T);
			bool doInvert = initialInverted ? !CurrentInverted : CurrentInverted;
			if (doInvert)
			{
				position = PrefabsHelper.Translate(foundation.position, foundation.rotation, LadderPositionsAlt[direction].Key);
				rotation.eulerAngles = new Vector3(newAngles.x, newAngles.y + LadderPositionsAlt[direction].Value, newAngles.z);
			}
			else
			{
				position = PrefabsHelper.Translate(foundation.position, foundation.rotation, LadderPositions[direction].Key);
				rotation.eulerAngles = new Vector3(newAngles.x, newAngles.y + LadderPositions[direction].Value, newAngles.z);
			}
			TempLadderDirections[position] = new KeyValuePair<int, bool>(direction, doInvert);
		}

		private static int GetFacingDirection(Transform foundation, Vector3 hitPoint)
		{
			int facing = 0;
			float min = Vector3.Distance(hitPoint, PrefabsHelper.Translate(foundation.position, foundation.rotation, NorthLadder));
			float dist = Vector3.Distance(hitPoint, PrefabsHelper.Translate(foundation.position, foundation.rotation, EastLadder));
			if (dist < min)
			{
				min = dist;
				facing = 1;
			}
			dist = Vector3.Distance(hitPoint, PrefabsHelper.Translate(foundation.position, foundation.rotation, SouthLadder));
			if (dist < min)
			{
				min = dist;
				facing = 2;
			}
			dist = Vector3.Distance(hitPoint, PrefabsHelper.Translate(foundation.position, foundation.rotation, WestLadder));
			if (dist < min)
				facing = 3;
			return facing;
		}

		private static bool PlaceOutdoorLadder(GameObject foundation, Vector3 hitPoint, ref Vector3 position, ref Quaternion rotation)
        {
			if (foundation != null)
			{
				// Get initial direction.
				int initialDirection = GetFacingDirection(foundation.transform, hitPoint);
				// Get initial inverted.
				bool initialInverted = foundation.transform.position.y > Player.main.camRoot.transform.position.y;
				// Apply rotation on mouse wheel.
				Rotate();
				// Set ladder orientation and position.
				PositionLadder(ref position, ref rotation, foundation.transform, initialDirection, initialInverted);
				// Return false to prevent origin function call.
				return false;
			}
			// Give back execution to origin function.
			return true;
		}

		//private static void SetPlaceOnSurface(RaycastHit hit, ref Vector3 position, ref Quaternion rotation)
		public static bool SetPlaceOnSurface_Prefix(RaycastHit hit, ref Vector3 position, ref Quaternion rotation)
		{
			GameObject ghostModel = (GameObject)_ghostModel.GetValue(null);
			if (Input.GetKey(KeyCode.G))
			{
				Logger.Log("DEBUG: SetPlaceOnSurface() GhostModel tag=[" + (ghostModel?.tag != null ? ghostModel.tag : "?") + "] name=[" + (ghostModel?.name != null ? ghostModel.name : "?") + "] tr=[" + (ghostModel?.transform?.name != null ? ghostModel.transform.name : "?") + "] parent=[" + (ghostModel?.transform?.parent?.name != null ? ghostModel.transform.parent.name : "?") + "]");
				Logger.Log("DEBUG: Initial hit gameobject=[" + (hit.collider?.gameObject?.name != null ? hit.collider.gameObject.name : "?") + "] tr=[" + (hit.collider?.gameObject?.transform?.name != null ? hit.collider.gameObject.transform.name : "?") + "] parent=[" + (hit.collider?.gameObject?.transform?.parent?.name != null ? hit.collider.gameObject.transform.parent.name : "?") + "] parentParent=[" + (hit.collider?.gameObject?.transform?.parent?.parent?.name != null ? hit.collider.gameObject.transform.parent.parent.name : "?") + "]");
			}
			// If object being built is our Outdoor Ladder.
			if (ghostModel?.name != null && ghostModel.name.StartsWith("OutdoorLadderModel"))
			{
				// If our Outdoor Ladder is being placed on a Foundation.
				if (hit.collider?.gameObject?.name != null && hit.collider.gameObject.name.StartsWith("BaseFoundationPlatform"))
					return PlaceOutdoorLadder(hit.collider.gameObject.transform?.parent?.parent?.gameObject, hit.point, ref position, ref rotation);
			}
			// Give back execution to origin function.
			return true;
		}

		/*
		 private static bool UpdateAllowed()
	{
		Builder.SetDefaultPlaceTransform(ref Builder.placePosition, ref Builder.placeRotation);
		bool flag = false;
		ConstructableBase componentInParent = Builder.ghostModel.GetComponentInParent<ConstructableBase>();
		bool flag2;
		if (componentInParent != null)
		{
			Transform transform = componentInParent.transform;
			transform.position = Builder.placePosition;
			transform.rotation = Builder.placeRotation;
			flag2 = componentInParent.UpdateGhostModel(Builder.GetAimTransform(), Builder.ghostModel, default(RaycastHit), out flag, componentInParent);
			Builder.placePosition = transform.position;
			Builder.placeRotation = transform.rotation;
			if (flag)
			{
				Builder.renderers = MaterialExtensions.AssignMaterial(Builder.ghostModel, Builder.ghostStructureMaterial);
				Builder.InitBounds(Builder.ghostModel);
			}
		}
		else
		{
			flag2 = Builder.CheckAsSubModule();
		}
		if (flag2)
		{
			List<GameObject> list = new List<GameObject>();
			Builder.GetObstacles(Builder.placePosition, Builder.placeRotation, Builder.bounds, list);
			flag2 = (list.Count == 0);
			list.Clear();
		}
		return flag2;
	}
		 */
		/*
		public static void UpdateAllowed_Postfix(bool __result)
		{
			GameObject ghostModel = (GameObject)_ghostModel.GetValue(null);
			if (ghostModel != null)
			{
				foreach (Transform tr in ghostModel.transform)
					if (tr.name.StartsWith("DecorationsModLocator"))
					{
						BaseGhost component = ghostModel.GetComponent<BaseGhost>();
						Logger.Log("DEBUG: Cyclops Docking Hatch identified. BaseGhost name=[" + (component?.name != null ? component.name : "?") + "] targetBaseName=[" + (component?.TargetBase?.name != null ? component.TargetBase.name : "?") + "]");
						Transform aimTransform = Builder.GetAimTransform();
						float pmd = (float)_placeMaxDistance.GetValue(null);
						LayerMask lm = (LayerMask)_placeLayerMask.GetValue(null);
						bool allowed = false;
						// If our Outdoor Ladder is being placed on a foundation.
						if (Physics.Raycast(aimTransform.position, aimTransform.forward, out RaycastHit hit, pmd, lm.value, QueryTriggerInteraction.Ignore))
							if (hit.collider?.gameObject?.transform != null) // && hit.collider.gameObject.transform?.name == "BaseFoundationPlatform")
							{
								Logger.Log("DEBUG: Found colliding struct: name=[" + hit.collider.gameObject.transform.name + "] parent=[" + (hit.collider.gameObject.transform.parent?.name != null ? hit.collider.gameObject.transform.parent.name : "?") + "] parentParent=[" + (hit.collider.gameObject.transform.parent?.parent?.name != null ? hit.collider.gameObject.transform.parent.parent.name : "?") + "]");
								allowed = true;
							}
						if (!allowed)
							__result = false;
						break;
					}
				ConstructableBase componentInParent = ghostModel.GetComponentInParent<ConstructableBase>();
				if (componentInParent != null)
				{
					Logger.Log("DEBUG: Entering UpdateAllowed_Postfix for ghostModel name=[" + ghostModel.name + "] contructableBaseName=[" + componentInParent.name + "]");
				}
			}
		}
		*/

		//private static bool CheckSurfaceType(SurfaceType surfaceType)
		public static void CheckSurfaceType_Postfix(ref bool __result, SurfaceType surfaceType)
        {
			if (__result)
			{
				GameObject ghostModel = (GameObject)_ghostModel.GetValue(null);
				// If there's a hit and object being built is our Outdoor Ladder.
				if (ghostModel?.name != null)
				{
					if (ghostModel.name.StartsWith("OutdoorLadderModel"))
					{
						Transform aimTransform = Builder.GetAimTransform();
						float pmd = (float)_placeMaxDistance.GetValue(null);
						LayerMask lm = (LayerMask)_placeLayerMask.GetValue(null);
						bool allowed = false;
						// If our Outdoor Ladder is being placed on a foundation.
						if (Physics.Raycast(aimTransform.position, aimTransform.forward, out RaycastHit hit, pmd, lm.value, QueryTriggerInteraction.Ignore))
							if (hit.collider?.gameObject != null && hit.collider.gameObject.transform?.name == "BaseFoundationPlatform")
								allowed = true;
						if (!allowed)
							__result = false;
					}
					else //if (ghostModel.name.StartsWith("OutdoorLadderModel"))
                    {
						Logger.Log("DEBUG: Entering CheckSurfaceType for ghost name=[" + (ghostModel.name != null ? ghostModel.name : "?") + "]");
                    }
				}
			}
        }

		/*
		// private static bool CreateGhost()
		public static bool CreateGhost_Prefix(ref bool __result)
		{
			GameObject prefab = (GameObject)_prefab.GetValue(null);
			if (((GameObject)_ghostModel.GetValue(null)) == null && prefab?.GetComponent<ConstructableBase>() != null)
			{
				Constructable component = prefab.GetComponent<Constructable>();
				Logger.Log("DEBUG: Entering CreateGhost_Prefix(). SettingUpCyclopsHatch=[" + uGUI_BuilderMenuFixer.SettingUpCyclopsDockingHatch.ToString() + "] componentTechType=[" + component.techType.AsString() + "]");
				// If base part being built is our Cyclops Docking Hatch.
				if (uGUI_BuilderMenuFixer.SettingUpCyclopsDockingHatch && component.techType == TechType.BaseCorridor)
				{
					Logger.Log("DEBUG: Creating CyclopsHatchConnector ghost.");
					_constructableTechType.SetValue(null, component.techType);
					_placeMinDistance.SetValue(null, component.placeMinDistance);
					_placeMaxDistance.SetValue(null, component.placeMaxDistance);
					_placeDefaultDistance.SetValue(null, component.placeDefaultDistance);
					_allowedSurfaceTypes.SetValue(null, component.allowedSurfaceTypes);
					_forceUpright.SetValue(null, component.forceUpright);
					_allowedInSub.SetValue(null, component.allowedInSub);
					_allowedInBase.SetValue(null, component.allowedInBase);
					_allowedOutside.SetValue(null, component.allowedOutside);
					_allowedOnConstructables.SetValue(null, component.allowedOnConstructables);
					_rotationEnabled.SetValue(null, component.rotationEnabled);
					if ((bool)_rotationEnabled.GetValue(null) == true)
						Builder.ShowRotationControlsHint();

					ConstructableBase cb = UnityEngine.Object.Instantiate<GameObject>(prefab).GetComponent<ConstructableBase>();
					GameObject gm = cb.model;
					Logger.Log("DEBUG: CreateGhost_Prefix(): Adding base part locator. ConstructableBase techType=[" + cb.techType.AsString() + "]");
					uGUI_BuilderMenuFixer.SettingUpCyclopsDockingHatch = false;
					GameObject locator = new GameObject("DecorationsModLocator");
					locator.transform.parent = gm.transform;
					locator.transform.localPosition = Vector3.zero;
					locator.transform.localRotation = Quaternion.identity;
					locator.transform.localScale = Vector3.one;
					BaseFixer.SetupCyclopsDockingHatchModel(gm.transform);

					_ghostModel.SetValue(null, gm);
					((GameObject)_ghostModel.GetValue(null)).GetComponent<BaseGhost>().SetupGhost();
					_ghostModelPosition.SetValue(null, Vector3.zero);
					_ghostModelRotation.SetValue(null, Quaternion.identity);
					_ghostModelScale.SetValue(null, Vector3.one);
					_renderers.SetValue(null, MaterialExtensions.AssignMaterial((GameObject)_ghostModel.GetValue(null), (Material)_ghostStructureMaterial.GetValue(null)));
					_InitBounds.Invoke(null, new object[] { (GameObject)_ghostModel.GetValue(null) });

					__result = true;
					return false;
				}
				else
					return true;
			}
			else
				return true;
		}
		*/
		/*
		public static bool CreateGhost_Prefix(ref bool __result)
        {
			if (((GameObject)_ghostModel.GetValue(null)) != null)
			{
				__result = false;
				return false;
			}
			Constructable component = ((GameObject)_prefab.GetValue(null)).GetComponent<Constructable>();
			_constructableTechType.SetValue(null, component.techType);
			_placeMinDistance.SetValue(null, component.placeMinDistance);
			_placeMaxDistance.SetValue(null, component.placeMaxDistance);
			_placeDefaultDistance.SetValue(null, component.placeDefaultDistance);
			_allowedSurfaceTypes.SetValue(null, component.allowedSurfaceTypes);
			_forceUpright.SetValue(null, component.forceUpright);
			_allowedInSub.SetValue(null, component.allowedInSub);
			_allowedInBase.SetValue(null, component.allowedInBase);
			_allowedOutside.SetValue(null, component.allowedOutside);
			_allowedOnConstructables.SetValue(null, component.allowedOnConstructables);
			_rotationEnabled.SetValue(null, component.rotationEnabled);
			if ((bool)_rotationEnabled.GetValue(null) == true)
				Builder.ShowRotationControlsHint();
			GameObject prefab = (GameObject)_prefab.GetValue(null);
			if (prefab.GetComponent<ConstructableBase>() != null)
			{
				GameObject prefabInstance = UnityEngine.Object.Instantiate<GameObject>(prefab);
				ConstructableBase cb = prefabInstance.GetComponent<ConstructableBase>();
				GameObject gm = cb.model;

				Logger.Log("DEBUG: Entering CreateGhost_Prefix(). SettingUpCyclopsHatch=[" + uGUI_BuilderMenuFixer.SettingUpCyclopsHatch.ToString() + "] componentTechType=[" + component.techType.AsString() + "] constructableBaseTechType=[" + cb.techType.AsString() + "]");
				if (uGUI_BuilderMenuFixer.SettingUpCyclopsHatch && component.techType == TechType.BaseCorridor)
				{
					uGUI_BuilderMenuFixer.SettingUpCyclopsHatch = false;
					Logger.Log("DEBUG: Creating CyclopsHatchConnector ghost. Adding locator.");
					GameObject locator = new GameObject("DecorationsModLocator");
					if (locator != null && gm != null)
					{
						locator.transform.parent = gm.transform;
						locator.transform.localPosition = Vector3.zero;
						locator.transform.localRotation = Quaternion.identity;
						locator.transform.localScale = Vector3.one;
					}
				}

                _ghostModel.SetValue(null, gm);
				((GameObject)_ghostModel.GetValue(null)).GetComponent<BaseGhost>().SetupGhost();
				_ghostModelPosition.SetValue(null, Vector3.zero);
				_ghostModelRotation.SetValue(null, Quaternion.identity);
				_ghostModelScale.SetValue(null, Vector3.one);
				_renderers.SetValue(null, MaterialExtensions.AssignMaterial((GameObject)_ghostModel.GetValue(null), (Material)_ghostStructureMaterial.GetValue(null)));
				_InitBounds.Invoke(null, new object[] { (GameObject)_ghostModel.GetValue(null) });
			}
			else
			{
				_ghostModel.SetValue(null, UnityEngine.Object.Instantiate<GameObject>(component.model));
				((GameObject)_ghostModel.GetValue(null)).SetActive(true);
				Transform component2 = component.GetComponent<Transform>();
				Transform component3 = component.model.GetComponent<Transform>();
				Quaternion quaternion = Quaternion.Inverse(component2.rotation);
				Vector3 pos = quaternion * (component3.position - component2.position);
				_ghostModelPosition.SetValue(null, pos);
				Quaternion rot = quaternion * component3.rotation;
				_ghostModelRotation.SetValue(null, rot);
				_ghostModelScale.SetValue(null, component3.lossyScale);
				Collider[] componentsInChildren = ((GameObject)_ghostModel.GetValue(null)).GetComponentsInChildren<Collider>();
				for (int i = 0; i < componentsInChildren.Length; i++)
					UnityEngine.Object.Destroy(componentsInChildren[i]);
				_renderers.SetValue(null, MaterialExtensions.AssignMaterial(((GameObject)_ghostModel.GetValue(null)), (Material)_ghostStructureMaterial.GetValue(null)));
				_SetupRenderers.Invoke(null, new object[] { (GameObject)_ghostModel.GetValue(null), Player.main.IsInSub() });
				_CreatePowerPreview.Invoke(null, new object[] { (TechType)_constructableTechType.GetValue(null), (GameObject)_ghostModel.GetValue(null) });
				_InitBounds.Invoke(null, new object[] { (GameObject)_prefab.GetValue(null) });
			}
			__result = true;
			return false;
		}
		*/

		/*
		//private static void SetDefaultPlaceTransform(ref Vector3 position, ref Quaternion rotation)
		private static readonly MethodInfo _SetDefaultPlaceTransform = typeof(Builder).GetMethod("SetDefaultPlaceTransform", BindingFlags.NonPublic | BindingFlags.Static);
		//private static bool CheckAsSubModule()
		private static readonly MethodInfo _CheckAsSubModule = typeof(Builder).GetMethod("CheckAsSubModule", BindingFlags.NonPublic | BindingFlags.Static);
		//private static Vector3 placePosition;
		private static readonly FieldInfo _placePosition = typeof(Builder).GetField("placePosition", BindingFlags.NonPublic | BindingFlags.Static);
		//private static Quaternion placeRotation;
		private static readonly FieldInfo _placeRotation = typeof(Builder).GetField("placeRotation", BindingFlags.NonPublic | BindingFlags.Static);
		//private static List<OrientedBounds> bounds = new List<OrientedBounds>();
		private static readonly FieldInfo _bounds = typeof(Builder).GetField("bounds", BindingFlags.NonPublic | BindingFlags.Static);

		private delegate void SetDefaultPlaceTransformDelegate(ref Vector3 pos, ref Quaternion rot);

		public static bool UpdateAllowed_Prefix(ref bool __result)
		{
			Vector3 placePosition = (Vector3)_placePosition.GetValue(null);
			Quaternion placeRotation = (Quaternion)_placeRotation.GetValue(null);
			SetDefaultPlaceTransformDelegate p = (SetDefaultPlaceTransformDelegate)Delegate.CreateDelegate(typeof(SetDefaultPlaceTransformDelegate), null, _SetDefaultPlaceTransform);
			p.Invoke(ref placePosition, ref placeRotation);
			_placePosition.SetValue(null, placePosition);
			_placeRotation.SetValue(null, placeRotation);
			bool flag = false;
			ConstructableBase componentInParent = ((GameObject)_ghostModel.GetValue(null)).GetComponentInParent<ConstructableBase>();
			bool flag2;
			if (componentInParent != null)
			{
				//Logger.Log(string.Format(CultureInfo.InvariantCulture, "DEBUG: Found ConstructableBase! name=[{0}] goName=[{1}] goType=[{2}] modelName=[{3}] modelTrName=[{4}]", componentInParent.name, componentInParent.gameObject?.name, componentInParent.gameObject?.GetType().ToString(), componentInParent.model?.name, componentInParent.model?.transform?.name));
				if (componentInParent.model?.transform != null)
				{
					if (Input.GetKey(KeyCode.G))
					{
						Logger.Log("DEBUG: Model printing:");
						DebugTools.PrintTransform(componentInParent.model.transform);
					}
					if (Input.GetKey(KeyCode.H))
                    {
						Logger.Log("DEBUG: Prefab printing:");
						DebugTools.PrintTransform(((GameObject)_prefab.GetValue(null)).transform);
                    }
					if (Input.GetKey(KeyCode.U))
					{
						Logger.Log("DEBUG: Ghost model printing:");
						DebugTools.PrintTransform(((GameObject)_ghostModel.GetValue(null)).transform);
					}
				}
				Transform transform = componentInParent.transform;
				transform.position = (Vector3)_placePosition.GetValue(null);
				transform.rotation = (Quaternion)_placeRotation.GetValue(null);
				flag2 = componentInParent.UpdateGhostModel(Builder.GetAimTransform(), (GameObject)_ghostModel.GetValue(null), default(RaycastHit), out flag, componentInParent);
				_placePosition.SetValue(null, transform.position);
				_placeRotation.SetValue(null, transform.rotation);
				if (flag)
				{
					_renderers.SetValue(null, MaterialExtensions.AssignMaterial((GameObject)_ghostModel.GetValue(null), (Material)_ghostStructureMaterial.GetValue(null)));
					_InitBounds.Invoke(null, new object[] { (GameObject)_ghostModel.GetValue(null) });
				}
			}
			else
			{
				flag2 = (bool)_CheckAsSubModule.Invoke(null, null);
			}
			if (flag2)
			{
				List<GameObject> list = new List<GameObject>();
				Builder.GetObstacles((Vector3)_placePosition.GetValue(null), (Quaternion)_placeRotation.GetValue(null), (List<OrientedBounds>)_bounds.GetValue(null), list);
				flag2 = (list.Count == 0);
				list.Clear();
			}
			__result = flag2;
			return false;
		}
		*/
	}
}
