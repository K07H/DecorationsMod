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

		//private static bool CheckSurfaceType(SurfaceType surfaceType)
		public static void CheckSurfaceType_Postfix(ref bool __result, SurfaceType surfaceType)
        {
			if (__result)
			{
				GameObject ghostModel = (GameObject)_ghostModel.GetValue(null);
				// If there's a hit and object being built is our Outdoor Ladder.
				if (ghostModel?.name != null && ghostModel.name.StartsWith("OutdoorLadderModel"))
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
			}
        }
	}
}
