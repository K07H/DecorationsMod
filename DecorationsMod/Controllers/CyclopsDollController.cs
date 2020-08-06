using FMOD;
using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UWE;

namespace DecorationsMod.Controllers
{
    public class CyclopsDollController : HandTarget, IHandTarget, IProtoEventListener
    {
        #region Audio assets

        private static readonly FMODAsset _cyclopsAI_abandon = AssetsHelper.CreateAsset("2052", "173fbf7a-0998-4c9b-936d-fa8a997c3256", "event:/sub/cyclops/AI_abandon");
        private static readonly FMODAsset _cyclopsAI_ahead_flank = AssetsHelper.CreateAsset("2053", "5434bf0d-c107-4cb8-8317-0ab5dcdedbe5", "event:/sub/cyclops/AI_ahead_flank");
        private static readonly FMODAsset _cyclopsAI_ahead_slow = AssetsHelper.CreateAsset("2054", "dfc276a6-84dc-4eac-be66-f48672d17cf6", "event:/sub/cyclops/AI_ahead_slow");
        private static readonly FMODAsset _cyclopsAI_ahead_standard = AssetsHelper.CreateAsset("2055", "bb16d731-c830-4080-91ce-4573ba15503f", "event:/sub/cyclops/AI_ahead_standard");
        private static readonly FMODAsset _cyclopsAI_attack = AssetsHelper.CreateAsset("2056", "017341c1-11cc-4310-9ba6-39a8ef9e2bea", "event:/sub/cyclops/AI_attack");
        private static readonly FMODAsset _cyclopsAI_cavitate = AssetsHelper.CreateAsset("2057", "40ff60ee-addb-4ca4-93f4-749d00fb3f00", "event:/sub/cyclops/AI_cavitate");
        private static readonly FMODAsset _cyclopsAI_decoy = AssetsHelper.CreateAsset("2058", "7dc58e6b-bab8-494e-9f0e-38e83c97fd21", "event:/sub/cyclops/AI_decoy");
        private static readonly FMODAsset _cyclopsAI_depth_warning_1 = AssetsHelper.CreateAsset("2061", "d1eb6017-96d2-4b1d-a52d-65de4ff73cec", "event:/sub/cyclops/AI_depth_warning_1");
        private static readonly FMODAsset _cyclopsAI_depth_warning_2 = AssetsHelper.CreateAsset("2062", "304462f5-6db6-47e3-bde5-e05daee86c93", "event:/sub/cyclops/AI_depth_warning_2");
        private static readonly FMODAsset _cyclopsAI_drain = AssetsHelper.CreateAsset("2063", "b478202d-56b9-4d02-8486-2fe58b9b88e0", "event:/sub/cyclops/AI_drain");
        private static readonly FMODAsset _cyclopsAI_engine_down = AssetsHelper.CreateAsset("2064", "8bc901e1-b4e5-4c3f-95b7-947f8d99a1c3", "event:/sub/cyclops/AI_engine_down");
        private static readonly FMODAsset _cyclopsAI_engine_heat_critical = AssetsHelper.CreateAsset("2065", "65963382-60e8-47d3-aaf3-06ad8c6cdb88", "event:/sub/cyclops/AI_engine_heat_critical");
        private static readonly FMODAsset _cyclopsAI_engine_overheat = AssetsHelper.CreateAsset("2066", "03b3a17a-bd79-4a88-9a28-c35e9fa267b5", "event:/sub/cyclops/AI_engine_overheat");
        private static readonly FMODAsset _cyclopsAI_engine_up = AssetsHelper.CreateAsset("2067", "1edbe623-223a-4730-8e5a-2fcea22f66a9", "event:/sub/cyclops/AI_engine_up");
        private static readonly FMODAsset _cyclopsAI_external_damage = AssetsHelper.CreateAsset("2068", "257a1479-0460-48a5-b979-875b37d0c9ea", "event:/sub/cyclops/AI_external_damage");
        private static readonly FMODAsset _cyclopsAI_fire_detected = AssetsHelper.CreateAsset("2069", "2cfaff88-de96-4a16-a4a3-3f51bc9fb5f1", "event:/sub/cyclops/AI_fire_detected");
        private static readonly FMODAsset _cyclopsAI_fire_extinguished = AssetsHelper.CreateAsset("2070", "ba853ba6-8d31-48da-a1c3-4bd58fc878de", "event:/sub/cyclops/AI_fire_extinguished");
        private static readonly FMODAsset _cyclopsAI_fire_system = AssetsHelper.CreateAsset("2071", "3dec4e22-3fab-4b43-9538-86c9386dd0bb", "event:/sub/cyclops/AI_fire_system");
        private static readonly FMODAsset _cyclopsAI_hull_crit = AssetsHelper.CreateAsset("2072", "0a6ecb47-ee7e-4424-bf8e-195b732b6e80", "event:/sub/cyclops/AI_hull_crit");
        private static readonly FMODAsset _cyclopsAI_hull_low = AssetsHelper.CreateAsset("2073", "fb501c73-29cd-48e0-9941-24e34a8b19a4", "event:/sub/cyclops/AI_hull_low");
        private static readonly FMODAsset _cyclopsAI_leak = AssetsHelper.CreateAsset("2074", "0ef99c4b-dba4-4046-874b-75d0ee5b9fea", "event:/sub/cyclops/AI_leak");
        private static readonly FMODAsset _cyclopsAI_no_power = AssetsHelper.CreateAsset("2075", "a92535a3-c5bf-49ae-8eea-4fd82ba0dad1", "event:/sub/cyclops/AI_no_power");
        private static readonly FMODAsset _cyclopsAI_silent_running = AssetsHelper.CreateAsset("2076", "e82aad1e-4ede-4553-a502-590f581f5393", "event:/sub/cyclops/AI_silent_running");
        private static readonly FMODAsset _cyclopsAI_system_failure = AssetsHelper.CreateAsset("2077", "ca03e00c-7df6-402f-8eae-8984fb7702e2", "event:/sub/cyclops/AI_system_failure");
        private static readonly FMODAsset _cyclopsAI_welcome = AssetsHelper.CreateAsset("2078", "445b93f2-95d1-41e6-b454-2be968953b9c", "event:/sub/cyclops/AI_welcome");
        private static readonly FMODAsset _cyclopsAI_welcome_attention = AssetsHelper.CreateAsset("2079", "355d6773-bedf-4377-bcd5-288479651fc7", "event:/sub/cyclops/AI_welcome_attention");
        private static readonly FMODAsset _cyclopscyclops_loop_epic_fast = AssetsHelper.CreateAsset("2088", "a949e8ca-fdd7-46bb-962c-e3ba7976aabf", "event:/sub/cyclops/cyclops_loop_epic_fast");
        private static readonly FMODAsset _cyclopscyclops_loop_fast = AssetsHelper.CreateAsset("2089", "08b1648e-5536-4099-b430-a56d6ca5e5e7", "event:/sub/cyclops/cyclops_loop_fast");
        private static readonly FMODAsset _cyclopscyclops_loop_normal = AssetsHelper.CreateAsset("2090", "3af04011-298d-4737-8f12-e5c675f626dc", "event:/sub/cyclops/cyclops_loop_normal");
        private static readonly FMODAsset _cyclopsdecoy_loop = AssetsHelper.CreateAsset("301213", "6ac91603-321a-4991-bca9-1a9bf8e7da8f", "event:/sub/cyclops/decoy_loop");
        private static readonly FMODAsset _cyclopshorn = AssetsHelper.CreateAsset("2100", "592740b7-197a-4799-b6f0-d9d974dba12e", "event:/sub/cyclops/horn");
        private static readonly FMODAsset _cyclopsproximity = AssetsHelper.CreateAsset("2107", "1d34951a-537d-494d-92f9-a1c75d5582b5", "event:/sub/cyclops/proximity");
        private static readonly FMODAsset _cyclopsshield_on_loop = AssetsHelper.CreateAsset("2108", "2dd4ff7b-64c7-4e10-8776-50bb586e16ba", "event:/sub/cyclops/shield_on_loop");
        private static readonly FMODAsset _cyclopssiren = AssetsHelper.CreateAsset("2109", "cdb87689-5582-499e-8539-8ec54dd577c0", "event:/sub/cyclops/siren");
        private static readonly FMODAsset _cyclopssiren_3d = AssetsHelper.CreateAsset("2110", "a2363529-5e9d-427e-bcdb-3cf0a00c2057", "event:/sub/cyclops/siren_3d");
        private static readonly FMODAsset _cyclopssonar = AssetsHelper.CreateAsset("2111", "8e64a613-52b2-4dbb-baba-6e230148448d", "event:/sub/cyclops/sonar");
        private static readonly FMODAsset _cyclopsstart = AssetsHelper.CreateAsset("2112", "13de1734-9078-4dbf-bc1b-17312276829d", "event:/sub/cyclops/start");

        private static List<FMODAsset> _sounds = null;
        public static List<FMODAsset> Sounds
        {
            get
            {
                if (_sounds == null)
                {
                    _sounds = new List<FMODAsset>()
                    {
                        _cyclopsAI_abandon,
                        _cyclopsAI_ahead_flank,
                        _cyclopsAI_ahead_slow,
                        _cyclopsAI_ahead_standard,
                        _cyclopsAI_attack,
                        _cyclopsAI_cavitate,
                        _cyclopsAI_decoy,
                        _cyclopsAI_depth_warning_1,
                        _cyclopsAI_depth_warning_2,
                        _cyclopsAI_drain,
                        _cyclopsAI_engine_down,
                        _cyclopsAI_engine_heat_critical,
                        _cyclopsAI_engine_overheat,
                        _cyclopsAI_engine_up,
                        _cyclopsAI_external_damage,
                        _cyclopsAI_fire_detected,
                        _cyclopsAI_fire_extinguished,
                        _cyclopsAI_fire_system,
                        _cyclopsAI_hull_crit,
                        _cyclopsAI_hull_low,
                        _cyclopsAI_leak,
                        _cyclopsAI_no_power,
                        _cyclopsAI_silent_running,
                        _cyclopsAI_system_failure,
                        _cyclopsAI_welcome,
                        _cyclopsAI_welcome_attention,
                        _cyclopscyclops_loop_epic_fast,
                        _cyclopscyclops_loop_fast,
                        _cyclopscyclops_loop_normal,
                        _cyclopsdecoy_loop,
                        _cyclopshorn,
                        _cyclopsproximity,
                        _cyclopsshield_on_loop,
                        _cyclopssiren,
                        _cyclopssiren_3d,
                        _cyclopssonar,
                        _cyclopsstart
                    };
                }
                return _sounds;
            }
        }

        private static readonly List<string> LoopingAssets = new List<string>(new string[7]
        {
            "event:/sub/cyclops/cyclops_loop_epic_fast",
            "event:/sub/cyclops/cyclops_loop_fast",
            "event:/sub/cyclops/cyclops_loop_normal",
            "event:/sub/cyclops/decoy_loop",
            "event:/sub/cyclops/shield_on_loop",
            "event:/sub/cyclops/siren",
            "event:/sub/cyclops/siren_3d"
        });

        private static readonly float[] ProximitySequence = new float[24] { 0.1f, 1.0f, 2.0f, 3.0f, 3.75f, 4.25f, 4.5f, 4.7f, 4.9f, 5.1f, 5.3f, 5.5f, 5.7f, 5.9f, 6.1f, 6.3f, 6.5f, 6.7f, 6.9f, 7.1f, 7.3f, 7.5f, 7.7f, 7.9f };

        private static readonly float[] SonarSequence = new float[3] { 0.1f, 6.1f, 12.1f };

        #endregion

        #region Attributes

        // Current size step
        private int sizeStep = 7;
        // Last sound ID played
        private int lastSoundId = -1;
        // Currently playing sounds
        private readonly Dictionary<EventInstance?, float> playingSounds = new Dictionary<EventInstance?, float>();

        #endregion

        #region Methods

        public void StopSounds()
        {
            List<EventInstance?> toDel = new List<EventInstance?>();
            if (playingSounds.Count > 0)
                foreach (KeyValuePair<EventInstance?, float> snd in playingSounds)
                    if (snd.Key != null && snd.Key.HasValue && Time.time > snd.Value)
                    {
                        snd.Key.Value.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                        snd.Key.Value.release();
                        toDel.Add(snd.Key);
                    }
            if (toDel.Count > 0)
                foreach (EventInstance? it in toDel)
                    if (playingSounds.ContainsKey(it))
                        playingSounds.Remove(it);
        }

        public void PlayProximity() => PlayOneShot(_cyclopsproximity, MainCamera.camera.transform.position, 1.0f, -1.0f);

        public void PlaySonar() => PlayOneShot(_cyclopssonar, MainCamera.camera.transform.position, 1.0f, -1.0f);

        public EventInstance? PlayOneShot(FMODAsset asset, Vector3 position, float volume = 1.0f, float duration = -1.0f)
        {
            EventInstance? result = null;
            try
            {
                EventInstance eventInstance2 = FMODUWE.GetEvent(asset);
                eventInstance2.setVolume(volume);
                eventInstance2.set3DAttributes(position.To3DAttributes());
                eventInstance2.start();
                if (duration > 0.0f)
                {
                    playingSounds.Add(new EventInstance?(eventInstance2), Time.time + duration);
                    Invoke("StopSounds", duration + 0.1f);
                }
                else
                    eventInstance2.release();
                result = eventInstance2;
            }
            catch { }
            return result;
        }

        #endregion

        #region Hand click event

        public void OnHandClick(GUIHand hand)
        {
            if (!enabled)
                return;

            if (Input.GetKey(KeyCode.E))
            {
                GameObject model = this.gameObject.FindChild("CyclopsDoll");
                if (model == null)
                    return;

                BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
#if DEBUG_CYCLOPS_DOLL
                Logger.Log("DEBUG: Initial position=[" + model.transform.localPosition.x.ToString() + ";" + model.transform.localPosition.y.ToString() + ";" + model.transform.localPosition.z.ToString() + "] colliderCenter=[" + collider.center.x.ToString() + ";" + collider.center.y.ToString() + ";" + collider.center.z.ToString() + "] scale=[" + model.transform.localScale.x.ToString() + ";" + model.transform.localScale.y.ToString() + ";" + model.transform.localScale.z.ToString() + "] colliderSize=[" + collider.size.x.ToString() + ";" + collider.size.y.ToString() + ";" + collider.size.z.ToString() + "]");
#endif
                if (model.transform.localScale.y > 0.0006f)
                {
                    model.transform.localScale = new Vector3(0.00006f, 0.00006f, 0.00006f);
                    model.transform.localPosition = new Vector3(0.0f, 0.0675f, 0.0f);
                    collider.size = new Vector3(0.03f, 0.074f, 0.21f);
                    collider.center = new Vector3(-0.01f, 0.0675f, -0.0525f);
                    sizeStep = 1;
                }
                else
                {
                    model.transform.localScale *= 1.12f;
                    model.transform.localPosition *= 1.12f;
                    collider.size *= 1.12f;
                    collider.center *= 1.12f;
                    sizeStep++;
                }
                ErrorMessage.AddDebug(LanguageHelper.GetFriendlyWord("CyclopsSize") + sizeStep.ToString() + "/22");
            }
            else
            {
                if (Sounds.Count > 1)
                {
                    int rnd = UnityEngine.Random.Range(0, Sounds.Count);
                    if (lastSoundId >= 0)
                        while (rnd == lastSoundId && Sounds.Count > 1)
                            rnd = UnityEngine.Random.Range(0, Sounds.Count);
                    if (rnd >= 0 && rnd < Sounds.Count)
                    {
                        FMODAsset toPlay = Sounds[rnd];
#if DEBUG_CYCLOPS_DOLL
                        ErrorMessage.AddDebug("Playing [" + (toPlay.path.StartsWith("event:/sub/cyclops/") ? toPlay.path.Substring("event:/sub/cyclops/".Length) : toPlay.path) + "] ");
#endif
                        if (LoopingAssets.Contains(toPlay.path))
                            PlayOneShot(toPlay, MainCamera.camera.transform.position, 1.0f, 5.0f);
                        else if (toPlay.path == "event:/sub/cyclops/sonar")
                        {
                            foreach (float time in SonarSequence)
                                Invoke("PlaySonar", time);
                        }
                        else if (toPlay.path == "event:/sub/cyclops/proximity")
                        {
                            foreach (float time in ProximitySequence)
                                Invoke("PlayProximity", time);
                        }
                        else
                            PlayOneShot(toPlay, MainCamera.camera.transform.position, 1.0f, -1.0f);
                    }
                    lastSoundId = rnd;
                }
            }
        }

        #endregion

        #region Hand hover event

        public void OnHandHover(GUIHand hand)
        {
            if (!enabled)
                return;

            var reticle = HandReticle.main;
            reticle.SetIcon(HandReticle.IconType.Hand, 1f);
#if BELOWZERO
            reticle.SetTextRaw(HandReticle.TextType.Hand, LanguageHelper.GetFriendlyWord(ConfigSwitcher.UseCompactTooltips ? "CyclopsDollTooltipCompact" : "CyclopsDollTooltip"));
#else
            reticle.SetInteractText(ConfigSwitcher.UseCompactTooltips ? "CyclopsDollTooltipCompact" : "CyclopsDollTooltip");
#endif
        }

        #endregion

        #region Save doll state

        public void OnProtoSerialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

            string saveFolder = FilesHelper.GetSaveFolderPath();
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            GameObject model = this.gameObject.FindChild("CyclopsDoll");
            string toSave = string.Format("{0}{3}{1}{3}{2}{3}",
                Convert.ToString(model.transform.localScale.x, CultureInfo.InvariantCulture),
                Convert.ToString(model.transform.localScale.y, CultureInfo.InvariantCulture),
                Convert.ToString(model.transform.localScale.z, CultureInfo.InvariantCulture),
                Environment.NewLine);
            BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
            toSave += string.Format("{0}{3}{1}{3}{2}{3}",
                Convert.ToString(collider.size.x, CultureInfo.InvariantCulture),
                Convert.ToString(collider.size.y, CultureInfo.InvariantCulture),
                Convert.ToString(collider.size.z, CultureInfo.InvariantCulture),
                Environment.NewLine);
            toSave += Convert.ToString(sizeStep, CultureInfo.InvariantCulture) + Environment.NewLine;

            File.WriteAllText(Path.Combine(saveFolder, "cyclopsdoll_" + id.Id + ".txt"), toSave);
        }

        #endregion

        #region Restore doll state

        public void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            PrefabIdentifier id = GetComponentInParent<PrefabIdentifier>();
            if (id == null)
                if ((id = GetComponent<PrefabIdentifier>()) == null)
                    return;

            string filePath = Path.Combine(FilesHelper.GetSaveFolderPath(), "cyclopsdoll_" + id.Id + ".txt");
            if (File.Exists(filePath))
            {
                string tmpSize = File.ReadAllText(filePath);
                if (tmpSize == null)
                    return;
                string[] sizes = tmpSize.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (sizes != null && sizes.Length == 7)
                {
                    GameObject model = this.gameObject.FindChild("CyclopsDoll");
                    if (model != null)
                    {
                        float sizeX = float.Parse(sizes[0], CultureInfo.InvariantCulture);
                        float sizeY = float.Parse(sizes[1], CultureInfo.InvariantCulture);
                        float sizeZ = float.Parse(sizes[2], CultureInfo.InvariantCulture);
                        model.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
                    }
                    BoxCollider collider = this.gameObject.GetComponent<BoxCollider>();
                    if (collider != null)
                    {
                        float colliderSizeX = float.Parse(sizes[3], CultureInfo.InvariantCulture);
                        float colliderSizeY = float.Parse(sizes[4], CultureInfo.InvariantCulture);
                        float colliderSizeZ = float.Parse(sizes[5], CultureInfo.InvariantCulture);
                        collider.size = new Vector3(colliderSizeX, colliderSizeY, colliderSizeZ);
                    }
                    sizeStep = int.Parse(sizes[6], CultureInfo.InvariantCulture);
                }
            }
        }

        #endregion
    }
}
